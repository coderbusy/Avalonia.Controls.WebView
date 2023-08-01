using System;
using Avalonia;
using Avalonia.Controls;
using AvaloniaWebView.TinyMCE;

namespace AvaloniaWebView.Sample;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void NativeWebView_OnNavigationCompleted(object? sender, WebViewNavigationCompletedEventArgs e)
    {
        LogList.Text += "\r\nNativeWebView_OnNavigationCompleted " + e.Request;

        await ((NativeWebView)sender!).InvokeScript(""" invokeCSharpAction("{'key': 10}") """);
    }

    private void NativeWebView_OnNavigationStarted(object? sender, WebViewNavigationStartingEventArgs e)
    {
        LogList.Text += "\r\nNativeWebView_OnNavigationStarted " + e.Request;
    }

    private void NativeWebView_OnWebMessageReceived(object? sender, WebMessageReceivedEventArgs e)
    {
        LogList.Text += "\r\nNativeWebView_OnWebMessageReceived " + e.Body;
    }
}
