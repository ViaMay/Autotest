using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebWorms.FuncTests;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class SupportFreshDeskPage : CommonPageBase
    {
        public SupportFreshDeskPage()
        {
            LabelDirectory = new StaticText(By.CssSelector("body > div > header > div > div > h1"));
        }

        public StaticText LabelDirectory { get; set; }
        
        public override void BrowseWaitVisible()
        {
            LabelDirectory.WaitVisible();
        }
    }
}