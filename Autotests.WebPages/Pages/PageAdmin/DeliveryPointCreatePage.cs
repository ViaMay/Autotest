using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class DeliveryPointCreatePage : AdminPageBase
    {
        public DeliveryPointCreatePage()
        {
            City = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            CompanyName = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 1));
            DeliveryPointName = new TextInput(By.Name("name"));
            CompanyCode = new TextInput(By.Name("company_code"));
            Address = new TextInput(By.Name("address"));

            SaveButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }

        public AutocompleteControl City { get; set; }
        public AutocompleteControl CompanyName { get; set; }
        public TextInput DeliveryPointName { get; set; }
        public TextInput CompanyCode { get; set; }
        public TextInput Address { get; set; }

        public ButtonInput SaveButton { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            City.WaitVisible();
        }
    }
}