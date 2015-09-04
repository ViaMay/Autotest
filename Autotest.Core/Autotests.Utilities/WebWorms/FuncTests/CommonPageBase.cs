using System;
using System.Diagnostics;
using System.Threading;
using Autotests.Utilities.WebTestCore.Pages;
using NUnit.Framework;

namespace Autotests.Utilities.WebWorms.FuncTests
{
    public abstract class CommonPageBase : PageBase
    {
        protected static TPage RefreshUntil<TPage>(TPage page, Func<TPage, bool> conditionFunc, int timeout = 65000,
            int waitTimeout = 100)
            where TPage : PageBase, new()
        {
            Stopwatch w = Stopwatch.StartNew();
            if (conditionFunc(page))
                return page;
            do
            {
                page = RefreshPage(page);
                if (conditionFunc(page))
                    return page;
                Thread.Sleep(waitTimeout);
            } while (w.ElapsedMilliseconds < timeout);
            Assert.Fail("Не смогли дождаться страницу за {0} мс", timeout);
            return default(TPage);
        }
    }
}