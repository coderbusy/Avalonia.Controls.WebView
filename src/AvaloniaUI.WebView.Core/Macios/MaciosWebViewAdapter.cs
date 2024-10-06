using System;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using AppleInterop;
using Avalonia.Platform;
using AvaloniaUI.WebView.Macios.Interop;

namespace AvaloniaUI.WebView.Macios;

[SupportedOSPlatform("macos")]
[SupportedOSPlatform("ios")]
public class MaciosWebViewAdapter : IWebViewAdapterWithFocus
{
    private const string PostAvWebViewMessageName = "postAvWebViewMessage";

    private readonly WKWebViewConfiguration _config;
    private readonly WKWebView _webView;
    private readonly WKNavigationDelegate _navDelegate;
    private readonly WKScriptMessageHandler _scriptHandler;

    public MaciosWebViewAdapter()
    {
        _scriptHandler = new WKScriptMessageHandler();
        _scriptHandler.DidReceiveScriptMessage += (sender, args) =>
        {
            if (args.Name == PostAvWebViewMessageName)
            {
                WebMessageReceived?.Invoke(this, new WebMessageReceivedEventArgs
                {
                    Body = args.Body
                });
            }
        };

        _config = new WKWebViewConfiguration { JavaScriptEnabled = true };
        _config.AddScriptMessageHandler(_scriptHandler, PostAvWebViewMessageName);

        _navDelegate = new WKNavigationDelegate();
        _navDelegate.DidFinishNavigation += async (sender, args) =>
        {
            _ = await InvokeScript(
                $"function invokeCSharpAction(data){{window.webkit.messageHandlers.{PostAvWebViewMessageName}.postMessage(data);}}");

            using var url = _webView!.Url;
            NavigationCompleted?.Invoke(this, new WebViewNavigationCompletedEventArgs
            {
                Request = Uri.TryCreate(url.AbsoluteString, UriKind.Absolute, out var uri) ? uri : null,
                IsSuccess = true
            });
        };
        _navDelegate.DecidePolicyNavigation += (sender, args) =>
        {
            var startedArgs = new WebViewNavigationStartingEventArgs { Request = args.Request };
            NavigationStarted?.Invoke(this, startedArgs);
            args.Cancel = startedArgs.Cancel;
        };

        _webView = new WKWebView(_config) { NavigationDelegate = _navDelegate };
    }

    public IntPtr Handle => _webView.Handle;
    public string? HandleDescriptor => OperatingSystemEx.IsMacOS() ? "NSView" : "UIView";
    public bool IsInitialized => true;
    public event EventHandler? Initialized;

    public bool CanGoBack => _webView.CanGoBack;
    public bool CanGoForward => _webView.CanGoForward;
    public Uri Source
    {
        get
        {
            using var sourceUrl = _webView.Url;
            return Uri.TryCreate(sourceUrl.AbsoluteString, UriKind.RelativeOrAbsolute, out var source) ?
                source : WebViewHelper.EmptyPage;
        }
        set => Navigate(value);
    }
    public event EventHandler<WebViewNavigationCompletedEventArgs>? NavigationCompleted;
    public event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarted;
    public event EventHandler<WebMessageReceivedEventArgs>? WebMessageReceived;
    public bool GoBack() => _webView.GoBack() != default;
    public bool GoForward() => _webView.GoForward() != default;

    public Task<string?> InvokeScript(string script) => _webView.EvaluateJavaScriptAsync(script);

    public void Navigate(Uri url)
    {
        using var nsStr = NSString.Create(url.ToString());
        using var nsUrl = new NSUrl(nsStr);
        using var request = new NSURLRequest(nsUrl);
        _ = _webView.LoadRequest(request);
    }

    public void NavigateToString(string text)
    {
        using var baseUrlStr = NSString.Create("http://localhost:12345/");
        using var baseUrl = new NSUrl(baseUrlStr);
        using var html = NSString.Create(text);
        _ = _webView.LoadHtmlString(html, baseUrl);
    }

    public bool Refresh() => _webView.Reload() != default;

    public bool Stop()
    {
        _webView.StopLoading();
        return true;
    }

    public void Dispose()
    {
        _webView.NavigationDelegate = null;
        _webView.Dispose();
        _config.Dispose();
        _navDelegate.Dispose();
    }

    public void SizeChanged()
    {
    }

    public void SetParent(IPlatformHandle parent)
    {
        // no-op
        // macOS control don't need to be explicitly parented
    }

    public bool Focus()
    {
        return true;
    }

    public bool ResignFocus()
    {
        return true;
    }

    public event EventHandler? GotFocus;
    public event EventHandler? LostFocus;
}
