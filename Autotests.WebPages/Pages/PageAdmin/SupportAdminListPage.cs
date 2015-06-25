using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class SupportAdminListPage : AdminPageBase
    {
        public SupportAdminListPage()
        {
            Create = new Link(By.LinkText("Создать"));
        }
        
        public Link Create { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Create.WaitVisible();
        }
    }
}