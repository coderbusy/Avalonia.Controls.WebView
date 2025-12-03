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
[Guid("44d6524d-55a4-4d8b-891c-931d8e25d42e")]
internal partial interface IWebViewControlWebResourceRequestedEventArgs : IInspectable
{
#if !COM_SOURCE_GEN
    void _VtblGap1_3();
#endif

    [PreserveSig]
    unsafe int GetDeferral(void** deferral);
    IHttpRequestMessage GetRequest();
    IntPtr GetResponse();
    void SetResponse(IntPtr response);
}
