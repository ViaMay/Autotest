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
            Longitude = new TextInput(By.Name("longitude"));
            Latitude = new TextInput(By.Name("latitude"));

            HasFittingRoom = new CheckBox(By.Name("has_fitting_room"));
            IsCash = new CheckBox(By.Name("is_cash"));
            IsCard = new CheckBox(By.Name("is_card"));

            SaveButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }

        public AutocompleteControl City { get; set; }
        public AutocompleteControl CompanyName { get; set; }
        public TextInput DeliveryPointName { get; set; }
        public TextInput CompanyCode { get; set; }
        public TextInput Address { get; set; }
        public TextInput Longitude { get; set; }
        public TextInput Latitude { get; set; }
        public CheckBox HasFittingRoom { get; set; }
        public CheckBox IsCash { get; set; }
        public CheckBox IsCard { get; set; }

        public ButtonInput SaveButton { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            City.WaitVisible();
        }
    }
}