using System;
using Avalonia.Controls;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Avalonia.Platform;

public sealed class AppleWKWebViewEnvironmentRequestedEventArgs : WebViewEnvironmentRequestedEventArgs
{
    /// <summary>
    /// If you don’t assign a value to this property, the configuration object uses the default data store object to store data persistently.
    /// To create a private web-browsing session, create a nonpersistent data store using the <see cref="WebsiteDataStore.NonPersistent"/> and assign it to this property.
    /// </summary>
    public WebsiteDataStore? DataStore { get; set; }

    /// <summary>
    /// The app name that appears in the user agent string.
    /// </summary>
    public string? ApplicationNameForUserAgent { get; set; }

    /// <summary>
    /// A Boolean value that indicates whether the web view should automatically upgrade supported HTTP requests to HTTPS.
    /// </summary>
    public bool UpgradeKnownHostsToHTTPS { get; set; } = true;

    /// <summary>
    /// A Boolean value that indicates whether the web view limits navigation to pages within the app’s domain.
    /// </summary>
    public bool LimitsNavigationsToAppBoundDomains { get; set; } = false;

    public sealed record WebsiteDataStore
    {
        private WebsiteDataStore(string identifier)
        {
            Identifier = identifier;
        }
        internal string Identifier { get; }

        /// <summary>
        /// Returns the default data store, which stores data persistently to disk.
        /// </summary>
        public static WebsiteDataStore Default { get; } = new(nameof(Default));

        /// <summary>
        /// Creates a new data store object that stores website data in memory, and does not write that data to disk.
        /// </summary>
        public static WebsiteDataStore NonPersistent { get; } = new(nameof(NonPersistent));

        /// <summary>
        /// Returns the persistent data store with the unique identifier you provide
        /// </summary>
        public static WebsiteDataStore ForIdentifier(string identifier) =>
            new(identifier ?? throw new ArgumentNullException(nameof(identifier)));
    }
}
