﻿using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using OrangeHRM.Common.Drivers;
using OrangeHRM.Common.Extensions;

namespace OrangeHRM.Common.WebElements
{
    public class MyWebElement : IWebElement
    {
        protected By By { get; set; }
        protected IWebElement WebElement => WebDriverFactory.Driver.GetWebElementWhenExist(By);
        public string TagName => WebElement.TagName;
        public string Text => WebElement.Text;
        public bool Enabled => WebElement.Enabled;
        public bool Selected => WebElement.Selected;
        public Point Location => WebElement.Location;
        public Size Size => WebElement.Size;
        public bool Displayed => WebElement.Displayed;

        public MyWebElement(By by)
        {
            By = by;
        }

        public void Clear() => WebElement.Clear();

        public void Click()
        {
            WebDriverFactory.Driver
                .GetWebDriverWait(30, TimeSpan.FromMilliseconds(500),
                typeof(ElementClickInterceptedException),
                typeof(NoSuchElementException),
                typeof(ElementNotInteractableException))
                .Until(_ =>
                {
                    WebElement.Click();

                    return true;
                });
        }

        public IWebElement FindElement(By by) => WebElement.FindElement(by);

        public ReadOnlyCollection<IWebElement> FindElements(By by) => WebElement.FindElements(by);

        public string GetAttribute(string attributeName) => WebElement.GetAttribute(attributeName);

        public string GetCssValue(string propertyName) => WebElement.GetCssValue(propertyName);

        public string GetDomAttribute(string attributeName) => WebElement.GetDomAttribute(attributeName);

        public string GetDomProperty(string propertyName) => WebElement.GetDomProperty(propertyName);

        public ISearchContext GetShadowRoot() => WebElement.GetShadowRoot();

        public void SendKeys(string text)
        {
            WebDriverFactory.Driver
                .GetWebDriverWait(30, TimeSpan.FromMilliseconds(500), typeof(NoSuchElementException))
                .Until(_ =>
                {
                    WebElement.SendKeys(text);

                    return true;
                });
        }

        public void SendKeysAfterClear(string text)
        {
            Clear();
            SendKeys(text);
        }

        public bool IsDisplayed()
        {
            WebDriverFactory.Driver.GetWebDriverWait(30, TimeSpan.FromMilliseconds(500)).Until(drv => drv.FindElements(By).Count > 0);

            if (WebDriverFactory.Driver.FindElements(By).Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Submit() => WebElement.Submit();
    }
}