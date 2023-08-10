using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AvaloniaWebView.Interop;
using MicroCom.Runtime;

namespace AvaloniaWebView.Win;

internal unsafe class WebBrowserAdapter : IWebViewAdapter
{
    private readonly IWebBrowser2 _webBrowser;

    public WebBrowserAdapter()
    {
        var guid = Guid.Parse("8856f961-340a-11d0-a96b-00c04fd705a2");
        var iunknown = Guid.Parse("00000000-0000-0000-C000-000000000046");
        IntPtr result;
        var res = WinApiHelpers.CoCreateInstance(guid, default, 0x1, iunknown, &result);
        if (res != 0)
        {
            throw new Win32Exception(res);
        }

        var browser = MicroComRuntime.CreateProxyFor<IWebBrowser>(result, false);
        _webBrowser = browser.QueryInterface<IWebBrowser2>();
        Handle = result;
    }
    
    public IntPtr Handle { get; }
    public string? HandleDescriptor => "HWDN";
    public event EventHandler<WebViewNavigationCompletedEventArgs>? NavigationCompleted;
    public event EventHandler<WebViewNavigationStartingEventArgs>? NavigationStarted;
    public event EventHandler<WebMessageReceivedEventArgs>? WebMessageReceived;
    public bool CanGoBack { get; }
    public bool CanGoForward { get; }
    public Uri? Source { get; set; }

    public bool GoBack()
    {
        _webBrowser.GoBack();
        return true;
    }

    public bool GoForward()
    {
        _webBrowser.GoForward();
        return true;
    }

    public Task<string?> InvokeScript(string script)
    {
        return Task.FromResult<string>(null);
    }

    public void Navigate(Uri url)
    {
        var bstr = Marshal.StringToBSTR(url.AbsoluteUri);
        int[] arr = new[] { 0 };
        fixed (void* p = arr)
            _webBrowser.Navigate(bstr, p, null, null, null);
        Marshal.FreeBSTR(bstr);
    }

    public void NavigateToString(string text)
    {
        throw new NotImplementedException();
    }

    public bool Refresh()
    {
        throw new NotImplementedException();
    }

    public bool Stop()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        
    }

    public event EventHandler? Initialized;
    public bool IsInitialized => true;
    public void SizeChanged()
    {
    }
}
