using System;
using System.Runtime.InteropServices;

namespace AppleInterop;

internal unsafe class NSManagedObjectBase<TSelf> : NSObject
    where TSelf : NSManagedObjectBase<TSelf>
{
    private GCHandle _managedHandle;

    public NSManagedObjectBase(IntPtr handle, bool owns) : base(handle, owns)
    {
        WriteManagedSelf();
    }

    public NSManagedObjectBase(IntPtr classHandle) : base(classHandle)
    {
        WriteManagedSelf();
    }

    protected static bool RegisterManagedSelfIVar(IntPtr delegateClass)
    {
        var result = Libobjc.class_addIvar(delegateClass, "_managedSelf", new IntPtr(sizeof(IntPtr)), 0, "@");
        return result == 1;
    }

    private void WriteManagedSelf()
    {
        _managedHandle = GCHandle.Alloc(this);
        _ = SetIvarValue("_managedSelf", GCHandle.ToIntPtr(_managedHandle));
    }

    protected static TSelf? ReadManagedSelf(IntPtr ptr)
    {
        var managedHandle = GetIvarValue(ptr, "_managedSelf");
        return managedHandle == default ?
            null :
            GCHandle.FromIntPtr(managedHandle).Target as TSelf;
    }
}
