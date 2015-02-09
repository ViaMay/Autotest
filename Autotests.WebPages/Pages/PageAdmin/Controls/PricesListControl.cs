using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class PricesListControl : BaseTableListControl
    {
        public PricesListControl(By className)
            : base(className)
        {
        }

        public PricesRowControl GetRow(int index)
        {
            var row = new PricesRowControl(index);
            row.WaitPresenceWithRetries();
            return row;
        }
    }
}