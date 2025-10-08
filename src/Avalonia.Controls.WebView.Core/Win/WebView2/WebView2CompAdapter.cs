using System;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia.Controls.Rendering;
using Avalonia.Controls.Win.WebView2.Interop;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Avalonia.Controls.Win.WebView2;

[SupportedOSPlatform("windows10.0.17763.0")]
internal partial class WebView2CompAdapter(ICoreWebView2CompositionController controller)
    // ICoreWebView2Controller can be queried from ICoreWebView2CompositionController. 
    // ReSharper disable once SuspiciousTypeConversion.Global
    : WebView2BaseAdapter((ICoreWebView2Controller)controller), IWebViewAdapterWithOffscreenBuffer
{
    
    public override IntPtr Handle => default; // TODO complete webview2 compositor
    public override string HandleDescriptor => "Windows.UI.Composition.ContainerVisual";

    public static async Task<WebViewAdapter.OffscreenWebViewAdapterBuilder> CreateBuilder(
        WindowsWebView2EnvironmentRequestedEventArgs environmentArgs)
    {
        var env = await CoreWebView2Environment.CreateAsync(environmentArgs);

        return async (parent) =>
        {
            var parentHandle = TopLevel.GetTopLevel(parent)?.TryGetPlatformHandle()?.Handle
                               ?? throw new InvalidOperationException("Parent must be a TopLevel control.");
            var handler = new WebView2CompositionControllerHandler();

            var hasCustomOptions = environmentArgs.IsInPrivateModeEnabled
                                   || !string.IsNullOrEmpty(environmentArgs.ProfileName);
            if (hasCustomOptions && env is ICoreWebView2Environment10 env10)
            {
                var options = env10.CreateCoreWebView2ControllerOptions();
                options.SetIsInPrivateModeEnabled(environmentArgs.IsInPrivateModeEnabled);
                if (environmentArgs.ProfileName is not null)
                    options.SetProfileName(environmentArgs.ProfileName);

                env10.CreateCoreWebView2CompositionControllerWithOptions(parentHandle, options, handler);
            }
            else if (env is ICoreWebView2Environment3 env3)
            {
                env3.CreateCoreWebView2CompositionController(parentHandle, handler);
            }
            else
            {
                throw new NotSupportedException();
            }

            var controller = await handler.Result.Task;
            var webView = new WebView2CompAdapter(controller);
            await webView.InitializeAsync(environmentArgs);
            return webView;
        };
    }

    public event Action? DrawRequested;
    public Task UpdateWriteableBitmap(FrameChainBase<WriteableBitmap, PixelSize>.IProducer producer)
    {
        throw new NotImplementedException();
    }

#if COM_SOURCE_GEN
    [GeneratedComClass]
#endif
    private partial class WebView2CompositionControllerHandler : GenericCompletedHandler<ICoreWebView2CompositionController>,
        ICoreWebView2CreateCoreWebView2CompositionControllerCompletedHandler;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
        base.Dispose(disposing);
    }
}
