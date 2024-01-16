// <copyright file="FilterTests.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Tests.ComponentTests
{
    using FluentAssertions;
    using FluentAssertions.Execution;
    using ReportPortal.Business.DataSets;
    using ReportPortal.Business.Pages;
    using ReportPortal.Business.Pages.Modal;
    using ReportPortal.Core.Config;
    using ReportPortal.Core.Utilities;

    [TestFixture]
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
        public async Task TearDown()
        {
            await this.ApiClean();
        }

        [Test]
        [TestCaseSource(typeof(FilterDataSet), nameof(FilterDataSet.DifferentFilters))]
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

        [Test]
        [TestCaseSource(typeof(FilterDataSet), nameof(FilterDataSet.DifferentFilters))]
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

        [Test]
        [TestCaseSource(typeof(FilterDataSet), nameof(FilterDataSet.DifferentFilters))]
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

        [Test]
        [TestCaseSource(typeof(FilterDataSet), nameof(FilterDataSet.DifferentFilters))]
        public void UserIsAbleToEditAFilter(string filterName, string value, int expectedRowsCount)
        {
            // Arrange
            var randomName = RandomHelper.AlphabeticalString(5);
            this.filtersPage
                .ClickAddFilterButton()
                .ClickMoreButton()
                .SelectEntities(filterName)
                .TypeValue(filterName, value);

            // Act
            this.filtersPage
                .ClickSaveButton();
            this.addFilterModal
                .WaitUntilModalHeaderIsDisplayed()
                .SetFilterName(filterName)
                .ClickAddFilterButton();

            this.filtersPage.ClickEditButton();
            this.addFilterModal
                .WaitUntilModalHeaderIsDisplayed()
                .SetFilterName(randomName)
                .ClickUpdateFilterButton();

            // Assert
            this.filtersPage
                .WaitUntilFilterListItemIsDisplayed(randomName);
        }

        [Test]
        [TestCaseSource(typeof(FilterDataSet), nameof(FilterDataSet.DifferentFilters))]
        public void UserIsAbleToCloneAFilter(string filterName, string value, int temp)
        {
            // Arrange
            var expectedName = $"Copy {filterName}";
            this.filtersPage
                .ClickAddFilterButton()
                .ClickMoreButton()
                .SelectEntities(filterName)
                .TypeValue(filterName, value);

            // Act
            this.filtersPage
                .ClickSaveButton();
            this.addFilterModal
                .WaitUntilModalHeaderIsDisplayed()
                .SetFilterName(filterName)
                .ClickAddFilterButton();
            this.filtersPage
               .WaitUntilFilterListItemIsDisplayed(filterName);
            this.filtersPage.ClickCloneFilterButton();

            // Assert
            this.filtersPage
                .WaitUntilFilterListItemIsDisplayed(expectedName);
        }

        [Test]
        public void ResizeElementUsingJavaScript()
        {
            // Arrange
            var expectedHeight = 300;
            var expectedWidth = 400;

            this.filtersPage
                .ClickAddFilterButton()
                .ClickDropdownButton();

            // Act
            WebDriverExtensions.ResizeElementUsingJavaScript(this.driver!, expectedWidth, expectedHeight, this.filtersPage.GetDropdownComponent());

            // Assert
            using (new AssertionScope())
            {
                this.filtersPage.GetDropdownComponent().Size.Height.Should().Be(expectedHeight);
                this.filtersPage.GetDropdownComponent().Size.Width.Should().Be(expectedWidth);
            }
        }

        [Test]
        public void DropdownIsDisplayedWhenActionClick()
        {
            // Arrange
            this.filtersPage
                .ClickAddFilterButton();

            // Act
            WebDriverExtensions.ClickOnElementUsingActions(this.driver!, this.filtersPage.GetDropdownButton());

            // Assert
            this.filtersPage.GetDropdownComponent().Displayed.Should().BeTrue();
        }

        [Test]
        public void ScrollToElement()
        {
            // Arrange
            this.filtersPage
                .ClickAddFilterButton();
            var element = this.filtersPage.GetNthResultRow(4);

            // Act
            WebDriverExtensions.ScrollToElement(this.driver!, element);

            // Assert
            WebDriverExtensions.IsElementInView(this.driver!, element).Should().BeTrue();
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void UserIsAbleToEnableOrDisableAFilter(bool isDisplayed)
        {
            // Arrange
            var filterName = "Passed";
            var value = "30";
            this.filtersPage
                .ClickAddFilterButton()
                .ClickMoreButton();

            // Act
            this.filtersPage
                .SelectEntities(filterName)
                .TypeValue(filterName, value);
            this.filtersPage
                .ClickSaveButton();
            this.addFilterModal
                .WaitUntilModalHeaderIsDisplayed()
                .SetFilterName(filterName)
                .ClickAddFilterButton();
            this.menuPage.ClickFiltersButton();

            // Assert
            this.filtersPage.SetDisplayLaunches(isDisplayed, filterName);
            this.filtersPage
                .ClickAddFilterButton();
            if (isDisplayed)
            {
                this.filtersPage
                    .WaitUntilFilterListItemIsDisplayed(filterName);
            }
            else
            {
                this.filtersPage
                    .WaitUntilFilterListItemIsNotDisplayed(filterName);
            }
        }
    }
}
