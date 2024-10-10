using System;

namespace AppleInterop;

internal class NSError(IntPtr handle) : NSObject(handle, false)
{
    private static readonly IntPtr s_localizedDescription = Libobjc.sel_getUid("localizedDescription");
    private static readonly IntPtr s_domain = Libobjc.sel_getUid("domain");
    private static readonly IntPtr s_code = Libobjc.sel_getUid("code");

    public static NSErrorException ToException(IntPtr nsError)
    {
        if (nsError == default)
            throw new ArgumentNullException(nameof(nsError));

        return new NSErrorException(
            NSString.GetString(Libobjc.intptr_objc_msgSend(nsError, s_localizedDescription))!)
        {
            Domain = NSString.GetString(Libobjc.intptr_objc_msgSend(nsError, s_domain)),
            Code = Libobjc.int_objc_msgSend(nsError, s_code)
        };
    }
}

internal class NSErrorException(string message) : Exception(message)
{
    public string? Domain { get; init; }
    public int Code { get; init; }
}
