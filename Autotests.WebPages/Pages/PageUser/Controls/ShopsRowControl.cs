using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class ShopsRowControl
        : HtmlControl
    {

        public ShopsRowControl(int index)
            : base(By.XPath(string.Format("//tbody")))
        {
            Name = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[1]", index)));
            Address = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[2]", index)));

            OrdersCreateSelf = new Link(By.XPath(string.Format("//tbody/tr[{0}]/td[3]/strong[1]/a", index)));
            OrdersCreateCourier = new Link(By.XPath(string.Format("//tbody/tr[{0}]/td[3]/strong[2]/a", index)));
            ActionsEdit = new Link(By.XPath(string.Format("//tbody/tr[{0}]/td[3]/a[1]", index)));
            ActionsDelete = new Link(By.XPath(string.Format("//tbody/tr[{0}]/td[3]/a[2]", index)));
        }

        public StaticText Name { get; private set; }
        public StaticText Address { get; private set; }
        public Link OrdersCreateSelf { get; private set; }
        public Link OrdersCreateCourier { get; private set; }
        public Link ActionsEdit { get; private set; }
        public Link ActionsDelete { get; private set; }
    }
}