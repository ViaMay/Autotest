using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class WarehousesRowControl
        : HtmlControl
    {

        public WarehousesRowControl(int index)
            : base(By.XPath(string.Format("//tbody")))
        {
            Name = new Link(By.XPath(string.Format("//tbody/tr[{0}]/td[1]", index)));
            Documents = new Link(By.XPath(string.Format("//tbody/tr[{0}]/td[2]", index)));
            City = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[3]", index)));
            Address = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[4]", index)));
            Contact = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[5]", index)));
            TimeWork = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[6]", index)));
        }
        public Link Name { get; private set; }
        public Link Documents { get; private set; }
        public StaticText City { get; private set; }
        public StaticText Address { get; private set; }
        public StaticText Contact { get; private set; }
        public StaticText TimeWork { get; private set; }
    }
}