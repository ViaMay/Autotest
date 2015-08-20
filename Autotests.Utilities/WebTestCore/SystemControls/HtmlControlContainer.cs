using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class HtmlControlContainer : HtmlControl
    {
        public HtmlControlContainer(By locator, HtmlControl container = null)
            : base(locator, container)
        {
        }

        public HtmlControlContainer(string idLocator, HtmlControl container = null)
            : base(idLocator, container)
        {
        }
    }
}