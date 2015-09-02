using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class PricesPage : AdminBaseListPage
    {
        public PricesPage()
        {
            Table = new PricesListControl(By.ClassName("table"));
        }


        public PricesPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<PricesPage>();
        }
        public PricesListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitTextContains(@"Справочник ""Цены на");
            Create.WaitVisible();
        }
    }
}