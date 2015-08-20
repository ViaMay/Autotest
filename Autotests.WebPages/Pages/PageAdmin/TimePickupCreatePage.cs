using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class TimePickupCreatePage : AdminBaseListCreatePage
    {
        public TimePickupCreatePage()
        {
            CompanyName = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            City = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 1));
            MinTime = new TextInput(By.Name("min_time"));
            MaxTime = new TextInput(By.Name("max_time"));
        }
        public TextInput MinTime { get; set; }
        public TextInput MaxTime { get; set; }
        public AutocompleteControl CompanyName { get; set; }
        public AutocompleteControl City { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            MinTime.WaitVisible();
        }
    }
}