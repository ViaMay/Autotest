using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class OrdersRowControl
        : HtmlControl
    {
        public OrdersRowControl(int index)
            : base(By.XPath(string.Format("//tbody")))
        {
            Checkbox = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[1]", index)));
            ID = new Link(By.XPath(string.Format("//tbody/tr[{0}]/td[2]/strong", index)));
            Type = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[3]", index)));
            Number = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[4]", index)));
            Date = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[5]", index)));
            Route = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[6]", index)));
            Recipient = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[7]", index)));
            Goods = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[8]", index)));
            Delivery = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[9]", index)));
            Status = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[10]", index)));
            Сonfirm = new ButtonInput(By.XPath(string.Format("//tbody/tr[{0}]/td[11]/form/button", index)));
            Undo = new ButtonInput(By.XPath(string.Format("//tbody/tr[{0}]/td[11]/form/button", index)));
            Edit = new ButtonInput(By.XPath(string.Format("//tbody/tr[{0}]/td[11]/a", index)));
        }

        public StaticText Checkbox { get; private set; }
        public Link ID { get; private set; }
        public StaticText Type { get; private set; }
        public StaticText Number { get; private set; }
        public StaticText Date { get; private set; }
        public StaticText Route { get; private set; }
        public StaticText Recipient { get; private set; }
        public StaticText Goods { get; private set; }
        public StaticText Delivery { get; private set; }
        public StaticText Status { get; private set; }
        public ButtonInput Сonfirm { get; private set; }
        public ButtonInput Undo { get; private set; }
        public ButtonInput Edit { get; private set; }
    }
}