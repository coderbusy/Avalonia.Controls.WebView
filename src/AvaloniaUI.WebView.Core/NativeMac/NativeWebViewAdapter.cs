using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform;
using Avalonia.Threading;

namespace AvaloniaUI.WebView.NativeMac;

[SupportedOSPlatform("macOS")]
internal sealed class NativeWebViewAdapter : IWebViewAdapterWithFocus, IWebViewAdapterWithInputRedirect
{
    private readonly NativeWebViewCallbacks _callbacks;
    private readonly INativeWebView _nativeWebView;
    private readonly Dictionary<int, TaskCompletionSource<string?>> _scriptResults = new();
    private int _scriptResultsCurrent;

    static NativeWebViewAdapter()
    {
        AppDomain.CurrentDomain.ProcessExit += (_, _) =>
        {
            using var factory = NativeBootstrap.CreateWebViewNativeFactory();
            factory.InvalidateAllManagedReferences();
        };
    }

    public NativeWebViewAdapter()
    {
        _callbacks = new NativeWebViewCallbacks(this);
        using var factory = NativeBootstrap.CreateWebViewNativeFactory();
        _nativeWebView = factory.CreateWebView(_callbacks);
        AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
    }

    public event EventHandler<WebViewNavigationCompletedEventArgs>? NavigationCompleted;
    public event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarted;
    public event EventHandler<WebMessageReceivedEventArgs>? WebMessageReceived;

    public event EventHandler? GotFocus;
    public event EventHandler? LostFocus;
    public event Action<RoutedEventArgs>? Input;
    public bool CanGoBack => _nativeWebView.CanGoBack == 1;
    public bool CanGoForward => _nativeWebView.CanGoForward == 1;

    public Uri Source
    {
        get
        {
            using var sourceStr = _nativeWebView.Source;
            return Uri.TryCreate(sourceStr.String, UriKind.RelativeOrAbsolute, out var source) ?
                source : WebViewHelper.EmptyPage;
        }
        set => Navigate(value);
    }

    public bool GoBack()
    {
        return _nativeWebView.GoBack() == 1;
    }

    public bool GoForward()
    {
        return _nativeWebView.GoForward() == 1;
    }

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

    public bool Refresh()
    {
        return _nativeWebView.Refresh() == 1;
    }

    public bool Stop()
    {
        return _nativeWebView.Refresh() == 1;
    }

    public void Dispose()
    {
        AppDomain.CurrentDomain.ProcessExit -= CurrentDomainOnProcessExit;
        _nativeWebView.ReleaseUnmanaged();
        _nativeWebView.Dispose();
        _callbacks.Dispose();
    }

    public unsafe IntPtr Handle => new(_nativeWebView.AsNsView());
    public string HandleDescriptor => "NSView";
    public event EventHandler? Initialized;
    public bool IsInitialized => true;
    public void SizeChanged() { }
    public void SetParent(IPlatformHandle parent)
    {
        // no-op
        // macOS control don't need to be explicitly parented
    }

    public bool Focus() => _nativeWebView.Focus() == 1;

    private void OnScriptResult(int id, bool isError, string? result)
    {
        var tcs = _scriptResults[id];
        _scriptResults.Remove(id);

        if (isError)
            tcs.TrySetException(new Exception(result ?? "Unknown script execution error"));
        else
            tcs.TrySetResult(result);
    }

    private async void OnNavigationCompleted(string url, bool success)
    {
        await InvokeScript(
            "function invokeCSharpAction(data){window.webkit.messageHandlers.postWebViewMessage.postMessage(data);}");

        NavigationCompleted?.Invoke(this,
            new WebViewNavigationCompletedEventArgs { IsSuccess = success, Request = new Uri(url) });
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

    private void OnLostFocus()
    {
        LostFocus?.Invoke(this, EventArgs.Empty);
    }

    private void OnGotFocus()
    {
        GotFocus?.Invoke(this, EventArgs.Empty);
    }

    private void OnInput(RoutedEventArgs args)
    {
        Input?.Invoke(args);
    }

    private void CurrentDomainOnProcessExit(object? sender, EventArgs e)
    {
        Dispose();
    }

    private class NativeWebViewCallbacks(NativeWebViewAdapter adapter) : CallbackBase, INativeWebViewHandlers
    {
        private INativeWebViewHandlers _nativeWebViewHandlersImplementation;

        public void OnScriptResult(int id, int isError, IAvnString ppv)
        {
            using (ppv)
            {
                adapter.OnScriptResult(id, isError == 1, ppv.String);
            }
        }

        public void OnWebMessageReceived(IAvnString body)
        {
            using (body)
            {
                adapter.OnWebMessageReceived(body.String!);
            }
        }

        public void OnNavigationCompleted(IAvnString url, int success)
        {
            using (url)
            {
                adapter.OnNavigationCompleted(url.String!, success == 1);
            }
        }

        public unsafe void OnNavigationStarted(IAvnString url, int* cancel)
        {
            using (url)
            {
                *cancel = adapter.OnNavigationStarted(url.String!) ? 1 : 0;
            }
        }

        public void OnBecameFirstResponder()
        {
            adapter.OnGotFocus();
        }

        public void OnResignedFirstResponder()
        {
            adapter.OnLostFocus();
        }

        public int OnKeyDown(AvnInputModifiers modifiers, AvnPhysicalKey physicalKey)
        {
            var args = new KeyEventArgs
            {
                RoutedEvent = InputElement.KeyDownEvent,
                Route = RoutingStrategies.Bubble,
                PhysicalKey = (PhysicalKey)physicalKey,
                Key = ((PhysicalKey)physicalKey).ToQwertyKey(),
                KeyModifiers = (KeyModifiers)modifiers
            };
            Dispatcher.UIThread.Invoke(() => adapter.OnInput(args), DispatcherPriority.Input);
            return args.Handled ? 1 : 0;
        }

        public int OnKeyUp(AvnInputModifiers modifiers, AvnPhysicalKey physicalKey)
        {
            var args = new KeyEventArgs
            {
                RoutedEvent = InputElement.KeyUpEvent,
                Route = RoutingStrategies.Bubble,
                PhysicalKey = (PhysicalKey)physicalKey,
                Key = ((PhysicalKey)physicalKey).ToQwertyKey(),
                KeyModifiers = (KeyModifiers)modifiers
            };
            Dispatcher.UIThread.Invoke(() => adapter.OnInput(args), DispatcherPriority.Input);
            return args.Handled ? 1 : 0;
        }
    }
}
