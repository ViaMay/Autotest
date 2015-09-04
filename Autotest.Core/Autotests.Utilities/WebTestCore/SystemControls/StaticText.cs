using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class StaticText : HtmlControl
    {
        public StaticText(By locator, HtmlControl container = null)
            : base(locator, container)
        {
        }

        public StaticText(string idLocator, HtmlControl container = null)
            : base(idLocator, container)
        {
        }
    }
}