using System;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia.Controls.Win.WebView2.Interop;
using Avalonia.Platform;

namespace Avalonia.Controls.Win.WebView2;

[SupportedOSPlatform("windows6.1")] // win7
internal partial class WebView2HwndAdapter(IPlatformHandle handle, WindowsWebView2EnvironmentRequestedEventArgs environmentArgs)
    : WebView2BaseAdapter(handle, environmentArgs)
{
    public override IntPtr Handle { get; } = handle.Handle;
    public override string HandleDescriptor { get; } = handle.HandleDescriptor!; // Expected to be HWND always.

    protected override Task<ICoreWebView2Controller> CreateWebView2Controller(ICoreWebView2Environment env,
        IntPtr handle, WindowsWebView2EnvironmentRequestedEventArgs environmentArgs)
    {
        var handler = new WebView2ControllerHandler();

        var hasCustomOptions = environmentArgs.IsInPrivateModeEnabled
                               || !string.IsNullOrEmpty(environmentArgs.ProfileName);
        if (hasCustomOptions && env is ICoreWebView2Environment10 env10)
        {
            var options = env10.CreateCoreWebView2ControllerOptions();
            options.SetIsInPrivateModeEnabled(environmentArgs.IsInPrivateModeEnabled);
            if (environmentArgs.ProfileName is not null)
                options.SetProfileName(environmentArgs.ProfileName);

            env10.CreateCoreWebView2ControllerWithOptions(handle, options, handler);
        }
        else
        {
            env.CreateCoreWebView2Controller(handle, handler);
        }

        return handler.Result.Task;
    }

#if COM_SOURCE_GEN
    [GeneratedComClass]
#endif
    private partial class WebView2ControllerHandler : GenericCompletedHandler<ICoreWebView2Controller>,
        ICoreWebView2CreateCoreWebView2ControllerCompletedHandler;
}
