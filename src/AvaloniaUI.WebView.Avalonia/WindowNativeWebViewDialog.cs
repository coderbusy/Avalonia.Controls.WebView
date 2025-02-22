using System;
using System.Threading.Tasks;
using Avalonia.Platform;
#if WPF
using System.Windows;
using AvaloniaUI.Xpf.WpfAbstractions;
#elif AVALONIA
using Avalonia.Controls;
#endif

namespace AvaloniaUI.WebView;

internal class WindowNativeWebViewDialog : Window, INativeWebViewDialog
{
    private readonly NativeWebView _nativeWebView = new();

    public WindowNativeWebViewDialog()
    {
        Content = _nativeWebView;
        _nativeWebView.NavigationCompleted += (_, a) => NavigationCompleted?.Invoke(this, a);
        _nativeWebView.NavigationStarted += (_, a) => NavigationStarted?.Invoke(this, a);
        _nativeWebView.WebMessageReceived += (_, a) => WebMessageReceived?.Invoke(this, a);
    }

    public IWebView WebView => _nativeWebView;

    public bool CanGoBack => _nativeWebView.CanGoBack;
    public bool CanGoForward => _nativeWebView.CanGoForward;

    public Uri Source
    {
        get => _nativeWebView.Source;
        set => _nativeWebView.Source = value;
    }

    public event EventHandler<WebViewNavigationCompletedEventArgs>? NavigationCompleted;
    public event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarted;
    public event EventHandler<WebMessageReceivedEventArgs>? WebMessageReceived;
    public bool GoBack() => _nativeWebView.GoBack();
    public bool GoForward() => _nativeWebView.GoForward();
    public Task<string?> InvokeScript(string script) => _nativeWebView.InvokeScript(script);
    public void Navigate(Uri url) => _nativeWebView.Navigate(url);
    public void NavigateToString(string text) => _nativeWebView.NavigateToString(text);
    public bool Refresh() => _nativeWebView.Refresh();
    public bool Stop() => _nativeWebView.Stop();

    public void Dispose() {}

    void INativeWebViewDialog.Show(IPlatformHandle _) => throw new NotSupportedException();

#if WPF
    public IPlatformHandle? TryGetPlatformHandle() => XpfWpfAbstraction.GetAvaloniaTopLevelForWindow(this)?.TryGetPlatformHandle();
#endif
}
