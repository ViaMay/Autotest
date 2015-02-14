using System.Threading;
using Autotests.Utilities.WebTestCore.TestSystem;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class AutocompleteControl : TextInput
    {
//        private readonly AutocompleteList autocompleteList;
//        private readonly By id;
//        private readonly Page page;

        public AutocompleteControl(By locator, HtmlControl container = null)
            : base(locator, container)
        {

        }

        public void SetFirstValueSelect(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                SetValueAndWait(value);
            }
            else
            {
                SetValue(value);
                Thread.Sleep(1500);
                WebDriverCache.WebDriver.WaitForAjax();
//                SendKeys(Keys.Tab);
                SendKeys(Keys.Tab);
                Thread.Sleep(500);
//            }



            }
        }
    }
}