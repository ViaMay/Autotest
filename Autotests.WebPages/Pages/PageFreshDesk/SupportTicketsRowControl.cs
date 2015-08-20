using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageFreshDesk
{
    public class SupportTicketsRowControl
        : HtmlControl
    {
        public SupportTicketsRowControl(int index)
            : base("ticket-list")
        {
            TicketLink = new Link(By.XPath(string.Format("//*[@id='ticket-list']/div[1]/div[{0}]/div/div[1]/a", index)));
            TicketInfo = new StaticText(By.XPath(string.Format("//*[@id='ticket-list']/div[1]/div[{0}]/div/div[2]", index)));
            TicketStatus = new StaticText(By.XPath(string.Format("//*[@id='ticket-list']/div[1]/div[{0}]/span[2]", index)));
            
        }

        public StaticText TicketInfo { get; private set; }
        public StaticText TicketStatus { get; private set; }
        public Link TicketLink { get; private set; }
        
    }
}