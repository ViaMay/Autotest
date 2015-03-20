using System;
using Autotests.Utilities.WebTestCore.SystemControls;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class ShopsListControl : HtmlControl
    {
        public ShopsListControl(By className)
            : base(className)
        {
        }
        public int Count
        {
            get { return element.FindElements(By.XPath("//tbody/tr")).Count; }
        }

        public ShopsRowControl FindRowByName(string name)
        {
            for (var i = 0; i < Count; i++)
            {
                if (GetRow(i).Name.GetText().Contains(name))
                    return GetRow(i);
            }
            throw new Exception(string.Format("Не найдена строка с именем '{0}' в таблице", name));
        }

        public ShopsRowControl GetRow(int index)
        {
            var row = new ShopsRowControl(index + 1);
            row.WaitPresenceWithRetries();
            return row;
        }
    }
}