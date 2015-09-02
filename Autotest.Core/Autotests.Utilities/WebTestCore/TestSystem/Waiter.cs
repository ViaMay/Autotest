using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace Autotests.Utilities.WebTestCore.TestSystem
{
    public static class Waiter
    {
        private const int waitTimeout = 100;
        private const int defaultTimeout = 20000;
        private const int firstTestTimeoutFactor = 3;
        private static string firstTestName;

        public static void Wait(Func<bool> tryFunc, string actionDescription, int? timeout = null)
        {
////            if (TeamCityEnvironment.IsExecutionViaTeamCity)
////            {
            if (TestContext.CurrentContext != null && TestContext.CurrentContext.Test != null &&
                TestContext.CurrentContext.Test.FullName != null)
            {
                if (firstTestName == null || firstTestName == TestContext.CurrentContext.Test.FullName)
                {
                    timeout = GetActualTimeout(timeout)*firstTestTimeoutFactor;
                    firstTestName = TestContext.CurrentContext.Test.FullName;
                }
            }
//            }
            DoWait(tryFunc,
                () => Assert.Fail("действие {0} не выполнилось за {1} мс", actionDescription, GetActualTimeout(timeout)),
                timeout);
        }

        public static void DoWait(Func<bool> tryFunc, Action doIfWaitFails, int? timeout = null)
        {
            Stopwatch w = Stopwatch.StartNew();
            do
            {
                if (tryFunc())
                    return;
                Thread.Sleep(waitTimeout);
            } while (w.ElapsedMilliseconds < GetActualTimeout(timeout));
            doIfWaitFails();
        }

        private static int GetActualTimeout(int? timeout)
        {
            return timeout ?? defaultTimeout;
        }
    }
}