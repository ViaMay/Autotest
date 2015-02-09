using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class PricesPickupPage : AdminPageBase
    {
        public PricesPickupPage()
        {
            LabelDirectoryPricesPickup = new StaticText(By.CssSelector("legend"));
            PricePickupCreate = new Link(By.LinkText("Создать"));

            Table = new PricesPickupListControl(By.ClassName("table"));
        }


        public PricesPickupPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<PricesPickupPage>();
        }

        public StaticText LabelDirectoryPricesPickup { get; set; }
        public Link PricePickupCreate { get; set; }
        public PricesPickupListControl Table { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryPricesPickup.WaitText(@"Справочник ""Цены забора""");
            PricePickupCreate.WaitVisible();
        }
    }
}