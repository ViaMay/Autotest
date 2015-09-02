using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class ManagersCreatePage : AdminBaseListCreatePage
    {
        public ManagersCreatePage()
        {
            Name = new TextInput(By.Name("name"));
            Phone = new TextInput(By.Name("phone"));
            Email = new TextInput(By.Name("email"));
            Sign = new TextInput(By.Name("sign"));
            
            User = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
        }

        public TextInput Name { get; set; }
        public TextInput Phone { get; set; }
        public TextInput Email { get; set; }
        public TextInput Sign { get; set; }

        public AutocompleteControl User { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Name.WaitVisible();
        }
    }
}