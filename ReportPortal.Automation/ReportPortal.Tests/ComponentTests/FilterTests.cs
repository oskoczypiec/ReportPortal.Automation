// <copyright file="FilterTests.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Tests.ComponentTests
{
    using ReportPortal.Business.DataSets;
    using ReportPortal.Business.Pages;
    using ReportPortal.Business.Pages.Modal;

    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class FilterTests : BaseTest
    {
        private LoginPage loginPage;
        private MenuPage menuPage;
        private FiltersPage filtersPage;
        private AddFilterModal addFilterModal;

        [SetUp]
        public void SetUp()
        {
            this.loginPage = new LoginPage(this.driver!);
            this.menuPage = new MenuPage(this.driver!);
            this.filtersPage = new FiltersPage(this.driver!);
            this.addFilterModal = new AddFilterModal(this.driver!);

            this.loginPage.LogInDefaultUser();
            this.menuPage.ClickFiltersButton();
        }

        [TearDown]
        public void TearDown()
        {
            this.menuPage.ClickFiltersButton();
            this.menuPage.RemoveAllFilters();
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

        [Test]
        [TestCaseSource(typeof(FilterDataSet), nameof(FilterDataSet.DifferentFilters))]
        public void UserIsAbleToSaveFilter(string filterName, string value, int expectedRowsCount)
        {
            // Assembly
            this.filtersPage
                .ClickAddFilterButton()
                .ClickMoreButton()
                .SelectEntities(filterName)
                .TypeValue(filterName, value)
                .WaitUntilExpectedRowsCount(expectedRowsCount);

            // Act
            this.filtersPage
                .ClickSaveButton();
            this.addFilterModal
                .WaitUntilModalHeaderIsDisplayed()
                .SetFilterName(filterName)
                .ClickAddFilterButton();

            // Assert
            this.filtersPage
                .WaitUntilFilterListItemIsDisplayed(filterName);
        }

        [Test]
        [TestCaseSource(typeof(FilterDataSet), nameof(FilterDataSet.DifferentFilters))]
        public void UserIsAbleToDeleteFilter(string filterName, string value, int expectedRowsCount)
        {
            // Assembly
            this.filtersPage
                .ClickAddFilterButton()
                .ClickMoreButton()
                .SelectEntities(filterName)
                .TypeValue(filterName, value)
                .WaitUntilExpectedRowsCount(expectedRowsCount);
            this.filtersPage
                .ClickSaveButton();
            this.addFilterModal
                .WaitUntilModalHeaderIsDisplayed()
                .SetFilterName(filterName)
                .ClickAddFilterButton();

            // Act
            this.filtersPage
                .WaitUntilFilterListItemIsDisplayed(filterName)
                .ClickRemoveActiveFilterButton();

            // Assert
            this.filtersPage
                .WaitUntilFilterListItemIsNotDisplayed();
        }
    }
}
