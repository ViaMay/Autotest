using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class SupportRowControl
        : HtmlControl
    {
        public SupportRowControl(int index)
            : base(By.XPath(string.Format("//tbody")))
        {
            TicketId = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[1]", index)));
            Time = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[2]/strong", index)));
            TicketText = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[3]", index)));
            Content = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[4]", index)));
            Status = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[5]", index)));
        }

        public StaticText TicketId { get; private set; }
        public StaticText Time { get; private set; }
        public StaticText TicketText { get; private set; }
        public StaticText Content { get; private set; }
        public StaticText Status { get; private set; }
    }
}