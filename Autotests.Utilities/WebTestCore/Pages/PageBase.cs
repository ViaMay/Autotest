using System;
using Autotests.Utilities.WebTestCore.Pages.Timing;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.Utilities.WebTestCore.Utils;

namespace Autotests.Utilities.WebTestCore.Pages
{
    public abstract class PageBase
    {
        public abstract void BrowseWaitVisible();

        public TPage GoTo<TPage>(int pageLoads = 1) where TPage : PageBase, new()
        {
            return PageTimeStatistics.Instance.InvolveAction(typeof(TPage), () =>
            {
                VerifyPageIsAlive();
                PageLoadCounter.WaitPageLoaded(pageLoads, WaitPageLoadTimeout);
                CleanFields(this);
                var newPage = new TPage();
                newPage.BrowseWaitVisible();
                InitPage();
                return newPage;
            });
        }

        public TPage ChangePageType<TPage>() where TPage : PageBase, new()
        {
            VerifyPageIsAlive();
            CleanFields(this);
            var newPage = new TPage();
            newPage.BrowseWaitVisible();
            return newPage;
        }

        public static TPage GoToUri<TPage>(Uri uri) where TPage : PageBase, new()
        {
            return PageTimeStatistics.Instance.InvolveAction(typeof(TPage), () =>
            {
                WebDriverCache.WebDriver.GoToUri(uri);
//                PageLoadCounter.InitPageLoadCounterCookie();
                PageLoadCounter.WaitPageLoaded(timeout: WaitPageLoadTimeout);
                WebDriverCache.WebDriver.SetCookie("testingMode", "1");
                var newPage = new TPage();
                newPage.BrowseWaitVisible();
                InitPage();
                return newPage;
            });
        }

        public string GetUrlParameter(string parameterName)
        {
            return WebDriverCache.WebDriver.GetUrlParameter(parameterName);
        }

        public string GetUrl()
        {
            return WebDriverCache.WebDriver.Url.ToString();
        }

        public static TimeSpan WaitPageLoadTimeout { get { return waitPageLoadTimeout; } set { waitPageLoadTimeout = value; } }

        public static TPage RefreshPage<TPage>(TPage page) where TPage : PageBase, new()
        {
            return RefreshPage<TPage, TPage>(page);
        }

        public static TPage1 RefreshPage<TPage, TPage1>(TPage page)
            where TPage : PageBase, new()
            where TPage1 : PageBase, new()
        {
            return PageTimeStatistics.Instance.InvolveAction(typeof(TPage1), () =>
            {
                page.VerifyPageIsAlive();
                WebDriverCache.WebDriver.Refresh();
                PageLoadCounter.WaitPageLoaded(timeout: WaitPageLoadTimeout);
                CleanFields(page);
                var newPage = new TPage1();
                newPage.BrowseWaitVisible();
                InitPage();
                return newPage;
            });
        }

        private static void InitPage()
        {
            WebDriverCache.WebDriver.ExecuteScript("$(document.body).addClass('testingMode')");
        }

        private static void CleanFields(PageBase from)
        {
            FieldsCleanerCache.Clean(from);
        }

        private void VerifyPageIsAlive()
        {
            //JsLogger.Complete();
            if (alive == null)
                throw new InvalidOperationException("Данная страница уже закрыта");
        }

        private readonly object alive = new object();

        [DoNotClean]
        private static TimeSpan waitPageLoadTimeout = TimeSpan.FromSeconds(60);
    }
}