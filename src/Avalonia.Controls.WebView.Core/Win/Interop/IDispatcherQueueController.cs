using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Windows.Win32.Foundation;

namespace Avalonia.Controls.Win.Interop;

#if COM_SOURCE_GEN
[GeneratedComInterface(Options = ComInterfaceOptions.ComObjectWrapper)]
#else
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
[Guid("22f34e66-50db-4e36-a98d-61c01b384d20")]
internal partial interface IDispatcherQueueController : IInspectable
{
    IDispatcherQueue GetDispatcherQueue();
    void ShutdownQueueAsync(/*Windows.Foundation.IAsyncAction*/ IntPtr operation);
};
