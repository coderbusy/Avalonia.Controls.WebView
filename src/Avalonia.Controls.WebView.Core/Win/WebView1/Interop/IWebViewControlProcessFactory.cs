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
[Guid("47B65CF9-A2D2-453C-B097-F6779D4B8E02")]
internal partial interface IWebViewControlProcessFactory : IInspectable
{
#if !COM_SOURCE_GEN
    void _VtblGap1_3();
#endif

    IWebViewControlProcess CreateWithOptions(IWebViewControlProcessOptions processOptions);
}
