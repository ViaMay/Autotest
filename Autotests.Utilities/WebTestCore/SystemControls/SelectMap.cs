using System;
using System.Threading;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class SelectMap : HtmlControl
    {
        private readonly StaticText selectedText;

        public SelectMap(By locator, HtmlControl container = null)
            : base(locator, container)
        {
            var controlContainer = new HtmlControlContainer(locator, container);
            selectedText = new StaticText(By.XPath("//div/input"), controlContainer);
        }

        public bool IsMultiple { get; private set; }

        public override string GetText()
        {
            return selectedText.GetAttributeValue("title");
        }

        public void SelectValueFirst(string value)
        {
            WaitPresenceWithRetries();
            if (GetAttributeValue("title").Contains(value))
                return;
            element.SendKeys(value);
            Thread.Sleep(1000);
            element.SendKeys(Keys.Tab + Keys.Enter);
            var clickelement = new LinkMap(By.XPath("//div[3]/div[3]/h2"));
            var second = 0;
            Link:
            try
            {
                clickelement.WaitVisibleWithRetries();
                clickelement.WaitPresenceWithRetries();
                clickelement.Click();
            }
            catch (Exception)
            {
                if (second >= 1000)
                    return;
                Thread.Sleep(10);
                goto Link;
            }
            clickelement.WaitVisibleWithRetries();
        }

//        public void SelectValue(string value)
//        {
//            WaitEnabled();
//            if (GetText() == value)
//                return;
//            IList<IWebElement> list = element.FindElements(By.XPath(".//div[1]/ul[2]/li/a/strong"));
//            foreach (IWebElement option in list)
//            {
//                if (option.Text == value)
//                {
//                    SetSelected(option);
//                    Click();
//                }
//            }
//        }
//
//        private static void SetSelected(IWebElement option)
//        {
//            if (option.Selected)
//                return;
//            option.Click();
//        }
    }
}