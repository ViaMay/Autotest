using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class PriceCreatePage : AdminPageBase
    {
        public PriceCreatePage()
        {
            Price = new TextInput(By.Name("price"));
            PriceOverFlow = new TextInput(By.Name("weight_overflow_price"));
            Route = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            CompanyName = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 1));
            Weight = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 2));
            Dimension = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 3));
            SaveButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }
        
        public TextInput Price { get; set; }
        public TextInput PriceOverFlow { get; set; }
        public AutocompleteControl Route { get; set; }
        public AutocompleteControl CompanyName { get; set; }
        public AutocompleteControl Weight { get; set; }
        public AutocompleteControl Dimension { get; set; }
        public ButtonInput SaveButton { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Price.WaitVisible();
        }
    }
}