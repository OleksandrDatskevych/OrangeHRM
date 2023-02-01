using System.Data;
using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class AppPage : BasePage
    {
        private readonly MyWebElement _sidebarSearchTextBox = new(By.XPath("//*[@class='oxd-main-menu-search']/input"));
        private readonly MyWebElement _collapseSidebarButton = new(By.ClassName("oxd-main-menu-button"));

        private IReadOnlyList<IWebElement> SidebarItems => WebDriverFactory.Driver.FindElements(By.XPath("//*[@class='oxd-main-menu']/child::*"));

        public bool SidebarInitState() => _sidebarSearchTextBox.IsDisplayed() && _collapseSidebarButton.IsDisplayed() &&
            Driver.GetWebDriverWait().Until(drv => drv.FindElements(By.XPath("//*[@class='oxd-main-menu']/child::*"))).Count == 11;

        public void EnterToSidebarSearch(string text) => _sidebarSearchTextBox.SendKeysAfterCtrlABackspace(text);

        public void ClearSidebarSearch() => _sidebarSearchTextBox.ClearAfterCtrlABackspace();

        public List<string> GetListOfItemsInSidebar()
        {
            var list = new List<string>();
            
            for (var i = 0; i < SidebarItems.Count; i++)
            {
                list.Add(SidebarItems[i].FindElements(By.XPath("//span"))[i].Text);
            }

            return list;
        }

        public void ClickCollapseButton() => _collapseSidebarButton.Click();

        public bool IsSidebarCollapsed() => _sidebarSearchTextBox.GetAttribute("class").Contains("toggled");

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
    }
}
