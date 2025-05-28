using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Avalonia.Controls.Win.WebView1.Interop;

#if COM_SOURCE_GEN
[GeneratedComInterface(Options = ComInterfaceOptions.ComObjectWrapper)]
#else
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
[Guid("20409918-4a15-4c46-a55d-f79edb0bde8b")]
internal partial interface IWebViewControlNavigationCompletedEventArgs : IInspectable
{
#if !COM_SOURCE_GEN
    void _VtblGap1_3();
#endif

    IUriRuntimeClass get_Uri();

    [return: MarshalAs(UnmanagedType.Bool)]
    bool get_IsSuccess();
}
