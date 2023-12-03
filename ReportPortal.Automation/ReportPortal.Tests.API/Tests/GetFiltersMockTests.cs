using FluentAssertions;
using Moq;
using ReportPortal.Business.DataSets;
using ReportPortal.Tests.API.Mock;

namespace ReportPortal.Tests.API.Tests
{
    public class GetFiltersMockTests
    {
        private FiltersDataProvider filtersDataProvider;

        [SetUp]
        public void Setup()
        {
            filtersDataProvider = new FiltersDataProvider();
        }

        [Test]
        public async Task GetFilters_ReturnsOneFilter()
        {
            // Arrange
            var mockFilterApiClient = new Mock<IFilterApiClient>();
            var expectedFiltersRootModel = filtersDataProvider.GenerateRandomGetFiltersResponse();

            mockFilterApiClient.Setup(x => x.GetFiltersAsync()).ReturnsAsync(expectedFiltersRootModel);

            // Act
            var result = await mockFilterApiClient.Object.GetFiltersAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedFiltersRootModel);

            mockFilterApiClient.Verify(x => x.GetFiltersAsync(), Times.Once);
            mockFilterApiClient.VerifyNoOtherCalls();
        }
    }
}
