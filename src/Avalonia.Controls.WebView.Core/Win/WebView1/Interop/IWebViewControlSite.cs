using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Windows.Win32.Foundation;

namespace Avalonia.Controls.Win.WebView1.Interop;

internal enum WebViewControlMoveFocusReason
{
    Programmatic = 0,
    Next = 1,
    Previous = 2
}

#if COM_SOURCE_GEN
[GeneratedComInterface(Options = ComInterfaceOptions.ComObjectWrapper)]
#else
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
[Guid("133F47C6-12DC-4898-BD47-04967DE648BA")]
internal partial interface IWebViewControlSite : IInspectable
{
#if !COM_SOURCE_GEN
    void _VtblGap1_3();
#endif

    IWebViewControlProcess get_Process();

    void put_Scale(double value);

    double get_Scale();

    void put_Bounds(winrtRect value);

    winrtRect get_Bounds();

    void put_IsVisible([MarshalAs(UnmanagedType.Bool)] bool value);

    [return: MarshalAs(UnmanagedType.Bool)]
    bool get_IsVisible();

    void Close();

    void MoveFocus(WebViewControlMoveFocusReason reason);
}
