using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class PricesCourierPage : AdminPageBase
    {
        public PricesCourierPage()
        {
            LabelDirectoryPricesCourier = new StaticText(By.CssSelector("legend"));
            PriceCourierCreate = new Link(By.LinkText("Создать"));
            Table = new PricesListControl(By.ClassName("table"));
        }


        public PricesCourierPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<PricesCourierPage>();
        }

        public StaticText LabelDirectoryPricesCourier { get; set; }
        public Link PriceCourierCreate { get; set; }
        public PricesListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryPricesCourier.WaitText(@"Справочник ""Цены на курьерку""");
            PriceCourierCreate.WaitVisible();
        }
    }
}