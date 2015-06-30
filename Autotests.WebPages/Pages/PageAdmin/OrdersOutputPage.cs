using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class OrdersOutputPage : AdminPageBase
    {
        public OrdersOutputPage()
        {
            LabelDirectory = new StaticText(By.CssSelector("legend"));
            Table = new BaseTableListControl(By.ClassName("table"));
        }

        public OrdersOutputPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<OrdersOutputPage>();
        }

        public StaticText LabelDirectory { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitText(@"Справочник ""Исходящие заявки""");
        }
    }
}