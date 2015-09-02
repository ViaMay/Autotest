using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class UsersRowSearchControl : BaseRowSearchControl
    {
        public UsersRowSearchControl(By className)
            : base(className)
        {
            UserEmail = new TextInput(By.Name("username"));
            OfficialName = new TextInput(By.Name("official_name"));
            Logins = new TextInput(By.Name("logins"));
            UserLastLogin = new TextInput(By.Name("last_login"));
            UserActive = new Select(By.Name("active"));
        }

        public TextInput UserEmail { get; private set; }
        public TextInput OfficialName { get; private set; }
        public TextInput Logins { get; private set; }
        public TextInput UserLastLogin { get; private set; }
        public Select UserActive { get; private set; }
    }
}