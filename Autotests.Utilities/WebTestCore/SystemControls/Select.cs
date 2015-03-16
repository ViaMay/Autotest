using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class Select : HtmlControl
    {
        private readonly StaticText selectedText;

        public Select(By locator, HtmlControl container = null)
            : base(locator, container)
        {
            var controlContainer = new HtmlControlContainer(locator, container);
            selectedText = new StaticText(By.CssSelector("select > option"), controlContainer);
        }

        public bool IsMultiple { get; private set; }

        public override string GetText()
        {
            return selectedText.GetText();
        }

        public override void WaitText(string expectedText)
        {
            string description =
                FormatWithLocator(string.Format("Ожидание появления текста '{0}' в элементе", expectedText));
            Waiter.Wait(() => selectedText.GetText() == expectedText, description);
        }


        public void SelectValue(string value)
        {
            WaitEnabled();
            if (GetText() == value)
                return;
            IList<IWebElement> list = element.FindElements(By.XPath(".//option"));
            foreach (IWebElement option in list)
            {
                if (option.Text == value)
                {
                    SetSelected(option);
                    Click();
                }
            }
        }


        public void SelectByText(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text", "text must not be null");
//            IList<IWebElement> list = element.FindElements(By.XPath(".//option"));
            IList<IWebElement> list = element.FindElements(By.XPath(".//option[normalize-space(.) = " + EscapeQuotes(text) + "]"));
            bool flag = false;
            foreach (IWebElement option in list)
            {
                SetSelected(option);
                if (!IsMultiple)
                    return;
                flag = true;
            }
            if (list.Count == 0 && text.Contains(" "))
            {
                string substringWithoutSpace = GetLongestSubstringWithoutSpace(text);
                foreach (
                    IWebElement option in
                        !string.IsNullOrEmpty(substringWithoutSpace)
                            ? (IEnumerable<IWebElement>)
                                element.FindElements(
                                    By.XPath(".//option[contains(., " +
                                            EscapeQuotes(substringWithoutSpace) + ")]"))
                            : (IEnumerable<IWebElement>) element.FindElements(By.TagName("option")))
                {
                    if (text == option.Text)
                    {
                        SetSelected(option);
                        if (!IsMultiple)
                            return;
                        flag = true;
                    }
                }
            }
            if (!flag)
                throw new NoSuchElementException("Cannot locate element with text: " + text);
        }

        private static string EscapeQuotes(string toEscape)
        {
            if (toEscape.IndexOf("\"", StringComparison.OrdinalIgnoreCase) > -1 && toEscape.IndexOf("'", StringComparison.OrdinalIgnoreCase) > -1)
            {
                bool flag = false;
                if (toEscape.IndexOf("\"", StringComparison.OrdinalIgnoreCase) == toEscape.Length - 1)
                    flag = true;
                string[] strArray = toEscape.Split(new char[1]
        {
          '"'
        });
                StringBuilder stringBuilder = new StringBuilder("concat(");
                for (int index = 0; index < strArray.Length; ++index)
                {
                    stringBuilder.Append("\"").Append(strArray[index]).Append("\"");
                    if (index == strArray.Length - 1)
                    {
                        if (flag)
                            stringBuilder.Append(", '\"')");
                        else
                            stringBuilder.Append(")");
                    }
                    else
                        stringBuilder.Append(", '\"', ");
                }
                return ((object)stringBuilder).ToString();
            }
            else if (toEscape.IndexOf("\"", StringComparison.OrdinalIgnoreCase) > -1)
                return string.Format(CultureInfo.InvariantCulture, "'{0}'", new object[1]
        {
           toEscape
        });
            else
                return string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[1]
        {
          toEscape
        });
        }

        private static string GetLongestSubstringWithoutSpace(string s)
        {
            string str1 = string.Empty;
            string str2 = s;
            var chArray = new char[1]
            {
                ' '
            };
            foreach (string str3 in str2.Split(chArray))
            {
                if (str3.Length > str1.Length)
                    str1 = str3;
            }
            return str1;
        }

        private static void SetSelected(IWebElement option)
        {
            if (option.Selected)
                return;
            option.Click();
        }
    }
}