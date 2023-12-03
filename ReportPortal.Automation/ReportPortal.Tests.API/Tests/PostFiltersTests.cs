using FluentAssertions;
using Newtonsoft.Json;
using ReportPortal.Business.DataSets;
using ReportPortal.Core.API;
using ReportPortal.Core.API.Models;
using ReportPortal.Core.Data;
using System.Net;

namespace ReportPortal.Tests.API.Tests
{
    public class PostFiltersTests : BaseApiTest
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
        public async Task Post_Success_Filter()
        {
            // Assembly
            var expectedFilterModel = filtersDataProvider.GenerateRandomAddFiltersResponse();
            var response = await filtersEndpoints.PostFilter(expectedFilterModel);

            // Act
            var actualFilter = await filtersEndpoints.GetFilterNames();
            var actualFilters = JsonConvert.DeserializeObject<FiltersRootModel>(actualFilter.Content!);
            var expectedFilterName = expectedFilterModel.Name;

            // Assert
            actualFilters.Content.Should().Contain(x => x.Name == expectedFilterName);
        }

        [Test]
        public async Task Post_Failure_Filter_Empty_Filter()
        {
            // Assembly
            var expectedFilterModel = new NewFilterModel();
            var response = await filtersEndpoints.PostFilter(expectedFilterModel);

            // Act
            var responseModel = JsonConvert.DeserializeObject<ErrorModel>(response.Content!);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseModel.Message.Should().ContainEquivalentOf(ErrorMessages.EmptyFiltersError);
        }

        [Test]
        public async Task Post_Failure_Filter_Wrong_Type()
        {
            // Assembly
            var expectedFilterModel = filtersDataProvider.GenerateIncorrectAddFilters();
            var response = await filtersEndpoints.PostFilter(expectedFilterModel);

            // Act
            var responseModel = JsonConvert.DeserializeObject<ErrorModel>(response.Content!);
            var conditionText = expectedFilterModel.Conditions.First().ConditionText;
            var expectedErrorMessage = $"{ErrorMessages.IncorrectFilterType}{conditionText}";

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseModel.Message.Should().ContainEquivalentOf(expectedErrorMessage);
        }
    }
}
