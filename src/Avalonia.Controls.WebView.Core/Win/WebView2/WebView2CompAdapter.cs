using System;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia.Controls.Win.WebView2.Interop;
using Avalonia.Platform;

namespace Avalonia.Controls.Win.WebView2;

[SupportedOSPlatform("windows10.0.17763.0")]
internal partial class WebView2CompAdapter : WebView2BaseAdapter
{
    private WebView2CompAdapter(IPlatformHandle handle, WindowsWebView2EnvironmentRequestedEventArgs environmentArgs)
        : base(handle, environmentArgs)
    {
    }

    public override IntPtr Handle => default; // TODO complete webview2 compositor

    public override string HandleDescriptor => "Windows.UI.Composition.ContainerVisual";

    protected override async Task<ICoreWebView2Controller> CreateWebView2Controller(ICoreWebView2Environment env,
        IntPtr handle, WindowsWebView2EnvironmentRequestedEventArgs environmentArgs)
    {
        var handler = new WebView2CompositionControllerHandler();

        var hasCustomOptions = environmentArgs.IsInPrivateModeEnabled
                               || !string.IsNullOrEmpty(environmentArgs.ProfileName);
        if (hasCustomOptions && env is ICoreWebView2Environment10 env10)
        {
            var options = env10.CreateCoreWebView2ControllerOptions();
            options.SetIsInPrivateModeEnabled(environmentArgs.IsInPrivateModeEnabled);
            if (environmentArgs.ProfileName is not null)
                options.SetProfileName(environmentArgs.ProfileName);

            env10.CreateCoreWebView2CompositionControllerWithOptions(handle, options, handler);
        }
        else if (env is ICoreWebView2Environment3 env3)
        {
            env3.CreateCoreWebView2CompositionController(handle, handler);
        }
        else
        {
            throw new NotSupportedException();
        }

        // ICoreWebView2Controller can be queried from ICoreWebView2CompositionController. 
        // ReSharper disable once SuspiciousTypeConversion.Global
        return (ICoreWebView2Controller)await handler.Result.Task;
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
