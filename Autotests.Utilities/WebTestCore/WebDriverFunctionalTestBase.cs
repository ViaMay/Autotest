using System;
using Autotests.Utilities.Core.Web.Controllers;
using Autotests.Utilities.WebTestCore.Pages;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.Utilities.WebTestCore.Utils;
using NUnit.Framework;

namespace Autotests.Utilities.WebTestCore
{
    [TestFixture]
    public abstract class WebDriverFunctionalTestBase
    {
        [SetUp]
        public virtual void SetUp()
        {
            Log4NetConfiguration.InitializeOnce();
            WebDriverCache.RestartIfNeed();
            WebDriverCache.WebDriver.DeleteAllCookies();
            WebDriverCache.WebDriver.CleanDownloadDirectory();
            PageLoadCounter.Reset();
            JsLogger.Reset();
        }

        [TearDown]
        public virtual void TearDown()
        {
            try
            {
                if (WebDriverCache.Initialized)
                    TearDownInternal();
            }
            finally
            {
                FieldsCleanerCache.Clean(this);
            }
        }

        private void TearDownInternal()
        {
            try
            {
                PageLoadCounter.AssertPageNotLoaded();
            }
            finally
            {
                TestContext testContext = TestContext.CurrentContext;
                try
                {
                    if (testContext.Result.Status == TestStatus.Failed)
                    {
                        CaptureJavascriptErrors();
                        JsLogger.Show();
                        WebDriverCache.WebDriver.CaptureScreenshot();
                    }
                }
                catch
                {
                    Console.Out.WriteLine("Cannot save screenshot. Probably TearDown method called not in NUnit context");
                }
            }
        }

        protected TPage LoadPage<TPage>(string localPath) where TPage : PageBase, new()
        {
            return PageBase.GoToUri<TPage>(new Uri(new Uri(string.Format("http://{0}/", ApplicationBaseUrl)), localPath));
        }

        public virtual string ApplicationBaseUrl { get { return "stage.ddelivery.ru"; } }

        private static void CaptureJavascriptErrors()
        {
            var errors = WebDriverCache.WebDriver.ExecuteScript("return window.jsErrors") as string;
            if (!string.IsNullOrEmpty(errors))
                Console.WriteLine("Javascript errors:\n" + errors);
        }

    }
}
