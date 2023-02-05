using NUnit.Framework;
using OrangeHRM.PageObjects;

namespace OrangeHRM.Tests
{
    [TestFixture]
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
        public void DeleteSingleNationality()
        {
            var nationalitiesPage = new NationalitiesPage();
            var nationality = "Macedonian";
            nationalitiesPage.DeleteSingleNationality(nationality);
        }

        [Test]
        public void DeleteMultipleNationalities()
        {
            var nationalitiesPage = new NationalitiesPage();
            var nationalities = new[] { "Scottish", "Dominican", "Togolese" };
            var recordsBefore = nationalitiesPage.GetNumberOfNationalities();
            nationalitiesPage.DeleteMultipleNationalities(nationalities);
            Assert.AreEqual(recordsBefore - nationalities.Length, nationalitiesPage.GetNumberOfNationalities());
        }
    }
}
