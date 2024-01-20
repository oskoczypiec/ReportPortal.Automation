namespace ReportPortal.BDD.Tests.Support
{
    using BoDi;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using ReportPortal.Core.API;
    using ReportPortal.Core.API.Models;
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
        private FiltersEndpoints endpoints;
        private DemoDataGeneratedModel dataGenerated;

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
        public async Task BeforeScenario()
        {
            driver = WebDriverFactory.CreateDriver();
            objectContainer.RegisterInstanceAs(driver);
            this.driver.Navigate().GoToUrl("http://localhost:8080/ui/#login");

            ApplicationConfiguration.SetUp();
            endpoints = new FiltersEndpoints();
            var response = await endpoints.GenerateDemoData();

            var responseContent = response?.Content;
            if (responseContent != null)
            {
                dataGenerated = JsonConvert.DeserializeObject<DemoDataGeneratedModel>(responseContent)!;
            }

            Logger.Log.Info($"Running test: {TestContext.CurrentContext.Test.Name}");
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            var plainTestName = testName.Replace("\"", string.Empty);

            if (driver != null)
            {
                ScreenshotUtil.TakeScreenshot(driver, plainTestName);
                objectContainer.ResolveAll<IWebDriver>();
                objectContainer.Dispose();
                driver.Quit();
            }

            var responseFilters = await endpoints.GetFilter();
            var responseGetLaunches = await endpoints.GetLaunches();

            var actualFilters = JsonConvert.DeserializeObject<FiltersRootModel>(responseFilters.Content!);
            var filtersIds = actualFilters.Content.Select(x => x.Id).ToList();

            foreach (var filterId in filtersIds)
            {
                await endpoints.DeleteFiltersById(filterId);
            }

            var actualLaunches = JsonConvert.DeserializeObject<FiltersRootModel>(responseGetLaunches.Content!);
            var allLaunchesIds = actualLaunches.Content.Select(x => x.Id).ToList<int>();

            LaunchRequestModel launchRequest = new LaunchRequestModel()
            {
                LaunchIds = allLaunchesIds,
            };

            await endpoints.DeleteLaunchByIds(launchRequest);
            await endpoints.DeleteDashboardById(id: dataGenerated.DashboardId.ToString());
        }


    }
}