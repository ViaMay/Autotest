using System;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageUser.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class СalculatorListAdminControl : HtmlControl
    {
        public СalculatorListAdminControl(By className)
            : base(className)
        {
        }

        public СalculatorRowAdminControl GetRow(int index)
        {
            var row = new СalculatorRowAdminControl(index);
            return row;
        }

        public int Count
        {
            get { return element.FindElements(By.XPath("tbody/tr[@class='warning']")).Count; }
        }

        public СalculatorRowAdminControl FindRowByName(string name)
        {
            for (var i = 0; i < Count; i++)
            {
                if (GetRow(i).CompanyDeliveryName.GetText() == name)
                    return GetRow(i);
            }
            throw new Exception(string.Format("Не найдена строка с именем '{0}' в таблице", name));
        }
    }
}