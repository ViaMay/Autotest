using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class PickupTimetableCreatePage : AdminBaseListCreatePage
    {
        public PickupTimetableCreatePage()
        {
            Company = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            PickupTime = new Select(By.Name("pickup_time"));
            PickupPeriod = new Select(By.Name("delta"));
        }

        public Select PickupTime { get; set; }
        public Select PickupPeriod { get; set; }
        public AutocompleteControl Company { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Company.WaitVisible();
        }
    }
}