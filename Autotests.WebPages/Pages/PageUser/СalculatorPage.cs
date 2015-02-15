using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.WebPages.Pages.PageUser.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class СalculatorPage : UserPageBase
    {
        public СalculatorPage()
        {
            CityFromConbobox = new ComboboxControl(BY.NthOfClass("combobox-container", 0));
            CityFrom = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));

            CityToConbobox = new ComboboxControl(BY.NthOfClass("combobox-container", 1));
            CityTo = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 1));

            ShopConbobox = new ComboboxControl(BY.NthOfClass("combobox-container", 2));
            Shop = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 2));

            DeclaredPrice = new TextInput(By.Name("declared_price"));
            Weight = new TextInput(By.Name("weight"));

            Width = new TextInput(By.Name("dimension_side1"));
            Height = new TextInput(By.Name("dimension_side2"));
            Length = new TextInput(By.Name("dimension_side3"));

            WidthErrorText = new StaticText(BY.NthOfClass("help-inline", 0));
            HeightErrorText = new StaticText(BY.NthOfClass("help-inline", 1));
            LengthErrorText = new StaticText(BY.NthOfClass("help-inline", 2));

            СountedButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));

            TableFirst = new СalculatorListControl(By.XPath("//table[1]"));
            TableSecond = new СalculatorListControl(By.XPath("//table[2]"));

            AlertErrorText = new AlertErrorTextControl(By.ClassName("form-horizontal"), null);
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            CityFrom.WaitVisible();
        }

        public AutocompleteControl CityFrom { get; set; }
        public ComboboxControl CityFromConbobox { get; set; }
        public AutocompleteControl CityTo { get; set; }
        public ComboboxControl CityToConbobox { get; set; }
        public StaticText HeightErrorText { get; set; }
        public StaticText LengthErrorText { get; set; }

        public ButtonInput СountedButton { get; set; }

        public СalculatorListControl TableFirst { get; set; }
        public СalculatorListControl TableSecond { get; set; }
        public TextInput DeclaredPrice { get; set; }
        public AutocompleteControl Shop { get; set; }
        public ComboboxControl ShopConbobox { get; set; }
        public TextInput Weight { get; set; }

        public TextInput Width { get; set; }
        public TextInput Height { get; set; }
        public TextInput Length { get; set; }

        public StaticText WidthErrorText { get; set; }

        public AlertErrorTextControl AlertErrorText { get; set; }
    }
}