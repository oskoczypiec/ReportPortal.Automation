using ReportPortal.Business.Pages;

namespace ReportPortal.BDD.Tests.StepDefinitions
{
    [Binding]
    public class MenuStepDefinitions
    {
        private IWebDriver _driver;
        private MenuPage menuPage => new MenuPage(_driver);

        public MenuStepDefinitions(IWebDriver driver)
        {
            this._driver = driver;
        }

        [Given(@"filters page is opened")]
        public void GivenFiltersPageIsOpened()
        {
            menuPage.ClickFiltersButton();
        }
    }
}
