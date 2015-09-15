using Autotests.Utilities.WebTestCore;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

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
            try
            {
                LoadPage<DefaultPage>("auth/logout");
            }
            catch (UnhandledAlertException)
            {
                var alert = WebDriverCache.WebDriver.Alert();
                alert.Dismiss();
            }
            finally
            {
                base.TearDown();
            }
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

        public void WaitDocuments(int value = 90000)
        {
            Thread.Sleep(value);
        }

        protected DefaultPage DefaultPage { get; private set; }
    }
}