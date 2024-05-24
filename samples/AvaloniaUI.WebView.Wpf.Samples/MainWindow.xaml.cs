using System.Windows;
using AvaloniaUI.WebView;

namespace AvaloniaUI.WebView.Wpf.Samples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void NativeWebView_OnNavigationCompleted(object? sender, WebViewNavigationCompletedEventArgs e)
        {
            LogList.Text += "\r\nNativeWebView_OnNavigationCompleted " + e.Request;
            LogList.Text += "\r\nInvoking JS script with invokeCSharpAction";

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
}
