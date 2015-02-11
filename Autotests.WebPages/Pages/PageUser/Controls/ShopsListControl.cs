using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class ShopsListControl : HtmlControl
    {
        public ShopsListControl(By className)
            : base(className)
        {
        }

        public ShopsRowControl GetRow(int index)
        {
            var row = new ShopsRowControl(index + 1);
            row.WaitPresenceWithRetries();
            return row;
        }
    }
}