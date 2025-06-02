using System;
using System.Threading.Tasks;
using Core = Avalonia.Controls;
using Avalonia.Platform;
#if WPF
using System.Windows;
using AvaloniaUI.Xpf.WpfAbstractions;
using WindowStartupLocation = System.Windows.WindowStartupLocation;
using Window = System.Windows.Window;
#elif AVALONIA
using Avalonia.Controls;
using Window = Avalonia.Controls.Window;
using WindowStartupLocation = Avalonia.Controls.WindowStartupLocation;
#endif

#if AVALONIA
namespace Avalonia.Controls
#elif WPF
namespace Avalonia.Xpf.Controls
#endif
{
    internal class WindowNativeWebViewDialog : Window, Core.INativeWebViewDialog
    {
        private readonly NativeWebView _nativeWebView = new();
        private EventHandler? _closing;

        public WindowNativeWebViewDialog()
        {
            Content = _nativeWebView;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Closing += (_, args) =>
            {
                _closing?.Invoke(this, args);
            };
        }

        public Core.IWebView WebView => _nativeWebView;

#if WPF
        public bool CanUserResize { get => ResizeMode != ResizeMode.NoResize; set => ResizeMode = value ? ResizeMode.CanResize : ResizeMode.NoResize; }
#elif AVALONIA
        public bool CanUserResize { get => CanResize; set => CanResize = value; }
#endif

        public void Dispose() {}

        event EventHandler? Core.INativeWebViewDialog.Closing
        {
            add => _closing += value;
            remove => _closing -= value;
        }

        bool Core.INativeWebViewDialog.Show(IPlatformHandle _) => false;

        public bool Resize(int width, int height)
        {
            Width = width;
            Height = height;
            return true;
        }

        public bool Move(int x, int y)
        {
#if WPF
            Left = x;
            Top = y;
#elif AVALONIA
            Position = new PixelPoint(x, y);
#endif
            return true;
        }

#if WPF
        public IPlatformHandle? TryGetPlatformHandle() => XpfWpfAbstraction.GetAvaloniaTopLevelForWindow(this)?.TryGetPlatformHandle();
#endif
    }
}
