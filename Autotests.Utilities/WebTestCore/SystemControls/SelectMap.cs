using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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
            var second = 0;
            while (!IsPresent)
            {
                second = second + 1;
                if (second >= 1000) Assert.AreEqual(IsPresent, true, "Время ожидание завершено. Не найден элемент");
            }
            GetAttributeValue("title");
            element.SendKeys(value + Keys.Tab + Keys.Enter);
            var rowSelected = new Link(By.XPath(".//div[1]/ul[2]/li/a/strong"));
            second = 0;
            while (rowSelected.IsVisible)
            {
                second = second + 1;
                if (second >= 1000) Assert.AreEqual(IsPresent, true, "Время ожидание завершено. Не найден элемент");
            }
//            SwitchToDefaultContent();
//            SwitchToFrame();
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

        private static void SetSelected(IWebElement option)
        {
            if (option.Selected)
                return;
            option.Click();
        }
    }
}