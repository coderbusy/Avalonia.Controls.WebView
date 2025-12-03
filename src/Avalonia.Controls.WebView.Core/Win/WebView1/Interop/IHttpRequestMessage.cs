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
[Guid("F5762B3C-74D4-4811-B5DC-9F8B4E2F9ABF")]
internal partial interface IHttpRequestMessage : IInspectable
{
#if !COM_SOURCE_GEN
    void _VtblGap1_3();
#endif

    /* IHttpContent */ IntPtr GetContent();
    void SetContent(/* IHttpContent */ IntPtr value);
    IHttpRequestHeaderCollection GetHeaders();
    IHttpMethod GetMethod();
    void SetMethod(IHttpMethod value);
    IntPtr Properties();
    IUriRuntimeClass GetRequestUri();
    void SetRequestUri(IUriRuntimeClass value);
    IntPtr GetTransportInformation();
}
