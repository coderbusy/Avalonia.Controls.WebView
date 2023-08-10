using System;
using System.Runtime.InteropServices;

namespace AvaloniaWebView.Win;

internal static unsafe class WinApiHelpers
{
    [DllImport("ole32.dll", ExactSpelling = true)]
    public static extern int CoCreateInstance(
        in Guid rclsid,
        IntPtr punkOuter,
        uint dwClsContext,
        in Guid riid,
        IntPtr* ppv);
    
    [DllImport("user32.dll", SetLastError=true)]
    public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }
}
