// <copyright file="FilterTests.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Tests.ComponentTests
{
    using ReportPortal.Business.DataSets;
    using ReportPortal.Business.Pages;

    public class FilterTests : BaseTest
    {
        private LoginPage loginPage;
        private MenuPage menuPage;
        private FiltersPage filtersPage;

        [SetUp]
        public void SetUp()
        {
            this.loginPage = new LoginPage(this.driver!);
            this.menuPage = new MenuPage(this.driver!);
            this.filtersPage = new FiltersPage(this.driver!);

            this.loginPage.LogInDefaultUser();
            this.menuPage.ClickFiltersButton();
        }

        [Test]
        [TestCaseSource(typeof(FilterDataSet), nameof(FilterDataSet.DifferentFilters))]
        public void UserIsAbleToAddAFilter(string filterName, string value, int expectedRowsCount)
        {
            // Assembly
            this.filtersPage
                .ClickAddFilterButton()
                .ClickMoreButton();

            // Act
            this.filtersPage
                .SelectEntities(filterName)
                .TypeValue(filterName, value);

            // Assert
            this.filtersPage
                .WaitUntilExpectedRowsCount(expectedRowsCount);
        }
    }
}
