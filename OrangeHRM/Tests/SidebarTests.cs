﻿using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OrangeHRM.PageObjects;

namespace OrangeHRM.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class SidebarTests : BaseTest
    {
        [OneTimeSetUp]
        public void Login()
        {
            var loginPage = new LoginPage();
            loginPage.LogInWithCredentials("Admin", "admin123");
            var dashboardPage = new DashboardPage();
            Assert.True(dashboardPage.IsDashboardDisplayed());
        }

        [Test]
        [AllureName("Sidebar test")]
        [AllureSuite("App page")]
        [AllureDescription("Testing sidebar search and presence of its items")]
        public void Sidebar()
        {
            var appPage = new AppPage();
            var searchQuery = "an";
            var emptyQuery = "  ";
            Assert.True(appPage.SidebarInitState());
            Assert.True(appPage.IsSidebarInDefaultState());
            appPage.EnterToSidebarSearch(searchQuery);
            Assert.True(appPage.GetListOfItemsInSidebar().All(i => i.Contains(searchQuery)));
            appPage.EnterToSidebarSearch(emptyQuery);
            Assert.True(appPage.GetListOfItemsInSidebar().Count == 0);
            appPage.ClickCollapseButton();
            Assert.True(appPage.IsSidebarCollapsed());
            appPage.ClickCollapseButton();
            Assert.False(appPage.IsSidebarCollapsed());
            appPage.ClearSidebarSearch();
            appPage.ClickSidebarItem<AdminPage>("Admin");
            Assert.True(new AdminPage().PageInitState());
        }
    }
}
