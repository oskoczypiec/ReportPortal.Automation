// <copyright file="Reporter.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Reporter
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using ReportPortal.Core.Enums;
    using ReportPortal.Core.Utilities;

    /// <summary>
    /// Reporter class for generating test result report.
    /// </summary>
    public static class Reporter
    {
        /// <summary>
        /// Attaches a screenshot to the test report if the test has failed.
        /// </summary>
        /// <param name="driver">The WebDriver instance used for the test.</param>
        /// <param name="result">The result status of the test.</param>
        /// <param name="testName">The name of the test being executed.</param>
        public static void AttachScreenshotIfFailed(IWebDriver driver, TestStatus result, string testName)
        {
            var plainTestName = testName.Replace("\"", string.Empty);

            if (result != TestStatus.Passed)
            {
                var screenshotFile = ScreenshotUtil.TakeScreenshot(driver, plainTestName);
                TestContext.AddTestAttachment(screenshotFile, $"Screenshot_{plainTestName}");
            }
        }
    }
}
