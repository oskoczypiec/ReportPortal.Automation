using FluentAssertions;
using Newtonsoft.Json;
using ReportPortal.Business.DataSets;
using ReportPortal.Core.API;
using ReportPortal.Core.API.Models;

namespace ReportPortal.Tests.API.Tests
{
    public class GetFiltersTests : BaseApiTest
    {
        private FiltersEndpoints filtersEndpoints;
        private FiltersDataProvider filtersDataProvider;

        [SetUp]
        public void Setup()
        {
            filtersEndpoints = new FiltersEndpoints();
            filtersDataProvider = new FiltersDataProvider();
        }

        [Test]
        public async Task Get_Success_All_Filters()
        {
            // Assembly
            var response = await filtersEndpoints.GetFilter();

            // Act
            var actualFilterModel = JsonConvert.DeserializeObject<FiltersRootModel>(response.Content!);
            var expectedFilterModel = filtersDataProvider.GenerateRealFilterResponse(actualFilterModel);

            // Assert
            actualFilterModel.Should().BeEquivalentTo(expectedFilterModel);
        }

        [Test]
        public async Task Get_Failure_All_Filters()
        {
            // Assembly
            var filter = await filtersEndpoints.GetFilter();      

            // Act
            var actualFilterModel = JsonConvert.DeserializeObject<FiltersRootModel>(filter.Content!);
            var expectedFilterModel = filtersDataProvider.GenerateRandomGetFiltersResponse();

            // Assert
            actualFilterModel.Should().NotBeEquivalentTo(expectedFilterModel);
        }
    }
}