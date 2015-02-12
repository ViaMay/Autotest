using Autotests.WebPages.Pages.PageUser.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class OrdersListPage : UserPageBase
    {
        public OrdersListPage()
        {
            Table = new OrdersListControl(By.ClassName("table"));
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Table.WaitVisible();
        }

        public OrdersListControl Table { get; set; }
    }
}
