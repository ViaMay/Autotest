using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class CalculatorPage : AdminPageBase
    {
        public CalculatorPage()
        {
            LabelDirectory = new StaticText(By.CssSelector("legend"));
            CityFromConbobox = new ComboboxControl(BY.NthOfClass("combobox-container", 0));
            CityFrom = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));

            CityToConbobox = new ComboboxControl(BY.NthOfClass("combobox-container", 1));
            CityTo = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 1));

            ShopConbobox = new ComboboxControl(BY.NthOfClass("combobox-container", 2));
            Shop = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 2));

            Weight = new TextInput(By.Name("weight"));
            Width = new TextInput(By.Name("dimension_side1"));
            Height = new TextInput(By.Name("dimension_side2"));
            Length = new TextInput(By.Name("dimension_side3"));

            PriceDeclared = new TextInput(By.Name("declared_price"));
            PricePayment = new TextInput(By.Name("payment_price"));

            RadioButtonList = new RadioButtonListControl(By.Name("controls"));

            СountedButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));

            Table = new СalculatorListAdminControl(By.XPath("//table"));
        }
        public СalculatorListAdminControl Table { get; set; }

        public AutocompleteControl CityFrom { get; set; }
        public ComboboxControl CityFromConbobox { get; set; }
        public AutocompleteControl CityTo { get; set; }
        public ComboboxControl CityToConbobox { get; set; }
        public AutocompleteControl Shop { get; set; }
        public ComboboxControl ShopConbobox { get; set; }

        public TextInput PriceDeclared { get; set; }
        public TextInput PricePayment { get; set; }
        public TextInput Weight { get; set; }
        public TextInput Width { get; set; }
        public TextInput Height { get; set; }
        public TextInput Length { get; set; }

        public RadioButtonListControl RadioButtonList { get; set; }
        public ButtonInput СountedButton { get; set; }
        public StaticText LabelDirectory { get; set; }
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitVisible();
        }
    }
}