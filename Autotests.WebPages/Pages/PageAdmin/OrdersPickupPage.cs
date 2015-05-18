using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class OrdersPickupPage : AdminPageBase
    {
        public OrdersPickupPage()
        {
            LabelDirectoryOrdersPickup = new StaticText(By.CssSelector("legend"));
            Table = new BaseTableListControl(By.ClassName("table"));
        }

        public OrdersPickupPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<OrdersPickupPage>();
        }

        public StaticText LabelDirectoryOrdersPickup { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryOrdersPickup.WaitText(@"Справочник ""Заявки на забор""");
        }
    }
}