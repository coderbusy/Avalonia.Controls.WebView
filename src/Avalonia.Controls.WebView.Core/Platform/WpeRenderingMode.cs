namespace Avalonia.Platform;

/// <summary>
/// Controls the rendering backend used by the WPE WebKit adapter.
/// Note: the choice is process-global — <c>wpe_fdo_initialize_shm()</c> and
/// <c>wpe_fdo_initialize_for_egl_display()</c> are mutually exclusive.
/// </summary>
public enum WpeRenderingMode
{
    /// <summary>Default priority: SHM → EGL → DMABuf.</summary>
    Auto = 0,

    /// <summary>Force SHM (pure software rendering, no GPU required).</summary>
    Shm,

    /// <summary>Force EGL (GPU render + CPU readback via glReadPixels).</summary>
    Egl,

    /// <summary>Force DMABuf (GPU zero-copy frame export).</summary>
    DmaBuf,
}
