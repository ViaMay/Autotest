using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class ManagersPage : AdminPageBase
    {
        public ManagersPage()
        {
            LabelDirectoryManagers = new StaticText(By.CssSelector("legend"));
            ManagersCreate = new Link(By.LinkText("Создать"));
            Table = new BaseTableListControl(By.ClassName("table"));
        }

        public ManagersPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<ManagersPage>();
        }

        public StaticText LabelDirectoryManagers { get; set; }
        public Link ManagersCreate { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryManagers.WaitText(@"Справочник ""Менеджеры""");
            ManagersCreate.WaitVisible();
        }
    }
}