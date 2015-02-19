using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class AdminMaintenancePage : AdminPageBase
    {
        public AdminMaintenancePage()
        {
            AlertText = new StaticText(By.CssSelector("div.container > div.alert.alert-info"));
        }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            AlertText.WaitVisible();
        }
        public StaticText AlertText { get; set; }
    }
}