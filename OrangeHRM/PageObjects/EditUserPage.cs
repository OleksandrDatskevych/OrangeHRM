using OpenQA.Selenium;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class EditUserPage : AddUserPage
    {
        private readonly MyWebElement ChangePasswordCheckbox = new(By.XPath("//i[contains(@class, 'oxd-icon bi-check')]"));

        public void ClickChangePasswordCheckbox() => ChangePasswordCheckbox.Click();
    }
}
