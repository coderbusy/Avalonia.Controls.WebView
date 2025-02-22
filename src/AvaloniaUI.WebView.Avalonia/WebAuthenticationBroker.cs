using System;
using System.Threading.Tasks;
using AvTopLevel = Avalonia.Controls.TopLevel;
#if WPF
using System.Windows;
using AvaloniaUI.Xpf.WpfAbstractions;
#elif AVALONIA
using Avalonia.Controls;
#endif

namespace AvaloniaUI.WebView;

public static class WebAuthenticationBroker
{
    public static Task<WebAuthenticationResult> AuthenticateAsync
#if WPF
        (Window topLevel, WebAuthenticatorOptions options)
#elif AVALONIA
        (AvTopLevel topLevel, WebAuthenticatorOptions options)
#endif
    {
        var supportsNativeWebDialog =
            OperatingSystemEx.IsWindows() || OperatingSystemEx.IsLinux() || OperatingSystemEx.IsMacOS();

        if (!(supportsNativeWebDialog & options.PreferNativeWebViewDialog)
            && GetAvTopLevel(topLevel) is { } avTopLevel
            && (OperatingSystemEx.IsIOSVersionAtLeast(13, 0) || OperatingSystemEx.IsMacOSVersionAtLeast(10, 15)))
        {
            return MaciosWebAuthenticationBroker.AuthenticateAsync(avTopLevel, options);
        }

        if (supportsNativeWebDialog)
        {
            return AuthenticateDialogAsync(topLevel, options);
        }

        throw new PlatformNotSupportedException();
    }

#if WPF
    private static AvTopLevel? GetAvTopLevel(Window window) =>
        XpfWpfAbstraction.GetAvaloniaTopLevelForWindow(window);
#elif AVALONIA
    private static AvTopLevel? GetAvTopLevel(AvTopLevel topLevel) => topLevel;
#endif

    private static async Task<WebAuthenticationResult> AuthenticateDialogAsync
#if WPF
        (Window topLevel, WebAuthenticatorOptions options)
#elif AVALONIA
        (AvTopLevel topLevel, WebAuthenticatorOptions options)
#endif
    {
        using var dialog = new NativeWebViewDialog();
        var tcs = new TaskCompletionSource<WebAuthenticationResult>();

        dialog.NavigationStarted += OnNavigationStarted;

        try
        {
            dialog.Title = "Authentication";
            dialog.Source = options.RequestUri;
            if (topLevel is Window owner && dialog.TryGetWindow() is { } window)
            {
#if WPF
                window.Owner = owner;
                window.Show();
#elif AVALONIA
                window.Show(owner);
#endif
            }
            else if (GetAvTopLevel(topLevel)?.TryGetPlatformHandle() is { } platformHandle)
            {
                dialog.Show(platformHandle);
            }
            else
            {
                dialog.Show();
            }

            return await tcs.Task;
        }
        finally
        {
            dialog.NavigationStarted -= OnNavigationStarted;
            dialog.Close();
        }

        void OnNavigationStarted(object? sender, WebViewNavigationStartingEventArgs e)
        {
            if (e.Request is not null && IsCallbackUri(e.Request, options.CallbackUri))
            {
                e.Cancel = true;
                tcs.TrySetResult(new WebAuthenticationResult(e.Request));
            }
        }
    }

    private static bool IsCallbackUri(Uri navigatingUri, Uri callbackUri)
    {
        return navigatingUri.Scheme == callbackUri.Scheme
               && navigatingUri.Host == callbackUri.Host
               && navigatingUri.AbsolutePath == callbackUri.AbsolutePath;
    }
}
