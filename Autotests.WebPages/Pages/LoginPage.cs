using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebWorms.FuncTests;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class LoginPage : CommonPageBase
    {
        public LoginPage()
        {
            LoginInput = new TextInput(By.Name("username"));
            PasswordInput = new TextInput(By.Name("password"));
            LoginButton = new Link(By.ClassName("btn"));
        }

        public TextInput LoginInput { get; set; }
        public Link LoginButton { get; set; }
        public TextInput PasswordInput { get; set; }

        public override void BrowseWaitVisible()
        {
            LoginInput.WaitVisible();
            PasswordInput.WaitVisible();
            LoginButton.WaitVisible();
        }

        public UserHomePage LoginAsUser(string login, string password)
        {
            LoginInput.SetValue(login);
            PasswordInput.SetValue(password);
            LoginButton.Click();
            return GoTo<UserHomePage>();
        }

        public AdminHomePage LoginAsAdmin(string login, string password)
        {
            LoginInput.SetValue(login);
            PasswordInput.SetValue(password);
            LoginButton.Click();
            return GoTo<AdminHomePage>();
        }
        
        public LoginPage InvalidLogin()
        {
            LoginButton.Click();
            return GoTo<LoginPage>();
        }
    }
}