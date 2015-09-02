using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class OrderedIttemplatesCreatePage : AdminBaseListCreatePage
    {
        public OrderedIttemplatesCreatePage()
        {
            CompanyName = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));

            Through = new Select(By.Name("through"));
            Action = new Select(By.Name("action"));
            ThroughEmail = new TextInput(By.Name("through_email"));
            Subject = new TextInput(By.Name("subject"));
            Message = new TextInput(By.Name("message"));
        }

        public AutocompleteControl CompanyName { get; set; }
        public Select Through { get; set; }
        public Select Action { get; set; }
        public TextInput ThroughEmail { get; set; }
        public TextInput Subject { get; set; }
        public TextInput Message { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Subject.WaitVisible();
        }
    }
}