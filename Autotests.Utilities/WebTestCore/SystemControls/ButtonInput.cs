using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class ButtonInput : HtmlControl
    {
        public ButtonInput(By locator, HtmlControl container = null)
            : base(locator, container)
        {
        }

        public ButtonInput(string idLocator, HtmlControl container = null)
            : base(idLocator, container)
        {
        }

        public override bool IsEnabled
        {
            get { return !HasClass("disabled"); }
        }
    }
}