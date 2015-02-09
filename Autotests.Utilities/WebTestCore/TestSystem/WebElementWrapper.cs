using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Autotests.Utilities.WebTestCore.TestSystem
{
    public class WebElementWrapper : IWebElement
    {
        private readonly string description;
        private readonly By locator;
        private readonly ISearchContext searchContext;
        private IWebElement nativeWebElement;

        public WebElementWrapper(ISearchContext searchContext, By locator, string description)
        {
            this.searchContext = searchContext;
            this.locator = locator;
            this.description = description;
        }

        public IWebElement FindElement(By by)
        {
            return Execute(() =>
            {
                ReadOnlyCollection<IWebElement> element = FindNativeWebElement().FindElements(by);
                IWebElement result = element.FirstOrDefault();
                if (result == null)
                    throw new NoSuchElementException(by.ToString()); 
                return result;
            });
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Execute(() => FindNativeWebElement().FindElements(by));
        }

        public void Clear()
        {
            Execute(() =>
            {
                FindNativeWebElement().Clear();
                return 0;
            });
        }

        public void SendKeys(string text)
        {
            Execute(() =>
            {
                FindNativeWebElement().SendKeys(text);
                return 0;
            });
        }


        public void Submit()
        {
            Execute(() =>
            {
                FindNativeWebElement().Submit();
                return 0;
            });
        }

        public void Click()
        {
            Execute(() =>
            {
                FindNativeWebElement().Click();
                return 0;
            });
        }

        public string GetAttribute(string attributeName)
        {
            return Execute(() => FindNativeWebElement().GetAttribute(attributeName));
        }

        public string GetCssValue(string propertyName)
        {
            return Execute(() => FindNativeWebElement().GetCssValue(propertyName));
        }

        public string TagName
        {
            get { return Execute(() => FindNativeWebElement().TagName); }
        }

        public string Text
        {
            get { return Execute(() => FindNativeWebElement().Text); }
        }

        public bool Enabled
        {
            get { return Execute(() => FindNativeWebElement().Enabled); }
        }

        public bool Selected
        {
            get { return Execute(() => FindNativeWebElement().Selected); }
        }

        public Point Location
        {
            get { return Execute(() => FindNativeWebElement().Location); }
        }

        public Size Size
        {
            get { return Execute(() => FindNativeWebElement().Size); }
        }

        public bool Displayed
        {
            get { return Execute(() => FindNativeWebElement().Displayed); }
        }

        public void ClickViaJavascript()
        {
            Execute(() =>
            {
                WebDriverCache.WebDriver.ExecuteScript("arguments[0].click();", FindNativeWebElement());
                return 0;
            });
        }

        public void ClickLeftUp()
        {
            Execute(() =>
            {
                var actions = new Actions((IWebDriver) GetRootSearchContext());
                actions.MoveToElement(FindNativeWebElement(), 0, 0).Click().Perform();
                return 0;
            });
        }

        public void Mouseover()
        {
            Execute(() =>
            {
                var actions = new Actions((IWebDriver) GetRootSearchContext());
                actions.MoveToElement(FindNativeWebElement()).Perform();
                return 0;
            });
        }

        public void SendKeysToBody(string text)
        {
            GetRootSearchContext().FindElement(By.TagName("body")).SendKeys(text);
        }

        public void ScrollDown()
        {
            WebDriverCache.WebDriver.ExecuteScript("arguments[0].scrollIntoView();", FindNativeWebElement());
        }

        private void BlurCurrentActiveElement()
        {
            WebDriverCache.WebDriver.ExecuteScript(
                "if (document.activeElement != null) {document.activeElement.blur();}");
        }

        internal IWebElement FindNativeWebElement()
        {
            return nativeWebElement ?? (nativeWebElement = FindNativeWebElementInternal());
        }

        private IWebElement FindNativeWebElementInternal()
        {
            ISearchContext context = searchContext;
            var wrapper = context as WebElementWrapper;
            if (wrapper != null)
                context = wrapper.FindNativeWebElementInternal();
            return context.FindElement(locator);
        }

        private ISearchContext GetRootSearchContext()
        {
            var wrapper = searchContext as WebElementWrapper;
            if (wrapper != null)
                return wrapper.GetRootSearchContext();
            return searchContext;
        }

        private T Execute<T>(Func<T> func)
        {
            for (int i = 5; i >= 0; i--)
            {
                try
                {
                    return func();
                }
                catch (InvalidElementStateException)
                {
                    ClearCache();
                }
                catch (StaleElementReferenceException)
                {
                    ClearCache();
                }
                catch (NoSuchElementException e)
                {
                    throw new NoSuchElementException(string.Format("Не найден элемент '{0}'", description), e);
                }
                catch (InvalidOperationException exception)
                {
                    if (
                        exception.Message.IndexOf("Element is not clickable at point",
                            StringComparison.OrdinalIgnoreCase) == -1)
                        throw;
                    TryFixPage();
                }
                catch (ElementNotVisibleException e)
                {
                    if (i == 0)
                        throw new ElementNotVisibleException(string.Format("Элемент '{0}' невидимый", description), e);
                    TryFixPage();
                }
            }
            return func();
        }

        private void TryFixPage()
        {
            ScrollDown();
            BlurCurrentActiveElement();
        }

        private void ClearCache()
        {
            nativeWebElement = null;
        }
    }
}