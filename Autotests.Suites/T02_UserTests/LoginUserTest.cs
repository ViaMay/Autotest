using Autotests.WebPages.Pages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class LoginUserTest : ConstVariablesTestBase
    {
        [Test]
        public void Test()
        {
            var defaultPage = LoadPage<DefaultPage>("");
            var loginPage = defaultPage.LoginButtonClickAndGo();
            loginPage.LoginInput.SetValue(userNameAndPass);
            loginPage.PasswordInput.SetValue(userNameAndPass);
            loginPage.LoginButton.Click();
            loginPage.GoTo<UserHomePage>();
        }
    }
}