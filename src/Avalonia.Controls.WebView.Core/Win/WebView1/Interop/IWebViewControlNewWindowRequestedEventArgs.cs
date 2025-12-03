using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Avalonia.Controls.Win.Interop;

namespace Avalonia.Controls.Win.WebView1.Interop;

#if COM_SOURCE_GEN
[GeneratedComInterface(Options = ComInterfaceOptions.ComObjectWrapper)]
#else
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
[Guid("3df44bbb-a124-46d5-a083-d02cacdff5ad")]
internal partial interface IWebViewControlNewWindowRequestedEventArgs : IInspectable
{
#if !COM_SOURCE_GEN
    void _VtblGap1_3();
#endif

    IUriRuntimeClass get_Uri();
    IUriRuntimeClass get_Referrer();
    [return: MarshalAs(UnmanagedType.Bool)]
    bool get_Handled();
    void put_Handled([MarshalAs(UnmanagedType.Bool)] bool value);
}
