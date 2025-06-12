using System;

namespace Avalonia.Controls.WebView.Tests;

public abstract class HeadlessTestsBase : IDisposable
{
    public HeadlessTestsBase()
    {
        WebViewAdapter.UseHeadless = true;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            WebViewAdapter.UseHeadless = false;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
