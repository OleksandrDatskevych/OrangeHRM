using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class AdminPage : AppPage
    {
        private MyWebElement _addUserButton = new(By.XPath("//button[@type='button' and ./parent::*[contains(@class, 'header-container')]]"));
        private MyWebElement _successMessage = new(By.XPath("//*[contains(@class, 'oxd-toast-container')]/descendant::p[1]"));

        public bool PageInitState() => _addUserButton.IsDisplayed();

        public void ClickAddUser() => _addUserButton.Click();

        public bool IsSuccessMessageDisplayed() => _successMessage.IsDisplayed();

        public string GetSuccessMessage()
        {
            WebDriverFactory.Driver.GetWebDriverWait().Until(_ => IsSuccessMessageDisplayed());
            var message = _successMessage.Text;

            return message;
        }
    }
}
