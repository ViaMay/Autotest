using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class CalendarCreatePage : AdminBaseListCreatePage
    {
        public CalendarCreatePage()
        {
            Date = new TextInput(By.Name("date"));
            Company = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            Type = new Select(By.Name("type"));
        }

        public TextInput Date { get; set; }
        public Select Type { get; set; }
        public AutocompleteControl Company { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Company.WaitVisible();
        }
    }
}