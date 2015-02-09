using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class UsersPage : AdminPageBase
    {
        public UsersPage()
        {
            LabelDirectoryUsers = new StaticText(By.CssSelector("legend"));
            UsersCreate = new Link(By.LinkText("Создать"));

            UsersTable = new UsersListControl(By.ClassName("table"));
        }


        public UsersPage SeachButtonRowClickAndGo()
        {
            UsersTable.RowSearch.SeachButton.Click();
            return GoTo<UsersPage>();
        }

        public StaticText LabelDirectoryUsers { get; set; }
        public Link UsersCreate { get; set; }
        public UsersListControl UsersTable { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryUsers.WaitText(@"Справочник ""Пользователи""");
            UsersCreate.WaitVisible();
        }
    }
}