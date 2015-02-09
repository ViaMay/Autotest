using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class PricesPickupListControl : HtmlControl
    {
        public PricesPickupListControl(By className)
            : base(className)
        {
            RowSearch = new BaseRowSearchControl(By.XPath("//thead/tr[2]"));
        }

        public PricesPickupRowControl GetRow(int index)
        {
            var row = new PricesPickupRowControl(index);
            row.WaitPresenceWithRetries();
            return row;
        }

        public BaseRowSearchControl RowSearch { get; set; }
    }
}