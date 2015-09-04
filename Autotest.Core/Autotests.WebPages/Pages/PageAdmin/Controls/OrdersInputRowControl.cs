using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class OrdersInputRowControl
        : HtmlControl
    {

        public OrdersInputRowControl(int index)
            : base(By.XPath(string.Format("//tbody")))
        {
            ID = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[1]", 1 + 3 * index)));
            OrderOutput = new Link(By.XPath(string.Format("//tbody/tr[{0}]/*/a[1]", 3 + 3 * index)));
            MoreInfo = new Link(BY.NthOfBy(By.LinkText("Подробнее..."), index));
        }
        public StaticText ID { get; private set; }
        public Link MoreInfo { get; private set; }
        public Link OrderOutput { get; private set; }
    }
}