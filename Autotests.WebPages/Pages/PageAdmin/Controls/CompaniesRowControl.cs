using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class CompaniesRowControl
        : BaseRowControl
    {

        public CompaniesRowControl(int index)
            : base(index)
        {
            SinglePickup = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[13]", index)));
        }
        public StaticText SinglePickup { get; private set; }
    }
}