using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class PaymentPriceCreatePage : AdminBaseListCreatePage
    {
        public PaymentPriceCreatePage()
        {
            Company = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            City = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 1));
        }

        public AutocompleteControl Company { get; set; }
        public AutocompleteControl City { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            City.WaitVisible();
        }
    }
}