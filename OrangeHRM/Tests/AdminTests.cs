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
            loginPage.LogInWithCreds("Admin", "admin123");
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
            adminPage.ClickAddUser();
            addUserPage.ClickSubmitButton();
            Assert.AreEqual(redColorBorder, addUserPage.UserRoleBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.StatusBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.EmployeeNameBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.UsernameBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.PasswordBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.ConfirmPasswordBorderColor());
            addUserPage.SelectUserRole("ESS");
            addUserPage.SelectStatus("Enabled");
            addUserPage.SelectEmployeeName("al");
            addUserPage.EnterUsername("Adooo");
            addUserPage.EnterPassword("12345Aa+");
            addUserPage.EnterConfirmPassword("12345Aa+");
            addUserPage.ClickSubmitButton();
            Assert.AreEqual("Success", addUserPage.GetSuccessMessage());
        }
    }
}
