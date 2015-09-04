using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class СalculatorRowAdminControl
        : HtmlControl
    {
        public СalculatorRowAdminControl(int index)
            : base(By.XPath(string.Format("//table")))
        {
            CompanyDeliveryName = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[1]", 3 + 5*index)));
            PriceDeliveryBase = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[7]", 3 + 5 * index)));
        }

        public StaticText CompanyDeliveryName { get; private set; }
        public StaticText PriceDeliveryBase { get; private set; }
    }
}