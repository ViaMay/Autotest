using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class PricesPickupRowControl
        : BaseRowControl
    {

        public PricesPickupRowControl(int index)
            : base(index)
        {
            CompanyName = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[2]", index)));
        }
        public StaticText CompanyName { get; private set; }
    }
}