using System;

namespace Autotests.Utilities.WebTestCore.Pages.Timing
{
    public class DefaultPageTimeStatistics : PageTimeStatistics
    {
        public override void AddStatisticsValue(string pageName, TimeSpan value, DateTime completeTime, bool failed)
        {
        }
    }
}