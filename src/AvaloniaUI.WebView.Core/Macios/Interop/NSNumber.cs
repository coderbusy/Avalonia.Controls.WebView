using System;

namespace AppleInterop;

internal sealed class NSNumber : NSObject
{
    private static readonly IntPtr s_class = Libobjc.objc_getClass("NSNumber");
    private static readonly IntPtr s_numberWithBool = Libobjc.sel_getUid("numberWithBool:");

    public static NSNumber Yes { get; } = new(true);
    public static NSNumber No { get; } = new(false);

    public NSNumber(bool value) : base(Libobjc.intptr_objc_msgSend(s_class, s_numberWithBool, value ? 1 : 0), true)
    {
    }
}
