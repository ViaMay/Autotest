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

        public WarehousesRowControl GetRow(int index)
        {
            var row = new WarehousesRowControl(index);
            row.WaitPresenceWithRetries();
            return row;
        }
    }
}