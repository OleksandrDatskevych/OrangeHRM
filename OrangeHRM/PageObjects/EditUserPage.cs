using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class EditUserPage : AddUserPage
    {
        private readonly MyWebElement _changePasswordCheckbox = new(By.XPath("//i[contains(@class, 'oxd-icon bi-check')]"));

        public void ClickChangePasswordCheckbox() => _changePasswordCheckbox.Click();
    }
}
