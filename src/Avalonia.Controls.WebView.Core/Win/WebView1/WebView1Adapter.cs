using System;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using Windows.Win32;
using Windows.Win32.Foundation;
using Avalonia.Controls.Platform;
using Avalonia.Controls.Win.WebView1.Interop;
using Avalonia.Controls.Win.WebView2;
using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Threading;

namespace Avalonia.Controls.Win.WebView1;

[SupportedOSPlatform("windows6.1")]
internal sealed class WebView1Adapter : IWebViewAdapter, IWindowsWebView1PlatformHandle
{
    private readonly WebView1Process _process;
    private IWebViewControl? _webViewControl;
    private IWebViewControlSite? _webViewControlSite;
    private Action? _subscriptions;

    public WebView1Adapter(IPlatformHandle parent, IPlatformHandle handle, WebView1Process process)
    {
        process.AddOne();
        _process = process;
        Handle = handle.Handle;
        Initialize(parent);
    }

    public IntPtr Handle { get; }
    public string? HandleDescriptor => "HWDN";

    private async void Initialize(IPlatformHandle parent)
    {
        try
        {
            // Seems like WebView1 doesn't support transparency at all
            // WindowsUtility.MakeHwndTransparent(parent.Handle);

            if (!PInvoke.GetWindowRect(new HWND(Handle), out var rect))
                rect = RECT.FromXYWH(0, 0, 100, 100);

            var control = await _process.CreateWebViewControl(Handle, rect.Width, rect.Height);
            if (control.get_Settings() is { } settings)
            {
                settings.put_IsJavaScriptEnabled(true);
                settings.put_IsScriptNotifyAllowed(true);
            }

            // Doesn't work for some reason.
            // Instead injecting script in the NavigationCompleted
            // if (control is IWebViewControl2 control2)
            // {
            //      var initScript =
            //         new HStringInterop("""
            //                            window.invokeCSharpAction = function(data) {
            //                                var message = typeof data === 'object' ? JSON.stringify(data) : data;
            //                                window.external.notify(message);
            //                            };
            //                            """);
            //     control2.AddInitializeScript(initScript.Handle);
            // }

            _webViewControl = control;
            // ReSharper disable once SuspiciousTypeConversion.Global
            // IWebViewControlSite can be queried from the IWebViewControl
            _webViewControlSite = (IWebViewControlSite)control;

            _webViewControlSite.put_IsVisible(true);
            SizeChanged(default);

            _subscriptions = AddHandlers(_webViewControl);

            IsInitialized = true;
            Initialized?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Logger.TryGet(LogEventLevel.Error, "WebView")?
                .Log(null, "WebView1 initialization failed with unhandled exception", ex);
        }
    }

    public bool IsInitialized { get; private set; }

    public event EventHandler? Initialized;
    public event EventHandler<WebViewNewWindowRequestedEventArgs>? NewWindowRequested;
    public event EventHandler<WebMessageReceivedEventArgs>? WebMessageReceived;
    public event EventHandler<WebResourceRequestedEventArgs>? WebResourceRequested;
    public event EventHandler<WebViewNavigationCompletedEventArgs>? NavigationCompleted;
    public event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarted;

    public bool CanGoBack => _webViewControl?.get_CanGoBack() ?? false;

    public bool CanGoForward => _webViewControl?.get_CanGoForward() ?? false;

    public Uri Source
    {
        get
        {
            var absoluteUri = _webViewControl?.get_Source()?.get_AbsoluteUri();
            return absoluteUri.HasValue
                   && Uri.TryCreate(HStringInterop.FromIntPtr(absoluteUri.Value), UriKind.Absolute, out var uri)
                ? uri : WebViewHelper.EmptyPage;
        }
        set
        {
            Navigate(value);
        }
    }

    public bool GoBack()
    {
        if (!CanGoBack) return false;
        _webViewControl?.GoBack();
        return true;
    }

    public bool GoForward()
    {
        if (!CanGoForward) return false;
        _webViewControl?.GoForward();
        return true;
    }

