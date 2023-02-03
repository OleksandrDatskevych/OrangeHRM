using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OrangeHRM.Common.Drivers;

namespace OrangeHRM.PageObjects
{
    public class BasePage
    {
        protected static IWebDriver Driver => WebDriverFactory.Driver;
        protected static Actions Actions => WebDriverFactory.Actions;
    }
}
