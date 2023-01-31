using NUnit.Framework;
using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.Extensions;
using OrangeHRM.PageObjects;

namespace OrangeHRM.Tests
{
    [TestFixture]
    public class SidebarTests : BaseTest
    {
        [OneTimeSetUp]
        public void Login()
        {
            var loginPage = new LoginPage();
            loginPage.LogInWithCreds("Admin", "admin123");
        }

        [Test]
        public void Sidebar()
        {
            var appPage = new AppPage();
            var listOfItems = new List<string>() 
            { 
                "Admin", 
                "PIM",
                "Leave",
                "Time",
                "Recruitment",
                "My Info",
                "Performance", 
                "Dashboard",
                "Directory", 
                "Maintenance", 
                "Buzz"
            };
            var searchQuery = "an";
            var emptyQuery = "  ";
            Assert.True(appPage.SidebarInitState());
            string List() => string.Join(",", appPage.GetListOfItemsInSidebar());
            Assert.AreEqual(listOfItems, appPage.GetListOfItemsInSidebar(), List());
            appPage.EnterToSidebarSearch(searchQuery);
            Assert.True(appPage.GetListOfItemsInSidebar().All(i => i.Contains(searchQuery)));
            appPage.EnterToSidebarSearch(emptyQuery);
            Assert.True(appPage.GetListOfItemsInSidebar().Count == 0);
            appPage.ClickCollapseButton();
            Assert.True(appPage.IsSidebarCollapsed());
            appPage.ClickCollapseButton();
            Assert.False(appPage.IsSidebarCollapsed());
            appPage.ClearSidebarSearch();
            appPage.ClickSidebarItem("Admin");
            var adminPage = new AdminPage();
            Assert.True(adminPage.PageInitState());
        }
    }
}
