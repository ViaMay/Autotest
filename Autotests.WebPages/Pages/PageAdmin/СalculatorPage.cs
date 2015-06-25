using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class СalculatorPage : AdminPageBase
    {
        public СalculatorPage()
        {
            LabelDirectory = new StaticText(By.CssSelector("legend"));
        }

        public StaticText LabelDirectory { get; set; }
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitVisible();
        }
    }
}