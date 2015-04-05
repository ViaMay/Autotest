using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class СompanyCreatePage : AdminPageBase
    {
        public СompanyCreatePage()
        {
            Name = new TextInput(By.Name("name"));
            CompanyDriver = new Select(By.Name("driver"));
            CompanyAddress = new TextInput(By.Name("address"));
            CompanyPickup = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            CompanyPickupAddButton = new ButtonInput(By.XPath("//button[@type='button']"));

            Manager = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 1));
            ManagersPickup = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 2));
            ManagersLegalEntity = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 3));

            SinglePickup = new CheckBox(By.Name("single_pickup"));
            SaveButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }

        public TextInput Name { get; set; }
        public Select CompanyDriver { get; set; }
        public TextInput CompanyAddress { get; set; }
        public AutocompleteControl CompanyPickup { get; set; }
        public ButtonInput CompanyPickupAddButton { get; set; }

        public AutocompleteControl Manager { get; set; }
        public AutocompleteControl ManagersPickup { get; set; }
        public AutocompleteControl ManagersLegalEntity { get; set; }

        public CheckBox SinglePickup { get; set; }
        public ButtonInput SaveButton { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Name.WaitVisible();
        }
    }
}