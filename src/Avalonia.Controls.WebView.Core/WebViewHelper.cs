using System;

namespace Avalonia.Controls;

internal class WebViewHelper
{
    public static Uri EmptyPage { get; } = new("about:blank");
    public static bool GtkOffscreenAvailable()
    {
        var options = AvaloniaLocator.Current.GetService<WebViewOptions>();
        return options?.ExperimentalGtkOffscreen ?? false;
    }
}
