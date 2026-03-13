using System;
using Avalonia.Controls.Linux.Interop;
using Avalonia.Threading;

namespace Avalonia.Controls.Linux;

/// <summary>
/// Pumps the GLib main context on the Avalonia UI thread via a DispatcherTimer.
/// Reference-counted — multiple WPE views share one pump instance.
/// </summary>
internal static class WpeGLibIntegration
{
    private static int s_refCount;
    private static DispatcherTimer? s_timer;
    private static IntPtr s_mainContext;

    public static void Start()
    {
        Dispatcher.UIThread.VerifyAccess();
        s_refCount++;
        if (s_refCount > 1)
            return;

        s_mainContext = WpeInterop.g_main_context_default();
        s_timer = new DispatcherTimer(DispatcherPriority.Render)
        {
            Interval = TimeSpan.FromMilliseconds(4)
        };
        s_timer.Tick += OnTick;
        s_timer.Start();
    }

    public static void Stop()
    {
        Dispatcher.UIThread.VerifyAccess();
        s_refCount--;
        if (s_refCount > 0)
            return;

        if (s_timer != null)
        {
            s_timer.Stop();
            s_timer.Tick -= OnTick;
            s_timer = null;
        }
    }

    private static void OnTick(object? sender, EventArgs e)
    {
        // Drain all pending GLib events without blocking
        while (WpeInterop.g_main_context_pending(s_mainContext))
        {
            WpeInterop.g_main_context_iteration(s_mainContext, false);
        }
    }
}
