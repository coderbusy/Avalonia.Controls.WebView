using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;

namespace AvaloniaWebView;

public class NativeWebView : NativeControlHost, IWebView
{
    private bool _ignoreNavigation = false;
    private TaskCompletionSource<IWebViewAdapter> _webViewReadyCompletion = new();

    public event EventHandler<WebViewNavigationCompletedEventArgs>? NavigationCompleted;

    public event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarted;
    public event EventHandler<WebMessageReceivedEventArgs>? WebMessageReceived;

    public static readonly StyledProperty<Uri?> SourceProperty = AvaloniaProperty.Register<NativeWebView, Uri?>(
        nameof(Source), new Uri("about:blank"));

    public Uri? Source
    {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }
    
    public bool CanGoBack => TryGetAdapter()?.CanGoBack ?? false;

    public bool CanGoForward => TryGetAdapter()?.CanGoForward ?? false;

    public bool GoBack() => TryGetAdapter()?.GoBack() ?? false;

    public bool GoForward() => TryGetAdapter()?.GoForward() ?? false;

    public async Task<string?> InvokeScript(string scriptName)
    {
        try
        {
            var adapter = await _webViewReadyCompletion.Task;
            return await adapter.InvokeScript(scriptName);
        }
        catch (OperationCanceledException)
        {
            return null;
        }
    }

    public async void Navigate(Uri url)
    {
        try
        {
            var adapter = await _webViewReadyCompletion.Task;
            adapter.Navigate(url);
        }
        catch (OperationCanceledException)
        {
        }
    }

    public async void NavigateToString(string text)
    {
        try
        {
            var adapter = await _webViewReadyCompletion.Task;
            adapter.NavigateToString(text);
        }
        catch (OperationCanceledException)
        {
        }
    }

    public bool Refresh() => TryGetAdapter()?.Refresh() ?? false;

    public bool Stop() => TryGetAdapter()?.Stop() ?? false;

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        if (Design.IsDesignMode)
        {
            return base.CreateNativeControlCore(parent);
        }

        IWebViewAdapter adapter;
        
#if WINDOWS
        if (WebViewCapabilities.IsMsWebView2Available)
        {
            adapter = new Win.WebView2Adapter(base.CreateNativeControlCore(parent));
        }
        else if (WebViewCapabilities.IsMsWebView1Available)
        {
            adapter = new Win.WebView1Adapter(base.CreateNativeControlCore(parent));
        }
        else
        {
            return base.CreateNativeControlCore(parent);
        }
#else
        if (OperatingSystem.IsMacOS())
        {
            adapter = new NativeWebViewAdapter();
        }
        // if (OperatingSystem.IsLinux())
        // {
        //     new Gtk.GtkWebView2Adapter();
        //
        //     return base.CreateNativeControlCore(parent);
        //     // adapter = new Gtk.GtkWebView2Adapter();
        // }
        // else if (OperatingSystem.IsBrowser())
        // {
        //     adapter = new BrowserIFrameAdapter();
        // }
        // else
        else
        {
            return base.CreateNativeControlCore(parent);
        }
#endif

        if (adapter.IsInitialized)
        {
            WebViewAdapterOnInitialized(adapter, EventArgs.Empty);
        }
        else
        {
            adapter.Initialized += WebViewAdapterOnInitialized;
        }

        return adapter;
    }

    private IWebViewAdapter? TryGetAdapter() => _webViewReadyCompletion.Task.IsCompletedSuccessfully ?
        _webViewReadyCompletion.Task.Result :
        null;

    private void WebViewAdapterOnWebMessageReceived(object? sender, WebMessageReceivedEventArgs e)
    {
        WebMessageReceived?.Invoke(this, e);
    }

    private void WebViewAdapterOnNavigationStarted(object? sender, WebViewNavigationStartingEventArgs e)
    {
        NavigationStarted?.Invoke(this, e);
    }

    private void WebViewAdapterOnNavigationCompleted(object? sender, WebViewNavigationCompletedEventArgs e)
    {
        _ignoreNavigation = true;
        try
        {
            SetCurrentValue(SourceProperty, e.Request);
            NavigationCompleted?.Invoke(this, e);
        }
        finally
        {
            _ignoreNavigation = false;
        }
    }
    
    private void WebViewAdapterOnInitialized(object? sender, EventArgs e)
    {
        var adapter = (IWebViewAdapter)sender!;
        adapter.Initialized -= WebViewAdapterOnInitialized;
        adapter.NavigationStarted += WebViewAdapterOnNavigationStarted;
        adapter.NavigationCompleted += WebViewAdapterOnNavigationCompleted;
        adapter.WebMessageReceived += WebViewAdapterOnWebMessageReceived;

        _webViewReadyCompletion.TrySetResult(adapter);

        if (IsSet(SourceProperty)
            && Source is { } source
            && adapter.Source != source)
        {
            adapter.Navigate(source);
        }
    }
    
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == SourceProperty)
        {
            if (!_ignoreNavigation
                && change.GetNewValue<Uri?>() is { } source)
            {
                Navigate(source);
            }
        }
        else if (change.Property == BoundsProperty)
        {
            TryGetAdapter()?.SizeChanged();
        }
    }

    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        if (control is IWebViewAdapter adapter)
        {
            Debug.Assert(!(TryGetAdapter() is { } oldAdapter && oldAdapter != adapter));
            
            _webViewReadyCompletion.TrySetCanceled();
            _webViewReadyCompletion = new TaskCompletionSource<IWebViewAdapter>();
            adapter.NavigationStarted -= WebViewAdapterOnNavigationStarted;
            adapter.NavigationCompleted -= WebViewAdapterOnNavigationCompleted;
            adapter.WebMessageReceived -= WebViewAdapterOnWebMessageReceived;
            adapter.Initialized -= WebViewAdapterOnInitialized;
            adapter.Dispose();
        }
    }
}
