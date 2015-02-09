using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class Link : HtmlControl
    {
        public Link(By locator, HtmlControl container = null)
            : base(locator, container)
        {
        }

        public Link(string idLocator, HtmlControl container = null)
            : base(idLocator, container)
        {
        }

        public override bool IsEnabled
        {
            get { return !HasClass("disabled"); }
        }

        public string Href
        {
            get { return GetAttributeValue("href"); }
        }

        public static Link ByLinkText(string linkText, HtmlControl container = null)
        {
            return new Link(By.LinkText(linkText), container);
        }
    }
}