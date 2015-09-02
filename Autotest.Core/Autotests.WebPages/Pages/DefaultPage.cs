using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebWorms.FuncTests;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class DefaultPage : CommonPageBase
    {
        public DefaultPage()
        {
            LoginButton = new Link(By.LinkText("Вход в личный кабинет"));
        }

        public Link LoginButton { get; private set; }

        public override void BrowseWaitVisible()
        {
            LoginButton.WaitVisible();
        }

        public LoginPage LoginButtonClickAndGo()
        {
            LoginButton.Click();
            return GoTo<LoginPage>();
        }

        public UserHomePage LoginAsUser(string login, string password)
        {
            var loginPage = LoginButtonClickAndGo();
            loginPage.LoginInput.SetValue(login);
            loginPage.PasswordInput.SetValue(password);
            loginPage.LoginButton.Click();
            return GoTo<UserHomePage>();
        }

        public AdminHomePage LoginAsAdmin(string login, string password)
        {
            var loginPage = LoginButtonClickAndGo();
            loginPage.LoginInput.SetValue(login);
            loginPage.PasswordInput.SetValue(password);
            loginPage.LoginButton.Click();
            return GoTo<AdminHomePage>();
        }
    }
}