namespace ReportPortal.Core.Tests
{
    using OpenQA.Selenium;
    using ReportPortal.Core.Config;
    using ReportPortal.Core.Logger;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class BaseTest
    {
        protected IWebDriver? Driver;

        [SetUp]
        public void Initialize()
        {
            Driver = WebDriverFactory.CreateDriver();
            Driver.Navigate().GoToUrl("http://localhost:8080/ui/#login");
            Logger.Log.Info($"Running test: {TestContext.CurrentContext.Test.Name}");
        }

        [OneTimeSetUp]
        public static void BaseOneTimeSetUp()
        {
            Logger.Log.Info($"Starting test run");
        }

        [OneTimeTearDown]
        public static void BaseOneTearDown()
        {
            Logger.Log.Info($"Test run completed");
        }

        [TearDown]
        public void BaseTearDown()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            var result = TestContext.CurrentContext.Result.Outcome.Status;
            Logger.Log.Info($"Test {testName} is {result}");
            Driver.Quit();
            Driver = null;
        }
    }
}