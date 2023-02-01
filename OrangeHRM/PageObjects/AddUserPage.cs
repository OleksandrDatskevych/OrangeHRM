using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class AddUserPage : AdminPage
    {
        private readonly MyWebElement _userRoleSelectDropbox = new(By.XPath("//label[text()='User Role']" +
                                                                            "/following::*[contains(@class, 'select-wrapper')][1]"));
        private readonly MyWebElement _statusSelectDropbox = new(By.XPath("//label[text()='Status']/following::*[contains(@class, 'select-wrapper')][1]"));
        private readonly MyWebElement _employeeNameTextBox = new(By.XPath("//label[text()='Employee Name']/following::input[1]"));
        private readonly MyWebElement _usernameTextBox = new(By.XPath("//label[text()='Username']/following::input[1]"));
        private readonly MyWebElement _passwordTextBox = new(By.XPath("//label[text()='Password']/following::input[1]"));
        private readonly MyWebElement _confirmPasswordTextBox = new(By.XPath("//label[text()='Confirm Password']/following::input[1]"));
        private readonly MyWebElement _submitButton = new(By.XPath("//button[@type='submit']"));
        private readonly MyWebElement _cancelButton = new(By.XPath("//button[text()=' Cancel ']"));
        private readonly MyWebElement _userRoleError = new(By.XPath("//*[text()='User Role']/following::span[1]"));
        private readonly MyWebElement _statusSelectError = new(By.XPath("//*[text()='Status']/following::span[1]"));
        private readonly MyWebElement _employeeNameError = new(By.XPath("//*[text()='Employee Name']/following::span[1]"));
        private readonly MyWebElement _usernameError = new(By.XPath("//*[text()='Username']/following::span[1]"));
        private readonly MyWebElement _passwordError = new(By.XPath("//*[text()='Password']/following::span[1]"));
        private readonly MyWebElement _confirmPasswordError = new(By.XPath("//*[text()='Confirm Password']/following::span[1]"));

        private IReadOnlyList<IWebElement> UserRoleOptions => WebDriverFactory.Driver.FindElements(By
            .XPath("//label[text()='User Role']/following::*[@role='listbox']//span"));
        private IReadOnlyList<IWebElement> StatusOptions => WebDriverFactory.Driver.FindElements(By
            .XPath("//label[text()='Status']/following::*[@role='listbox']//span"));
        private IReadOnlyList<IWebElement> EmployeeListInAutoComplete => WebDriverFactory.Driver.FindElements(By
            .XPath("//label[text()='Employee Name']/following::*[@role='listbox']//span"));

        public string UserRoleBorderColor() => new MyWebElement(By.XPath("//*[text()='User Role']/following::*[contains(@class, 'oxd-select-text')][1]"))
            .GetCssValue("border");

        public string StatusBorderColor() => new MyWebElement(By.XPath("//*[text()='Status']/following::*[contains(@class, 'oxd-select-text')][1]"))
            .GetCssValue("border");

        public string EmployeeNameBorderColor() => new MyWebElement(By.XPath("(//*[contains(@class, 'oxd-autocomplete-text-input')])[1]"))
            .GetCssValue("border");

        public string UsernameBorderColor() => _usernameTextBox.GetCssValue("border");

        public string PasswordBorderColor() => _passwordTextBox.GetCssValue("border");

        public string ConfirmPasswordBorderColor() => _confirmPasswordTextBox.GetCssValue("border");

        public void SelectUserRole(string role)
        {
            try
            {
                _userRoleSelectDropbox.Click();
                var element = UserRoleOptions.First(i => i.Text == role);
                element.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException("User role error: " + GetUserRoleError());
            }
        }

        public void SelectStatus(string status)
        {
            try
            {
                _statusSelectDropbox.Click();
                var element = StatusOptions.First(i => i.Text == status);
                element.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException("Status select error: " + GetStatusError());
            }
        }

        public void SelectEmployeeName(string name)
        {
            try
            {
                _employeeNameTextBox.SendKeysAfterCtrlABackspace(name);
                WebDriverFactory.Driver.GetWebDriverWait().Until(_ => EmployeeListInAutoComplete.Count > 0);
                var element = EmployeeListInAutoComplete.First(i => i.Text.Contains(name));
                element.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException("Employee name error: " + GetEmployeeNameError());
            }
        }

        public void EnterUsername(string username)
        {
            try
            {
                _usernameTextBox.SendKeysAfterCtrlABackspace(username);
                WebDriverFactory.Driver.GetWebDriverWait(5).Until(_ => UsernameBorderColor() != "1px solid rgb(235, 9, 16)");
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException("Username error: " + GetUsernameError());
            }
        }

        public void EnterPassword(string password)
        {
            try
            {
                _passwordTextBox.SendKeysAfterClear(password);
                WebDriverFactory.Driver.GetWebDriverWait(5).Until(_ => PasswordBorderColor() != "1px solid rgb(235, 9, 16)");
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException("Password error: " + GetPasswordError());
            }
        }

        public void EnterConfirmPassword(string password)
        {
            try
            {
                _confirmPasswordTextBox.SendKeysAfterClear(password);
                WebDriverFactory.Driver.GetWebDriverWait(5).Until(_ => ConfirmPasswordBorderColor() != "1px solid rgb(235, 9, 16)");
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException("Confirm password error: " + GetConfirmPasswordError());
            }
        }

        public void ClickSubmitButton() => _submitButton.Click();

        public void ClickCancelButton() => _cancelButton.Click();

        private string GetUserRoleError() => _userRoleError.Text;

        private string GetStatusError() => _statusSelectError.Text;

        private string GetEmployeeNameError() => _employeeNameError.Text;

        private string GetUsernameError() => _usernameError.Text;

        private string GetPasswordError() => _passwordError.Text;

        private string GetConfirmPasswordError() => _confirmPasswordError.Text;
    }
}
