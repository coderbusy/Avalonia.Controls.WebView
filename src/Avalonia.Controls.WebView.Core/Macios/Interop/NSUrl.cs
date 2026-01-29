using System;

namespace Avalonia.Controls.Macios.Interop;

internal class NSUrl : NSObject
{
    private static readonly IntPtr s_class = Foundation.objc_getClass("NSURL");
    private static readonly IntPtr s_createWithUrl = Libobjc.sel_getUid("URLWithString:");
    private static readonly IntPtr s_fileURLWithPath = Libobjc.sel_getUid("fileURLWithPath:");
    private static readonly IntPtr s_absoluteString = Libobjc.sel_getUid("absoluteString");

    public NSUrl(IntPtr handle, bool owns) : base(handle, owns)
    {
    }

    public NSUrl(NSString nsString) : this(Libobjc.intptr_objc_msgSend(s_class, s_createWithUrl, nsString.Handle), true)
    {
    }

    public static NSUrl FileURLWithPath(string path)
    {
        using var nsPath = NSString.Create(path);
        var handle = Libobjc.intptr_objc_msgSend(s_class, s_fileURLWithPath, nsPath.Handle);
        return new NSUrl(handle, true);
    }

    public string? AbsoluteString
    {
        get
        {
            var nsString = Libobjc.intptr_objc_msgSend(Handle, s_absoluteString);
            return NSString.GetString(nsString);
        }
    }
}
