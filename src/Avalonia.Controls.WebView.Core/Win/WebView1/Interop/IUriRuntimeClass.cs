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
[Guid("9E365E57-48B2-4160-956F-C7385120BBFC")]
internal partial interface IUriRuntimeClass : IInspectable
{
#if !COM_SOURCE_GEN
    void _VtblGap1_3();
#endif

    IntPtr get_AbsoluteUri();
    IntPtr get_DisplayUri();
    IntPtr get_Domain();
    IntPtr get_Extension();
    IntPtr get_Fragment();
    IntPtr get_Host();
    IntPtr get_Password();
    IntPtr get_Path();
    IntPtr get_Query();
    IntPtr get_QueryParsed();
    IntPtr get_RawUri();
    IntPtr get_SchemeName();
    IntPtr get_UserName();

    int get_Port();

    [return: MarshalAs(UnmanagedType.Bool)]
    bool get_Suspicious();

    [return: MarshalAs(UnmanagedType.Bool)]
    bool Equals(IntPtr uri);

    IntPtr CombineUri(IntPtr relativeUri);
}
