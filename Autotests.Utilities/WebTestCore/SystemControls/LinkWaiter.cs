using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class LinkWaiter : HtmlControl
    {
        public LinkWaiter(By locator, HtmlControl container = null)
            : base(locator, container)
        {
        }

        public LinkWaiter(string idLocator, HtmlControl container = null)
            : base(idLocator, container)
        {
        }

        public override void Click()
        {
            var second = 0;
            while (!IsPresent)
            {
                second = second + 1;
                if (second >= 1000) Assert.AreEqual(IsPresent, false, "Время ожидание завершено. Не найден элемент");
            }
            element.Click();
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