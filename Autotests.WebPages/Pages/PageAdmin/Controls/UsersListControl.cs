using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class UsersListControl : BaseTableListControl
    {
        public UsersListControl(By className)
            : base(className)
        {
            RowSearch = new UsersRowSearchControl(By.XPath("//thead/tr[2]"));
        }

        public UsersRowControl GetRow(int index)
        {
            var row = new UsersRowControl(index + 1);
            row.WaitPresenceWithRetries();
            return row;
        }
        public UsersRowSearchControl RowSearch { get; set; }
    }
}