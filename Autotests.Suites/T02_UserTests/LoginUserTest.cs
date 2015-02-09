using Autotests.WebPages.Pages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class LoginUserTest : SimpleFunctionalTestBase
    {
        public override void SetUp()
        {
            base.SetUp();
        }
        
        [Test]
        public void Test01()
        {
            var defaultPage = LoadPage<DefaultPage>("");
            var loginPage = defaultPage.LoginButtonClickAndGo();
            loginPage.LoginInput.SetValue("slonas@ukr.net");
            loginPage.PasswordInput.SetValue("slonas@ukr.net");
            loginPage.LoginButton.Click();
            loginPage.GoTo<UserHomePage>();
        }
    }
}