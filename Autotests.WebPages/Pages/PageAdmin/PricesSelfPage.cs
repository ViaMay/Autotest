using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class PricesSelfPage : AdminPageBase
    {
        public PricesSelfPage()
        {
            LabelDirectoryPricesSelf = new StaticText(By.CssSelector("legend"));
            PriceSelfCreate = new Link(By.LinkText("Создать"));

            Table = new PricesListControl(By.ClassName("table"));
        }


        public PricesSelfPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<PricesSelfPage>();
        }

        public StaticText LabelDirectoryPricesSelf { get; set; }
        public Link PriceSelfCreate { get; set; }
        public PricesListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryPricesSelf.WaitText(@"Справочник ""Цены на самовывоз""");
            PriceSelfCreate.WaitVisible();
        }
    }
}