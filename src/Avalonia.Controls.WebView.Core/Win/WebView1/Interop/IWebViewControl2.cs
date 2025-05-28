using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Avalonia.Controls.Win.WebView1.Interop;

#if COM_SOURCE_GEN
[GeneratedComInterface(Options = ComInterfaceOptions.ComObjectWrapper)]
#else
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
[Guid("4D3C06F9-C8DF-41CC-8BD5-2A947B204503")]
internal partial interface IWebViewControl2 : IWebViewControl
{
#if !COM_SOURCE_GEN
    void _VtblGap1_60();
#endif

    void AddInitializeScript(IntPtr script);
}
