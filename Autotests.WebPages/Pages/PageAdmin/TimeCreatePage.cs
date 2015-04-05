using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class TimeCreatePage : AdminPageBase
    {
        public TimeCreatePage()
        {
            CompanyName = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 1));
            Route = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            MinTime = new TextInput(By.Name("min_time"));
            MaxTime = new TextInput(By.Name("max_time"));

            SaveButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }
        public TextInput MinTime { get; set; }
        public TextInput MaxTime { get; set; }
        public AutocompleteControl CompanyName { get; set; }
        public AutocompleteControl Route { get; set; }
        public ButtonInput SaveButton { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            MinTime.WaitVisible();
        }
    }
}