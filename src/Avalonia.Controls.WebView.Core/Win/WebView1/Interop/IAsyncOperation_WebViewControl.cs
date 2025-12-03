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
[Guid("ac3d28ac-8362-51c6-b2cc-16f3672758f1")]
internal partial interface IAsyncOperation_WebViewControl : IInspectable
{
#if !COM_SOURCE_GEN
    void _VtblGap1_3();
#endif

    void put_Completed(IAsyncOperationCompletedHandler_WebViewControl handler);

    IAsyncOperationCompletedHandler_WebViewControl get_Completed();

    IWebViewControl GetResults();
}

#if COM_SOURCE_GEN
[GeneratedComInterface(Options = ComInterfaceOptions.ManagedObjectWrapper)]
#else
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
[Guid("d61963d6-806d-50a8-a81c-75d9356ad5d7")]
internal partial interface IAsyncOperationCompletedHandler_WebViewControl
{
    void Invoke(IAsyncOperation_WebViewControl asyncInfo, AsyncStatus asyncStatus);
}
