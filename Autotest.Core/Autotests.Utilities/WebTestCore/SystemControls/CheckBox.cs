using System;
using Autotests.Utilities.WebTestCore.TestSystem;
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

        public virtual void WaitChecked()
        {
            string description =
                FormatWithLocator(string.Format("Ожидание Checked в элементе"));
            Waiter.Wait(() => Checked == true, description);
        }

        public virtual void WaitUnchecked()
        {
            string description =
                FormatWithLocator(string.Format("Ожидание Checked в элементе"));
            Waiter.Wait(() => Checked == false, description);
        }
    }
}