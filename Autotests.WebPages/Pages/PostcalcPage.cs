using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebWorms.FuncTests;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class PostcalcPage : CommonPageBase
    {
        public PostcalcPage()
        {
            PriceParcel = new StaticText(By.XPath("//tbody/tr[5]/td[2]/pre"));
        }

        public StaticText PriceParcel { get; private set; }

        public override void BrowseWaitVisible()
        {
            PriceParcel.WaitVisible();
        }
    }
}