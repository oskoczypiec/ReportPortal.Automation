using OpenQA.Selenium;
using ReportPortal.Business.Pages;

namespace ReportPortal.BDD.Tests.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        private IWebDriver _driver;
        private LoginPage loginPage => new LoginPage(_driver);

        public LoginStepDefinitions(IWebDriver driver)
        {
            this._driver = driver;
        }

        [Given(@"default user is logged in")]
        public void GivenDefaultUserIsLoggedIn()
        {
            loginPage.LogInDefaultUser();
        }
    }
}
