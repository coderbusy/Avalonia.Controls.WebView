using System;
using System.Runtime.Versioning;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Avalonia.Controls.Win;

[SupportedOSPlatform("windows6.1")]
internal static class WindowsUtility
{
    public static void MakeHwndTransparent(IntPtr hwnd)
    {
        var p = new HWND(hwnd);
        var exStyle = PInvoke.GetWindowLong(p, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
        // Add WS_EX_TRANSPARENT (0x00000020L) to first intermediate window while removing WS_EX_LAYERED (0x00080000L)
        // This combination ensures:
        // 1. WS_EX_TRANSPARENT: Makes the window visually transparent but still blocks content from behind the application
        // 2. Removing WS_EX_LAYERED: Prevents the window from creating its own compositing surface with default black background
        PInvoke.SetWindowLong(p, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, 
            (exStyle | (int)0x00000020L) ^ (int)0x00080000L);
    }
}
