using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class OrderInputEditingPage : AdminPageBase
    {
        public OrderInputEditingPage()
        {
            LabelDirectory = new StaticText(By.CssSelector("legend"));
            PickupDate = new StaticText(By.Name("pickup_date"));
            TransferCDDate = new StaticText(By.Name("transfer_date"));
            ProcessDate = new StaticText(By.Name("process_date"));
        }

        public StaticText LabelDirectory { get; set; }
        public StaticText PickupDate { get; set; }
        public StaticText TransferCDDate { get; set; }
        public StaticText ProcessDate { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitTextContains("Заявка");
        }
    }
}