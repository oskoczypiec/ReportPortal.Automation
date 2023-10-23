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

        private IEnumerable<IWebElement> DeleteFilterButtons => this.driver.GetElements(this.DeleteFilterButtonsLocator);

        private IWebElement DeleteFilterModalButton => this.driver.GetElement(By.XPath("//button[contains(@class, 'bigButton__big-button') and text()='Delete']"));

        private By DeleteFilterButtonsLocator => By.CssSelector("div[class*='deleteFilterButton__bin-icon']");

        public MenuPage ClickFiltersButton()
        {
            this.FiltersSideBarButton.Click();
            return this;
        }

        public void RemoveAllFilters()
        {
            var elements = this.driver.GetElements(this.DeleteFilterButtonsLocator);
            if (elements.Count > 0)
            {
                foreach (var element in elements)
                {
                    element.Click();
                    this.DeleteFilterModalButton.Click();
                }
            }
        }
    }
}
