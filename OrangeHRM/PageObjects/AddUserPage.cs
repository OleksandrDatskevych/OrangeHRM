using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class AddUserPage : AdminPage
    {
        private readonly MyWebElement UserRoleSelectDropbox = new(By.XPath("//label[text()='User Role']" +
                                                                            "/following::*[contains(@class, 'select-wrapper')][1]"));
        private readonly MyWebElement StatusSelectDropbox = new(By.XPath("//label[text()='Status']/following::*[contains(@class, 'select-wrapper')][1]"));
        private readonly MyWebElement EmployeeNameTextBox = new(By.XPath("//label[text()='Employee Name']/following::input[1]"));
        private readonly MyWebElement UsernameTextBox = new(By.XPath("//label[text()='Username']/following::input[1]"));
        private readonly MyWebElement PasswordTextBox = new(By.XPath("//label[text()='Password']/following::input[1]"));
        private readonly MyWebElement ConfirmPasswordTextBox = new(By.XPath("//label[text()='Confirm Password']/following::input[1]"));
        private readonly MyWebElement SubmitButton = new(By.XPath("//button[@type='submit']"));
        private readonly MyWebElement UserRoleError = new(By.XPath("//*[text()='User Role']/following::span[1]"));
        private readonly MyWebElement StatusSelectError = new(By.XPath("//*[text()='Status']/following::span[1]"));
        private readonly MyWebElement EmployeeNameError = new(By.XPath("//*[text()='Employee Name']/following::span[1]"));
        private readonly MyWebElement UsernameError = new(By.XPath("//*[text()='Username']/following::span[1]"));
        private readonly MyWebElement PasswordError = new(By.XPath("//*[text()='Password']/following::span[1]"));
        private readonly MyWebElement ConfirmPasswordError = new(By.XPath("//*[text()='Confirm Password']/following::span[1]"));

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

        public string UsernameBorderColor() => UsernameTextBox.GetCssValue("border");

        public string PasswordBorderColor() => PasswordTextBox.GetCssValue("border");

        public string ConfirmPasswordBorderColor() => ConfirmPasswordTextBox.GetCssValue("border");

        public void SelectUserRole(string role)
        {
            try
            {
                UserRoleSelectDropbox.Click();
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
                StatusSelectDropbox.Click();
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
                EmployeeNameTextBox.SendKeysAfterCtrlABackspace(name);
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
                UsernameTextBox.SendKeysAfterCtrlABackspace(username);
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
                PasswordTextBox.SendKeysAfterClear(password);
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
                ConfirmPasswordTextBox.SendKeysAfterClear(password);
                WebDriverFactory.Driver.GetWebDriverWait(5).Until(_ => ConfirmPasswordBorderColor() != "1px solid rgb(235, 9, 16)");
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException("Confirm password error: " + GetConfirmPasswordError());
            }
        }

        public void ClickSubmitButton() => SubmitButton.Click();

        private string GetUserRoleError() => UserRoleError.Text;

        private string GetStatusError() => StatusSelectError.Text;

        private string GetEmployeeNameError() => EmployeeNameError.Text;

        private string GetUsernameError() => UsernameError.Text;

        private string GetPasswordError() => PasswordError.Text;

        private string GetConfirmPasswordError() => ConfirmPasswordError.Text;
    }
}
