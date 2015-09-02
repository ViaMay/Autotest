using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public static class HtmlControlExtensions
    {
        public static string GetId(this HtmlControl container)
        {
            return container.GetAttributeValue("id");
        }

        public static ISearchContext GetSearchContext(this HtmlControl container)
        {
            return container != null ? container.element : WebDriverCache.WebDriver.GetSearchContext();
        }
    }
}