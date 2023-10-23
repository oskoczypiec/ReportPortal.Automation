// <copyright file="WebDriverExtensions.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Config
{
    using System.Collections.ObjectModel;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using ReportPortal.Core.Logger;

    public static class WebDriverExtensions
    {
        public static IWebElement GetElement(this IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            if (timeoutInSeconds > 0)
            {
                try
                {
                    Logger.Log.Info($"Attempting to find element: {by}");
                    return wait.Until(drv => drv.FindElement(by));
                }
                catch (StaleElementReferenceException)
                {
                    Logger.Log.Warn($"StaleElementReferenceException occurred. Retrying...");
                    return wait.Until(drv => drv.FindElement(by));
                }
            }

            return driver.FindElement(by);
        }

        public static IWebElement GetElement(this IWebDriver driver, IWebElement element, By by, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            if (timeoutInSeconds > 0)
            {
                Logger.Log.Info($"Attempting to find element: {by}");

                return wait.Until(drv => drv.FindElement(by));
            }

            return element.FindElement(by);
        }

        public static ReadOnlyCollection<IWebElement> GetElements(this IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            if (timeoutInSeconds > 0)
            {
                try
                {
                    Logger.Log.Info($"Attempting to find element: {by}");
                    return wait.Until(drv => (drv.FindElements(by).Count > 0) ? drv.FindElements(by) : null);
                }
                catch (StaleElementReferenceException)
                {
                    Logger.Log.Warn($"StaleElementReferenceException occurred. Retrying...");
                    return wait.Until(drv => (drv.FindElements(by).Count > 0) ? drv.FindElements(by) : null);
                }
            }

            return driver.FindElements(by);
        }

        public static void WaitUntilElementCountIs(this IWebDriver driver, By by, int expectedCount, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            wait.Until(d => d.GetElements(by).Count == expectedCount);
        }
    }
}
