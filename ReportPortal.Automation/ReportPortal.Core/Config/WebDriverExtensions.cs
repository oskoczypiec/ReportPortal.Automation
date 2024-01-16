// <copyright file="WebDriverExtensions.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Config
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using ReportPortal.Core.Logger;
    using System.Collections.ObjectModel;

    public static class WebDriverExtensions
    {
        public static IWebElement GetElement(this IWebDriver driver, By by, int timeoutInSeconds = 20)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));

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

        public static int WaitUntilElementCountIs(this IWebDriver driver, By by, int expectedCount, int timeoutInSeconds = 10)
        {
            ReadOnlyCollection<IWebElement> actualElements;
            try
            {
                if (timeoutInSeconds > 0)
                {
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                    wait.IgnoreExceptionTypes(typeof(OpenQA.Selenium.WebDriverTimeoutException));

                    var elements = wait.Until(driver =>
                    {
                        var actualElements = driver.GetElements(by);
                        return actualElements.Count == expectedCount ? actualElements : null;
                    });
                }

                actualElements = driver.GetElements(by);
                return actualElements.Count;
            }
            catch (WebDriverTimeoutException)
            {
                actualElements = driver.GetElements(by);
                Console.WriteLine($"Timeout! Actual element count: {actualElements.Count}");
                return actualElements.Count;
            }
        }

        public static void WaitUntilElementIsVisible(this IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(d => d.GetElement(by).Displayed == true);
        }

        public static void WaitUntilElementIsNotVisible(this IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(d =>
            {
                var elements = d.FindElements(by);
                return elements.Count == 0 || elements.All(e => !e.Displayed);
            });
        }

        public static IWebElement ResizeElementUsingJavaScript(this IWebDriver driver, int width, int height, IWebElement element)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript($"arguments[0].style.width='{width}px'; arguments[0].style.height='{height}px';", element);
            return element;
        }

        public static IWebElement ClickOnElementUsingActions(this IWebDriver driver, IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.Click(element)
                   .Perform();
            return element;
        }

        public static void ScrollToElement(this IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static bool IsElementInView(this IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            long viewportHeight = (long)js.ExecuteScript("return window.innerHeight || document.documentElement.clientHeight;");
            long elementTop = (long)js.ExecuteScript("return arguments[0].getBoundingClientRect().top;", element);

            return elementTop >= 0 && elementTop <= viewportHeight;
        }

        public static void JavaScriptClick(this IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }
    }
}
