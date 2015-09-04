using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class SupportListControl : HtmlControl
    {
        public SupportListControl(By className)
            : base(className)
        {
        }

        public SupportRowControl GetRow(int index)
        {
            var row = new SupportRowControl(index + 1);
            row.WaitPresenceWithRetries();
            return row;
        }
    }
}