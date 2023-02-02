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
            Assert.True(new DashboardPage().IsDashboardDisplayed());
        }

        [SetUp]
        public void Init()
        {
            var appPage = new AppPage();
            Assert.True(new DashboardPage().IsDashboardDisplayed());
            appPage.ClickSidebarItem("Admin");
            var adminPage = new AdminPage();
            Assert.True(adminPage.PageInitState());
            adminPage.ClickNationalitiesButton();
            Assert.True(new NationalitiesPage().PageInitState());
        }

        [Test]
        public void DeleteSingleNationality()
        {
            var nationalitiesPage = new NationalitiesPage();
            var nationality = "Liberian";
            nationalitiesPage.DeleteSingleNationality(nationality);
        }

        [Test]
        public void DeleteMultipleNationalities()
        {
            var nationalitiesPage = new NationalitiesPage();
            var nationalities = new[] { "Albanian", "Kuwaiti", "Liberian" };
            nationalitiesPage.DeleteMultipleNationalities(nationalities);
        }
    }
}
