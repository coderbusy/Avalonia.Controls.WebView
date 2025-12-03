using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Avalonia.Controls.Win.Interop;

#if COM_SOURCE_GEN
[GeneratedComInterface]
#else
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
[Guid("AF86E2E0-B12D-4c6a-9C5A-D7AA65101E90")]
internal partial interface IInspectable
{
    void GetIids(out ulong iidCount, out IntPtr iids);
    void GetRuntimeClassName(out IntPtr className);
    int GetTrustLevel();
}
