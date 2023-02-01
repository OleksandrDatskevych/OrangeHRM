using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class AdminPage : AppPage
    {
        private readonly MyWebElement _addUserButton = new(By.XPath("//button[@type='button' and ./parent::*[contains(@class, 'header-container')]]"));
        private readonly MyWebElement _successMessage = new(By.XPath("//*[contains(@class, 'oxd-toast-container')]/descendant::p[1]"));
        private readonly MyWebElement _deleteSelectedButton = new(By.XPath("//button[@type='button' and ./*[contains(@class, 'bi-trash-fill')]]"));
        private readonly MyWebElement _confirmDeleteButton = new(By.XPath("//*[contains(@class, 'orangehrm-modal-footer')]" + 
                                                                          "/button[./*[contains(@class, 'bi-trash')]]"));

        private readonly MyWebElement _userRoleDropbox = new(By.XPath("//label[text()='User Role']/following::*" +
                                                                      "[contains(@class, 'oxd-select-text-input')][1]"));

        private IReadOnlyList<IWebElement> UserRolesOptions => Driver.FindElements(
            By.XPath($"{_userRoleDropbox.Selector.GetLocator()}/following::*[@role='listbox']//span"));
        private IReadOnlyList<IWebElement> UserRoleColumnElements => Driver.FindElements(
            By.XPath("//*[@role='row']//*[@role='cell'][count(//*[@role='columnheader' and text()='User Role']/preceding-sibling::*) + 1]/*"));

        public bool PageInitState() => _addUserButton.IsDisplayed();

        public void ClickAddUser() => _addUserButton.Click();

        public string GetSuccessMessage()
        {
            WebDriverFactory.Driver.GetWebDriverWait().Until(_ => IsSuccessMessageDisplayed());
            var message = _successMessage.Text;

            return message;
        }

        public void DeleteUserInTable(string username)
        {
            try
            {
                var deleteButton = new MyWebElement(By.XPath("//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')] and " +
                    $"./*[count(//*[@role='columnheader' and text()='Username']/preceding-sibling::*) + 1]/*[text()='{username}'] ]/*" +
                    "/descendant::button/i[contains(@class, 'bi-trash')]"));
                deleteButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException("User not found");
            }

            _confirmDeleteButton.Click();
        }

        public void DeleteUserInTable(int row)
        {
            try
            {
                var deleteButton = new MyWebElement(By.XPath($"(//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')]])[{row}]" +
                    "/descendant::button/i[contains(@class, 'bi-trash')]"));
                deleteButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException("User not found");
            }

            _confirmDeleteButton.Click();
        }

        public bool IsUserInTable(string columnName, string value)
        {
            var userRow = WebDriverFactory.Driver.FindElements(By.XPath("//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')]" +
                $" and ./*[count(//*[@role='columnheader' and text()='{columnName}']/preceding-sibling::*) + 1]/*[text()='{value}'] ]/*" +
                "/descendant::button/i[contains(@class, 'bi-trash')]"));

            return userRow.Count != 0;
        }

        public void DeleteMultipleUsers(params string[] users)
        {
            foreach (var user in users)
            {
                var checkboxByUsername = new MyWebElement(By.XPath("//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')] and " +
                    $"./*[count(//*[@role='columnheader' and text()='Username']/preceding-sibling::*) + 1]/*[text()='{user}'] ]/*" +
                    "/descendant::label"));
                checkboxByUsername.Click();
            }

            _deleteSelectedButton.Click();
            _confirmDeleteButton.Click();
        }

        public void EditUserInTable(string username)
        {
            try
            {
                var editButton = new MyWebElement(By.XPath($"//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')] and " +
                    $"./*[count(//*[@role='columnheader' and text()='Username']/preceding-sibling::*) + 1]/*[text()='{username}'] ]/*" +
                    $"/descendant::button/i[contains(@class, 'bi-pencil-fill')]"));
                editButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException("User not found");
            }
        }

        public void SearchUsersByUserRole(string userRole)
        {
            _userRoleDropbox.Click();
            var option = UserRolesOptions.First(i => i.Text == userRole);
            option.Click();
        }

        public bool AreUsersFilteredByUserRole(string userRole) => UserRoleColumnElements.All(i => i.Text == userRole);

        private bool IsSuccessMessageDisplayed() => _successMessage.IsDisplayed();
    }
}
