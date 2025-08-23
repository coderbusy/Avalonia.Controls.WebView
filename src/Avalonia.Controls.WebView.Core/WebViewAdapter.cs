using System;
using System.Threading.Tasks;
using Avalonia.Platform;

namespace Avalonia.Controls;

internal static class WebViewAdapter
{
    public static bool UseHeadless { get; set; }

    public abstract record AdapterFactory;

    public record AdapterWrapper(IPlatformHandle AdapterHandle, Task<IWebViewAdapter> AdapterInitializeTask);
    public delegate AdapterWrapper NativeWebViewAdapterBuilder(IPlatformHandle parent, Func<IPlatformHandle, IPlatformHandle> createChild);

    public record NativeHostAdapterFactory(NativeWebViewAdapterBuilder InvokeAsync) : AdapterFactory;

    public delegate Task<IWebViewAdapterWithOffscreenBuffer> OffscreenWebViewAdapterBuilder(Control parent);
    public record CompositorHostAdapterFactory(OffscreenWebViewAdapterBuilder InvokeAsync) : AdapterFactory;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public static async Task<AdapterFactory?> CreateFactory(Action<WebViewEnvironmentRequestedEventArgs> environmentRequested)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        if (UseHeadless)
        {
            var args = new HeadlessWebViewEnvironmentRequestedEventArgs();
            environmentRequested(args);
            await Task.Yield();
            // Headless platform doesn't support NativeControlHost yet,
            // But compositor solution kinda works there.
            // Even though it's not production ready part of WebView (compositor/offscreen impl is not enabled by default).
            return new CompositorHostAdapterFactory(async _ => await Headless.HeadlessWebViewAdapter.CreateAsync(args));
        }

#if ANDROID // Android is the only backend which conditionally compiled, the rest is always present and loaded in runtime
            // TODO: I would like to avoid Xamarin.Android here (as an extra target), and make it in-line with the rest. 
        if (OperatingSystem.IsAndroid())
        {
            var args = new AndroidWebViewEnvironmentRequestedEventArgs();
            environmentRequested(args);
            return new NativeHostAdapterFactory((parent, _) =>
            {
                IWebViewAdapter adapter = new Android.AndroidWebViewAdapter(parent, args);
                return new AdapterWrapper(adapter, Task.FromResult(adapter));
            });
        }
#else
        if (OperatingSystemEx.IsMacOS() || OperatingSystemEx.IsIOS())
        {
            var args = new AppleWKWebViewEnvironmentRequestedEventArgs();
            environmentRequested(args);
            return new NativeHostAdapterFactory((_, _) =>
            {
                // NOTE: we add double platform condition here to shut up Roslyn Analyzer false positives.
                // If you are from the future, please check if it's still the case.
                if (OperatingSystemEx.IsMacOS() || OperatingSystemEx.IsIOS())
                {
                    IWebViewAdapter adapter = new Macios.MaciosWebViewAdapter(args);
                    return new AdapterWrapper(adapter, Task.FromResult(adapter));
                }
                throw new PlatformNotSupportedException();
            });
        }

        if (OperatingSystemEx.IsWindows())
        {
            {
                var args = new WindowsWebView2EnvironmentRequestedEventArgs();
                environmentRequested(args);
                if (!args.PreferWebView1Instead
                    && Win.WebView2.CoreWebView2Environment.TryFindWebView2Runtime(args.BrowserExecutableFolder) !=
                    IntPtr.Zero)
                {
                    var builder = await Win.WebView2.WebView2HwndAdapter.CreateBuilder(args);
                    return new NativeHostAdapterFactory(builder);
                }
            }
            {
                var args = new WindowsWebView1EnvironmentRequestedEventArgs();
                environmentRequested(args);
                if (Win.WebView1.WebView1Process.GetOrCreateProcess(args) is { } process)
                {
                    var builder = await Win.WebView1.WebView1Adapter.CreateBuilder(process);
                    return new NativeHostAdapterFactory(builder);
                }
            }
        }

        if (OperatingSystemEx.IsLinux())
        {
            var args = new GtkWebViewEnvironmentRequestedEventArgs();
            environmentRequested(args);
            if (args.ExperimentalOffscreen)
            {
                var builder = await Gtk.GtkOffscreenAvaloniaWebViewAdapter.CreateBuilder(args);
                return new CompositorHostAdapterFactory(builder);
            }
            else
            {
                var builder = await Gtk.GtkX11WebViewAdapter.CreateBuilder(args);
                return new NativeHostAdapterFactory(builder);
            }
        }

        // if (OperatingSystemEx.IsBrowser())
        // {
        //     var args = new GtkWebViewEnvironmentRequestedEventArgs();
        //     environmentRequested(args);
        //     return new NativeHostAdapterFactory((parent, _) => new Browser.BrowserIFrameAdapter(args));
        // }
#endif

        return null;
    }
}
