using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Web;
using Autotests.Utilities.WebTestCore.SystemControls;
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

        public object ExecuteScript(string script, params object[] args)
        {
            return ((IJavaScriptExecutor) driver).ExecuteScript(script, args);
        }

        public void WaitForAjax()
        {
            while (true) // Handle timeout somewhere
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete)
                    break;
            }
            while (true) // Handle timeout somewhere
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return typeof(Ajax) != 'function' || Ajax.activeRequestCount == 0;");
                if (ajaxIsComplete)
                    break;
            }
            while (true) // Handle timeout somewhere
            {
                            var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return typeof(dojo) != 'function' ||  dojo.io.XMLHTTPTransport.inFlight.length == 0;");
                if (ajaxIsComplete)
                    break;
            }
        }

        public IAlert Alert()
        {
            try
            {
                return driver.SwitchTo().Alert();
            }
            catch (NoAlertPresentException)
            {
                Thread.Sleep(1000);
            }
            try
            {
                return driver.SwitchTo().Alert();
            }
            catch (NoAlertPresentException)
            {
                Thread.Sleep(1000);
            }
            try
            {
                return driver.SwitchTo().Alert();
            }
            catch (NoAlertPresentException)
            {
                Thread.Sleep(1000);
            }
            return driver.SwitchTo().Alert();
        }

        public IWebDriver SwitchToFrame(IWebElement value)
        {
            return driver.SwitchTo().Frame(value);
        }

        public IWebDriver SwitchToDefaultContent()
        {
            return driver.SwitchTo().DefaultContent();
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

        public void CaptureScreenshot()
        {
            Random random = new Random();
            int num = random.Next();
            String dir = Environment.CurrentDirectory;
            Directory.CreateDirectory(dir + @"\screenshots");
            String saveLocation = dir + @"\screenshots\screenshot" + num + ".png";
            try
            {
                ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile(saveLocation, ImageFormat.Png);
                Console.WriteLine("Screenshot saved to:\r\n{0}", saveLocation);
            }
            catch (IOException)
            {

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
                    if (directoryInfo.Name == "Assemblies" || directoryInfo.Name == "assemblies")
                        return directoryInfo.FullName;
                }
                currentDirectory = currentDirectory.Parent;
            }
        }
    }
}