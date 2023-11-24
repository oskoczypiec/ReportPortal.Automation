using Newtonsoft.Json;
using ReportPortal.Business.DataSets;
using ReportPortal.Core.API;
using ReportPortal.Core.API.Models;
using FluentAssertions;

namespace ReportPortal.Tests.API
{
    public class Tests : BaseApiTest
    {
        private Endpoints endpoints;
        private FiltersDataProvider filtersDataProvider;

        [SetUp]
        public void Setup()
        {
            endpoints = new Endpoints();
            filtersDataProvider = new FiltersDataProvider();
        }

        [Test]
        public async Task Get_Success_All_Filters()
        {
            // Assembly
            var filter = await endpoints.GetFilter();

            // Act
            var actualFilterModel = JsonConvert.DeserializeObject<FiltersRoot>(filter.Content!);
            var expectedFilterModel = filtersDataProvider.GenerateDefaultGetFiltersResponse();

            // Assert
            actualFilterModel.Should().BeEquivalentTo(expectedFilterModel);
        }

        [Test]
        public async Task Get_Failure_All_Filters()
        {
            // Assembly
            var filter = await endpoints.GetFilter();      

            // Act
            var actualFilterModel = JsonConvert.DeserializeObject<FiltersRoot>(filter.Content!);
            var expectedFilterModel = filtersDataProvider.GenerateRandomGetFiltersResponse();

            // Assert
            actualFilterModel.Should().NotBeEquivalentTo(expectedFilterModel);
        }
    }
}