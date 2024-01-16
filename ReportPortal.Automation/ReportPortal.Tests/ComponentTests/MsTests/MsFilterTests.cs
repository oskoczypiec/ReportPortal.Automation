// <copyright file="MsFilterTests.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Tests.ComponentTests.MsTest
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ReportPortal.Business.DataSets;
    using ReportPortal.Business.Pages;
    using ReportPortal.Business.Pages.Modal;

    [TestClass]
    [Ignore("MSTests ignored")]
    public class MsFilterTests : MsTestBase
    {
        private LoginPage loginPage;
        private MenuPage menuPage;
        private FiltersPage filtersPage;
        private AddFilterModal addFilterModal;

        [TestInitialize]
        public void SetUp()
        {
            this.loginPage = new LoginPage(this.driver!);
            this.menuPage = new MenuPage(this.driver!);
            this.filtersPage = new FiltersPage(this.driver!);
            this.addFilterModal = new AddFilterModal(this.driver!);

            this.loginPage.LogInDefaultUser();
            this.menuPage.ClickFiltersButton();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.menuPage.ClickFiltersButton();
            this.menuPage.RemoveAllFilters();
        }

        [DataTestMethod]
        [DynamicData(nameof(DifferentFilters))]
        public void UserIsAbleToAddAFilter(string filterName, string value, int expectedRowsCount)
        {
            // Arrange
            this.filtersPage
                .ClickAddFilterButton()
                .ClickMoreButton();

            // Act
            this.filtersPage
                .SelectEntities(filterName)
                .TypeValue(filterName, value);

            // Assert
            this.filtersPage
                .WaitUntilExpectedRowsCount(expectedRowsCount).Should().Be(expectedRowsCount);
        }

        [DataTestMethod]
        [DynamicData(nameof(DifferentFilters))]
        public void UserIsAbleToSaveFilter(string filterName, string value, int expectedRowsCount)
        {
            // Arrange
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

        [DataTestMethod]
        [DynamicData(nameof(DifferentFilters))]
        public void UserIsAbleToDeleteFilter(string filterName, string value, int expectedRowsCount)
        {
            // Arrange
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
                .WaitUntilFilterListIsNotDisplayed();
        }

        private static IEnumerable<object[]> DifferentFilters
        {
            get
            {
                return new[]
                {
                    new object[] { FilterNames.Passed, "30", 1 },
                    new object[] { FilterNames.Skipped, "3", 3 },
                    new object[] { FilterNames.Description, "Demo", 5 },
                    new object[] { FilterNames.Failed, "1", 4 },
                    new object[] { FilterNames.TotalToInvestigate, "1", 4 },
                };
            }
        }
    }
}
