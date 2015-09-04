using System;
using Autotests.Utilities.WebTestCore.SystemControls;
using NUnit.Framework;

namespace Autotests.Utilities.WebTestCore.TestSystem
{
    public static class PageLoadCounter
    {
        public static void Reset()
        {
            lastPageLoadCounter = 0;
            cookieName = "pageLoadCounterValue_" + Guid.NewGuid();
        }

        public static void AssertPageNotLoaded()
        {
            var expectedPageLoadCounter = lastPageLoadCounter;
//            Update();
            Assert.AreEqual(expectedPageLoadCounter, lastPageLoadCounter, "Был выполнен неожиданный переход на страницу");
        }

        public static void WaitPageLoaded(int pageLoads = 1, TimeSpan? timeout = null)
        {
//            var pageLoadCounter = lastPageLoadCounter + pageLoads;
//            Waiter.Wait(() => CheckPageLoaded(pageLoadCounter), "Ожидание завершения перехода на страницу", (int)(timeout ?? TimeSpan.FromSeconds(20)).TotalMilliseconds);
            CheckServerException();
//            Assert.AreEqual(pageLoadCounter, lastPageLoadCounter, "Был выполнен неожиданный переход на страницу");
        }

//        public static void InitPageLoadCounterCookie()
//        {
//            if (WebDriverCache.WebDriver.FindCookie(cookieName) == null)
//                WebDriverCache.WebDriver.SetCookie(cookieName, "1");
//        }

//        private static bool CheckPageLoaded(long pageLoadCounter)
//        {
////            Update();
//            if (lastPageLoadCounter >= pageLoadCounter)
//                return true;
//            CheckServerException();
//            return false;
//        }

//        private static void Update()
//        {
//            long.TryParse(WebDriverCache.WebDriver.FindCookie(cookieName), out lastPageLoadCounter);
//        }

        private static void CheckServerException()
        {
            var errorNumber = new StaticText("err-info");
            if (errorNumber.IsPresent)
            {
                errorNumber.SendKeysToBody("h");
                var exceptionText = new StaticText("stackTrace").GetText();
                if (!string.IsNullOrEmpty(exceptionText))
                    throw new Exception("Внутренняя ошибка сервера:" + Environment.NewLine + exceptionText);
            }
        }

        private static long lastPageLoadCounter;
        private static string cookieName;
    }
}