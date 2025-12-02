using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
// ReSharper disable InconsistentNaming

namespace Avalonia.Controls.Win.WebView2.Interop;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct COREWEBVIEW2_COLOR
{
    public byte A;
    public byte R;
    public byte G;
    public byte B;
}

#if COM_SOURCE_GEN
[GeneratedComInterface(Options = ComInterfaceOptions.ComObjectWrapper)]
#else
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
[Guid("C979903E-D4CA-4228-92EB-47EE3FA96EAB")]
internal partial interface ICoreWebView2Controller2 : ICoreWebView2Controller
{
    // See https://learn.microsoft.com/en-us/dotnet/standard/native-interop/comwrappers-source-generation#derived-interfaces
#if !COM_SOURCE_GEN
    void _VtblGap1_23();
#endif

    COREWEBVIEW2_COLOR GetDefaultBackgroundColor();
    void SetDefaultBackgroundColor(COREWEBVIEW2_COLOR color);
}
