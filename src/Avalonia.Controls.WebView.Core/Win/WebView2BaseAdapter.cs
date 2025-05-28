#if !ANDROID && (NET6_0_OR_GREATER || NETFRAMEWORK)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Windows.Win32;
using Windows.Win32.Foundation;
using Avalonia.Controls.Utils;
using Avalonia.Controls.Win.RawWebView2;
using Avalonia.MicroCom;
using Avalonia.Platform;
using Avalonia.Threading;
using MicroCom.Runtime;

namespace Avalonia.Controls.Win;

[SupportedOSPlatform("windows6.1")] // win7
internal abstract partial class WebView2BaseAdapter : IWebViewAdapterWithCookieManager
{
    private ICoreWebView2Controller? _controller;
    private Action? _subscriptions;

    protected WebView2BaseAdapter(IPlatformHandle parent)
    {
        Initialize(parent);
    }

    public abstract IntPtr Handle { get; }
    public abstract string? HandleDescriptor { get; }

    protected unsafe IWebView2WebView? TryGetWebView2()
    {
        try
        {
            void* webView = null;
            _controller?.get_CoreWebView2(&webView);
            return MicroComRuntime.CreateProxyFor<IWebView2WebView>(webView, true);
        }
        // That's what WPF control does.
        catch (COMException ex) when (ex.HResult == -2147019873)
        {
            return null;
        }
    }

    public bool IsInitialized { get; private set; }

    public bool CanGoBack => true;// TryGetWebView2()?.CanGoBack ?? false;

    public bool CanGoForward => true;// TryGetWebView2()?.CanGoForward ?? false;

    public Uri Source
    {
        get
        {
            return WebViewHelper.EmptyPage;
            //return Uri.TryCreate(TryGetWebView2()?.Source, UriKind.Absolute, out var url) ? url : null!;
        }
        set => Navigate(value);
    }

    public event EventHandler<WebViewNavigationCompletedEventArgs>? NavigationCompleted;
    public event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarted;
    public event EventHandler<WebViewNewWindowRequestedEventArgs>? NewWindowRequested;
    public event EventHandler<WebMessageReceivedEventArgs>? WebMessageReceived;
    public event EventHandler? Initialized;

    public bool GoBack()
    {
        if (TryGetWebView2() is { } webView2)
        {
            webView2.GoBack();
            return true;
        }
        return false;
    }

    public bool GoForward()
    {
        if (TryGetWebView2() is { } webView2)
        {
            webView2.GoForward();
            return true;
        }
        return false;
    }

    public Task<string?> InvokeScript(string scriptName)
    {
        return Task.FromResult<string?>(null);
        //return TryGetWebView2()?.ExecuteScriptAsync(scriptName) ?? Task.FromResult<string?>(null);
    }

    public unsafe void Navigate(Uri url)
    {
        fixed (char* p = url.AbsoluteUri)
            TryGetWebView2()?.Navigate(p);
    }

    public void NavigateToString(string text)
    {
        //TryGetWebView2()?.NavigateToString(text);
    }

    public bool Refresh()
    {
        TryGetWebView2()?.Reload();
        return true;
    }

    public bool Stop()
    {
        //TryGetWebView2()?.Stop();
        return true;
    }

    public virtual void SizeChanged(PixelSize containerSize)
    {
        Dispatcher.UIThread.Post(() =>
        {
            if (PInvoke.GetWindowRect(new HWND(Handle), out var rect)
                && _controller is not null)
            {
                // _controller.BoundsMode = CoreWebView2BoundsMode.UseRawPixels;
                // _controller.Bounds = new Rectangle(0, 0, rect.Width, rect.Height);
                _controller.NotifyParentWindowPositionChanged();
            }
        });
    }

    public virtual void SetParent(IPlatformHandle parent)
    {
        if (_controller is null)
            return;

        if (parent.HandleDescriptor != "HWND")
            throw new InvalidOperationException("IPlatformHandle.HandleDescriptor must be HWND");

        //_controller.ParentWindow = parent.Handle;
    }

    enum WebView2RunTimeType { kInstalled = 0x0, kRedistributable = 0x1 }
    private unsafe int CreateEnv(IntPtr createEnvProc, WebView2RunTimeType runTimeType, string? userDataFolder, ICoreWebView2EnvironmentOptions options, ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler envCallback)
    {
        var callbackPtr = envCallback.GetNativeIntPtr(true);
        var optionsPtr = options.GetNativeIntPtr(true);

        // TODO, we might want to keep userDataFolder pinned until callback is called.
        // But it's null anyway atm, so ignoring.
        var createEnvFunc = (delegate* unmanaged[Stdcall]<int, WebView2RunTimeType, IntPtr, IntPtr, IntPtr, int>)createEnvProc;
        fixed (char* userDataFolderPtr = userDataFolder)
            return createEnvFunc(1, runTimeType, new IntPtr(userDataFolderPtr), optionsPtr, callbackPtr);
    }

    private async void Initialize(IPlatformHandle parentHost)
    {
        var webViewRuntime = ManagedWebView2Loader.FindWebView2Runtime();
        if (webViewRuntime is null)
            return;

        var lib = NativeLibraryEx.Load(webViewRuntime);
        if (NativeLibraryEx.TryGetExport(lib, "CreateWebViewEnvironmentWithOptionsInternal", out var createEnvPtr))
            throw new InvalidOperationException("CreateWebViewEnvironmentWithOptions is not supported.");

        ICoreWebView2Environment env;
        using (var envCallback = new WebView2EnvHandler())
        using (var options = new Options())
        {
            var res = CreateEnv(createEnvPtr, WebView2RunTimeType.kInstalled, null, options, envCallback);
            if (res != 0)
                throw new Win32Exception(res);
            using (var envRes = await envCallback.Result.Task)
                env = envRes.CloneReference();
        }

        var controller = await CreateWebView2Controller(env, parentHost.Handle);
        // controller.get_CoreWebView2();
        //await webView.AddScriptToExecuteOnDocumentCreatedAsync(
        //    "function invokeCSharpAction(data){window.chrome.webview.postMessage(data);}");
        controller.put_IsVisible(1);// .IsVisible = true;
        //controller.ShouldDetectMonitorScaleChanges = false;
        _controller = controller;

        SizeChanged(default);

        _subscriptions = AddHandlers(TryGetWebView2()!);

        IsInitialized = true;
        Initialized?.Invoke(this, EventArgs.Empty);
    }

