using System;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class RadioButtonListControl : HtmlControl
    {
        public RadioButtonListControl(By locator, HtmlControl container)
            : base(locator, container)
        {
        }
        public RadioButtonListControl(string idLocator, HtmlControl container = null)
            : base(idLocator, container)
        {
        }

        public int Count {
            get { return element.FindElements(By.ClassName("radio")).Count; }
        }

        public RadioButtonControl this[int index]
        {
            get { return new RadioButtonControl(BY.NthOfClass("radio", index)); }
        }
    }
}