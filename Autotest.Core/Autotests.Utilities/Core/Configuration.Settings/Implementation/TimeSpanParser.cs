using System;

namespace Autotests.Utilities.Core.Configuration.Settings.Implementation
{
    internal static class TimeSpanParser
    {
        public static bool TryParseTimeSpan(string s, out TimeSpan result)
        {
            int milliseconds;
            if (int.TryParse(s, out milliseconds))
            {
                result = TimeSpan.FromMilliseconds(milliseconds);
                return true;
            }
            return TimeSpan.TryParse(s, out result);
        }
    }
}