// <copyright file="LoginPage.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Business.Pages
{
    using OpenQA.Selenium;
    using ReportPortal.Core.Config;

    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement Logo => this.driver.GetElement(By.ClassName("loginPage__logo"));

        private IWebElement UserNameInput => this.driver.GetElement(By.Name("login"));

        private IWebElement PasswordInput => this.driver.GetElement(By.Name("password"));

        private IWebElement LoginButton => this.driver.GetElement(By.XPath("//*[contains(@class, 'bigButton')]"));

        public bool IsLogoVisible()
        {
            return this.Logo.Displayed;
        }

        public void SetUserName(string userName)
        {
            this.UserNameInput.Click();
            this.UserNameInput.SendKeys(userName);
        }

        public void SetPassword(string password)
        {
            this.PasswordInput.Click();
            this.PasswordInput.SendKeys(password);
        }

        public void ClickLoginButton()
        {
            this.LoginButton.Click();
        }

        public void PressTabOnUserNameInput()
        {
            this.UserNameInput.SendKeys(Keys.Tab);
        }

        public void LogInDefaultUser()
        {
            string? userName = ApplicationConfiguration.configuration?["User"];
            string? password = ApplicationConfiguration.configuration?["Pass"];

            this.SetUserName(userName!);
            this.SetPassword(password!);
            this.ClickLoginButton();
        }
    }
}
