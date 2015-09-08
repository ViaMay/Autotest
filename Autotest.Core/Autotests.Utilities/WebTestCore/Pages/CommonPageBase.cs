using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace Autotests.Utilities.WebTestCore.Pages
{
    public abstract class CommonPageBase : PageBase
    {
        protected static TPage RefreshUntil<TPage>(TPage page, Func<TPage, bool> conditionFunc, int timeout = 65000, int waitTimeout = 100)
            where TPage : PageBase, new()
        {
            var w = Stopwatch.StartNew();
            if (conditionFunc(page))
                return page;
            do
            {
                page = RefreshPage(page);
                if (conditionFunc(page))
                    return page;
                Thread.Sleep(waitTimeout);
            } while (w.ElapsedMilliseconds < timeout);
            Assert.Fail(string.Format("Не смогли дождаться страницу за {0} мс", timeout));
            return default(TPage);
        }
    }
}