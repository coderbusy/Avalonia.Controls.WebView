using System;
using Avalonia.Controls;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Avalonia.Platform;

public sealed class AppleWKWebViewEnvironmentRequestedEventArgs : WebViewEnvironmentRequestedEventArgs
{
    public WebsiteDataStore? DataStore { get; set; }
    public string? ApplicationNameForUserAgent { get; set; }
    public bool UpgradeKnownHostsToHTTPS { get; set; } = true;
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
        /// Creates a new data store object that stores website data in memory, and doesn’t write that data to disk.
        /// </summary>
        public static WebsiteDataStore NonPersistent { get; } = new(nameof(NonPersistent));

        /// <summary>
        /// Returns the persistent data store with the unique identifier you provide
        /// </summary>
        public static WebsiteDataStore ForIdentifier(string identifier) =>
            new(identifier ?? throw new ArgumentNullException(nameof(identifier)));
    }
}
