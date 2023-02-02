using NUnit.Framework;
using OrangeHRM.PageObjects;

namespace OrangeHRM.Tests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {
        [Test]
        public void LoginAndLogout()
        {
            var loginPage = new LoginPage();
            Assert.True(loginPage.PageInitState());
            loginPage.LogInWithCredentials("Admin", "admin123");
            var dashboardPage = new DashboardPage();
            Assert.True(dashboardPage.IsDashboardDisplayed());
            dashboardPage.ClickUserDropdown();
            dashboardPage.ClickLogout();
            Assert.True(loginPage.PageInitState());
        }

        [Test]
        public void NegativeLogin()
        {
            var loginPage = new LoginPage();
            Assert.True(loginPage.PageInitState());
            loginPage.LogInWithCredentials("admon", "12345678");
            Assert.AreEqual("Invalid credentials", loginPage.GetErrorMessageText());
        }

        [Test]
        public void EmptyLogin()
        {
            var loginPage = new LoginPage();
            var redBorder = "1px solid rgb(235, 9, 16)";
            Assert.True(loginPage.PageInitState());
            loginPage.ClickSubmitButton();
            Assert.AreEqual("Required", loginPage.GetUsernameErrorMessage());
            Assert.AreEqual(redBorder, loginPage.GetUsernameBorderColor());
            Assert.AreEqual("Required", loginPage.GetPasswordErrorMessage());
            Assert.AreEqual(redBorder, loginPage.GetPasswordBorderColor());
        }
    }
}
