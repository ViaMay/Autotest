using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class UsersWarehousesPage : AdminPageBase
    {
        public UsersWarehousesPage()
        {
            LabelDirectoryWarehouses = new StaticText(By.CssSelector("legend"));
            WarehousesCreate = new Link(By.LinkText("Создать"));

            Table = new BaseTableListControl(By.ClassName("table"));
        }


        public UsersWarehousesPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<UsersWarehousesPage>();
        }

        public StaticText LabelDirectoryWarehouses { get; set; }
        public Link WarehousesCreate { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryWarehouses.WaitText(@"Справочник ""Склады""");
        }
    }
}