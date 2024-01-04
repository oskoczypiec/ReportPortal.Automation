using FluentAssertions;
using Newtonsoft.Json;
using ReportPortal.Business.DataSets;
using ReportPortal.Core.API;
using ReportPortal.Core.API.Models;
using ReportPortal.Core.Data;
using ReportPortal.Core.Utilities;
using System.Net;

namespace ReportPortal.Tests.API.Tests
{
    internal class PutFiltersTests : BaseApiTest
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
        public async Task Put_Success_Filter()
        {
            // Arrange
            var descriptionText = "PUT test";
            var postFilterModel = filtersDataProvider.GenerateRandomAddFiltersResponse();
            var putFilterModel = filtersDataProvider.GenerateRandomAddFiltersResponse();

            var postedFilter = await filtersEndpoints.PostFilter(postFilterModel);
            var postedFilterId = JsonConvert.DeserializeObject<FiltersId>(postedFilter.Content!).Id.ToString();

            putFilterModel.Description = descriptionText;

            // Act
            var response = filtersEndpoints.PutFilterById(putFilterModel, postedFilterId);
            var getFilters = await filtersEndpoints.GetFilterNames();
            var actualFilters = JsonConvert.DeserializeObject<FiltersRootModel>(getFilters.Content!);

            // Assert
            response.Result.StatusCode.Should().Be(HttpStatusCode.OK);
            actualFilters.Content.Should().Contain(x => x.Description == descriptionText);
        }

        [Test]
        public async Task Put_Failure_Not_Existing_Filter()
        {
            // Arrange
            var putFilterModel = filtersDataProvider.GenerateRandomAddFiltersResponse();
            var randomId = RandomHelper.RandomNumber().ToString();

            // Act
            var response = await filtersEndpoints.PutFilterById(putFilterModel, randomId);
            var actualFilters = JsonConvert.DeserializeObject<ErrorModel>(response.Content!);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            actualFilters.Message.Should().Contain($"User filter with ID '{randomId}' not found on project 'default_personal'. Did you use correct User Filter ID?");
        }

        [Test]
        public async Task Put_Failure_Incorrect_Body()
        {
            // Arrange
            var putFilterModel = filtersDataProvider.GenerateIncorrectAddFilters();
            var randomId = RandomHelper.RandomNumber().ToString();
            var conditionText = putFilterModel.Conditions.First().ConditionText;
            var expectedErrorMessage = $"{ErrorMessages.IncorrectFilterType}{conditionText}";

            // Act
            var response = await filtersEndpoints.PutFilterById(putFilterModel, randomId);
            var actualFilters = JsonConvert.DeserializeObject<ErrorModel>(response.Content!);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            actualFilters.Message.Should().Contain(expectedErrorMessage);
        }
    }
}
