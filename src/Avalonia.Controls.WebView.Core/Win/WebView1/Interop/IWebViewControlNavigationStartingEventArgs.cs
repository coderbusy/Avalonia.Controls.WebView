using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Avalonia.Controls.Win.WebView1.Interop;

#if COM_SOURCE_GEN
[GeneratedComInterface(Options = ComInterfaceOptions.ComObjectWrapper)]
#else
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
[Guid("0c9057c5-0a08-41c7-863b-71e3a9549137")]
internal partial interface IWebViewControlNavigationStartingEventArgs : IInspectable
{
#if !COM_SOURCE_GEN
    void _VtblGap1_3();
#endif

    IUriRuntimeClass get_Uri();

    [return: MarshalAs(UnmanagedType.Bool)]
    bool get_Cancel();

    void put_Cancel([MarshalAs(UnmanagedType.Bool)] bool value);
}
