using System;

namespace AppleInterop;

internal class NSURLRequest : NSObject
{
    private static readonly IntPtr s_class = Libobjc.objc_getClass("NSURLRequest");
    private static readonly IntPtr s_initWithUrl = Libobjc.sel_getUid("initWithURL:");
    private static readonly IntPtr s_url = Libobjc.sel_getUid("URL");

    public NSURLRequest(IntPtr handle, bool owns) : base(handle, owns)
    {
    }

    public NSURLRequest(NSUrl nsUrl) : base(s_class)
    {
        _ = Libobjc.intptr_objc_msgSend(Handle, s_initWithUrl, nsUrl.Handle);
    }

    public NSUrl Url => new(Libobjc.intptr_objc_msgSend(Handle, s_url), false);
}
