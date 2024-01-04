// <copyright file="Chrome.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Config
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    /// <summary>
    /// Helper class for configuring and creating instances of the Chrome WebDriver with custom options.
    /// </summary>
    public class Chrome
    {
        /// <summary>
        /// Configures and creates a Chrome WebDriver instance with custom options.
        /// </summary>
        /// <returns>The configured Chrome WebDriver instance.</returns>
        public IWebDriver ConfigureChromeDriver()
        {
            ChromeOptions options = new ();
            options.AddExcludedArgument("--enable-automation");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--start-maximized");
            options.AddArgument("--headless");
            options.AddArgument("--incognito");

            return new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
        }
    }
}