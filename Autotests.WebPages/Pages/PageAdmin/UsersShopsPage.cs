using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class UsersShopsPage : AdminPageBase
    {
        public UsersShopsPage()
        {
            LabelDirectoryShops = new StaticText(By.CssSelector("legend"));
            ShopsCreate = new Link(By.LinkText("Создать"));

            Table = new ShopsListControl(By.ClassName("table"));
        }


        public UsersShopsPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<UsersShopsPage>();
        }

        public StaticText LabelDirectoryShops { get; set; }
        public Link ShopsCreate { get; set; }
        public ShopsListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryShops.WaitText(@"Справочник ""Интернет-Магазины""");
        }
    }
}