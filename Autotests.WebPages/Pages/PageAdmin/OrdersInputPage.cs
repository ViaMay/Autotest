using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class OrdersInputPage : AdminPageBase
    {
        public OrdersInputPage()
        {
            LabelDirectoryOrdersInput = new StaticText(By.CssSelector("legend"));
            Table = new OrdersInputListControl(By.ClassName("table"));
        }

        public OrdersInputPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<OrdersInputPage>();
        }

        public StaticText LabelDirectoryOrdersInput { get; set; }
        public OrdersInputListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryOrdersInput.WaitText(@"Справочник ""Заявки""");
        }
    }
}