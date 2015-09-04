using Autotests.Utilities.WebTestCore;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.WebPages.Pages
{
    [TestFixture]
    public class SimpleFunctionalTestBase : WebDriverFunctionalTestBase
    {
        public override void SetUp()
        {
            base.SetUp();
            DefaultPage = LoadPage<DefaultPage>("");
        }

        public override void TearDown()
        {
            LoadPage<DefaultPage>("auth/logout");
            base.TearDown();
        }

        protected UserHomePage LoginAsUser(string login, string password)
        {
            var enterPage = DefaultPage.LoginButtonClickAndGo();
            return enterPage.LoginAsUser(login, password);
        }

        protected AdminHomePage LoginAsAdmin(string login, string password)
        {
            var enterPage = DefaultPage.LoginButtonClickAndGo();
            return enterPage.LoginAsAdmin(login, password);
        }

        protected void ResetDownloadFilesState()
        {
            WebDriverCache.WebDriver.CleanDownloadDirectory();
        }

        protected DefaultPage LoginDefaultPage()
        {
            var partyCreationPage = LoadPage<DefaultPage>("");
            return partyCreationPage;
        }

        protected DefaultPage DefaultPage { get; private set; }
    }
}