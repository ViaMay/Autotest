using System;
using log4net.Config;

namespace Autotests.Utilities.Core.Web.Controllers
{
    internal static class Log4NetConfiguration
    {
        private static bool initialized;

        public static void InitializeOnce()
        {
            if (!initialized)
            {
                Type type = typeof (Log4NetConfiguration);
                XmlConfigurator.Configure(type.Assembly.GetManifestResourceStream(type, "log4net.config"));
                initialized = true;
            }
        }
    }
}