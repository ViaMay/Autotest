using System.Web.UI.WebControls;
using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class OrderOutputEditingPage : AdminPageBase
    {
        public OrderOutputEditingPage()
        {
            LabelDirectoryOrderInput = new StaticText(By.CssSelector("legend"));
            TransferCDDate = new StaticText(By.Name("pickup_date"));
            TestSentToTC = new ButtonInput(By.LinkText("Тест отправки в ТК"));
        }



        public StaticText LabelDirectoryOrderInput { get; set; }
        public StaticText PickupDate { get; set; }
        public StaticText TransferCDDate { get; set; }
        public ButtonInput TestSentToTC { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryOrderInput.WaitTextContains("Исходящая заявка");
        }
    }
}