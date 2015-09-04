using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class PricesPickupPage : AdminBaseListPage
    {
        public PricesPickupPage()
        {
            Table = new PricesPickupListControl(By.ClassName("table"));
        }


        public PricesPickupPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<PricesPickupPage>();
        }

        public PricesPickupListControl Table { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitText(@"Справочник ""Цены забора""");
            Create.WaitVisible();
        }
    }
}