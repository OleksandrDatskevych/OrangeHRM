using OpenQA.Selenium;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class LoginPage : BasePage
    {
        private MyWebElement _usernameTextBox = new(By.XPath("//*[@name='username']"));
        private MyWebElement _passwordTextBox = new(By.XPath("//*[@name='password']"));
        private MyWebElement _submitButton = new(By.XPath("//*[@type='submit']"));

        public bool PageInitState() => _usernameTextBox.IsDisplayed() && _passwordTextBox.IsDisplayed() && _submitButton.IsDisplayed();

        public void EnterUsername(string username) => _usernameTextBox.SendKeys(username);

        public void EnterPassword(string password) => _passwordTextBox.SendKeys(password);

        public void ClickSubmitButton() => _submitButton.Click();

        public void LogInWithCreds(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickSubmitButton();
        }
    }
}
