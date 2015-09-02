using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class AdminMaintenancePage : AdminPageBase
    {
        public AdminMaintenancePage()
        {
            LabelDirectory = new StaticText(By.CssSelector("legend"));
            AlertText = new StaticText(By.CssSelector("div.container > div.alert.alert-info"));
        }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitVisible();
        }
        public StaticText LabelDirectory { get; set; }
        public StaticText AlertText { get; set; }
    }
}