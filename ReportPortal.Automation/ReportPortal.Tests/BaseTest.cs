// <copyright file="BaseTest.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Tests
{
    using Newtonsoft.Json;
    using OpenQA.Selenium;
    using ReportPortal.Core.API;
    using ReportPortal.Core.API.Models;
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
        private FiltersEndpoints endpoints;
        private DemoDataGeneratedModel dataGenerated;


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
        public async Task Initialize()
        {
            await ApiSetUp();

            this.driver = WebDriverFactory.CreateDriver();
            this.driver.Navigate().GoToUrl($"{Settings.URL}/ui/#login");
            Logger.Log.Info($"Running test: {TestContext.CurrentContext.Test.Name}");
        }

        private async Task ApiSetUp()
        {
            ApplicationConfiguration.SetUp();
            endpoints = new FiltersEndpoints();
            var response = await endpoints.GenerateDemoData();
            dataGenerated = JsonConvert.DeserializeObject<DemoDataGeneratedModel>(response.Content);
        }

        /// <summary>
        /// Cleans up and reports test results, including attaching screenshots if a test fails.
        /// </summary>
        [TearDown]
        public async Task BaseTearDown()
        {
            await ApiClean();

            var testName = TestContext.CurrentContext.Test.Name;
            var result = TestContext.CurrentContext.Result.Outcome.Status;
            var enumParse = (Core.Enums.TestStatus)Enum.Parse(typeof(Core.Enums.TestStatus), result.ToString());

            Logger.Log.Info($"Test {testName} is {result}");
            Reporter.AttachScreenshotIfFailed(this.driver!, enumParse, testName);

            this.driver?.Quit();
            this.driver?.Dispose();
            this.driver = null;
        }

        public async Task ApiClean()
        {
            var responseFilters = await endpoints.GetFilter();
            var actualFilters = JsonConvert.DeserializeObject<FiltersRootModel>(responseFilters.Content!);
            var filtersIds = actualFilters.Content.Select(x => x.Id).ToList();

            foreach (var filterId in filtersIds)
            {
                await endpoints.DeleteFiltersById(filterId);
            }

            var responseGetLaunches = await endpoints.GetLaunches();
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