    private class Options : CallbackBase, ICoreWebView2EnvironmentOptions
    {
        public unsafe void get_AdditionalBrowserArguments(char** value)
        {
//            throw new NotImplementedException();
        }

        public unsafe void put_AdditionalBrowserArguments(char* value)
        {
            //throw new NotImplementedException();
        }

        public unsafe void get_Language(char** value)
        {
            //throw new NotImplementedException();
        }

        public unsafe void put_Language(char* value)
        {
            //throw new NotImplementedException();
        }

        public unsafe void get_TargetCompatibleBrowserVersion(char** value)
        {
            const string targetver = "135.0.3179.45";
            var targetVerPtr = Marshal.StringToHGlobalUni(targetver);
            *value = (char*)targetVerPtr.ToPointer();
        }

        public unsafe void put_TargetCompatibleBrowserVersion(char* value)
        {
        }

        public unsafe void get_AllowSingleSignOnUsingOSPrimaryAccount(int* allow)
        {
            
        }

        public void put_AllowSingleSignOnUsingOSPrimaryAccount(int allow)
        {
            
        }
    }
    
    private class WebView2EnvHandler : CallbackBase, ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler
    {
        public TaskCompletionSource<ICoreWebView2Environment> Result { get; } = new();
        public void Invoke(int errorCode, ICoreWebView2Environment result)
        {
            if (errorCode != 0)
                Result?.TrySetException(new Win32Exception(errorCode));
            else
                Result?.TrySetResult(result);
        }
    }
    
    protected abstract Task<ICoreWebView2Controller> CreateWebView2Controller(ICoreWebView2Environment env, IntPtr handle);

    private Action AddHandlers(IWebView2WebView webView)
    {
        // webView.NavigationStarting += WebViewOnNavigationStarting;
        //
        // void WebViewOnNavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
        // {
        //     if (Uri.TryCreate(e.Uri, UriKind.Absolute, out var uri))
        //     {
        //         var args = new WebViewNavigationStartingEventArgs { Request = uri };
        //         NavigationStarted?.Invoke(this, args);
        //         if (args.Cancel) e.Cancel = true;
        //     }
        // }
        //
        // webView.NavigationCompleted += WebViewOnNavigationCompleted;
        //
        // void WebViewOnNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        // {
        //     NavigationCompleted?.Invoke(this,
        //         new WebViewNavigationCompletedEventArgs
        //         {
        //             Request = new Uri(((CoreWebView2)sender!).Source),
        //             IsSuccess = e.IsSuccess
        //         });
        // }
        //
        // webView.WebMessageReceived += WebViewOnWebMessageReceived;
        //
        // void WebViewOnWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        // {
        //     string? message = null;
        //
        //     try
        //     {
        //         // this `Try` method can throw undescriptive ArgumentException. Keep going WinRT.
        //         message = e.TryGetWebMessageAsString();
        //     }
        //     catch
        //     {
        //         // ignore
        //     }
        //
        //     message ??= e.WebMessageAsJson;
        //
        //     WebMessageReceived?.Invoke(this, new WebMessageReceivedEventArgs { Body = message });
        // }
        //
        // webView.NewWindowRequested += WebViewOnNewWindowRequested;
        //
        // void WebViewOnNewWindowRequested(object? sender, CoreWebView2NewWindowRequestedEventArgs e)
        // {
        //     if (Uri.TryCreate(e.Uri, UriKind.Absolute, out var uri))
        //     {
        //         var args = new WebViewNewWindowRequestedEventArgs { Request = uri };
        //         NewWindowRequested?.Invoke(this, args);
        //         if (args.Handled) e.Handled = true;
        //     }
        // }

        return () =>
        {
            // webView.NavigationStarting -= WebViewOnNavigationStarting;
            // webView.NavigationCompleted -= WebViewOnNavigationCompleted;
            // webView.WebMessageReceived -= WebViewOnWebMessageReceived;
        };
    }

    public void AddOrUpdateCookie(Cookie cookie)
    {
        if (TryGetWebView2() is { } webView)
        {
            // var webViewCookie = webView.CookieManager.CreateCookieWithSystemNetCookie(cookie);
            // webView.CookieManager.AddOrUpdateCookie(webViewCookie);
        }
    }

    public void DeleteCookie(string name, string domain, string path)
    {
        //TryGetWebView2()?.CookieManager.DeleteCookiesWithDomainAndPath(name, domain, path);
    }

    public Task<IReadOnlyList<Cookie>> GetCookiesAsync()
    {
        return Task.FromResult<IReadOnlyList<Cookie>>([]);
        // if (TryGetWebView2() is not { } webView)
        //     return [];
        // var cookies = await webView.CookieManager.GetCookiesAsync(null);
        // return cookies.Select(c => c.ToSystemNetCookie()).ToArray();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _controller?.Close();
            _subscriptions?.Invoke();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~WebView2BaseAdapter()
    {
        Dispose(false);
    }
}
#endif
