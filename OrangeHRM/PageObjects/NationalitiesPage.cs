﻿using OpenQA.Selenium;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class NationalitiesPage : BasePage
    {
        private const string RowLocatorTemplate = "//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')]";
        private readonly MyWebElement DeleteSelectedButton = new(By.XPath("//button/i[contains(@class, 'bi-trash-fill')]"));
        private readonly MyWebElement ConfirmDeleteButton = new(By.XPath("//*[contains(@class, 'orangehrm-modal-footer')]" +
                                                                          "/button[./*[contains(@class, 'bi-trash')]]"));
        private readonly MyWebElement PageHeader = new(By.XPath("//*[contains(@class, 'orangehrm-main-title') and text()='Nationalities']"));
        private readonly MyWebElement NextPageButton = new(By.XPath("//ul[contains(@class, 'pagination')]//button[./*[contains(@class, 'bi-chevron-right')]]"));
        private readonly MyWebElement NumberOfNationalities = new(By.XPath("//*[text()[contains(.,'Found')]]"));

        private IReadOnlyList<IWebElement> NationalitiesRows => Driver.FindElements(By.XPath($"{RowLocatorTemplate}]"));
        private IReadOnlyList<IWebElement> PageButtons => Driver.FindElements(By.XPath("//ul[contains(@class, 'pagination')]" +
                                                                                       "/li[position()<last()]/button"));

        public bool PageInitState() => PageHeader.IsDisplayed() && NextPageButton.IsDisplayed() && NationalitiesRows.Count > 0;

        public void DeleteSingleNationality(string nationality)
        {
            IReadOnlyList<IWebElement> deleteButton;

            while (true)
            {
                Driver.GetWebDriverWait().Until(_ => NationalitiesRows.Count > 0);
                deleteButton = Driver.GetWebDriverWait(1, TimeSpan.FromMilliseconds(200)).Until(drv =>
                        drv.FindElements(By.XPath($"{GetRowByNationality(nationality).Selector}/descendant::i[contains(@class, 'bi-trash')]")));

                if (deleteButton.Count == 0)
                {
                    NextPageButton.Click();
                    continue;
                }

                break;
            }

            Driver.GetWebDriverWait(15, null, typeof(ElementClickInterceptedException)).Until(_ =>
            {
                Actions.ScrollToElement(deleteButton[0]).Click(deleteButton[0]).Perform();

                return true;
            });
            ConfirmDeleteButton.Click();
        }

        public void DeleteMultipleNationalities(string[] nationalities)
        {
            var natsList = nationalities.ToList();
            natsList.Sort();
            var i = 0;

            while (i < natsList.Count)
            {
                Driver.GetWebDriverWait().Until(_ => NationalitiesRows.Count > 0);
                var j = 0;

                while (j < natsList.Count)
                {
                    var checkbox = Driver.GetWebDriverWait(10, TimeSpan.FromMilliseconds(200)).Until(drv =>
                        drv.FindElements(By.XPath($"{GetRowByNationality(natsList[j]).Selector}/descendant::label")));

                    if (checkbox.Count != 0)
                    {
                        Driver.GetWebDriverWait(15, null, typeof(ElementClickInterceptedException)).Until(_ =>
                        {
                            Actions.ScrollToElement(checkbox[0]).Click(checkbox[0]).Perform();

                            return true;
                        });
                        i++;
                    }

                    j++;
                }

                if (Driver.FindElements(By.XPath(DeleteSelectedButton.Selector)).Count != 0)
                {
                    DeleteSelectedButton.Click();
                    ConfirmDeleteButton.Click();
                }

                if (i < natsList.Count)
                {
                    NextPageButton.Click();
                }
            }
        }

        public int GetNumberOfNationalities() => int.Parse(NumberOfNationalities.Text.Split('(', ')')[1]);

        private MyWebElement GetRowByNationality(string nationality) => new(By
            .XPath($"{RowLocatorTemplate} and ./*[count( //*[@role='columnheader' and text()='Nationality']/preceding-sibling::*) +1]" +
                   $"/*[text()='{nationality}']]"));
    }
}
