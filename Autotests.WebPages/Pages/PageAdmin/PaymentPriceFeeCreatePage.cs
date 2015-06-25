using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class PaymentPriceCreateFeePage : AdminBaseListCreatePage
    {
        public PaymentPriceCreateFeePage()
        {
            Company = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            From = new TextInput(By.Name("from"));
            MinCommission = new TextInput(By.Name("min"));
            Percent = new TextInput(By.Name("percent"));
            PercentCard = new TextInput(By.Name("percent_card"));
        }

        public AutocompleteControl Company { get; set; }
        public TextInput From { get; set; }
        public TextInput MinCommission { get; set; }
        public TextInput Percent { get; set; }
        public TextInput PercentCard { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Company.WaitVisible();
        }
    }
}