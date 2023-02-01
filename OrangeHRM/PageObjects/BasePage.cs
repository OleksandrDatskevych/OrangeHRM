using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;

namespace OrangeHRM.PageObjects
{
    public class BasePage
    {
        protected static IWebDriver Driver => WebDriverFactory.Driver;
    }
}
