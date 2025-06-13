using System;
using Avalonia.Controls;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Avalonia.Platform;

public sealed class WindowsWebView2EnvironmentRequestedEventArgs : WebViewEnvironmentRequestedEventArgs
{
    internal bool PreferWebView1Instead { get; set; }

    // /// <summary>
    // /// This setting determines the UserAgent of WebView.
    // /// </summary>
    // /// <remarks>
    // /// This property may be overridden if the User-Agent header is set in a request.
    // /// </remarks>
    // See https://github.com/MicrosoftEdge/WebView2Feedback/issues/4993
    // public string? UserAgent { get; set; }

    /// <summary>
    /// Gets or sets already existing ICoreWebView2Environment COM reference handle that webview adapter will use instead of managing its own.
    /// </summary>
    public IntPtr ExplicitEnvironment { get; set; }

    /// <summary>
    /// ProfileName property is to specify a profile name, which is only allowed to contain the following ASCII characters.
    /// </summary>
    public string? ProfileName { get; set; }

    /// <summary>
    /// Gets or sets the value to pass as the browserExecutableFolder parameter of CreateAsync(String, String, CoreWebView2EnvironmentOptions) when creating an environment with this instance.
    /// </summary>
    public string? BrowserExecutableFolder { get; set; }

    /// <summary>
    /// Gets or sets the value to pass as the userDataFolder parameter of CreateAsync(String, String, CoreWebView2EnvironmentOptions) when creating an environment with this instance.
    /// </summary>
    public string? UserDataFolder { get; set; }

    /// <summary>
    /// Gets or sets the value to use for the AdditionalBrowserArguments property of the CoreWebView2EnvironmentOptions parameter passed to CreateAsync(String, String, CoreWebView2EnvironmentOptions) when creating an environment with this instance.
    /// </summary>
    /// <remarks>
    /// The arguments are passed to the browser process as part of the command. For more information about using command-line switches with Chromium browser processes, navigate to https://www.chromium.org/developers/how-tos/run-chromium-with-flags/.
    /// </remarks>
    public string? AdditionalBrowserArguments { get; set; }

    /// <summary>
    /// The default display language for WebView.
    /// </summary>
    /// <remarks>
    /// It applies to browser UI such as context menu and dialogs. It also applies to the accept-languages HTTP header that WebView sends to websites. The intended locale value is in the format of BCP 47 Language Tags
    /// </remarks>
    public string? Language { get; set; }

    /// <summary>
    /// IsInPrivateModeEnabled property is to enable/disable InPrivate mode.
    /// </summary>
    public bool IsInPrivateModeEnabled { get; set; }
}
