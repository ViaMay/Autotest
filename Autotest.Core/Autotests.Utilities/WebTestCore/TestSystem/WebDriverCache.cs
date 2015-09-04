using System;

namespace Autotests.Utilities.WebTestCore.TestSystem
{
    public class WebDriverCacheInitializationException : Exception
    {
        public WebDriverCacheInitializationException(string message)
            : base(message)
        {
        }
    }

    public static class WebDriverCache
    {
        private static int count;

        private static bool domainUnloadInitialized;

        private static WebDriverWrapper webDriver;

        public static WebDriverWrapper WebDriver
        {
            get
            {
                if (webDriver == null)
                    SetInstance(new WebDriverWrapper());
                return webDriver;
            }
        }

        public static bool Initialized
        {
            get { return webDriver != null; }
        }

        public static void RestartIfNeed()
        {
            if (++count%20 == 0)
            {
                if (webDriver != null)
                {
                    webDriver.Quit();
                    webDriver = null;
                }
            }
        }

        public static void SetInstance(WebDriverWrapper webDriverWrapper)
        {
            if (webDriver != null)
                throw new WebDriverCacheInitializationException(
                    "Cannot set instance when another instance is initialized");
            if (!domainUnloadInitialized)
            {
                AppDomain.CurrentDomain.DomainUnload += CurrentDomainOnDomainUnload;
                domainUnloadInitialized = true;
            }
            webDriver = webDriverWrapper;
        }

        public static void DestroyInstance()
        {
            webDriver.Quit();
            webDriver = null;
            AppDomain.CurrentDomain.DomainUnload -= CurrentDomainOnDomainUnload;
        }

        private static void CurrentDomainOnDomainUnload(object sender, EventArgs e)
        {
            if (webDriver != null)
                DestroyInstance();
        }
    }
}