    public async Task<string?> InvokeScript(string script)
    {
        if (_webViewControl is null)
            return null;

        using var args = new HStringIterator([script]);
        using var command = new HStringInterop("eval");

        var operation = _webViewControl.InvokeScriptAsync(command.Handle, args);
        var handler = new HStringResultHandler();
        operation.put_Completed(handler);

        return await handler.Task;
    }

    public void Navigate(Uri url)
    {
        var factory = NativeWinRTMethods.CreateActivationFactory<IUriRuntimeClassFactory>("Windows.Foundation.Uri");
        using var hstring = new HStringInterop(url.AbsoluteUri);
        var uri = factory?.CreateUri(hstring.Handle);
        if (uri is not null)
            _webViewControl?.put_Source(uri);
    }

    public void NavigateToString(string text)
    {
        using var hstring = new HStringInterop(text);
        _webViewControl?.NavigateToString(hstring.Handle);
    }

    public bool Refresh()
    {
        _webViewControl?.Refresh();
        return true;
    }

    public bool Stop()
    {
        _webViewControl?.Stop();
        return true;
    }

    public Color DefaultBackground
    {
        set
        {
            _webViewControl?.put_DefaultBackgroundColor(new winrtColor
            {
                A = 255,// value.A, -- doesn't seem to be supported with interop
                R = value.R,
                G = value.G,
                B = value.B,
            });
        }
    }

    public void SizeChanged(PixelSize containerSize)
    {
        Dispatcher.UIThread.Post(() =>
        {
            if (PInvoke.GetWindowRect(new HWND(Handle), out var rect)
                && _webViewControlSite is not null)
            {
                _webViewControlSite.put_Bounds(new winrtRect
                {
                    Width = rect.Width,
                    Height = rect.Height
                });
            }
        });
    }

    public void SetParent(IPlatformHandle parent)
    {
        if (parent.HandleDescriptor != "HWND")
            throw new InvalidOperationException("IPlatformHandle.HandleDescriptor must be HWND");

        PInvoke.SetParent(new HWND(Handle), new HWND(parent.Handle));
    }

    internal EventHandler<WebViewNavigationStartingEventArgs>? GetNavigationStarted() => NavigationStarted;
    internal EventHandler<WebViewNavigationCompletedEventArgs>? GetNavigationCompleted() => NavigationCompleted;
    internal EventHandler<WebMessageReceivedEventArgs>? GetWebMessageReceived() => WebMessageReceived;
    internal EventHandler<WebResourceRequestedEventArgs>? GetWebResourceRequested() => WebResourceRequested;
    internal EventHandler<WebViewNewWindowRequestedEventArgs>? GetNewWindowRequested() => NewWindowRequested;

    private Action AddHandlers(IWebViewControl webView)
    {
        var callbacks = new WebViewCallbacks(new WeakReference<WebView1Adapter>(this));
        webView.add_NavigationStarting(callbacks, out var token1);
        webView.add_NavigationCompleted(callbacks, out var token2);
        webView.add_ScriptNotify(callbacks, out var token3);
        webView.add_WebResourceRequested(callbacks, out var token4);
        webView.add_NewWindowRequested(callbacks, out var token5);

        return () =>
        {
            webView.remove_NavigationStarting(token1);
            webView.remove_NavigationCompleted(token2);
            webView.remove_ScriptNotify(token3);
            webView.remove_WebResourceRequested(token4);
            webView.remove_NewWindowRequested(token5);
        };
    }

    unsafe IntPtr IWindowsWebView1PlatformHandle.WebViewControl => _webViewControl is not null ?
        new(ComInterfaceMarshaller<IWebViewControl>.ConvertToUnmanaged(_webViewControl)) :
        IntPtr.Zero;

    private void ReleaseUnmanagedResources()
    {
        // Since process was technically created on UI thread, it's expected to release it on UI thread.
        // Not to mention, this method call might be done on finalizer thread.
        Dispatcher.UIThread.InvokeAsync(() => _process.ReleaseOne());
    }

    public void Dispose()
    {
        _webViewControl = null;
        Interlocked.Exchange(ref _subscriptions, null)?.Invoke();
        Interlocked.Exchange(ref _webViewControlSite, null)?.Close();

        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~WebView1Adapter()
    {
        ReleaseUnmanagedResources();
    }
}
