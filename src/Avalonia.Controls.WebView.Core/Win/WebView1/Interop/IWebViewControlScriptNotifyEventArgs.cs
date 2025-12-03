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
[Guid("491de57b-6f49-41bb-b591-51b85b817037")]
internal partial interface IWebViewControlScriptNotifyEventArgs : IInspectable
{
#if !COM_SOURCE_GEN
    void _VtblGap1_3();
#endif

    IUriRuntimeClass get_Uri();
    IntPtr get_Value();
}
