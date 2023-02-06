using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OrangeHRM.Common.WebElements;
using OrangeHRM.PageObjects;

namespace OrangeHRM.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class RecruitmentTests : BaseTest
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
            var recruitmentPage = appPage.ClickSidebarItem<RecruitmentPage>("Recruitment");
            Assert.True(recruitmentPage.PageInitState());
        }

        [Test]
        [AllureName("View application")]
        [AllureSuite("Recruitment page")]
        [AllureDescription("View application of specified person on recruitment page")]
        public void ViewApplication()
        {
            var recruitmentPage = new RecruitmentPage();
            var applicantFullName = "Jennifer Clinton";
            var applicantFirstName = applicantFullName.Split(' ')[0];
            var applicantLastName = applicantFullName.Split(' ')[1];
            recruitmentPage.ViewCandidateApplication(applicantFullName);
            Assert.True(new MyWebElement(By.XPath("//h6[contains(@class, 'oxd-text') and ./parent::span]")).IsDisplayed());
            Assert.AreEqual(applicantFullName, recruitmentPage.GetFullNameInHeader());
            Assert.AreEqual(applicantFirstName, recruitmentPage.GetFirstNameValue());
            Assert.AreEqual(applicantLastName, recruitmentPage.GetLastNameValue());
        }

        [Test]
        [AllureName("Edit application")]
        [AllureSuite("Recruitment page")]
        [AllureDescription("Edit application of specified person on recruitment page")]
        public void EditApplication()
        {
            var recruitmentPage = new RecruitmentPage();
            var applicantFullName = "Phil Hughes";
            var email = "jojo2@jjba.com";
            var vacancy = "Junior Account Assistant";
            var newFullName = "Joseph Joestar";
            recruitmentPage.EditApplication(applicantFullName, newFullName, email, vacancy);
            Assert.AreEqual(applicantFullName, recruitmentPage.GetFullNameInHeader());
            Assert.AreEqual("Success", recruitmentPage.GetSuccessMessage());
        }

        [Test]
        [AllureName("Mark interview as passed")]
        [AllureSuite("Recruitment page")]
        [AllureDescription("Mark first scheduled interview as passed")]
        public void MarkFirstInterviewPassed()
        {
            var recruitmentPage = new RecruitmentPage();
            recruitmentPage.PassFirstInterview();
            Assert.AreEqual("Success", recruitmentPage.GetSuccessMessage());
        }
    }
}
