using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.Utilities.WebTestCore.TestSystem.ByExtensions;
using NUnit.Framework.Constraints;
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
                Thread.Sleep(1000);
            }
        }

//        public void ClickListItem(int index)
//        {
//            autocompleteList.ClickItem(index);
//        }
//
//        public void WaitListItem(string text, int index)
//        {
//            autocompleteList.WaitItem(text, index);
//        }



        private class AutocompleteList : HtmlControl
        {
            private const string notFoundText = "Ничего не найдено";

            public AutocompleteList()
                : base(By.ClassName("controls"), null)
            {
            }

//            private string[] Items
//            {
//                get { return GetItems(); }
//            }

//            public void WaitNotFoundText()
//            {
//                Waiter.Wait(() =>
//                {
//                    ReadOnlyCollection<IWebElement> local_0 =
//                        element.FindElements(By.ClassName("reference_item_message"));
//                    return local_0.Count > 0 && GetTextSafety(local_0[0]) == "Ничего не найдено";
//                }, FormatWithLocator(string.Format("Ожидание появления текста «{0}»", "Ничего не найдено")), new int?());
//            }

//            public void WaitItemsWithRetries(string[] expectedResults)
//            {
//                string actionDescription = string.Format("Ожидание появления списка '{0}'",
//                    string.Join(",", expectedResults));
//                var constraint = new CollectionEquivalentConstraint(expectedResults);
//                Waiter.Wait(() => constraint.Matches(Items), actionDescription, new int?());
//            }

//            public void WaitItem(string itemText, int index)
//            {
//                Waiter.Wait(() =>
//                {
//                    ReadOnlyCollection<IWebElement> local_0 = element.FindElements(By.LinkText(itemText));
//                    if (local_0.Count <= index)
//                        return false;
//                    string local_1 = GetTextSafety(local_0[index]);
//                    return local_1 != null &&
//                           (itemText == "" ? local_1 == itemText : local_1.ToLower().Contains(itemText.ToLower()));
//                }, string.Format("Ожидание содержания текста '{0}' в позиции '{1}'", itemText, index), new int?());
//            }
//
//            public void ClickItem(int index = 0)
//            {
//                Waiter.Wait(() =>
//                {
//                    ReadOnlyCollection<IWebElement> local_0 = element.FindElements(By.CssSelector("li"));
//                    return local_0.Count > index && ClickSafety(local_0[index]);
//                }, string.Format("Попытка клинкнуть на элемент списка № '{0}'", index), new int?());
//            }
//
//            public void WaitItemsCount(int expectedCount)
//            {
//                Waiter.Wait(() => Items.Length == expectedCount,
//                    string.Format("Ожидание количества элементов в списке равным '{0}'", expectedCount), new int?());
//            }

//            private string GetTextSafety(IWebElement el)
//            {
//                try
//                {
//                    return el.Text;
//                }
//                catch (Exception ex)
//                {
//                    return null;
//                }
//            }
//
//            private bool ClickSafety(IWebElement el)
//            {
//                try
//                {
//                    el.Click();
//                    return true;
//                }
//                catch (Exception ex)
//                {
//                    return false;
//                }
//            }

//            private string[] GetItems()
//            {
//                ReadOnlyCollection<IWebElement> elements =
//                    element.FindElements(By.ClassName("autocompleteControl__item__label"));
//                var list = new List<string>();
//                foreach (IWebElement webElement in elements)
//                {
//                    try
//                    {
//                        list.Add(webElement.Text);
//                    }
//                    catch (Exception ex)
//                    {
//                    }
//                }
//                return list.ToArray();
//            }
//        }

//        private class Page : HtmlControl
//        {
//            public Page()
//                : base(By.Id("page"), null)
//            {
//            }
//
//            public void ClickHead()
//            {
//                element.FindElements(By.TagName("h1"))[0].Click();
//            }
//
//            public bool IsListVisible()
//            {
//                return
//                    new StaticControl(By.ClassName("element_type_autocompleteReferenceControl"), (HtmlControl) null)
//                        .IsVisible;
//            }
        }
    }


}