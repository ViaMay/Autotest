using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class OrdersInputListControl : BaseTableListControl
    {
        public OrdersInputListControl(By className)
            : base(className)
        {
        }

        public OrdersInputRowControl GetRow(int index)
        {
            var row = new OrdersInputRowControl(index);
            row.WaitPresenceWithRetries();
            return row;
        }
    }
}