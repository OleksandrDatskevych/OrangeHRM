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
            loginPage.EnterUsername("Admin");
            loginPage.EnterPassword("admin123");
            loginPage.ClickSubmitButton();
            var dashboardPage = new DashboardPage();
            Assert.True(dashboardPage.IsDashboardDisplayed());
            dashboardPage.ClickUserDropdown();
            dashboardPage.ClickLogout();
            Assert.True(loginPage.PageInitState());
        }
    }
}
