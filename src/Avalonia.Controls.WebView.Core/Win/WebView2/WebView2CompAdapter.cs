using System;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia.Controls.Rendering;
using Avalonia.Controls.Win.WebView2.Interop;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Avalonia.Controls.Win.WebView2;

[SupportedOSPlatform("windows10.0.17763.0")]
internal partial class WebView2CompAdapter(ICoreWebView2CompositionController controller)
    // ICoreWebView2Controller can be queried from ICoreWebView2CompositionController. 
    // ReSharper disable once SuspiciousTypeConversion.Global
    : WebView2BaseAdapter((ICoreWebView2Controller)controller), IWebViewAdapterWithOffscreenBuffer,
        IWebViewAdapterWithOffscreenInput
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
    private partial class WebView2CompositionControllerHandler :
        GenericCompletedHandler<ICoreWebView2CompositionController>,
        ICoreWebView2CreateCoreWebView2CompositionControllerCompletedHandler;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
        }

        base.Dispose(disposing);
    }

    public bool KeyInput(bool press, PhysicalKey physical, string? symbol, KeyModifiers modifiers)
    {
        // Will be implicitly handled by Windows itself.
        return true;
    }

    public bool PointerInput(PointerPoint point, KeyModifiers modifiers)
    {
        var virtualKeys = KeyModifiersToVirtualKey(modifiers, point);
        var position = ToPoint(point.Position);
        var changeType = point.Properties.PointerUpdateKind switch
        {
            PointerUpdateKind.LeftButtonPressed => COREWEBVIEW2_MOUSE_EVENT_KIND
                .COREWEBVIEW2_MOUSE_EVENT_KIND_LEFT_BUTTON_DOWN,
            PointerUpdateKind.MiddleButtonPressed => COREWEBVIEW2_MOUSE_EVENT_KIND
                .COREWEBVIEW2_MOUSE_EVENT_KIND_MIDDLE_BUTTON_DOWN,
            PointerUpdateKind.RightButtonPressed => COREWEBVIEW2_MOUSE_EVENT_KIND
                .COREWEBVIEW2_MOUSE_EVENT_KIND_RIGHT_BUTTON_DOWN,
            PointerUpdateKind.XButton1Pressed => COREWEBVIEW2_MOUSE_EVENT_KIND
                .COREWEBVIEW2_MOUSE_EVENT_KIND_X_BUTTON_DOWN,
            PointerUpdateKind.XButton2Pressed => COREWEBVIEW2_MOUSE_EVENT_KIND
                .COREWEBVIEW2_MOUSE_EVENT_KIND_X_BUTTON_DOWN,
            PointerUpdateKind.LeftButtonReleased => COREWEBVIEW2_MOUSE_EVENT_KIND
                .COREWEBVIEW2_MOUSE_EVENT_KIND_LEFT_BUTTON_UP,
            PointerUpdateKind.MiddleButtonReleased => COREWEBVIEW2_MOUSE_EVENT_KIND
                .COREWEBVIEW2_MOUSE_EVENT_KIND_MIDDLE_BUTTON_UP,
            PointerUpdateKind.RightButtonReleased => COREWEBVIEW2_MOUSE_EVENT_KIND
                .COREWEBVIEW2_MOUSE_EVENT_KIND_RIGHT_BUTTON_UP,
            PointerUpdateKind.XButton1Released => COREWEBVIEW2_MOUSE_EVENT_KIND
                .COREWEBVIEW2_MOUSE_EVENT_KIND_X_BUTTON_UP,
            PointerUpdateKind.XButton2Released => COREWEBVIEW2_MOUSE_EVENT_KIND
                .COREWEBVIEW2_MOUSE_EVENT_KIND_X_BUTTON_UP,
            PointerUpdateKind.Other => COREWEBVIEW2_MOUSE_EVENT_KIND
                .COREWEBVIEW2_MOUSE_EVENT_KIND_MOVE,
            _ => throw new ArgumentOutOfRangeException(nameof(point.Properties.PointerUpdateKind))
        };
        controller.SendMouseInput(changeType, virtualKeys, 0, position);
        return true;
    }

    public bool PointerWheelInput(Vector delta, PointerPoint point, KeyModifiers modifiers)
    {
        var virtualKeys = KeyModifiersToVirtualKey(modifiers, point);
        var position = ToPoint(point.Position);
        if (delta.Y != 0)
        {
            controller.SendMouseInput(
                COREWEBVIEW2_MOUSE_EVENT_KIND.COREWEBVIEW2_MOUSE_EVENT_KIND_WHEEL,
                virtualKeys, (uint)delta.Y, position);
        }

        if (delta.X != 0)
        {
            controller.SendMouseInput(
                COREWEBVIEW2_MOUSE_EVENT_KIND.COREWEBVIEW2_MOUSE_EVENT_KIND_HORIZONTAL_WHEEL,
                virtualKeys, (uint)delta.X, position);
        }

        return true;
    }

    private static tagPOINT ToPoint(Point point)
    {
        // TODO: Handle DPI scaling
        return new tagPOINT
        {
            x = (int)point.X,
            y = (int)point.Y
        };
    }
    
    private static COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS KeyModifiersToVirtualKey(
        KeyModifiers modifiers, PointerPoint point)
    {
        var flags = COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS.COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS_NONE;
        if (modifiers.HasFlag(KeyModifiers.Shift))
            flags |= COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS.COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS_SHIFT;
        if (modifiers.HasFlag(KeyModifiers.Control))
            flags |= COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS.COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS_CONTROL;
        if (point.Properties.IsLeftButtonPressed)
            flags |= COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS.COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS_LEFT_BUTTON;
        if (point.Properties.IsRightButtonPressed)
            flags |= COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS.COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS_RIGHT_BUTTON;
        if (point.Properties.IsMiddleButtonPressed)
            flags |= COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS.COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS_MIDDLE_BUTTON;
        if (point.Properties.IsXButton1Pressed)
            flags |= COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS.COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS_X_BUTTON1;
        if (point.Properties.IsXButton2Pressed)
            flags |= COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS.COREWEBVIEW2_MOUSE_EVENT_VIRTUAL_KEYS_X_BUTTON2;
        return flags;
    }
}
