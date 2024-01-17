using FluentAssertions;
using ReportPortal.Business.Pages;
using ReportPortal.Business.Pages.Modal;

namespace ReportPortal.BDD.Tests.StepDefinitions
{
    [Binding]
    public class FiltersStepDefinitions
    {
        private IWebDriver _driver;
        private FiltersPage filtersPage => new FiltersPage(_driver);
        private AddFilterModal addFilterModal => new AddFilterModal(_driver);

        public FiltersStepDefinitions(IWebDriver driver)
        {
            this._driver = driver;
        }

        [Given(@"user clicks add filter button")]
        [When(@"user clicks add filter button")]
        public void GivenUserClicksAddFilterButton()
        {
            filtersPage.ClickAddFilterButton();
        }

        [Given(@"user clicks add filter button in modal")]
        [When(@"user clicks add filter button in modal")]
        [When(@"user clicks save filter button in modal")]
        public void WhenUserClicksAddFilterButton()
        {
            addFilterModal.ClickAddFilterButton();
        }

        [Given(@"user clicks more button")]
        public void GivenUserClicksMoreButton()
        {
            filtersPage.ClickMoreButton();
        }

        [Given(@"user select (.*) filter")]
        [When(@"user select (.*) filter")]
        public void WhenUserSelectFilterName(string filterName)
        {
            filtersPage.SelectEntities(filterName);
        }

        [Given(@"user provides for filter (.*) value (.*)")]
        [When(@"user provides for filter (.*) value (.*)")]
        public void WhenUserProvidesValueForFilter(string filterName, string value)
        {
            filtersPage.TypeValue(filterName, value);
        }

        [Given(@"the row count should be (.*)")]
        [Then(@"the row count should be (.*)")]
        public void ThenTheRowCountShouldBe(int expectedRows)
        {
            var actualCount = filtersPage.WaitUntilExpectedRowsCount(expectedRows);
            actualCount.Should().Be(expectedRows);
        }

        [Given(@"user clicks save filter button")]
        [When(@"user clicks save filter button")]
        public void WhenUserClicksSaveFilterButton()
        {
            filtersPage.ClickSaveButton();
        }

        [Given(@"add filter modal is displayed")]
        [When(@"add filter modal is displayed")]
        public void WhenAddFilterModalIsDisplayed()
        {
            addFilterModal.WaitUntilModalHeaderIsDisplayed();
        }

        [Given(@"filter name is set to (.*)")]
        [When(@"filter name is set to (.*)")]
        public void WhenFilterNameIsSetToPassed(string filterName)
        {
            addFilterModal.SetFilterName(filterName);
        }

        [Given(@"new filter (.*) is saved")]
        [Then(@"new filter (.*) is saved")]
        public void ThenNewFilterPassedIsSaved(string filterName)
        {
            filtersPage.WaitUntilFilterListItemIsDisplayed(filterName);
        }

        [When(@"user clics remove active filter button")]
        public void WhenUserClicsRemoveActiveFilterButton()
        {
            filtersPage.ClickRemoveActiveFilterButton();
        }

        [Then(@"no active filter is displayed")]
        public void ThenNoActiveFilterIsDisplayed()
        {
            filtersPage.WaitUntilFilterListIsNotDisplayed();
        }
    }
}
