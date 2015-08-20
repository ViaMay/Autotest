using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class OrdersInputPage : AdminPageBase
    {
        public OrdersInputPage()
        {
            LabelDirectory = new StaticText(By.CssSelector("legend"));
            Table = new OrdersInputListControl(By.ClassName("table"));
        }

        public OrdersInputPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<OrdersInputPage>();
        }

        public StaticText LabelDirectory { get; set; }
        public OrdersInputListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitTextContains(@"Справочник ""Заявки");
        }
    }
}