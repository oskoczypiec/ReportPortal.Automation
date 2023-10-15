﻿// <copyright file="ApplicationConfiguration.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Config
{
    using Microsoft.Extensions.Configuration;
    using ReportPortal.Core.Enums;

    /// <summary>
    /// Configuration manager for retrieving and managing application settings.
    /// </summary>
    public static class ApplicationConfiguration
    {
        private static IConfiguration? config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfiguration"/> class, loading application settings from 'appsettings.json'.
        /// </summary>
        public static void SetUp()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        /// <summary>
        /// Retrieves the configured web browser from application settings.
        /// </summary>
        /// <returns>The selected web browser as an enum value.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Browser' setting is missing or unsupported.</exception>
        public static BrowserEnums GetBrowser()
        {
            var browser = config?["Browser"];

            if (browser == null)
            {
                throw new ArgumentException("Key 'Browser' cannot be null");
            }
            else
            {
                return Enum.Parse<BrowserEnums>(browser);
            }
        }

        public static string GetUrl()
        {
            var url = config?["URL"];

            if (url == null)
            {
                throw new ArgumentException("Key 'URL' cannot be null");
            }
            else
            {
                return url;
            }
        }
    }
}