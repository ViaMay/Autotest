using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class ShopsPage : AdminPageBase
    {
        public ShopsPage()
        {
            LabelDirectoryShops = new StaticText(By.CssSelector("legend"));
            ShopCreate = new Link(By.LinkText("Создать"));

            Table = new ShopsListControl(By.ClassName("table"));
        }

        public ShopsPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<ShopsPage>();
        }

        public StaticText LabelDirectoryShops { get; set; }
        public Link ShopCreate { get; set; }
        public ShopsListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryShops.WaitText(@"Справочник ""Интернет-Магазины""");
            ShopCreate.WaitVisible();
        }
    }
}