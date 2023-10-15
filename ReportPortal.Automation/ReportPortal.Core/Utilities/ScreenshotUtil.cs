// <copyright file="ScreenshotUtil.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Utilities
{
    using OpenQA.Selenium;
    using ReportPortal.Core.Logger;

    public static class ScreenshotUtil
    {
        private static readonly string ScreenshotFolder = "screenshots";

        public static string TakeScreenshot(IWebDriver driver, string testName)
        {
            string screenshotPath = string.Empty;
            try
            {
                Logger.Log.Info($"Creating screenshot for: {testName}");
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var screenshotName = $"{testName}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.png";
                screenshotPath = Path.Combine(ScreenshotFolder, screenshotName);
                screenshot.SaveAsFile(screenshotPath);
            }
            catch (Exception e)
            {
                Logger.Log.Error($"Error taking screenshot: {e.Message}");
            }

            return screenshotPath;
        }
    }
}
