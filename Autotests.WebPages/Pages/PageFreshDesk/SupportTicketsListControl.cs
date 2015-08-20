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

        public SupportTicketsRowControl GetRow(int index)
        {
            var row = new SupportTicketsRowControl(index + 1);
            row.WaitPresenceWithRetries();
            return row;
        }
    }
}