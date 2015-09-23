using System;
using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageFreshDesk
{
    public class SupportTicketsListControl : HtmlControl
    {
        public SupportTicketsListControl(By className)
            : base(className)
        {
        }

        public SupportTicketsListControl(string idLocator, HtmlControl container = null)
            : base(idLocator, container)
        {
        }

        public SupportTicketsRowControl GetRow(int index)
        {
            var row = new SupportTicketsRowControl(index + 1);
            row.WaitPresenceWithRetries();
            return row;
        }

        public SupportTicketsRowControl FindRowByName(string name)
        {
            for (var i = 0; i < Count; i++)
            {
                if (GetRow(i).TicketLink.GetText().Contains(name))
                    return GetRow(i);
            }
            throw new Exception(string.Format("не найдена строка с именем {0} в списке", name));
        }

        public int Count
        {
            get { return element.FindElements(By.ClassName("ticket-brief")).Count; }
        }
    }
}