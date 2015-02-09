using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class UserCreatePage : AdminPageBase
    {
        public UserCreatePage()
        {
            UserEmail = new TextInput(By.Name("username"));
            UserPassword = new TextInput(By.Name("password"));
            OfficialName = new TextInput(By.Name("official_name"));

            UserGroups = new AutocompleteControl(By.Name("__groups_search_field"));
            UserGroupsAddButton = new ButtonInput(By.XPath("//button[@type='button']"));

            SaveButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }

        public TextInput UserEmail { get; set; }
        public TextInput UserPassword { get; set; }
        public TextInput OfficialName { get; set; }

        public AutocompleteControl UserGroups { get; set; }
        public ButtonInput UserGroupsAddButton { get; set; }
        
        public ButtonInput SaveButton { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            UserEmail.WaitVisible();
        }
    }
}