using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class DashboardPage : AppPage
    {
        private readonly MyWebElement LogoutLink = new(By.PartialLinkText("Logout"));
        private readonly MyWebElement UserDropdown = new(By.XPath("//li[contains(@class, 'userdropdown')]"));

        private IReadOnlyList<IWebElement> DashboardGrid => WebDriverFactory.Driver.FindElements(By
            .XPath("//*[contains(@class, 'orangehrm-dashboard-grid')]/child::*"));

        public bool IsDashboardDisplayed()
        {
            WebDriverFactory.Driver.GetWebDriverWait().Until(_ => DashboardGrid.Count != 0);

            return DashboardGrid.Count != 0;
        }

        public void ClickUserDropdown() => UserDropdown.Click();

        public void ClickLogout() => LogoutLink.Click();
    }
}
