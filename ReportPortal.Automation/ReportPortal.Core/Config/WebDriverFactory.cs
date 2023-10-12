namespace ReportPortal.Core.Config
{
    using OpenQA.Selenium;

    /// <summary>
    /// Class for creating Webdriver based on Factory pattern.
    /// </summary>
    public class WebDriverFactory
    {
        /// <summary>
        /// Method for dynamic driver creation depending on browser type.
        /// </summary>
        /// <returns>Driver.</returns>
        public static IWebDriver CreateDriver()
        {
            // TODO: Research and select the best way to pass this argument through file (.json, .xml).
            var browserType = GetBrowserOptions(BrowserEnums.Chrome);
            return browserType!;
        }

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
