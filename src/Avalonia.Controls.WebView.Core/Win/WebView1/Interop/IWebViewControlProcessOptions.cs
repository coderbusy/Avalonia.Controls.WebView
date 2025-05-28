using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Avalonia.Controls.Win.WebView1.Interop;

internal enum WebViewControlProcessCapabilityState
{
    Default  = 0,
    Disabled = 1,
    Enabled  = 2
};

#if COM_SOURCE_GEN
[GeneratedComInterface(Options = ComInterfaceOptions.ComObjectWrapper)]
#else
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
[Guid("1CCA72A7-3BD6-4826-8261-6C8189505D89")]
internal partial interface IWebViewControlProcessOptions : IInspectable
{
#if !COM_SOURCE_GEN
    void _VtblGap1_3();
#endif

    void put_EnterpriseId(IntPtr value);

    IntPtr get_EnterpriseId();
    
    void put_PrivateNetworkClientServerCapability(WebViewControlProcessCapabilityState value);
    
    WebViewControlProcessCapabilityState get_PrivateNetworkClientServerCapability();
}
