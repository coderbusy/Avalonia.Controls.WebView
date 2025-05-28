// ReSharper disable once CheckNamespace
// ReSharper disable once EmptyNamespace
// Empty namespace for compatibility with pre-NET8

using System.Runtime.Versioning;

namespace System.Runtime.InteropServices.Marshalling
{
#if !COM_SOURCE_GEN
    [SupportedOSPlatform("windows")]
    public static unsafe class ComInterfaceMarshaller<TInterface>
    {
        public static void* ConvertToUnmanaged<TManaged>(TManaged obj)
            where TManaged : notnull
        {
            return Marshal.GetComInterfaceForObject<TManaged, TInterface>(obj).ToPointer();
        }

        public static TInterface ConvertToManaged(void* obj)
        {
            return (TInterface)Marshal.GetObjectForIUnknown(new IntPtr(obj));
        }

        public static void Free(void* native)
        {
            Marshal.Release(new IntPtr(native));
        }
    }
#endif
}

