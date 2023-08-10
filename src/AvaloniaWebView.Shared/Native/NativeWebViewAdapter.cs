using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.MicroCom;
using AvaloniaWebView.Interop;
using MicroCom.Runtime;

namespace AvaloniaWebView;

internal sealed class NativeWebViewAdapter : IWebViewAdapter
{
    [DllImport("libWebView")]
    private static extern IntPtr CreateWebViewNativeFactory();

    private readonly NativeWebViewCallbacks _callbacks;
    private readonly INativeWebView _nativeWebView;
    private readonly Dictionary<int, TaskCompletionSource<string?>> _scriptResults = new();
    private int _scriptResultsCurrent;

    static NativeWebViewAdapter()
    {
        AppDomain.CurrentDomain.ProcessExit += (_, _) =>
        {
            using var factory = MicroComRuntime.CreateProxyFor<IWebViewFactory>(CreateWebViewNativeFactory(), true);
            factory.InvalidateAllManagedReferences();
        };
    }
    
    public NativeWebViewAdapter()
    {
        using var factory = MicroComRuntime.CreateProxyFor<IWebViewFactory>(CreateWebViewNativeFactory(), true);
        _callbacks = new NativeWebViewCallbacks(this);
        _nativeWebView = factory.CreateWebView(_callbacks);
        AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
    }

    public event EventHandler<WebViewNavigationCompletedEventArgs>? NavigationCompleted;
    public event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarted;
    public event EventHandler<WebMessageReceivedEventArgs>? WebMessageReceived;
    public bool CanGoBack => _nativeWebView.CanGoBack == 1;
    public bool CanGoForward => _nativeWebView.CanGoForward == 1;

    public Uri? Source
    {
        get
        {
            using (_nativeWebView.Source)
            {
                return Uri.TryCreate(_nativeWebView.Source.String, UriKind.RelativeOrAbsolute, out var source) ?
                    source :
                    null;
            }
        }
        set => Navigate(value!);
    }

    public bool GoBack() => _nativeWebView.GoBack() == 1;

    public bool GoForward() => _nativeWebView.GoForward() == 1;

    public async Task<string?> InvokeScript(string script)
    {
        using var scriptStr = new AvnString(script);
        var index = _scriptResultsCurrent++;
        var tcs = new TaskCompletionSource<string?>();
        _scriptResults.Add(index, tcs);
        _nativeWebView.InvokeScript(scriptStr, index);
        return await tcs.Task;
    }

    public void Navigate(Uri url)
    {
        using var str = new AvnString(url.ToString());
        _nativeWebView.Navigate(str);
    }

    public void NavigateToString(string text)
    {
        using var str = new AvnString(text);
        using var baseUrl = new AvnString("http://localhost:12345/");
        _nativeWebView.NavigateToString(str, baseUrl);
    }

    public bool Refresh() => _nativeWebView.Refresh() == 1;

    public bool Stop() => _nativeWebView.Refresh() == 1;

    public void Dispose()
    {
        AppDomain.CurrentDomain.ProcessExit -= CurrentDomainOnProcessExit;
        _nativeWebView.ReleaseUnmanaged();
        _nativeWebView.Dispose();
        _callbacks.Dispose();
    }

    public unsafe IntPtr Handle => new(_nativeWebView.AsNsView());
    public string? HandleDescriptor => "NSView";
    public event EventHandler? Initialized;
    public bool IsInitialized => true;
    public void SizeChanged() {}

    private void OnScriptResult(int id, bool isError, string? result)
    {
        var tcs = _scriptResults[id];
        _scriptResults.Remove(id);

        if (isError)
        {
            tcs.TrySetException(new Exception(result ?? "Unknown script execution error"));
        }
        else
        {
            tcs.TrySetResult(result);
        }
    }
    
    private async void OnNavigationCompleted(string url, bool success)
    {
        await InvokeScript("function invokeCSharpAction(data){window.webkit.messageHandlers.postWebViewMessage.postMessage(data);}");

        NavigationCompleted?.Invoke(this, new WebViewNavigationCompletedEventArgs
        {
            IsSuccess = success,
            Request = new Uri(url)
        });
    }
    
    private bool OnNavigationStarted(string url)
    {
        var args = new WebViewNavigationStartingEventArgs { Request = new Uri(url) };
        NavigationStarted?.Invoke(this, args);
        return args.Cancel;
    }

    private void OnWebMessageReceived(string body)
    {
        WebMessageReceived?.Invoke(this, new WebMessageReceivedEventArgs { Body = body });
    }
    
    private void CurrentDomainOnProcessExit(object? sender, EventArgs e)
    {
        Dispose();
    }
    
    private class NativeWebViewCallbacks : CallbackBase, INativeWebViewHandlers
    {
        private readonly NativeWebViewAdapter _adapter;

        public NativeWebViewCallbacks(NativeWebViewAdapter adapter)
        {
            _adapter = adapter;
        }

        public void OnScriptResult(int id, int isError, IAvnString ppv)
        {
            using (ppv)
            {
                _adapter.OnScriptResult(id, isError == 1, ppv.String);
            }
        }

        public void OnWebMessageReceived(IAvnString body)
        {
            using (body)
            {
                _adapter.OnWebMessageReceived(body.String);
            }
        }

        public void OnNavigationCompleted(IAvnString url, int success)
        {
            using (url)
            {
                _adapter.OnNavigationCompleted(url.String, success == 1);
            }
        }

        public unsafe void OnNavigationStarted(IAvnString url, int* cancel)
        {
            using (url)
            {
                *cancel = _adapter.OnNavigationStarted(url.String) ? 1 : 0;
            }
        }
    }
}
