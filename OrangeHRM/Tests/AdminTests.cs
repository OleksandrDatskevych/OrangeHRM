﻿using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OrangeHRM.PageObjects;

namespace OrangeHRM.Tests
{
    [AllureNUnit]
    [TestFixture]
    public class AdminTests : BaseTest
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
        }

        [AllureName("Add user")]
        [AllureSuite("Admin page")]
        [AllureDescription("Addition of new user on admin page")]
        [Test]
        public void AddUser()
        {
            var adminPage = new AdminPage();
            var addUserPage = new AddUserPage();
            var redColorBorder = "1px solid rgb(235, 9, 16)";
            var userRole = "ESS";
            var status = "Enabled";
            var employeeName = "Odis";
            var username = "app099";
            var password = "12345Aa+";
            var confirmPassword = "12345Aa+";
            adminPage.ClickAddUser();
            addUserPage.ClickSubmitButton();
            Assert.AreEqual(redColorBorder, addUserPage.UserRoleBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.StatusBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.EmployeeNameBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.UsernameBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.PasswordBorderColor());
            Assert.AreEqual(redColorBorder, addUserPage.ConfirmPasswordBorderColor());
            addUserPage.SelectUserRole(userRole);
            addUserPage.SelectStatus(status);
            addUserPage.SelectEmployeeName(employeeName);
            addUserPage.EnterUsername(username);
            addUserPage.EnterPassword(password);
            addUserPage.EnterConfirmPassword(confirmPassword);
            addUserPage.ClickSubmitButton();
            Assert.AreEqual("Success", addUserPage.GetSuccessMessage());
        }

        [Test, Ignore("Automated user addition to perform other tests")]
        [TestCase("app100", "Paul")]
        [TestCase("app101", "Paul")]
        [TestCase("app102", "Paul")]
        [TestCase("app103", "Paul")]
        [TestCase("app104", "Paul")]
        [TestCase("app105", "Paul")]
        public void AddUsers(string username, string employeeName)
        {
            var adminPage = new AdminPage();
            var addUserPage = new AddUserPage();
            var userRole = "ESS";
            var status = "Enabled";
            var password = "12345Aa+";
            var confirmPassword = "12345Aa+";
            adminPage.ClickAddUser();
            addUserPage.SelectUserRole(userRole);
            addUserPage.SelectStatus(status);
            addUserPage.SelectEmployeeName(employeeName);
            addUserPage.EnterUsername(username);
            addUserPage.EnterPassword(password);
            addUserPage.EnterConfirmPassword(confirmPassword);
            addUserPage.ClickSubmitButton();
            Assert.AreEqual("Success", addUserPage.GetSuccessMessage());
        }

        [AllureName("Remove user by username")]
        [AllureSuite("Admin page")]
        [AllureDescription("Removing single user in a table of users on admin page, using their username")]
        [Test]
        public void RemoveUserByUsername()
        {
            var adminPage = new AdminPage();
            var userToDelete = "app103";
            var columnName = "Username";
            adminPage.DeleteUserInTable(userToDelete);
            Assert.False(adminPage.IsUserInTable(columnName, userToDelete));
        }

        [AllureName("Remove user by row number")]
        [AllureSuite("Admin page")]
        [AllureDescription("Removing single user in a table of users on admin page, using row number of a user in a table")]
        [Test]
        public void RemoveUserByRowNumber()
        {
            var adminPage = new AdminPage();
            var rowNumber = adminPage.GetNumberOfRows();
            var columnName = "Username";
            var username = adminPage.GetUsernameByRow(rowNumber);
            adminPage.DeleteUserInTable(rowNumber);
            Assert.False(adminPage.IsUserInTable(columnName, username));
        }

        [AllureName("Remove multiple users")]
        [AllureSuite("Admin page")]
        [AllureDescription("Removing multiple users in a table of users on admin page, using their username")]
        [Test]
        public void RemoveMultipleUsers()
        {
            var adminPage = new AdminPage();
            var users = new[] { "app102", "app104" };
            var columnName = "Username";
            adminPage.DeleteMultipleUsers(users);
            Assert.False(adminPage.IsUserInTable(columnName, users[0]));
            Assert.False(adminPage.IsUserInTable(columnName, users[1]));
        }

        [AllureName("Edit user")]
        [AllureSuite("Admin page")]
        [AllureDescription("Editing user info in a table of users on admin page, using their username")]
        [Test]
        public void EditUser()
        {
            var adminPage = new AdminPage();
            var editUserPage = new EditUserPage();
            var userToEdit = "app101";
            var userRole = "ESS";
            var status = "Enabled";
            var username = "eetgnzz1";
            var password = "09875oK=";
            var confirmPassword = "09875oK=";
            adminPage.EditUserInTable(userToEdit);
            editUserPage.SelectUserRole(userRole);
            editUserPage.SelectStatus(status);
            editUserPage.EnterUsername(username);
            editUserPage.ClickChangePasswordCheckbox();
            editUserPage.EnterPassword(password);
            editUserPage.EnterConfirmPassword(confirmPassword);
            editUserPage.ClickSubmitButton();
            Assert.AreEqual("Success", editUserPage.GetSuccessMessage());
        }

        [AllureName("Filter users by role")]
        [AllureSuite("Admin page")]
        [AllureDescription("Filtering users by their role in a table of users on admin page")]
        [Test]
        public void FilterByUserRole()
        {
            var adminPage = new AdminPage();
            var userRole = "Admin";
            adminPage.SearchUsersByUserRole(userRole);
            Assert.True(adminPage.AreUsersFilteredByUserRole(userRole));
        }
    }
}
