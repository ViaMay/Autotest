using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class DocumentsPreparedRowControl
        : HtmlControl
    {

        public DocumentsPreparedRowControl(int index)
            : base(By.XPath(string.Format("//tbody")))
        {
            Name = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td/a", index)));
        }

        public StaticText Name { get; private set; }
        public StaticText Address { get; private set; }
        public Link OrdersCreateSelf { get; private set; }
        public Link OrdersCreateCourier { get; private set; }
        public Link ActionsEdit { get; private set; }
        public Link ActionsDelete { get; private set; }
    }
}