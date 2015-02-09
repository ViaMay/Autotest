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
//            id = locator;
//            autocompleteList = new AutocompleteList();
//            LinkElement = new Link(By.ClassName(value), 0), this);
//            page = new Page();
        }


//        private readonly Link LinkElement;

        public void SetValueAndSelect(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                SetValueAndWait(value);
            }
            else
            {
                SetValue(value);
                Thread.Sleep(1000);
                WebDriverCache.WebDriver.WaitAjax();
//                WebDriverCache.WebDriver.WaitForAjaxComplete();
                SendKeys(Keys.Tab);
//                Thread.Sleep(1000);
            }
        }
    }
}