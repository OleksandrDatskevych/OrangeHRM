using NUnit.Framework;
using OrangeHRM.Common.Drivers;

namespace OrangeHRM.Tests
{
    public class BaseTest
    {
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
