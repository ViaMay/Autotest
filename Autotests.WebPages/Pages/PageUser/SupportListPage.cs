using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageUser.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class SupportListPage : UserPageBase
    {
        public SupportListPage()
        {
            SupportCreateNew = new Link(By.LinkText("Создать новый"));
            Table = new SupportListControl(By.ClassName("table"));
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Table.WaitVisible();
        }
        public Link SupportCreateNew { get; set; }
        public SupportListControl Table { get; set; }
    }
}
