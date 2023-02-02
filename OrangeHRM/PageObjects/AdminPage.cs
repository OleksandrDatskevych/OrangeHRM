using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class AdminPage : AppPage
    {
        private readonly MyWebElement AddUserButton = new(By.XPath("//button[./parent::*[contains(@class, 'header-container')]]"));
        private readonly MyWebElement ResultMessage = new(By.XPath("//*[contains(@class, 'oxd-toast-container')]/descendant::p[1]"));
        private readonly MyWebElement DeleteSelectedButton = new(By.XPath("//button[./*[contains(@class, 'bi-trash-fill')]]"));
        private readonly MyWebElement ConfirmDeleteButton = new(By
            .XPath("//*[contains(@class, 'orangehrm-modal-footer')]/button[./*[contains(@class, 'bi-trash')]]"));
        private readonly MyWebElement UserRoleDropbox = new(By
            .XPath("//label[text()='User Role']/following::*[contains(@class, 'oxd-select-text-input')][1]"));

        private readonly MyWebElement SearchButton = new(By.XPath("//button[@type='submit']"));

        private readonly MyWebElement NationalitiesButton = new(By
            .XPath("//li[contains(@class, 'oxd-topbar-body-nav-tab') and ./a[text()='Nationalities']]"));

        private IReadOnlyList<IWebElement> UserRolesOptions => Driver.FindElements(By
            .XPath($"{UserRoleDropbox.Selector}/following::*[@role='listbox']//span"));
        private IReadOnlyList<IWebElement> UserRoleColumnElements => Driver.FindElements(By
            .XPath("//*[@role='row']//*[@role='cell'][count(//*[@role='columnheader' and text()='User Role']/preceding-sibling::*) + 1]/*"));
        private IReadOnlyList<IWebElement> RowsOfUsers => Driver.FindElements(By
            .XPath("//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')]]"));

        public bool PageInitState() => AddUserButton.IsDisplayed();

        public void ClickAddUser() => AddUserButton.Click();

        public void ClickNationalitiesButton() => NationalitiesButton.Click();

        public string GetSuccessMessage()
        {
            Driver.GetWebDriverWait().Until(_ => IsSuccessMessageDisplayed());
            var message = ResultMessage.Text;

            return message;
        }

        public void DeleteUserInTable(string username)
        {
            try
            {
                var deleteButton = new MyWebElement(By
                    .XPath("//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')] and " +
                           $"./*[count(//*[@role='columnheader' and text()='Username']/preceding-sibling::*) + 1]/*[text()='{username}'] ]" +
                           "/*/descendant::button/i[contains(@class, 'bi-trash')]"));
                deleteButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"User {username} not found");
            }

            ConfirmDeleteButton.Click();
        }

        public void DeleteUserInTable(int row)
        {
            try
            {
                var deleteButton = new MyWebElement(By
                    .XPath($"(//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')]])[{row}]" +
                           "/descendant::button/i[contains(@class, 'bi-trash')]"));
                deleteButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Row {row} not found");
            }

            ConfirmDeleteButton.Click();
        }

        public bool IsUserInTable(string columnName, string value)
        {
            var userRow = WebDriverFactory.Driver.FindElements(By
                .XPath("//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')] and " +
                       $"./*[count(//*[@role='columnheader' and text()='{columnName}']/preceding-sibling::*) + 1]/*[text()='{value}'] ]" +
                       "/*/descendant::button/i[contains(@class, 'bi-trash')]"));

            return userRow.Count != 0;
        }

        public void DeleteMultipleUsers(params string[] users)
        {
            foreach (var user in users)
            {
                try
                {
                    var checkboxByUsername = new MyWebElement(By
                        .XPath("//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')] and " +
                               $"./*[count(//*[@role='columnheader' and text()='Username']/preceding-sibling::*) + 1]/*[text()='{user}'] ]" +
                               "/*/descendant::label"));
                    checkboxByUsername.Click();
                }
                catch (WebDriverTimeoutException)
                {
                    throw new WebDriverTimeoutException($"User {user} not found");
                }
            }

            DeleteSelectedButton.Click();
            ConfirmDeleteButton.Click();
        }

        public void EditUserInTable(string username)
        {
            try
            {
                var editButton = new MyWebElement(By.XPath("//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')] and " +
                    $"./*[count(//*[@role='columnheader' and text()='Username']/preceding-sibling::*) + 1]/*[text()='{username}'] ]/*" +
                    "/descendant::button/i[contains(@class, 'bi-pencil-fill')]"));
                editButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"User {username} not found");
            }
        }

        public void SearchUsersByUserRole(string userRole)
        {
            UserRoleDropbox.Click();
            var option = UserRolesOptions.First(i => i.Text == userRole);
            option.Click();
            SearchButton.Click();
        }

        public bool AreUsersFilteredByUserRole(string userRole) => UserRoleColumnElements.All(i => i.Text == userRole);

        public string GetUsernameByRow(int row)
        {
            var usernameCell = new MyWebElement(By
                .XPath("((//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')]])[2]//*[@role='cell'])" +
                       "[count(//*[@role='columnheader' and text()='Username']/preceding-sibling::*) + 1]/div"));
            var username = usernameCell.Text;

            return username;
        }

        public int GetNumberOfRows() => RowsOfUsers.Count;

        private bool IsSuccessMessageDisplayed() => ResultMessage.IsDisplayed();
    }
}
