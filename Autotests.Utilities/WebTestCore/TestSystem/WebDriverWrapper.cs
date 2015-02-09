using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading;
using System.Web;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Autotests.Utilities.WebTestCore.TestSystem
{
    public class WebDriverWrapper
    {
        private readonly IWebDriver driver;

        public WebDriverWrapper(string proxy = null)
        {
            DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
            string assembliesDirectory = FindAssembliesDirectory();
            string chromePath = Path.Combine(assembliesDirectory, "Chrome", "chrome.exe");
            SetChromeVersionToRegistry(chromePath);
            var chromeOptions = new ChromeOptions
            {
                BinaryLocation = chromePath
            };
            if (!string.IsNullOrEmpty(proxy))
                chromeOptions.Proxy = new Proxy {HttpProxy = proxy};
            chromeOptions.AddArguments("start-maximized");
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("download.default_directory",
                WebDriverDownloadDirectory.DirectoryPath);
            chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
            chromeOptions.AddUserProfilePreference("profile.content_settings.pattern_pairs",
                new Dictionary<string, object>
                {
                    {"http://*,*", new {plugins = 1}}
                });
            capabilities.SetCapability(ChromeOptions.Capability, chromeOptions);

//            driver = GetDriver(capabilities);

//            var directory = assembliesDirectory + "Selenium\\";
            string directory = Path.Combine(assembliesDirectory, "Selenium");
            driver = new ChromeDriver(directory, chromeOptions);
        }

        public string Url
        {
            get { return driver.Url; }
        }

        public void GoToUri(Uri uri)
        {
            driver.Navigate().GoToUrl(uri.AbsoluteUri);
        }

        public void Refresh()
        {
            driver.Navigate().Refresh();
        }

        public ISearchContext GetSearchContext()
        {
            return driver;
        }

        public void DeleteAllCookies()
        {
            driver.Manage().Cookies.DeleteAllCookies();
        }

        public void SetCookie(string cookieName, string cookieValue)
        {
            driver.Manage().Cookies.AddCookie(new Cookie(cookieName, cookieValue, "/"));
        }

//        public string FindCookie(string cookieName)
//        {
//            Cookie cookie = driver.Manage().Cookies.GetCookieNamed(cookieName);
//            if (cookie == null)
//                return null;
//            return cookie.Value;
//        }

        public object ExecuteScript(string script, params object[] args)
        {
            return ((IJavaScriptExecutor) driver).ExecuteScript(script, args);
        }

        public void WaitAjax()
        {
            ExecuteScript("return (typeof($) === 'undefined') ? true : !$.active;");
            ExecuteScript("return (typeof($) === 'userWindow.jQuery') ? true : !$.active;");
            ExecuteScript("return (typeof($) === 'userWindow.Ajax') ? true : !$.active;");
            ExecuteScript("return (typeof($) === 'userWindow.dojo') ? true : !$.active;");
        }

        public void WaitForAjaxComplete()
        {
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0,0,0,300));
            ExecuteScript("return (typeof MathJax === 'undefined') || (MathJax.Hub.signal.posted[window.MathJax.Hub.signal.posted.length-1][0]=='End Process')");
        }

        public IAlert Alert()
        {
            return driver.SwitchTo().Alert();
        }
        public string GetScreenshot()
        {
            return ((MyRemoteWebDriver) driver).GetScreenshot();
        }

        public void Quit()
        {
            try
            {
                try
                {
                    driver.Close();
                }
                finally
                {
                    try
                    {
                        driver.Quit();
                    }
                    finally
                    {
                        driver.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Ошибка при остановке ChromeDriver:\r\n{0}", exception);
            }
        }

        public void CleanDownloadDirectory()
        {
            WebDriverDownloadDirectory.Clean();
        }

        public string[] GetDownloadedFileNames()
        {
            return WebDriverDownloadDirectory.GetDownloadedFileNames();
        }

        public void WaitDownloadFiles(int expectedCountFiles, int maximalWaitTime = 15000, int sleepInterval = 100)
        {
            WebDriverDownloadDirectory.WaitDownloadFiles(expectedCountFiles, maximalWaitTime, sleepInterval);
        }

        internal string GetUrlParameter(string parameterName)
        {
            var url = new Uri(Url);
            return HttpUtility.ParseQueryString(url.Query).Get(parameterName);
        }

//        private MyRemoteWebDriver GetDriver(DesiredCapabilities capabilities)
//        {
//            for (var i = 3; i >= 0; i--)
//            {
//                try
//                {
//                    return new MyRemoteWebDriver(new Uri("http://localhost:9515/"), capabilities);
//                }
//                catch (InvalidOperationException e)
//                {
//                    if (i == 0)
//                        throw;
//                    if (e.Message.IndexOf("unable to discover open pages", StringComparison.Ordinal) != -1)
//                        continue;
//                    throw;
//                }
//            }
//            return new MyRemoteWebDriver(new Uri("http://localhost:9515/"), capabilities);
//        }

        private static void SetChromeVersionToRegistry(string chromePath)
        {
            string chromeVersion = FileVersionInfo.GetVersionInfo(chromePath).ProductVersion;
            RegistryKey key =
                Registry.CurrentUser.CreateSubKey(
                    @"Software\Google\Update\Clients\{8A69D345-D564-463c-AFF1-A69D9E530F96}");
            key.SetValue("pv", chromeVersion);
            key.Close();
        }

        private string FindAssembliesDirectory()
        {
            DirectoryInfo currentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
            while (true)
            {
                if (currentDirectory == null)
                    throw new Exception("The folder Assemblies not found");
                DirectoryInfo[] directories = currentDirectory.GetDirectories();
                foreach (DirectoryInfo directoryInfo in directories)
                {
                    if (directoryInfo.Name == "Assemblies")
                        return directoryInfo.FullName;
                }
                currentDirectory = currentDirectory.Parent;
            }
        }

        private class MyRemoteWebDriver : RemoteWebDriver
        {
            public MyRemoteWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
                : base(remoteAddress, desiredCapabilities, TimeSpan.FromSeconds(60))
            {
            }

            public string GetScreenshot()
            {
                return Execute(DriverCommand.Screenshot, null).Value.ToString();
            }
        }
    }
}