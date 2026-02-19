using Android.App;
using Android.Content.PM;
using Avalonia.Android;
using Avalonia.Controls.Android;
using static Android.Content.Intent;

namespace Avalonia.Controls.WebView.Samples.Android;

[Activity(Label = "ControlCatalog.Android", Theme = "@style/MyTheme.NoActionBar", Icon = "@drawable/icon",
    MainLauncher = true, Exported = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
// CategoryLeanbackLauncher is required for Android TV.
[IntentFilter([ActionView], Categories = [CategoryDefault, CategoryLeanbackLauncher])]
public class MainActivity : AvaloniaMainActivity;


[Activity(Label = "Avalonia.Controls.WebView.Samples", Theme = "@style/MyTheme.NoActionBar", Icon = "@drawable/icon",
    Exported = true)]
[IntentFilter(actions: ["android.intent.action.VIEW"],
    Categories = ["android.intent.category.DEFAULT", "android.intent.category.BROWSABLE"],
    DataScheme = "com.avaloniaui.webview.samples", DataHost = "oauth2redirect")]
public class RedirectActivity : RedirectUriReceiverActivity;
