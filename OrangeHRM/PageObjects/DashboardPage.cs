using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class DashboardPage : AppPage
    {
        private MyWebElement _logoutLink = new(By.PartialLinkText("Logout"));
        private MyWebElement _userDropdown = new(By.XPath("//li[contains(@class, 'userdropdown')]"));

        private IReadOnlyList<IWebElement> DashboardGrid => WebDriverFactory.Driver.FindElements(By
            .XPath("//*[contains(@class, 'orangehrm-dashboard-grid')]/child::*"));

        public bool IsDashboardDisplayed()
        {
            WebDriverFactory.Driver.GetWebDriverWait().Until(_ => DashboardGrid.Count != 0);

            if (DashboardGrid.Count != 0)
            {
                return true;
            }

            return false;
        }

        public void ClickUserDropdown() => _userDropdown.Click();

        public void ClickLogout() => _logoutLink.Click();
    }
}
