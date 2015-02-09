using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class СalculatorRowControl
        : HtmlControl
    {
        public СalculatorRowControl(int index, string locator)
            : base(By.XPath(string.Format(locator)))
        {
            Company = new StaticText(By.XPath(string.Format("{1}/tbody/tr[{0}]/td[1]", index, locator)));
            TimeDelivery = new StaticText(By.XPath(string.Format("{1}/tbody/tr[{0}]/td[2]", index, locator)));
            PriceDelivery = new StaticText(By.XPath(string.Format("{1}/tbody/tr[{0}]/td[3]", index, locator)));
            PricePickup = new StaticText(By.XPath(string.Format("{1}/tbody/tr[{0}]/td[4]", index, locator)));
        }

        public StaticText Company { get; private set; }
        public StaticText TimeDelivery { get; private set; }
        public StaticText PriceDelivery { get; private set; }
        public StaticText PricePickup { get; private set; }
    }
}