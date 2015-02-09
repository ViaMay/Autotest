using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class PriceCreatePage : AdminPageBase
    {
        public PriceCreatePage()
        {
            Price = new TextInput(By.Name("price"));
            PriceOverFlow = new TextInput(By.Name("weight_overflow_price"));
            Route = new AutocompleteControl(By.Name("route__value__"));
            CompanyName = new AutocompleteControl(By.Name("company__value__"));
            Weight = new AutocompleteControl(By.Name("weight__value__"));
            Dimension = new AutocompleteControl(By.Name("dimension__value__"));
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