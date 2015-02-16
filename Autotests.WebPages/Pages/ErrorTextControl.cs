using System;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class ErrorTextControl : HtmlControl
    {
        public ErrorTextControl(By locator, HtmlControl container)
            :
                base(locator, container)
        {
        }

        public int Count {
            get { return element.FindElements(By.ClassName("help-inline")).Count; }
        }

        public StaticText this[int index]
        {
            get { return new StaticText(BY.NthOfClass("help-inline", index), this); }
        }

        public StaticText FindByName(string value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].GetText() == value)
                    return this[i];
            }
            throw new Exception(string.Format("Не найден текст '{0}' в списке Error-oв", value));
        }
    }
}