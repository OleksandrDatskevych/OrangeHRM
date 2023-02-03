using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class AppPage : BasePage
    {
        private readonly List<string> DefaultItemsInSidebar = new()
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
        private readonly MyWebElement SidebarSearchTextBox = new(By.XPath("//*[@class='oxd-main-menu-search']/input"));
        private readonly MyWebElement CollapseSidebarButton = new(By.ClassName("oxd-main-menu-button"));

        private IReadOnlyList<IWebElement> SidebarItems => WebDriverFactory.Driver.FindElements(By.XPath("//*[@class='oxd-main-menu']/child::*"));

        public bool SidebarInitState() => SidebarSearchTextBox.IsDisplayed() && CollapseSidebarButton.IsDisplayed() &&
            Driver.GetWebDriverWait().Until(drv => drv.FindElements(By.XPath("//*[@class='oxd-main-menu']/child::*"))).Count == 11;

        public void EnterToSidebarSearch(string text) => SidebarSearchTextBox.SendKeysAfterCtrlABackspace(text);

        public void ClearSidebarSearch() => SidebarSearchTextBox.ClearAfterCtrlABackspace();

        public List<string> GetListOfItemsInSidebar()
        {
            return SidebarItems.Select((t, i) => t.FindElements(By.XPath("//span"))[i].Text).ToList();
        }

        public void ClickCollapseButton() => CollapseSidebarButton.Click();

        public bool IsSidebarCollapsed() => SidebarSearchTextBox.GetAttribute("class").Contains("toggled");

        public void ClickSidebarItem(string item)
        {
            var element = Driver.GetWebDriverWait().Until(drv => drv.FindElements(By.XPath("//*[@class='oxd-main-menu']//span")))
                .Where(i => i.Text == item).ToList();

            if (element.Count > 0)
            {
                element[0].Click();
            }
            else
            {
                throw new NoSuchElementException();
            }
        }

        public bool IsSidebarInDefaultState() => DefaultItemsInSidebar.SequenceEqual(GetListOfItemsInSidebar());
    }
}
