using System.Linq;
using AvaloniaUI.Licensing;

namespace Avalonia.Controls;

internal static class Licensing
{
    private const string WebViewProductName = "Avalonia.Controls.WebView";
    private const string RsaPublicKey = "AwAAAAEAAQABAACgpOJ+c/MPuDRg/wDeZAKs/qnZ9T2e6I+UzAgEYdzYkvtaQePZCeS5FOQyizuybIWpXfkg0/IwrpSCfjF5ZvNP4a2LikNb2B9Vv2g8JzSu2U/LU3nSbsVtCCSGh96RdSpZX3cTtY5fZ5tnNQuRE9E+bwOXp160RoLj8S6nJLwgYPpDkAfrIU+4SaWOfx1mN45Szh4O5ZCADFYOxxPGUu7pAB+M/uRPHBpji9pS8TnQgc4CTH97cjt6P3kGwzlZX8IjiOkqgwGYVOu0PxF9X/8Yy0ltz5xrw9t+Q2sGrFYenIm769p9nrYLbIlQPGQ3ciy4eoWfPmS+aaSRh+zsHbZ3";

    private static AvaloniaLicenseInformation? s_cachedLicense;

    public static void ValidateWebView()
    {
        var license = s_cachedLicense ??= AvaloniaLicenseInformation.LoadProduct("Avalonia.Controls.WebView", RsaPublicKey).FirstOrDefault()
            ?? throw new AvaloniaLicensingException($"Missing AvaloniaUILicenseKey with {WebViewProductName} product included.");
        license.ValidateLibrary(WebViewProductName);
    }
}
