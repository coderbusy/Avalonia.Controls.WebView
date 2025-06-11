using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Avalonia.Controls.Win.WebView1.Interop;

#if COM_SOURCE_GEN
[GeneratedComInterface(Options = ComInterfaceOptions.ManagedObjectWrapper)]
#else
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
[Guid("3a6ed2bc-032b-5ec7-a20a-c1ef49250c3c")]
internal partial interface IWebViewControlWebResourceRequestedHandler
{
    void Invoke(IntPtr sender, IWebViewControlWebResourceRequestedEventArgs args);
}
