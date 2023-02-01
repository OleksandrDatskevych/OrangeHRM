using NUnit.Framework;
using OrangeHRM.PageObjects;

namespace OrangeHRM.Tests
{
    [TestFixture]
    public class AdminTests : BaseTest
    {
        [OneTimeSetUp]
        public void Login()
        {
            var loginPage = new LoginPage();
            loginPage.LogInWithCredentials("Admin", "admin123");
            Assert.True(new DashboardPage().IsDashboardDisplayed());
        }

        [SetUp]
        public void NavToAdminPage()
        {
            var appPage = new AppPage();
            appPage.ClickSidebarItem("Admin");
            Assert.True(new AdminPage().PageInitState());
        }

        [TearDown]
        public void NavBackToAdminPage()
        {
            var appPage = new AppPage();
            appPage.ClickSidebarItem("Admin");
            Assert.True(new AdminPage().PageInitState());
        }

        [Test]
        public void AddUser()
        {
            var adminPage = new AdminPage();
            var addUserPage = new AddUserPage();
            var redColorBorder = "1px solid rgb(235, 9, 16)";
            var userRole = "ESS";
            var status = "Enabled";
            var employeeName = "Peter Mac";
            var username = "Abdool";
            var password = "12345Aa+";
            var confirmPassword = "12345Aa+";
            adminPage.ClickAddUser();
            addUserPage.ClickSubmitButton();
            Assert.AreEqual(redColorBorder, addUserPage.UserRoleBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.StatusBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.EmployeeNameBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.UsernameBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.PasswordBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.ConfirmPasswordBorderColor());
            addUserPage.SelectUserRole(userRole);
            addUserPage.SelectStatus(status);
            addUserPage.SelectEmployeeName(employeeName);
            addUserPage.EnterUsername(username);
            addUserPage.EnterPassword(password);
            addUserPage.EnterConfirmPassword(confirmPassword);
            addUserPage.ClickSubmitButton();
            Assert.AreEqual("Success", addUserPage.GetSuccessMessage());
        }

        [Test]
        public void RemoveUserByUsername()
        {
            var adminPage = new AdminPage();
            var userToDelete = "Goutam.Ganesh";
            var columnName = "Username";
            adminPage.DeleteUserInTable(userToDelete);
            Assert.False(adminPage.IsUserInTable(columnName, userToDelete));
        }

        [Test]
        public void RemoveUserByRowNumber()
        {
            var adminPage = new AdminPage();
            var rowNumber = 4;
            var columnName = "Username";
            var username = "Cecil.Bonaparte";
            adminPage.DeleteUserInTable(rowNumber);
            Assert.False(adminPage.IsUserInTable(columnName, username));
        }

        [Test]
        public void RemoveMultipleUsers()
        {
            var adminPage = new AdminPage();
            var users = new[] { "Alice.Duval", "Chenzira.Chuki" };
            var columnName = "Username";
            adminPage.DeleteMultipleUsers(users);
            Assert.False(adminPage.IsUserInTable(columnName, users[0]));
            Assert.False(adminPage.IsUserInTable(columnName, users[1]));
        }

        [Test]
        public void EditUser()
        {
            var adminPage = new AdminPage();
            var editUserPage = new EditUserPage();
            var userToEdit = "Lisa.Andrews";
            var userRole = "ESS";
            var status = "Enabled";
            var employeeName = "Odis";
            var username = "eetgnzz1";
            var password = "09875oK=";
            var confirmPassword = "09875oK=";
            adminPage.EditUserInTable(userToEdit);
            editUserPage.SelectUserRole(userRole);
            editUserPage.SelectStatus(status);
            editUserPage.SelectEmployeeName(employeeName);
            editUserPage.EnterUsername(username);
            editUserPage.ClickChangePasswordCheckbox();
            editUserPage.EnterPassword(password);
            editUserPage.EnterConfirmPassword(confirmPassword);
            editUserPage.ClickSubmitButton();
            Assert.AreEqual("Success", editUserPage.GetSuccessMessage());
        }

        [Test]
        public void FilterByUserRole()
        {
            var adminPage = new AdminPage();
            var userRole = "Admin";
            adminPage.SearchUsersByUserRole(userRole);
            Assert.True(adminPage.AreUsersFilteredByUserRole(userRole));
        }
    }
}
