using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AvaloniaUI.Xpf.WpfAbstractions;
using AvaloniaWebView.Wpf;

namespace AvaloniaWebView.Sample.Wpf
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
