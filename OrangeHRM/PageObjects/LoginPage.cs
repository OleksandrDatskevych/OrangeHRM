using OpenQA.Selenium;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class LoginPage : BasePage
    {
        private readonly MyWebElement UsernameTextBox = new(By.XPath("//*[@name='username']"));
        private readonly MyWebElement PasswordTextBox = new(By.XPath("//*[@name='password']"));
        private readonly MyWebElement SubmitButton = new(By.XPath("//*[@type='submit']"));
        private readonly MyWebElement ErrorBoxMessage = new(By.XPath("//p[./ancestor::*[contains(@class, 'oxd-alert')]]"));

        private MyWebElement UsernameErrorMessage => new(By.XPath($"{UsernameTextBox.Selector}/following::span[1]"));
        private MyWebElement PasswordErrorMessage => new(By.XPath($"{PasswordTextBox.Selector}/following::span[1]"));

        public bool PageInitState() => UsernameTextBox.IsDisplayed() && PasswordTextBox.IsDisplayed() && SubmitButton.IsDisplayed();

        public void ClickSubmitButton() => SubmitButton.Click();

        public void LogInWithCredentials(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickSubmitButton();
        }

        public string GetErrorMessageText() => ErrorBoxMessage.Text;

        public string GetUsernameBorderColor() => UsernameTextBox.GetCssValue("border");

        public string GetPasswordBorderColor() => PasswordTextBox.GetCssValue("border");

        public string GetUsernameErrorMessage() => UsernameErrorMessage.Text;

        public string GetPasswordErrorMessage() => PasswordErrorMessage.Text;

        private void EnterUsername(string username) => UsernameTextBox.SendKeys(username);

        private void EnterPassword(string password) => PasswordTextBox.SendKeys(password);
    }
}
