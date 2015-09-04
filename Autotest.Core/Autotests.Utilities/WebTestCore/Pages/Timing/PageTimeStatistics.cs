using System;
using System.Diagnostics;

namespace Autotests.Utilities.WebTestCore.Pages.Timing
{
    public abstract class PageTimeStatistics : IPageTimeStatistics
    {
        private static IPageTimeStatistics instance;

        public static IPageTimeStatistics Instance
        {
            get
            {
                if (instance == null)
                    SetInstance(new DefaultPageTimeStatistics());
                return instance;
            }
        }

        public T InvolveAction<T>(Type pageType, Func<T> func)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            bool failed = false;
            try
            {
                try
                {
                    return func();
                }
                catch (Exception)
                {
                    failed = true;
                    throw;
                }
            }
            finally
            {
                stopwatch.Stop();
                AddStatisticsValue(pageType.Name, stopwatch.Elapsed, DateTime.Now, failed);
            }
        }

        public void InvolveAction(Type pageType, Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            bool failed = false;
            try
            {
                try
                {
                    action();
                }
                catch (Exception)
                {
                    failed = true;
                    throw;
                }
            }
            finally
            {
                stopwatch.Stop();
                AddStatisticsValue(pageType.Name, stopwatch.Elapsed, DateTime.Now, failed);
            }
        }

        public abstract void AddStatisticsValue(string pageName, TimeSpan value, DateTime completeTime, bool failed);

        public static void SetInstance(IPageTimeStatistics pageTimeStatistics)
        {
            instance = pageTimeStatistics;
        }
    }
}