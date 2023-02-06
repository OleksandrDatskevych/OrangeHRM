using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OrangeHRM.Common.Drivers;

namespace OrangeHRM.Tests
{
    public class BaseTest
    {
        protected static IWebDriver Driver => WebDriverFactory.Driver;
        protected static Actions Actions => WebDriverFactory.Actions;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            WebDriverFactory.Driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            WebDriverFactory.QuitDriver();
        }
    }
}
