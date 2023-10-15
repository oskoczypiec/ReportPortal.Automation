// <copyright file="WebDriverFactory.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Config
{
    using OpenQA.Selenium;
    using ReportPortal.Core.Enums;

    /// <summary>
    /// Class for creating Webdriver based on Factory pattern.
    /// </summary>
    public class WebDriverFactory
    {
        /// <summary>
        /// Creates and configures a WebDriver instance based on the selected browser.
        /// </summary>
        /// <returns>The configured WebDriver instance.</returns>
        public static IWebDriver CreateDriver()
        {
            ApplicationConfiguration.SetUp();
            var browser = ApplicationConfiguration.GetBrowser();
            var browserType = GetBrowserOptions(browser);
            return browserType!;
        }

        /// <summary>
        /// Retrieves and configures browser-specific options based on the specified browser name.
        /// </summary>
        /// <param name="browserName">The name of the desired web browser.</param>
        /// <returns>The configured browser-specific options for WebDriver.</returns>
        /// <exception cref="ArgumentException">Thrown when an unsupported browser name is provided.</exception>
        private static dynamic? GetBrowserOptions(BrowserEnums browserName)
        {
            switch (browserName)
            {
                case BrowserEnums.Chrome:
                    return new Chrome().ConfigureChromeDriver();
                default:
                    throw new ArgumentException($"Unsupported browser name: {browserName}");
            }
        }
    }
}
