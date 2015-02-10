using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageUser.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class СalculatorPage : UserPageBase
    {
        public СalculatorPage()
        {
            CityFrom = new AutocompleteControl(By.Name("city_from__value__"));
            CityTo = new AutocompleteControl(By.Name("city_to__value__"));
            DeclaredPrice = new TextInput(By.Name("declared_price"));
            Shop = new AutocompleteControl(By.Name("shop__value__"));
            Weight = new TextInput(By.Name("weight"));
            DimensionSide1 = new TextInput(By.Name("dimension_side1"));
            DimensionSide2 = new TextInput(By.Name("dimension_side2"));
            DimensionSide3 = new TextInput(By.Name("dimension_side3"));

            СountedButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));

            TableFirst = new СalculatorListControl(By.XPath("//table[1]"));
            TableSecond = new СalculatorListControl(By.XPath("//table[2]"));
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            CityFrom.WaitVisible();
        }

        public AutocompleteControl CityFrom { get; set; }
        public AutocompleteControl CityTo { get; set; }
        public TextInput DeclaredPrice { get; set; }
        public AutocompleteControl Shop { get; set; }
        public TextInput Weight { get; set; }
        public TextInput DimensionSide1 { get; set; }
        public TextInput DimensionSide2 { get; set; }
        public TextInput DimensionSide3 { get; set; }

        public ButtonInput СountedButton { get; set; }


        public СalculatorListControl TableFirst { get; set; }
        public СalculatorListControl TableSecond { get; set; }
    }
}