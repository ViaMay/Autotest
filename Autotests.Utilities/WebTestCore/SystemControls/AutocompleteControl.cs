using System;
using System.Threading;
using Autotests.Utilities.WebTestCore.TestSystem;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class AutocompleteControl : TextInput
    {

        public readonly int index;
        public AutocompleteControl(By locator, HtmlControl container = null)
            : base(locator, container)
        {
            try
            {
                index = Int32.Parse(locator.ToString().Replace("ByNthOfClass: .ajax-combobox[", "").Replace("]", ""));
            }
            catch (FormatException e)
            {
                index = 0;
            }
        }

// TODO работает так только если ищем элемент по BY.Cla... так как используем индекс!
        public void SetFirstValueSelect(string value, string valueExperct = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                SetValue(value);
            }
            else
            {
                SetValue(value);
                if (valueExperct == null) valueExperct = value;
                WebDriverCache.WebDriver.WaitForAjax();
                var textElement = new StaticControl(By.XPath(string.Format("//html/body/ul[{0}]/li[1]", index + 1)));
                for (int i = 0; i < 20; i++)
                {
                    if (textElement.GetText() != "")
                    {
                        if (textElement.GetText().Contains(valueExperct))
                        {
                            SendKeys(Keys.Tab);
                            goto link1;
                        }
                    }
                    if (i == 5) SetValue(value);
                    if (i == 10) SetValue(value);
                    if (i == 15) SetValue(value);
                    WebDriverCache.WebDriver.WaitForAjax();
                }
                Assert.AreEqual(false, true, string.Format(
                    "Время ожидание завершено.не найден элемент {0} в AutocompleteControl", value));
                link1:
                ;
            }
        }
    }
}