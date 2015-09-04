using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageFreshDesk
{
    public class SupportTicketOpenPage : SupportFreshDeskPage
    {
        public SupportTicketOpenPage()
        {
            TicketStatus = new StaticText(By.CssSelector("div.alert.alert-ticket-status"));
            TicketHeading = new StaticText(By.XPath("//section/h2[@class='heading']"));
            TicketInfo = new StaticText(By.XPath("//*[@id='ticket-description']/div[2]/div"));
        }

        public StaticText TicketStatus { get; set; }
        public StaticText TicketHeading { get; set; }
        public StaticText TicketInfo { get; set; }

        public override void BrowseWaitVisible()
        {
            TicketStatus.WaitVisible();
        }
    }
}