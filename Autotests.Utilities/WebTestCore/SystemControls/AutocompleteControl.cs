using System.Threading;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class AutocompleteControl : TextInput
    {
//        private readonly AutocompleteList autocompleteList;
//        private readonly By id;
//        private readonly Page page;

        public AutocompleteControl(By locator)
            : base(locator, null)
        {

        }

        public void SetValueAndSelect(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                SetValueAndWait(value);
            }
            else
            {
                SetValue(value);
                Thread.Sleep(1500);
                WebDriverCache.WebDriver.WaitAjax(value);
                SendKeys(Keys.Tab);
                SendKeys(Keys.Tab);
                Thread.Sleep(500);
            }
        }
    }
}