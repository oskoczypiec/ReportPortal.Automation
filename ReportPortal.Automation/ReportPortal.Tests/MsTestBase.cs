// <copyright file="MsTestBase.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using ReportPortal.Core.Config;
    using ReportPortal.Core.Enums;
    using ReportPortal.Core.Logger;
    using ReportPortal.Core.Reporter;
    using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

    [TestClass]
    public class MsTestBase
    {
        protected IWebDriver? driver;

        private static string screenshotFolder = "screenshots";

        public TestContext? TestContext { get; set; }

        [ClassInitialize]
        public static void BaseClassInitialize(TestContext testContext)
        {
            Logger.Log.Info("Starting test run");

            if (!Directory.Exists(screenshotFolder))
            {
                Directory.CreateDirectory(screenshotFolder);
            }
        }

        [ClassCleanup]
        public static void BaseClassCleanup()
        {
            Logger.Log.Info("Test run completed");
            Logger.Log.CloseLogger();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.driver = WebDriverFactory.CreateDriver();
            this.driver.Navigate().GoToUrl("http://localhost:8080/ui/#login");
            Logger.Log.Info($"Running test: {TestContext.TestName}");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var testName = TestContext.TestName.ToString();
            var result = TestContext.CurrentTestOutcome;

            Logger.Log.Info($"Test {testName} is {result}");

            if (result == (UnitTestOutcome)TestStatus.Failed)
            {
                Reporter.AttachScreenshotIfFailed(this.driver, (TestStatus)result, testName);
            }

            this.driver.Quit();
            this.driver.Dispose();
            this.driver = null;
        }
    }
}