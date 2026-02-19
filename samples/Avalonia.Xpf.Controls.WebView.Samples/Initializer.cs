using System.Runtime.CompilerServices;
using AvaloniaUI.Xpf;

namespace Avalonia.Xpf.Controls.WebView.Samples;

public class XpfAvaloniaInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        if (Avalonia.Application.Current == null)
        {
            AppBuilder.Configure<AvaloniaUI.Xpf.Helpers.DefaultXpfAvaloniaApplication>()
                .UsePlatformDetect()
                // ATLANTIS TODO: move back to WithAvaloniaXpf once we have shared platforms project
                .With(new Win32PlatformOptions()
                {
                    // Default to System Dpi Aware. If process has a different awareness set in manifest, that value will be prioritized by the os
                    DpiAwareness = Win32DpiAwareness.SystemDpiAware
                })
                .With(new AvaloniaNativePlatformOptions
                {
                    RenderingMode = new[]
                    {
                        AvaloniaNativeRenderingMode.OpenGl, 
                        AvaloniaNativeRenderingMode.Metal,
                        AvaloniaNativeRenderingMode.Software
                    }
                })
                .With(new X11PlatformOptions
                {
                    // UseGLibMainLoop = false,
                    // ExterinalGLibMainLoopExceptionLogger = e =>
                    // {
                    //     Debug.WriteLine(e.ToString());
                    //     Debugger.Break();
                    // }
                })
                .WithAvaloniaXpf()
                .SetupWithClassicDesktopLifetime(
                    System.Environment.GetCommandLineArgs(),
                    lifetime => lifetime.ShutdownMode = Avalonia.Controls.ShutdownMode.OnExplicitShutdown);
        }
    }
}
