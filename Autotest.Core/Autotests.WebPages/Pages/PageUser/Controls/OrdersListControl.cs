using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class OrdersListControl : HtmlControl
    {
        public OrdersListControl(By className)
            : base(className)
        {
        }

        public OrdersRowControl GetRow(int index)
        {
            var row = new OrdersRowControl(index + 1);
            row.WaitPresenceWithRetries();
            return row;
        }
    }
}