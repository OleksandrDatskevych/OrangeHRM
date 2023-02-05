using OpenQA.Selenium;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class NationalitiesPage : AppPage
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
            var nationalitiesList = nationalities.OrderBy(e => e).ToList();
            // counter of found elements in all pages of table
            var i = 0;

            while (i < nationalitiesList.Count)
            {
                Driver.GetWebDriverWait().Until(_ => NationalitiesRows.Count > 0);
                // counter of tries to find elements on one page of table
                var j = 0;

                while (j < nationalitiesList.Count)
                {
                    var locator = By.XPath($"{GetRowByNationality(nationalitiesList[j]).Selector}/descendant::label").GetLocator();
                    var checkbox = Driver.GetWebDriverWait(10, TimeSpan.FromMilliseconds(200)).Until(drv =>
                        drv.FindElements(By.XPath($"{locator}")));

                    if (checkbox.Count != 0)
                    {
                        Driver.GetWebDriverWait(15, null, typeof(ElementClickInterceptedException)).Until(_ =>
                        {
                            // retry to click checkbox up to 3 times in case of StaleElementReferenceException
                            for (var t = 0; t < 3; t++)
                            {
                                try
                                {
                                    Actions.ScrollToElement(checkbox[0]).Click(checkbox[0]).Perform();
                                    break;
                                }
                                catch (StaleElementReferenceException)
                                {
                            
                                }
                            }

                            return true;
                        });
                        i++;
                    }

                    j++;
                }

                if (Driver.FindElements(By.XPath(DeleteSelectedButton.Selector)).Count != 0)
                {
                    DeleteSelectedButton.ScrollToClick();
                    ConfirmDeleteButton.Click();
                }

                if (i < nationalitiesList.Count)
                {
                    if (NextPageButton.IsDisplayed())
                    {
                        NextPageButton.Click();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public int GetNumberOfNationalities()
        {
            Driver.Navigate().Refresh();
            var result = int.Parse(NumberOfNationalities.Text.Split('(', ')')[1]);

            return result;
        }

        private MyWebElement GetRowByNationality(string nationality) => new(By
            .XPath($"{RowLocatorTemplate} and ./*[count( //*[@role='columnheader' and text()='Nationality']/preceding-sibling::*) +1]" +
                   $"/*[text()='{nationality}']]"));
    }
}
