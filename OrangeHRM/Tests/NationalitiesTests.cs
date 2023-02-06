using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OrangeHRM.PageObjects;

namespace OrangeHRM.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class NationalitiesTests : BaseTest
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
            appPage.ClickSidebarItem<AdminPage>("Admin");
            var adminPage = new AdminPage();
            Assert.True(adminPage.PageInitState());
            var nationalitiesPage = adminPage.ClickNationalitiesButton();
            Assert.True(nationalitiesPage.PageInitState());
        }

        [Test]
        [AllureName("Delete single nationality")]
        [AllureSuite("Nationalities page")]
        [AllureDescription("Delete single nationality using delete button in their corresponding row")]
        public void DeleteSingleNationality()
        {
            var nationalitiesPage = new NationalitiesPage();
            var nationality = "Kuwaiti";
            nationalitiesPage.DeleteSingleNationality(nationality);
        }

        [Test]
        [AllureName("Delete multiple nationalities")]
        [AllureSuite("Nationalities page")]
        [AllureDescription("Delete multiple nationalities using checkboxes in their corresponding row and then clicking \"Remove selected\" button")]
        public void DeleteMultipleNationalities()
        {
            var nationalitiesPage = new NationalitiesPage();
            var nationalities = new[] { "Algerian", "Bulgarian", "Russian" };
            var recordsBefore = nationalitiesPage.GetNumberOfNationalities();
            nationalitiesPage.DeleteMultipleNationalities(nationalities);
            Assert.AreEqual(recordsBefore - nationalities.Length, nationalitiesPage.GetNumberOfNationalities());
        }
    }
}
