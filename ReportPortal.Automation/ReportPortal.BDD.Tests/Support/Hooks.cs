namespace ReportPortal.BDD.Tests.Support
{
    using BoDi;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using ReportPortal.Core.Config;
    using ReportPortal.Core.Logger;
    using ReportPortal.Core.Utilities;
    using TechTalk.SpecFlow;

    [Binding]
    public class Hooks
    {
        private IWebDriver driver;
        private readonly IObjectContainer objectContainer;
        private readonly TestContext testContext;
        private static string screenshotFolder = "screenshots";

        public Hooks(IObjectContainer objectContainer, TestContext testContext)
        {
            this.objectContainer = objectContainer;
            this.testContext = testContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Logger.Log.Info($"Starting BDD test run");

            if (!Directory.Exists(screenshotFolder))
            {
                Directory.CreateDirectory(screenshotFolder);
            }
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = WebDriverFactory.CreateDriver();
            objectContainer.RegisterInstanceAs(driver);
            this.driver.Navigate().GoToUrl("http://localhost:8080/ui/#login");
            Logger.Log.Info($"Running test: {TestContext.CurrentContext.Test.Name}");

        }

        [AfterScenario]
        public void AfterScenario()
        {
            var testName = TestContext.CurrentContext.Test.Name;

            if (driver != null)
            {
                ScreenshotUtil.TakeScreenshot(driver, testName);
                objectContainer.ResolveAll<IWebDriver>();
                objectContainer.Dispose();
                driver.Quit();
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {

        }
    }
}