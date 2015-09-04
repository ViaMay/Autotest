using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class RadioButtonControl : HtmlControl
    {
        public RadioButtonControl(By locator, HtmlControl container = null)
            : base(locator, container)
        {
        }

        public RadioButtonControl(string idLocator, HtmlControl container = null)
            : base(idLocator, container)
        {
        }
    }
}