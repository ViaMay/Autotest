using Autotests.Utilities.WebTestCore.TestSystem;

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

        public void AcceptTextInput(string value)
        {
            WebDriverCache.WebDriver.Alert().SendKeys(value);
        }

        public void Сancel()
        {
            WebDriverCache.WebDriver.Alert().Dismiss();
        }

        public void WaitText(string expectedText)
        {
            Waiter.Wait(() => WebDriverCache.WebDriver.Alert().Text == expectedText, "Ожидание видимости элемента");
        }
    }
}
