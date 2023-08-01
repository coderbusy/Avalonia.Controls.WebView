using System.Text.Json;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;

namespace AvaloniaWebView.TinyMCE;

internal record JsPayload(string type, string body);

public class TinyMceView : Decorator
{
    private readonly NativeWebView _nativeWebView;
    private static readonly string s_htmlPage = HtmlPageBuilder.Build();
    private bool _ignoreChanges;

    
    public TinyMceView()
    {
        Child = _nativeWebView = new NativeWebView();
        _nativeWebView.WebMessageReceived += NativeWebViewOnWebMessageReceived;
        _nativeWebView.Initialized += NativeWebViewOnInitialized;
        _nativeWebView.NavigationCompleted += NativeWebViewOnNavigationCompleted;
    }

    public static readonly StyledProperty<string?> HtmlTextProperty = AvaloniaProperty.Register<TinyMceView, string?>(nameof(HtmlText));

    public string? HtmlText
    {
        get => GetValue(HtmlTextProperty);
        set => SetValue(HtmlTextProperty, value);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == HtmlTextProperty)
        {
            SendCurrentText();
        }
    }

    private void NativeWebViewOnInitialized(object? sender, EventArgs e)
    {
        _nativeWebView.NavigateToString(s_htmlPage);
    }

    private void NativeWebViewOnNavigationCompleted(object? sender, WebViewNavigationCompletedEventArgs e)
    {
        SendCurrentText();
    }

    private void NativeWebViewOnWebMessageReceived(object? sender, WebMessageReceivedEventArgs e)
    {
        if (_ignoreChanges) return;

        _ignoreChanges = true;
        var payload = JsonSerializer.Deserialize<JsPayload>(e.Body!);
        if (payload?.type == "textChanged")
        {
            SetCurrentValue(HtmlTextProperty, payload.body);
        }
        _ignoreChanges = false;
    }

    private void SendCurrentText()
    {
        if (_ignoreChanges) return;

        _ignoreChanges = true;
        var payload = JsonSerializer.Serialize(new JsPayload("textChanging", HtmlText ?? ""));
        _nativeWebView.InvokeScript($"sendPayload('{JsonEncodedText.Encode(payload)}')");
        _ignoreChanges = false;
    }
}
