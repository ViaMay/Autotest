using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.WebPages.Pages.PageUser;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class UserAdminShopCreatePage : AdminPageBase
    {
        public UserAdminShopCreatePage()
        {
            Name = new TextInput(By.Name("name"));
            Address = new TextInput(By.Name("address"));
            CompanyPickup = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            ManagersLegalEntity = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 1));
            Warehouse = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 2));

            CreateButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Name.WaitVisible();
            Address.WaitVisible();
            Warehouse.WaitVisible();
        }

        public TextInput Name { get; set; }
        public TextInput Address { get; set; }
        public AutocompleteControl CompanyPickup { get; set; }
        public AutocompleteControl Warehouse { get; set; }
        public AutocompleteControl ManagersLegalEntity { get; set; }
        public ButtonInput CreateButton { get; set; }
    }
}
