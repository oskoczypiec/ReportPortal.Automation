// <copyright file="MenuPage.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Business.Pages
{
    using OpenQA.Selenium;
    using ReportPortal.Core.Config;

    public class MenuPage
    {
        private IWebDriver driver;

        public MenuPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement FiltersSideBarButton => this.driver.GetElement(By.CssSelector("a[href*='filters']"));

        public MenuPage ClickFiltersButton()
        {
            this.FiltersSideBarButton.Click();
            return this;
        }
    }
}
