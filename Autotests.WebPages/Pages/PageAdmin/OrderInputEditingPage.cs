using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class OrderInputEditingPage : AdminPageBase
    {
        public OrderInputEditingPage()
        {
            LabelDirectoryOrderInput = new StaticText(By.CssSelector("legend"));
            PickupDate = new StaticText(By.Name("pickup_date"));
            TransferCDDate = new StaticText(By.Name("transfer_date"));
        }



        public StaticText LabelDirectoryOrderInput { get; set; }
        public StaticText PickupDate { get; set; }
        public StaticText TransferCDDate { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryOrderInput.WaitTextContains("Заявка");
        }
    }
}