using Autotests.Utilities.WebTestCore.TestSystem;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class LinkMap : HtmlControl
    {
        public LinkMap(By locator, HtmlControl container = null)
            : base(locator, container)
        {
        }

        public LinkMap(string idLocator, HtmlControl container = null)
            : base(idLocator, container)
        {
        }

        public override void Click()
        {
            WaiteVisible();
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

        public override void WaitText(string expectedText)
        {
            WaiteVisible();
            string description =
                FormatWithLocator(string.Format("Ожидание появления текста '{0}' в элементе", expectedText));
            Waiter.Wait(() => GetAttributeValue("alt") == expectedText, description);
        }

        public override void WaitTextNotContains(string expectedText)
        {
            WaiteVisible();
            string description =
                FormatWithLocator(string.Format("Ожидание отсутствия текста '{0}' внутри текста элемента", expectedText));
            StringAssert.DoesNotContain(expectedText, GetAttributeValue("alt"), description);
        }


        public void WaiteVisible()
        {
            var second = 0;
            while (!IsPresent)
            {
                second = second + 1;
                if (second >= 1000) Assert.AreEqual(IsPresent, true, "Время ожидание завершено. Не найден элемент");
            }
        }
        public static Link ByLinkText(string linkText, HtmlControl container = null)
        {
            return new Link(By.LinkText(linkText), container);
        }
    }
}