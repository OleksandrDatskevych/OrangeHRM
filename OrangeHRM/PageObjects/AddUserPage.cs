using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class AddUserPage : AdminPage
    {
        private MyWebElement _userRoleSelectDropbox = new(By
            .XPath("//label[text()='User Role']/following::*[contains(@class, 'select-wrapper')][1]"));
        private MyWebElement _statusSelectDropbox = new(By.XPath("//label[text()='Status']/following::*[contains(@class, 'select-wrapper')][1]"));
        private MyWebElement _employeeNameTextBox = new(By.XPath("//label[text()='Employee Name']/following::input[1]"));
        private MyWebElement _usernameTextBox = new(By.XPath("//label[text()='Username']/following::input[1]"));
        private MyWebElement _passwordTextBox = new(By.XPath("//label[text()='Password']/following::input[1]"));
        private MyWebElement _confirmPasswordTextBox = new(By.XPath("//label[text()='Confirm Password']/following::input[1]"));
        private MyWebElement _submitButton = new(By.XPath("//button[@type='submit']"));
        private MyWebElement _cancelButton = new(By.XPath("//button[text()=' Cancel ']"));

        private IReadOnlyList<IWebElement> UserRoleOptions => WebDriverFactory.Driver.FindElements(By
            .XPath("//label[text()='User Role']/following::*[@role='listbox']//span"));
        private IReadOnlyList<IWebElement> StatusOptions => WebDriverFactory.Driver.FindElements(By
            .XPath("//label[text()='Status']/following::*[@role='listbox']//span"));
        private IReadOnlyList<IWebElement> EmployeeListInAutoComplete => WebDriverFactory.Driver.FindElements(By
            .XPath("//label[text()='Employee Name']/following::*[@role='listbox']//span"));

        public string UserRoleBorderColor() => new MyWebElement(By
            .XPath("//*[text()='User Role']/following::*[contains(@class, 'oxd-select-text')][1]")).GetCssValue("border");

        public string StatusBorderColor() => new MyWebElement(By
            .XPath("//*[text()='Status']/following::*[contains(@class, 'oxd-select-text')][1]")).GetCssValue("border");

        public string EmployeeNameBorderColor() => new MyWebElement(By.XPath("(//*[contains(@class, 'oxd-autocomplete-text-input')])[1]"))
            .GetCssValue("border");

        public string UsernameBorderColor() => _usernameTextBox.GetCssValue("border");

        public string PasswordBorderColor() => _passwordTextBox.GetCssValue("border");

        public string ConfirmPasswordBorderColor() => _confirmPasswordTextBox.GetCssValue("border");

        public void SelectUserRole(string role)
        {
            _userRoleSelectDropbox.Click();
            var element = UserRoleOptions.First(i => i.Text == role);
            element.Click();
        }

        public void SelectStatus(string status)
        {
            _statusSelectDropbox.Click();
            var element = StatusOptions.First(i => i.Text == status);
            element.Click();
        }

        public void SelectEmployeeName(string name)
        {
            _employeeNameTextBox.SendKeysAfterClear(name);
            WebDriverFactory.Driver.GetWebDriverWait().Until(_ => EmployeeListInAutoComplete.Count > 0);
            var element = EmployeeListInAutoComplete.First(i => i.Text.Contains(name));
            element.Click();
        }

        public void EnterUsername(string username)
        {
            _usernameTextBox.SendKeysAfterClear(username);
            WebDriverFactory.Driver.GetWebDriverWait().Until(_ => UsernameBorderColor() != "1px solid rgb(235, 9, 16)");
        }

        public void EnterPassword(string password) => _passwordTextBox.SendKeysAfterClear(password);

        public void EnterConfirmPassword(string password) => _confirmPasswordTextBox.SendKeysAfterClear(password);

        public void ClickSubmitButton() => _submitButton.Click();

        public void ClickCancelButton() => _cancelButton.Click();
    }
}
