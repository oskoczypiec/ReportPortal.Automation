using Newtonsoft.Json;
using ReportPortal.Core.API;
using ReportPortal.Core.API.Models;
using ReportPortal.Core.Config;

namespace ReportPortal.Tests.API
{
    public class BaseApiTest
    {
        private FiltersEndpoints endpoints;
        private DemoDataGeneratedModel dataGenerated;

        [OneTimeSetUp]
        public async Task Init()
        {
            ApplicationConfiguration.SetUp();
            endpoints = new FiltersEndpoints();
            var response = await endpoints.GenerateDemoData();
            var responseContent = response?.Content;
            if (responseContent != null)
            {
                dataGenerated = JsonConvert.DeserializeObject<DemoDataGeneratedModel>(responseContent)!;
            }
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            LaunchRequestModel launchRequest = new LaunchRequestModel()
            {
                LaunchIds = dataGenerated.LaunchIds,
            };
            var responseFilters = await endpoints.GetFilter();
            var actualFilters = JsonConvert.DeserializeObject<FiltersRootModel>(responseFilters.Content!);
            var filtersIds = actualFilters.Content.Select(x => x.Id).ToList();

            foreach (var filterId in filtersIds)
            {
                await endpoints.DeleteFiltersById(filterId);

            }
            await endpoints.DeleteDashboardById(id: dataGenerated.DashboardId.ToString());
            await endpoints.DeleteLaunchByIds(launchRequest);
        }
    }
}
