using Autotests.Utilities.WebTestCore.TestSystem;
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

        public void WaitValue(string value)
        {
            string description = FormatWithLocator(string.Format("Ожидание value '{0}' в элементе", value));
            Waiter.Wait(() => GetAttributeValue("value") == value, description);
        }
    }
}