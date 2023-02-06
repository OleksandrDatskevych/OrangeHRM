using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OrangeHRM.PageObjects;

namespace OrangeHRM.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class CorporateBrandingTests : BaseTest
    {
        [OneTimeSetUp]
        public void Login()
        {
            var loginPage = new LoginPage();
            loginPage.LogInWithCredentials("Admin", "admin123");
            var dashboardPage = new DashboardPage();
            Assert.True(dashboardPage.IsDashboardDisplayed());
        }

        [SetUp]
        public void Init()
        {
            var appPage = new AppPage();
            var adminPage = appPage.ClickSidebarItem<AdminPage>("Admin");
            Assert.True(adminPage.PageInitState());
            var corporateBrandingPage = adminPage.ClickCorporateBrandingButton();
            Assert.True(corporateBrandingPage.PageInitState());
        }

        [Test]
        [AllureName("Color changing")]
        [AllureSuite("Corporate branding page")]
        [AllureDescription("Changing color scheme of web application")]
        public void ColorChanging()
        {
            var corporateBrandingPage = new CorporateBrandingPage();
            var primaryColor = "#f6100e";
            var secondaryColor = "#76bc21";
            var gradient1Color = "#0931fc";
            var gradient2Color = "#f35c17";
            corporateBrandingPage.SetNewColors(primaryColor, secondaryColor, gradient1Color, gradient2Color);
            corporateBrandingPage.RefreshPage();
            Assert.AreEqual(primaryColor, corporateBrandingPage.GetPrimaryColor());
            Assert.AreEqual(secondaryColor, corporateBrandingPage.GetSecondaryColor());
            Assert.AreEqual(gradient1Color, corporateBrandingPage.GetGradient1Color());
            Assert.AreEqual(gradient2Color, corporateBrandingPage.GetGradient2Color());
        }
    }
}
