// <copyright file="BaseTest.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Tests
{
    using OpenQA.Selenium;
    using ReportPortal.Core.Config;
    using ReportPortal.Core.Logger;
    using ReportPortal.Core.Reporter;

    /// <summary>
    /// Base test class for Selenium tests. Provides setup and teardown methods.
    /// </summary>
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class BaseTest
    {
        /// <summary>
        /// The WebDriver instance used for test automation. It can be accessed and manipulated by derived test classes.
        /// </summary>
        protected IWebDriver? driver;

        private static string screenshotFolder = "screenshots";

        /// <summary>
        /// One-time setup method for test run initialization.
        /// </summary>
        [OneTimeSetUp]
        public static void BaseOneTimeSetUp()
        {
            Logger.Log.Info($"Starting test run");

            if (!Directory.Exists(screenshotFolder))
            {
                Directory.CreateDirectory(screenshotFolder);
            }
        }

        /// <summary>
        /// One-time teardown method for test run completion.
        /// </summary>
        [OneTimeTearDown]
        public static void BaseOneTearDown()
        {
            Logger.Log.Info($"Test run completed");
            Logger.Log.CloseLogger();
        }

        /// <summary>
        /// Initializes the WebDriver and navigates to the test URL.
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            this.driver = WebDriverFactory.CreateDriver();
            this.driver.Navigate().GoToUrl("http://localhost:8080/ui/#login");
            Logger.Log.Info($"Running test: {TestContext.CurrentContext.Test.Name}");
        }

        /// <summary>
        /// Cleans up and reports test results, including attaching screenshots if a test fails.
        /// </summary>
        [TearDown]
        public void BaseTearDown()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            var result = TestContext.CurrentContext.Result.Outcome.Status;

            Logger.Log.Info($"Test {testName} is {result}");
            Reporter.AttachScreenshotIfFailed(this.driver!, result, testName);

            this.driver?.Quit();
            this.driver?.Dispose();
            this.driver = null;
        }
    }
}