// <copyright file="MsFilterTests.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Tests.ComponentTests.MsTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ReportPortal.Business.DataSets;
    using ReportPortal.Business.Pages;

    [TestClass]
    public class MsFilterTests : MsTestBase
    {
        private LoginPage loginPage;
        private MenuPage menuPage;
        private FiltersPage filtersPage;

        [TestInitialize]
        public void SetUp()
        {
            this.loginPage = new LoginPage(this.driver!);
            this.menuPage = new MenuPage(this.driver!);
            this.filtersPage = new FiltersPage(this.driver!);

            this.loginPage.LogInDefaultUser();
            this.menuPage.ClickFiltersButton();
        }

        [DataTestMethod]
        [DynamicData(nameof(DifferentFilters))]
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

        private static IEnumerable<object[]> DifferentFilters
        {
            get
            {
                return new[]
                {
                    new object[] { FilterNames.Passed, "30", 1 },
                    new object[] { FilterNames.LaunchNumber, "3", 3 },
                    new object[] { FilterNames.Description, "Demo", 5 },
                    new object[] { FilterNames.Owner, string.Empty, 5 },
                };
            }
        }
    }
}
