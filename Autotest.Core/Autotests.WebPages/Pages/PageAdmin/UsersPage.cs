using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class UsersPage : AdminBaseListPage
    {
        public UsersPage()
        {
            UsersTable = new UsersListControl(By.ClassName("table"));
        }


        public UsersPage SeachButtonRowClickAndGo()
        {
            UsersTable.RowSearch.SeachButton.Click();
            return GoTo<UsersPage>();
        }

        public UsersListControl UsersTable { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitText(@"Справочник ""Пользователи""");
            Create.WaitVisible();
        }
    }
}