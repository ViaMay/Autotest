using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class CompaniesListControl : BaseTableListControl
    {
        public CompaniesListControl(By className)
            : base(className)
        {
        }

        public CompaniesRowControl GetRow(int index)
        {
            var row = new CompaniesRowControl(index + 1);
            row.WaitPresenceWithRetries();
            return row;
        }
    }
}