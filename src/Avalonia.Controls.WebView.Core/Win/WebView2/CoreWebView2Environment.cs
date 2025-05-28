using System;
using System.ComponentModel;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia.Controls.Utils;
using Avalonia.Controls.Win.WebView2.Interop;
using Avalonia.Logging;
// ReSharper disable InconsistentNaming

namespace Avalonia.Controls.Win.WebView2;

[SupportedOSPlatform("windows")]
internal static partial class CoreWebView2Environment
{
    private enum WebView2RunTimeType { kInstalled = 0x0, kRedistributable = 0x1 }

    public static bool IsAvailable => s_createEnv.Value != IntPtr.Zero;

    public static async Task<ICoreWebView2Environment> CreateAsync()
    {
        var createEnvPtr = s_createEnv.Value;
        if (createEnvPtr == IntPtr.Zero)
            throw new InvalidOperationException("WebView2 runtime not found or CreateWebViewEnvironmentWithOptionsInternal not exported.");

        var envCallback = new WebView2EnvHandler();
        var options = new Options();
        var res = CreateEnv(createEnvPtr, WebView2RunTimeType.kInstalled, null, options, envCallback);
        if (res != 0)
            throw new Win32Exception(res);
        return await envCallback.Result.Task;
    }

    private static unsafe int CreateEnv(IntPtr createEnvProc, WebView2RunTimeType runTimeType, string? userDataFolder, Options options, WebView2EnvHandler envCallback)
    {
        var callbackPtr = ComInterfaceMarshaller<ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler>.ConvertToUnmanaged(envCallback);
        var optionsPtr = ComInterfaceMarshaller<ICoreWebView2EnvironmentOptions>.ConvertToUnmanaged(options);
        try
        {
            // TODO, we might want to keep userDataFolder pinned until callback is called.
            // But it's null anyway atm, so ignoring.
            var createEnvFunc = (delegate* unmanaged[Stdcall]<int, WebView2RunTimeType, IntPtr, void*, void*, int>)createEnvProc;
            fixed (char* userDataFolderPtr = userDataFolder)
                return createEnvFunc(1, runTimeType, new IntPtr(userDataFolderPtr), optionsPtr, callbackPtr);
        }
        finally
        {
            ComInterfaceMarshaller<ICoreWebView2EnvironmentOptions>.Free(optionsPtr);
            ComInterfaceMarshaller<ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler>.Free(callbackPtr);
        }
    }

    private static readonly Lazy<IntPtr> s_createEnv = new(() =>
    {
        var webViewRuntime = ManagedWebView2Loader.FindWebView2Runtime();
        if (webViewRuntime is null)
        {
            Logger.TryGet(LogEventLevel.Warning, "WebView")
                ?.Log(null, "WebView2 runtime not found. WebView2 will not be initialized.");
            return IntPtr.Zero;
        }

        var lib = NativeLibraryEx.Load(webViewRuntime);
        if (!NativeLibraryEx.TryGetExport(lib, "CreateWebViewEnvironmentWithOptionsInternal", out var createEnvPtr))
        {
            Logger.TryGet(LogEventLevel.Warning, "WebView")
                ?.Log(null , "CreateWebViewEnvironmentWithOptionsInternal not found in WebView2 runtime.");
            return IntPtr.Zero;
        }

        return createEnvPtr;
    });

#if COM_SOURCE_GEN
    [GeneratedComClass]
#endif
    private partial class Options : CallbackBase, ICoreWebView2EnvironmentOptions
    {
        public string? GetAdditionalBrowserArguments() => null;

        public void SetAdditionalBrowserArguments(string additionalBrowserArguments) {}

        public string? GetLanguage() => null;

        public void SetLanguage(string language) {}

        public string GetTargetCompatibleBrowserVersion() => "135.0.3179.45";

        public void SetTargetCompatibleBrowserVersion(string targetCompatibleBrowserVersion) { }

        public int GetAllowSingleSignOnUsingOSPrimaryAccount() => 0;

        public void SetAllowSingleSignOnUsingOSPrimaryAccount(int allowSingleSignOnUsingOSPrimaryAccount) {}
    }

#if COM_SOURCE_GEN
    [GeneratedComClass]
#endif
    private partial class WebView2EnvHandler : GenericCompletedHandler<ICoreWebView2Environment>,
        ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler;
}
