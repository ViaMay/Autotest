using System.Threading;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class AlertControl
    {
        public AlertControl()
        {
        }

        public void Accept()
        {
            WebDriverCache.WebDriver.Alert().Accept();
        }

        public void Сancel()
        {
            WebDriverCache.WebDriver.Alert().Dismiss();
        }

        public void WaitText()
        {
            var d = WebDriverCache.WebDriver.Alert().Text;
        }

        public void WaitText(string expectedText)
        {
            Waiter.Wait(() => WebDriverCache.WebDriver.Alert().Text == expectedText, "Ожидание видимости элемента");
        }
    }
}
