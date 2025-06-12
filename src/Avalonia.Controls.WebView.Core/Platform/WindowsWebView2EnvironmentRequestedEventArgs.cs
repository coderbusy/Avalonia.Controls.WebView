using System;
using Avalonia.Controls;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Avalonia.Platform;

public sealed class WindowsWebView2EnvironmentRequestedEventArgs : WebViewEnvironmentRequestedEventArgs
{
    public IntPtr ExplicitEnvironment { get; set; }

    public string? ProfileName { get; set; }

    public string? BrowserExecutableFolder { get; set; }

    public string? UserDataFolder { get; set; }

    public string? AdditionalBrowserArguments { get; set; }

    public string? Language { get; set; }

    public bool IsInPrivateModeEnabled { get; set; }
    
    public bool AllowSingleSignOnUsingOSPrimaryAccount { get; set; }
}
