using System;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace AvaloniaUI.WebView;

public static class WebAuthenticationBroker
{
    public static Task<WebAuthenticationResult> AuthenticateAsync(TopLevel topLevel, WebAuthenticatorOptions options)
    {
        if (OperatingSystemEx.IsIOS() || OperatingSystemEx.IsMacOS())
        {
            return MaciosWebAuthenticationBroker.AuthenticateAsync(topLevel, options);
        }

        if (OperatingSystemEx.IsWindows() || OperatingSystemEx.IsLinux() || OperatingSystemEx.IsMacOS())
        {
            return NativeWebViewDialogWebAuthenticationBroker.AuthenticateAsync(options);
        }

        throw new PlatformNotSupportedException();
    }
}

public record WebAuthenticatorOptions(Uri RequestUri, Uri CallbackUri);
public record WebAuthenticationResult(Uri CallbackUri);
