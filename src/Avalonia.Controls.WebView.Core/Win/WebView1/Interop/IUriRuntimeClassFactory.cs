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
[Guid("44A9796F-723E-4FDF-A218-033E75B0C084")]
internal partial interface IUriRuntimeClassFactory : IInspectable
{
#if !COM_SOURCE_GEN
    void _VtblGap1_3();
#endif

    IUriRuntimeClass CreateUri(IntPtr uri);

    IUriRuntimeClass CreateWithRelativeUri(IntPtr baseUri, IntPtr relativeUri);
}
