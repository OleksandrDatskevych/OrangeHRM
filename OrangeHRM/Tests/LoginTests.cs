using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OrangeHRM.PageObjects;

namespace OrangeHRM.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class LoginTests : BaseTest
    {
        [Test]
        [AllureName("Login and logout positive test")]
        [AllureSuite("Login page")]
        [AllureSeverity(SeverityLevel.blocker)]
        [AllureDescription("Login with correct credentials")]
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
        [AllureName("Login negative test")]
        [AllureSuite("Login page")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureDescription("Login with incorrect credentials")]
        public void NegativeLogin()
        {
            var loginPage = new LoginPage();
            Assert.True(loginPage.PageInitState());
            loginPage.LogInWithCredentials("admon", "12345678");
            Assert.AreEqual("Invalid credentials", loginPage.GetErrorMessageText());
        }

        [Test]
        [AllureName("Login negative test")]
        [AllureSuite("Login page")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureDescription("Login without entering credentials")]
        public void EmptyLogin()
        {
            var loginPage = new LoginPage();
            var redBorder = "1px solid rgb(235, 9, 16)";
            var errorMessage = "Required";
            Assert.True(loginPage.PageInitState());
            loginPage.ClickSubmitButton();
            Assert.AreEqual(errorMessage, loginPage.GetUsernameErrorMessage());
            Assert.AreEqual(redBorder, loginPage.GetUsernameBorderColor());
            Assert.AreEqual(errorMessage, loginPage.GetPasswordErrorMessage());
            Assert.AreEqual(redBorder, loginPage.GetPasswordBorderColor());
        }
    }
}
