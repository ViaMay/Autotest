using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class AdminBaseListCreatePage : AdminPageBase
    {
        public AdminBaseListCreatePage()
        {
            LabelDirectory = new StaticText(By.CssSelector("legend"));
            SaveButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }

        public StaticText LabelDirectory { get; set; }
        public ButtonInput SaveButton { get; set; }

        public override void BrowseWaitVisible()
        { 
            base.BrowseWaitVisible();
            LabelDirectory.WaitVisible();
        }
    }
}