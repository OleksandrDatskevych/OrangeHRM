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

        public List<string> GetListOfItemsInSidebar() => SidebarItems.Select((t, i) => t.FindElements(By.XPath("//span"))[i].Text).ToList();

        public void ClickCollapseButton() => CollapseSidebarButton.Click();

        public bool IsSidebarCollapsed() => SidebarSearchTextBox.GetAttribute("class").Contains("toggled");

        public T? ClickSidebarItem<T>(string itemName) where T : BasePage
        {
            var element = Driver.GetWebDriverWait().Until(drv => drv.FindElements(By.XPath("//*[@class='oxd-main-menu']//span")))
                .Where(i => i.Text == itemName).ToList();

            if (element.Count == 0)
            {
                throw new NoSuchElementException();
            }

            element[0].Click();

            return itemName switch
            {
                "Admin" => new AdminPage() as T,
                "Recruitment" => new RecruitmentPage() as T,
                _ => new AppPage() as T
            };

        }

        public bool IsSidebarInDefaultState() => DefaultItemsInSidebar.SequenceEqual(GetListOfItemsInSidebar());

        protected string GetCollapseButtonColor() => CollapseSidebarButton.GetCssValue("background-color");
    }
}
