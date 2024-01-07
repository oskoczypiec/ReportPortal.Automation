// <copyright file="ApplicationConfiguration.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Config
{
    using Microsoft.Extensions.Configuration;
    using ReportPortal.Core.Enums;
    using ReportPortal.Core.Interfaces;
    using ReportPortal.Core.Utilities;

    /// <summary>
    /// Configuration manager for retrieving and managing application settings.
    /// </summary>
    public static class ApplicationConfiguration
    {
        public static IConfigurationRoot? configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfiguration"/> class, loading application settings from 'appsettings.json'.
        /// </summary>
        public static IConfigurationRoot SetUp()
        {
            configuration = new ConfigurationBuilder()
                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json")
                 .AddEnvironmentVariables()
                 .Build();
            AssignValues();

            return configuration;
        }

        /// <summary>
        /// Retrieves the configured web browser from application settings.
        /// </summary>
        /// <returns>The selected web browser as an enum value.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Browser' setting is missing or unsupported.</exception>
        public static BrowserEnums GetBrowser()
        {
            var browser = configuration?["Browser"];

            if (browser == null)
            {
                throw new ArgumentException("Key 'Browser' cannot be null");
            }
            else
            {
                return Enum.Parse<BrowserEnums>(browser);
            }
        }

        public static string GetBaseUrl()
        {
            var url = configuration?["URL"];

            if (url == null)
            {
                throw new ArgumentException("Key 'URL' cannot be null");
            }
            else
            {
                return url;
            }
        }

        public static void AssignValues()
        {
            Settings.URL = configuration?["URL"];
            Settings.User = configuration?["User"];
            Settings.Pass = configuration?["Pass"];
            Settings.Browser = configuration?["Browser"];
            Settings.BearerKey = configuration?["BearerKey"];
        }
    }
}
