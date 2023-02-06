using System.Drawing;
using OpenQA.Selenium;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class CorporateBrandingPage : AppPage
    {
        private const string ColorLocatorTemplate = "//*[contains(@class, 'oxd-color-input') and ./preceding-sibling::label[text()='";
        private readonly MyWebElement PrimaryColorButton = new(By.XPath($"{ColorLocatorTemplate}Primary Color']]"));
        private readonly MyWebElement SecondaryColorButton = new(By.XPath($"{ColorLocatorTemplate}Secondary Color']]"));
        private readonly MyWebElement PrimaryGradientColor1 = new(By.XPath($"{ColorLocatorTemplate}Primary Gradient Color 1']]"));
        private readonly MyWebElement PrimaryGradientColor2 = new(By.XPath($"{ColorLocatorTemplate}Primary Gradient Color 2']]"));
        private readonly MyWebElement PublishButton = new(By.XPath("//button[@type='submit']"));
        private readonly MyWebElement HexTextBox = new(By.XPath("//input[./preceding-sibling::label[text()='HEX']]"));
        private readonly MyWebElement PageHeader = new(By.ClassName("oxd-topbar-header"));

        public bool PageInitState() => PrimaryColorButton.IsDisplayed() && 
                                       SecondaryColorButton.IsDisplayed() && 
                                       PrimaryGradientColor1.IsDisplayed() && 
                                       PrimaryGradientColor2.IsDisplayed();

        public void SetNewColors(string primary, string secondary, string gradient1, string gradient2)
        {
            PrimaryColorButton.Click();
            HexTextBox.SendKeysAfterCtrlABackspace(primary + Keys.Enter);
            PrimaryColorButton.Click();
            SecondaryColorButton.Click();
            HexTextBox.SendKeysAfterCtrlABackspace(secondary + Keys.Enter);
            SecondaryColorButton.Click();
            PrimaryGradientColor1.Click();
            HexTextBox.SendKeysAfterCtrlABackspace(gradient1 + Keys.Enter);
            PrimaryGradientColor1.Click();
            PrimaryGradientColor2.Click();
            HexTextBox.SendKeysAfterCtrlABackspace(gradient2 + Keys.Enter);
            PrimaryGradientColor2.Click();
            PublishButton.Click();
        }

        public void RefreshPage()
        {
            Driver.Navigate().Refresh();
            Driver.GetWebDriverWait().Until(_ => PageInitState() && SidebarInitState());
        }

        public string GetPrimaryColor()
        {
            var colorName = GetCollapseButtonColor();
            var rbg = colorName.Replace("rgba(", "").Replace(")!important", "").Split(", ");
            var red = rbg[0];
            var green = rbg[1];
            var blue = rbg[2];
            var hex = ("#" + int.Parse(red).ToString("X2") + int.Parse(green).ToString("X2") + int.Parse(blue).ToString("X2")).ToLower();

            return hex;
        }

        public string GetSecondaryColor()
        {
            var colorName = PublishButton.GetCssValue("background-color");
            var rbg = colorName.Replace("rgba(", "").Replace(")", "").Split(", ");
            var red = rbg[0];
            var green = rbg[1];
            var blue = rbg[2];
            var hex = ("#" + int.Parse(red).ToString("X2") + int.Parse(green).ToString("X2") + int.Parse(blue).ToString("X2")).ToLower();

            return hex;
        }

        public string GetGradient1Color()
        {
            var colorName = PageHeader.GetCssValue("background-image");
            var rbg = colorName.Substring(colorName.IndexOf("rgb")).Split("),");
            var color = colorName.Replace("linear-gradient(90deg, ", "").Replace(" 90%)", "").Replace("rgb(", "").Replace(")", "");
            var colors = color.Split(", ");
            var red = colors[0];
            var green = colors[1];
            var blue = colors[2];
            var hex = ("#" + int.Parse(red).ToString("X2") + int.Parse(green).ToString("X2") + int.Parse(blue).ToString("X2")).ToLower();

            return hex;
        }

        public string GetGradient2Color()
        {
            var colorName = PageHeader.GetCssValue("background-image");
            var rbg = colorName.Substring(colorName.IndexOf("rgb")).Split("),");
            var color = colorName.Replace("linear-gradient(90deg, ", "").Replace(" 90%)", "").Replace("rgb(", "").Replace(")", "");
            var colors = color.Split(", ");
            var red = colors[3];
            var green = colors[4];
            var blue = colors[5];
            var hex = ("#" + int.Parse(red).ToString("X2") + int.Parse(green).ToString("X2") + int.Parse(blue).ToString("X2")).ToLower();

            return hex;
        }
    }
}
