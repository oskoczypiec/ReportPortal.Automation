// <copyright file="AddFilterModal.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Business.Pages.Modal
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using ReportPortal.Core.Config;

    public class AddFilterModal
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private int timeoutInSeconds = 10;

        public AddFilterModal(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        private IWebElement FilterNameInput => this.driver.GetElement(By.CssSelector("input[class*='input__input']"));

        private IWebElement AddFilterButton => this.driver.GetElement(By.XPath("//button[contains(@class, 'bigButton__big-button') and text()='Add']"));

        private IWebElement UpdateFilterButton => this.driver.GetElement(By.XPath("//button[contains(@class, 'bigButton__big-button') and text()='Update']"));

        private By ModalHeaderLocator => By.CssSelector("span[class*='modalHeader__modal-title']");

        public AddFilterModal SetFilterName(string name)
        {
            this.FilterNameInput.Clear();
            wait.Until(x => this.FilterNameInput.Text == string.Empty);
            this.FilterNameInput.SendKeys(name);
            return this;
        }

        public AddFilterModal ClickAddFilterButton()
        {
            this.AddFilterButton.Click();
            return this;
        }

        public AddFilterModal WaitUntilModalHeaderIsDisplayed()
        {
            this.driver.WaitUntilElementIsVisible(this.ModalHeaderLocator);
            return this;
        }

        public AddFilterModal ClickUpdateFilterButton()
        {
            this.UpdateFilterButton.Click();
            return this;
        }
    }
}
