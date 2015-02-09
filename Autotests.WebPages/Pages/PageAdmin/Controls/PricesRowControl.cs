using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class PricesRowControl
        : BaseRowControl
    {

        public PricesRowControl(int index)
            : base(index)
        {
            CompanyName = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[8]", index)));
        }
        public StaticText CompanyName { get; private set; }
    }
}