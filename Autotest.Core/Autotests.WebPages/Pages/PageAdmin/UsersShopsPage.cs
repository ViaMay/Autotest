using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class UsersShopsPage : AdminBaseListPage
    {
        public UsersShopsPage()
        {
            Table = new ShopsListControl(By.ClassName("table"));
        }


        public UsersShopsPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<UsersShopsPage>();
        }

        public ShopsListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitText(@"Справочник ""Интернет-Магазины""");
        }
    }
}