using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
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
        private readonly MyWebElement NextPageButton = new(By.XPath("//ul[contains(@class, 'pagination')]/li[last()]/button"));

        private IReadOnlyList<IWebElement> NationalitiesRows => Driver.FindElements(By.XPath($"{RowLocatorTemplate}]"));
        private IReadOnlyList<IWebElement> PageButtons => Driver.FindElements(By.XPath("//ul[contains(@class, 'pagination')]" +
                                                                                       "/li[position()<last()]/button"));

        public bool PageInitState() => PageHeader.IsDisplayed() && NextPageButton.IsDisplayed() && NationalitiesRows.Count > 0;

        public void DeleteSingleNationality(string nationality)
        {
            IWebElement deleteButton;

            while (true)
            {
                try
                {
                    Driver.GetWebDriverWait().Until(_ => NationalitiesRows.Count > 0);
                    deleteButton = Driver.GetWebDriverWait(1, TimeSpan.FromMilliseconds(200)).Until(drv =>
                        drv.FindElement(By.XPath($"{GetRowByNationality(nationality).Selector}/descendant::i[contains(@class, 'bi-trash')]")));
                }
                catch (WebDriverTimeoutException)
                {
                    NextPageButton.Click();
                    continue;
                }

                break;
            }

            Driver.GetWebDriverWait(15, null, typeof(ElementClickInterceptedException)).Until(_ =>
            {
                WebDriverFactory.Actions.ScrollToElement(deleteButton).Click(deleteButton).Perform();

                return true;
            });
            ConfirmDeleteButton.Click();
        }

        public void DeleteMultipleNationalities(params string[] nationalities)
        {
            IWebElement checkbox;
            var i = 0;

            while (true)
            {
                try
                {
                    while (i < nationalities.Length)
                    {
                        Driver.GetWebDriverWait().Until(_ => NationalitiesRows.Count > 0);
                        checkbox = Driver.GetWebDriverWait(1, TimeSpan.FromMilliseconds(200)).Until(drv =>
                            drv.FindElement(By.XPath($"{GetRowByNationality(nationalities[i]).Selector}/descendant::label")));
                        Driver.GetWebDriverWait(15, null, typeof(ElementClickInterceptedException)).Until(_ =>
                        {
                            WebDriverFactory.Actions.ScrollToElement(checkbox).Click(checkbox).Perform();

                            return true;
                        });
                        i++;
                    }
                }
                catch (WebDriverTimeoutException)
                {
                    if (Driver.FindElements(By.XPath(DeleteSelectedButton.Selector)).Count > 0)
                    {
                        DeleteSelectedButton.Click();
                        ConfirmDeleteButton.Click();
                    }

                    if (i < nationalities.Length - 1)
                    {
                        NextPageButton.Click();
                        continue;
                    }
                }

                break;
            }
        }

        private MyWebElement GetRowByNationality(string nationality) => new(By
            .XPath($"{RowLocatorTemplate} and ./*[count( //*[@role='columnheader' and text()='Nationality']/preceding-sibling::*) +1]" +
                   $"/*[text()='{nationality}']]"));
    }
}
