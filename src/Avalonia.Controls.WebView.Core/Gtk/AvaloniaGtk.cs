using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Logging;
using Avalonia.Threading;

namespace Avalonia.Controls.Gtk;

internal static class AvaloniaGtk
{
    static AvaloniaGtk()
    {
#if NET6_0_OR_GREATER
        var map = new Dictionary<string, string[]>
        {
            [GtkInterop.LibGtk] = ["libgtk-3.so.0", "libgtk-3.so"],
            [GtkInterop.LibGdk] = ["libgdk-3.so.0", "libgdk-3.so"],
            [GtkInterop.LibGLib] = ["libglib-2.0.so.0", "libglib-2.0.so"],
            [GtkInterop.LibGObject] = ["libgobject-2.0.so.0", "libgobject-2.0.so"],
            [GtkInterop.LibGio] = ["libgio-2.0.so.0", "libgio-2.0.so"],
            [GtkInterop.LibWebKit] =
            [
                "libwebkit2gtk-4.1.so.0",
                "libwebkit2gtk-4.1.so",
                "libwebkit2gtk-4.0.so.37",
                "libwebkit2gtk-4.0.so"
            ],
            [GtkInterop.LibSoup] =
            [
                "libsoup-3.0.so.0",
                "libsoup-3.0.so",
                "libsoup-2.4.so.1",
                "libsoup-2.4.so"
            ]
        };

        NativeLibrary.SetDllImportResolver(typeof(AvaloniaGtk).Assembly, (name, assembly, searchPath) =>
        {
            if (map.TryGetValue(name, out var candidates))
            {
                foreach (var mapped in candidates)
                {
                    if (NativeLibrary.TryLoad(mapped, assembly, searchPath, out var ptr))
                        return ptr;
                }

                Logger.TryGet(LogEventLevel.Error, "WebView")?.Log(null,
                    "Unable to resolve GTK assembly {Name}. Expected options are: {Candidates}", name,
                    string.Join(',', candidates));
            }

            // Default
            return IntPtr.Zero;
        });
#endif
    }

    public static Task<T> RunOnGlibThreadAsync<T>(Func<T> callback) => CachedDelegate<T>.Run(callback);

    public static Task RunOnGlibThreadAsync(Action callback) => CachedDelegate<object?>.Run(() =>
    {
        callback();
        return (object?)null;
    });

    public static T RunOnGlibThread<T>(Func<T> callback)
    {
        var task = CachedDelegate<T>.Run(callback);
        if (!task.IsCompleted)
        {
            var frame = new DispatcherFrame();
            _ = task.ContinueWith(static (_, s) => ((DispatcherFrame)s!).Continue = false, frame);
            Dispatcher.UIThread.PushFrame(frame);
        }

        return task.GetAwaiter().GetResult();
    }

    public static void RunOnGlibThread(Action callback)
    {
        _ = RunOnGlibThread(() =>
        {
            callback();
            return (object?)null;
        });
    }

    private static class CachedDelegate<T>
    {
        // https://github.com/AvaloniaUI/Avalonia/blob/11.1.0/src/Avalonia.X11/Interop/GtkInteropHelper.cs#L9
        [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "Should be fine for generic ref types")]
        private static readonly Func<Func<T>, Task<T>>? s_runOnGlibThread = Type
            .GetType("Avalonia.X11.Interop.GtkInteropHelper, Avalonia.X11")?
            .GetMethod("RunOnGlibThread", BindingFlags.Public | BindingFlags.Static)?
            .MakeGenericMethod(typeof(T)) is not { } method
            ? null : (Func<Func<T>, Task<T>>?)Delegate.CreateDelegate(typeof(Func<Func<T>, Task<T>>), method);

        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, "Avalonia.X11.Interop.GtkInteropHelper",
            "Avalonia.X11")]
        public static Task<T> Run(Func<T> callback) => s_runOnGlibThread?.Invoke(callback)
                                                       ?? throw new InvalidOperationException("Avalonia.X11 is not referenced");
    }
}
