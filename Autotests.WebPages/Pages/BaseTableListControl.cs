using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class BaseTableListControl : HtmlControl
    {
        public BaseTableListControl(By className)
            : base(className)
        {
            RowSearch = new BaseRowSearchControl(By.XPath("//thead/tr[2]"));
        }

        public BaseRowControl GetRow(int index)
        {
            var row = new BaseRowControl(index + 1);
            row.WaitPresenceWithRetries();
            return row;
        }

        public BaseRowSearchControl RowSearch { get; set; }
    }
}