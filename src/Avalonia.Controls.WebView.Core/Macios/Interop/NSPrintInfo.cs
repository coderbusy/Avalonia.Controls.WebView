using System;

namespace Avalonia.Controls.Macios.Interop;

internal class NSPrintInfo : NSObject
{
    private static readonly IntPtr s_class = Foundation.objc_getClass("NSPrintInfo");

    // Job disposition
    private static readonly IntPtr s_setJobDisposition = Libobjc.sel_getUid("setJobDisposition:");
    private static readonly IntPtr s_jobSavingURL = Libobjc.sel_getUid("jobSavingURL");
    private static readonly IntPtr s_setJobSavingURL = Libobjc.sel_getUid("setJobSavingURL:");

    // Orientation
    private static readonly IntPtr s_orientation = Libobjc.sel_getUid("orientation");
    private static readonly IntPtr s_setOrientation = Libobjc.sel_getUid("setOrientation:");

    // Margins
    private static readonly IntPtr s_topMargin = Libobjc.sel_getUid("topMargin");
    private static readonly IntPtr s_setTopMargin = Libobjc.sel_getUid("setTopMargin:");
    private static readonly IntPtr s_bottomMargin = Libobjc.sel_getUid("bottomMargin");
    private static readonly IntPtr s_setBottomMargin = Libobjc.sel_getUid("setBottomMargin:");
    private static readonly IntPtr s_leftMargin = Libobjc.sel_getUid("leftMargin");
    private static readonly IntPtr s_setLeftMargin = Libobjc.sel_getUid("setLeftMargin:");
    private static readonly IntPtr s_rightMargin = Libobjc.sel_getUid("rightMargin");
    private static readonly IntPtr s_setRightMargin = Libobjc.sel_getUid("setRightMargin:");

    // Scaling
    private static readonly IntPtr s_scalingFactor = Libobjc.sel_getUid("scalingFactor");
    private static readonly IntPtr s_setScalingFactor = Libobjc.sel_getUid("setScalingFactor:");

    // NSPrintSaveJob constant
    private static readonly NSString s_printSaveJob = NSString.Create("NSPrintSaveJob");

    public NSPrintInfo() : base(s_class)
    {
        Init();
    }

    /// <summary>
    /// Sets the job disposition to save to a file (PDF).
    /// </summary>
    public void SetJobDispositionSaveJob()
    {
        Libobjc.void_objc_msgSend(Handle, s_setJobDisposition, s_printSaveJob.Handle);
    }

    /// <summary>
    /// Gets the URL where the job will be saved.
    /// </summary>
    public NSUrl? JobSavingURL
    {
        get
        {
            var ptr = Libobjc.intptr_objc_msgSend(Handle, s_jobSavingURL);
            return ptr != IntPtr.Zero ? new NSUrl(ptr, false) : null;
        }
        set => Libobjc.void_objc_msgSend(Handle, s_setJobSavingURL, value?.Handle ?? IntPtr.Zero);
    }

    /// <summary>
    /// Paper orientation: 0 = Portrait, 1 = Landscape
    /// </summary>
    public int Orientation
    {
        get => (int)Libobjc.long_objc_msgSend(Handle, s_orientation);
        set => Libobjc.void_objc_msgSend(Handle, s_setOrientation, (long)value);
    }

    /// <summary>
    /// Top margin in points.
    /// </summary>
    public double TopMargin
    {
        get => Libobjc.double_objc_msgSend(Handle, s_topMargin);
        set => Libobjc.void_objc_msgSend(Handle, s_setTopMargin, value);
    }

    /// <summary>
    /// Bottom margin in points.
    /// </summary>
    public double BottomMargin
    {
        get => Libobjc.double_objc_msgSend(Handle, s_bottomMargin);
        set => Libobjc.void_objc_msgSend(Handle, s_setBottomMargin, value);
    }

    /// <summary>
    /// Left margin in points.
    /// </summary>
    public double LeftMargin
    {
        get => Libobjc.double_objc_msgSend(Handle, s_leftMargin);
        set => Libobjc.void_objc_msgSend(Handle, s_setLeftMargin, value);
    }

    /// <summary>
    /// Right margin in points.
    /// </summary>
    public double RightMargin
    {
        get => Libobjc.double_objc_msgSend(Handle, s_rightMargin);
        set => Libobjc.void_objc_msgSend(Handle, s_setRightMargin, value);
    }

    /// <summary>
    /// Scaling factor (1.0 = 100%).
    /// </summary>
    public double ScalingFactor
    {
        get => Libobjc.double_objc_msgSend(Handle, s_scalingFactor);
        set => Libobjc.void_objc_msgSend(Handle, s_setScalingFactor, value);
    }
}
