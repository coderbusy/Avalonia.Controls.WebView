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
[Guid("ee8b81d3-bbc2-55b0-877b-6ba86e3ad899")]
internal partial interface IWebViewControlScriptNotifyHandler
{
    void Invoke(IntPtr sender, IWebViewControlScriptNotifyEventArgs args);
}
