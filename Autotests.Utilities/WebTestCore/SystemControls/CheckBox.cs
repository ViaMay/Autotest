using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class CheckBox : HtmlControl
    {
        static CheckBox()
        {
        }

        public CheckBox(By locator, HtmlControl container = null)
            : base(locator, container)
        {
        }

        public bool Checked
        {
            get { return Convert.ToBoolean(element.GetAttribute("checked")); }
        }

        public void CheckAndWait()
        {
            if (!Checked)
                element.Click();
        }

        public void UncheckAndWait()
        {
            if (Checked)
                element.Click();
        }
    }
}