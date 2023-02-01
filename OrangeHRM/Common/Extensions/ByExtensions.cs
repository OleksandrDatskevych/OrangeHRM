using OpenQA.Selenium;

namespace OrangeHRM.Common.Extensions
{
    public static class ByExtensions
    {
        public static string GetLocator(this By by)
        {
            var locator = by.ToString();

            return locator[locator.IndexOf(" ", StringComparison.Ordinal)..];
        }
    }
}
