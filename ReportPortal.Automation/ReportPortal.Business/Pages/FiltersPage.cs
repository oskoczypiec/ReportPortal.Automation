// <copyright file="FiltersPage.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Business.Pages
{
    using OpenQA.Selenium;
    using ReportPortal.Core.Config;

    public class FiltersPage
    {
        private IWebDriver driver;
        private string entityName = string.Empty;
        private string filterName = string.Empty;

        public FiltersPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement AddFilterButton => this.driver.GetElement(By.XPath("//span[contains(@class, 'ghostButton__text') and text()='Add Filter']"));

        private IWebElement MoreButton => this.driver.GetElement(By.XPath("//div[contains(@class, 'entitiesSelector__toggler') and text()='More']"));

        private IWebElement DropdownEntity => this.driver.GetElement(By.XPath($"//span[contains(@class, 'inputCheckbox__children-container') and text()='{this.entityName}']"));

        private IWebElement InputConditional => this.driver.GetElement(By.XPath($"//span[contains(@class, 'fieldFilterEntity__entity-name') and text()='{this.entityName}']/..//input"));

        private IWebElement SaveButton => this.driver.GetElement(By.XPath($"//span[contains(@class, 'ghostButton__text') and text()='Save']"));

        private IWebElement EditButton => this.driver.GetElement(By.XPath($"//span[contains(@class, 'ghostButton__text') and text()='Edit']"));

        private IWebElement DeleteActiveFilterButton => this.driver.GetElement(By.CssSelector("div[class*='filterItem__icon'] > svg"));

        private IWebElement DropdownButton => this.driver.GetElement(By.CssSelector("i[class*='inputConditional__arrow']"));

        private IWebElement DropdownConditionComponent => this.driver.GetElement(By.CssSelector("div[class*='inputConditional__visible']"));

        private IWebElement CloneFilterButton => this.driver.GetElement(By.XPath("//span[contains(@class, 'ghostButton__text') and text()='Clone']"));

        private IWebElement DisplayOnLaunchesSwitch => this.driver.GetElement(By.XPath($"//span[contains(@class, 'filterName__name') and text()='{filterName}']/ancestor::div[contains(@class, 'gridRow__grid-row-wrapper')]//input[contains(@class, 'inputSwitcher__input')]"));

        private By ActiveFiltersLocator => By.XPath("//span[contains(@class, 'filterItem__name')]");

        private By FilterListLocator => By.XPath($"//span[contains(@class, 'filterItem__name') and text()='{this.filterName}']");

        private By GridRowsLocator => By.CssSelector("div[class*='gridRow__grid-row-wrapper']");

        public FiltersPage ClickAddFilterButton()
        {
            this.AddFilterButton.Click();
            return this;
        }

        public FiltersPage ClickMoreButton()
        {
            this.MoreButton.Click();
            return this;
        }

        public FiltersPage TypeValue(string entityName, string value)
        {
            this.entityName = entityName;
            this.InputConditional.Click();
            this.InputConditional.SendKeys(value);
            this.InputConditional.SendKeys(Keys.Enter);
            return this;
        }

        public FiltersPage SelectEntities(string entityName)
        {
            this.entityName = entityName;
            this.DropdownEntity.Click();
            return this;
        }

        public void WaitUntilExpectedRowsCount(int expectedCount)
        {
            this.driver.WaitUntilElementCountIs(this.GridRowsLocator, expectedCount);
        }

        public FiltersPage ClickSaveButton()
        {
            this.SaveButton.Click();
            Thread.Sleep(1000);
            return this;
        }

        public FiltersPage ClickRemoveActiveFilterButton()
        {
            this.DeleteActiveFilterButton.Click();
            return this;
        }

        public FiltersPage WaitUntilFilterListItemIsDisplayed(string filterName)
        {
            this.filterName = filterName;
            this.driver.WaitUntilElementIsVisible(this.FilterListLocator);
            return this;
        }

        public void WaitUntilFilterListIsNotDisplayed()
        {
            this.driver.WaitUntilElementIsNotVisible(this.ActiveFiltersLocator);
        }

        public void WaitUntilFilterListItemIsNotDisplayed(string filterName)
        {
            this.filterName = filterName;
            this.driver.WaitUntilElementIsNotVisible(this.FilterListLocator);
        }

        public FiltersPage ClickDropdownButton()
        {
            this.DropdownButton.Click();
            return this;
        }

        public IWebElement GetDropdownComponent()
        {
            return this.DropdownConditionComponent;
        }

        public IWebElement GetDropdownButton()
        {
            return this.DropdownButton;
        }

        public IWebElement GetNthResultRow(int v)
        {
            return this.driver.GetElements(this.GridRowsLocator)[v];
        }

        public FiltersPage ClickEditButton()
        {
            this.EditButton.Click();
            return this;
        }

        public FiltersPage ClickCloneFilterButton()
        {
            this.CloneFilterButton.Click();
            return this;
        }

        public IWebElement GetDisplayOnLaunchesSwitch()
        {
            return this.DisplayOnLaunchesSwitch;
        }

        public void SetDisplayLaunches(bool isDisplayed, string filterName)
        {
            this.filterName = filterName;
            bool isCheckboxChecked = this.GetDisplayOnLaunchesSwitch().Selected;

            if (isDisplayed != isCheckboxChecked)
            {
                WebDriverExtensions.JavaScriptClick(this.driver, this.DisplayOnLaunchesSwitch);
            }
        }
    }
}
