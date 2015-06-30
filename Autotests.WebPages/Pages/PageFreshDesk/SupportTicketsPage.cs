using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebWorms.FuncTests;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageFreshDesk
{
    public class SupportTicketsPage : SupportFreshDeskPage
    {
        public SupportTicketsPage()
        {

        }


        
        public override void BrowseWaitVisible()
        {
            LabelDirectory.WaitVisible();
        }
    }
}