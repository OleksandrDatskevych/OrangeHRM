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
            var nationality = "Paraguayan";
            nationalitiesPage.DeleteSingleNationality(nationality);
        }

        [Test]
        public void DeleteMultipleNationalities()
        {
            var nationalitiesPage = new NationalitiesPage();
            var nationalities = new[] { "Slovenian", "Jordanian", "Ecuadorean" };
            var recordsBefore = nationalitiesPage.GetNumberOfNationalities();
            nationalitiesPage.DeleteMultipleNationalities(nationalities);
            Assert.AreEqual(recordsBefore - nationalities.Length, nationalitiesPage.GetNumberOfNationalities());
        }
    }
}
