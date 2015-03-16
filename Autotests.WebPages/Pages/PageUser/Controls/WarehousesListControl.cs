using System;
using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class WarehousesListControl : HtmlControl
    {
        public WarehousesListControl(By className)
            : base(className)
        {
        }
        public int Count
        {
            get { return element.FindElements(By.XPath("//tbody/tr")).Count; }
        }

        public WarehousesRowControl FindRowByName(string name)
        {
            for (var i = 0; i < Count; i++)
            {
                if (GetRow(i).Name.GetText() == name)
                    return GetRow(i);
            }
            throw new Exception(string.Format("Не найдена строка с именем '{0}' в таблице", name));
        }

        public WarehousesRowControl GetRow(int index)
        {
            var row = new WarehousesRowControl(index + 1);
            row.WaitPresenceWithRetries();
            return row;
        }
    }
}