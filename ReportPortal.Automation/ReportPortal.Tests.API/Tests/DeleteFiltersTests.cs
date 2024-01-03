using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using ReportPortal.Business.DataSets;
using ReportPortal.Core.API;
using ReportPortal.Core.API.Models;
using ReportPortal.Core.Data;
using ReportPortal.Core.Utilities;

namespace ReportPortal.Tests.API.Tests
{
    public class DeleteFiltersTests : BaseApiTest
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
        public async Task Delete_Success()
        {
            // Arrange
            var expectedFilterModel = filtersDataProvider.GenerateRandomAddFiltersResponse();
            var response = await filtersEndpoints.PostFilter(expectedFilterModel);
            var actualResponse = JsonConvert.DeserializeObject<FiltersId>(response.Content);

            // Act
            await filtersEndpoints.DeleteFiltersById(actualResponse.Id);
            var actualFilter = await filtersEndpoints.GetFilterNames();
            var actualFilters = JsonConvert.DeserializeObject<FiltersRootModel>(actualFilter.Content!);
            var expectedFilterName = expectedFilterModel.Name;

            // Assert
            actualFilters.Content.Should().NotContain(x => x.Name == expectedFilterName);
        }

        [Test]
        public async Task Delete_Failure()
        {
            // Arrange
            var randomId = RandomHelper.RandomNumber();

            // Act
            var response = await filtersEndpoints.DeleteFiltersById(randomId);
            var actualResponse = JsonConvert.DeserializeObject<ErrorModel>(response.Content!);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
            actualResponse.Message.Should().Contain(ErrorMessages.FilterNotFoundError(randomId.ToString()));
        }
    }
}
