using System;
using System.Linq;
using System.Threading;
using Autotests.Utilities.WebTestCore.TestSystem;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public abstract class HtmlControl
    {
        private readonly string controlDescription;
        public readonly WebElementWrapper element;
        private readonly By locator;
        private readonly ISearchContext searchContext;

        protected HtmlControl(By locator, HtmlControl container = null)
        {
            Container = container;
            this.locator = locator;
            controlDescription = FormatControlDescription(locator.ToString(), container);
            searchContext = container.GetSearchContext();
            element = new WebElementWrapper(searchContext, locator, controlDescription);
        }

        protected HtmlControl(string idLocator, HtmlControl container = null)
            : this(By.Id(idLocator), container)
        {
        }

        public void SwitchToFrame()
        {
            WebDriverCache.WebDriver.driver.SwitchTo().Frame(SearchContext.FindElement(By.XPath("//*[@id='ddelivery-container']/iframe")));
        }

        public void SwitchToDefaultContent()
        {
            WebDriverCache.WebDriver.driver.SwitchTo().DefaultContent();
        }

        public virtual bool IsEnabled
        {
            get { return !HasClass("disabled"); }
        }

        public bool IsPresent
        {
            get { return (Container == null || Container.IsPresent) && searchContext.FindElements(locator).Count > 0; }
        }

        public virtual bool IsVisible
        {
            get { return element.Displayed; }
        }

        public HtmlControl Container { get; private set; }

        public By Locator
        {
            get { return locator; }
        }

        public ISearchContext SearchContext
        {
            get { return searchContext; }
        }

        public void WaitVisible()
        {
            string description = FormatWithLocator("Ожидание видимости элемента");
            Assert.IsTrue(IsVisible, description);
        }

        public void WaitVisibleWithRetries(int? timeout = null)
        {
            string description = FormatWithLocator("Ожидание видимости элемента");
            Waiter.Wait(() => IsVisible, description, timeout);
        }

        public void WaitInvisible()
        {
            string description = FormatWithLocator("Ожидание невидимости элемента");
            Assert.IsFalse(IsVisible, description);
        }

        public void WaitInvisibleWithRetries(int? timeout = null)
        {
            string description = FormatWithLocator("Ожидание невидимости элемента");
            Waiter.Wait(() => !IsVisible, description, timeout);
        }

        public void WaitPresence()
        {
            string description = FormatWithLocator("Ожидание присутствия элемента");
            Assert.IsTrue(IsPresent, description);
        }

        public void WaitPresenceWithRetries(int? timeout = null)
        {
            string description = FormatWithLocator("Ожидание присутствия элемента");
            Waiter.Wait(() => IsPresent, description, timeout);
        }

        public bool CheckPresenceWithRetries()
        {
            bool result = true;
            Waiter.DoWait(() => IsPresent, () => { result = false; });
            return result;
        }

        public void WaitAbsence()
        {
            string description = FormatWithLocator("Ожидание отсутствия элемента");
            Assert.IsFalse(IsPresent, description);
        }

        public void WaitAbsenceWithRetries(int? timeout = null)
        {
            string description = FormatWithLocator("Ожидание отсутствия элемента");
            Waiter.Wait(() => !IsPresent, description, timeout);
        }

        public void WaitEnabled()
        {
            string description = FormatWithLocator("Ожидание доступности элемента");
            Assert.IsTrue(IsEnabled, description);
        }

        public void WaitEnabledWithRetries(int? timeout = null)
        {
            string description = FormatWithLocator("Ожидание доступности элемента");
            Waiter.Wait(() => IsEnabled, description, timeout);
        }

        public void WaitDisabled()
        {
            string description = FormatWithLocator("Ожидание недоступности элемента");
            Assert.IsFalse(IsEnabled, description);
        }

        public void WaitDisabledWithRetries(int? timeout = null)
        {
            string description = FormatWithLocator("Ожидание недоступности элемента");
            Waiter.Wait(() => !IsEnabled, description, timeout);
        }

        public virtual void WaitText(string expectedText)
        {
            string description =
                FormatWithLocator(string.Format("Ожидание появления текста '{0}' в элементе", expectedText));
            Waiter.Wait(() => GetText() == expectedText, description);
        }

        public void WaitTextStartsWith(string expectedText)
        {
            string description =
                FormatWithLocator(string.Format("Ожидание текста '{0}' в начале текста элемента", expectedText));
            StringAssert.StartsWith(expectedText, GetText(), description);
        }

        public void WaitTextContains(string expectedText)
        {
            string description =
                FormatWithLocator(string.Format("Ожидание текста '{0}' внутри текста элемента", expectedText));
            StringAssert.Contains(expectedText, GetText(), description);
        }

        public void WaitTextContainsWithRetries(string expectedText, int? timeout = null)
        {
            string description =
                FormatWithLocator(string.Format("Ожидание текста '{0}' внутри текста элемента", expectedText));
            Waiter.Wait(() => GetText().Contains(expectedText), description, timeout);
        }

        public virtual void WaitClassPresence(string className)
        {
            string description = FormatWithLocator(string.Format("Ожидание класса '{0}' у элемента", className));
            string actualClasses;
            Assert.IsTrue(HasClass(className, out actualClasses),
                string.Format("{0}, актуальный класс: '{1}'", description, actualClasses));
        }

        public virtual void WaitClassPresenceWithRetries(string className, int? timeout = null)
        {
            string description =
                FormatWithLocator(string.Format("Ожидание появления класса '{0}' у элемента", className));
            Waiter.Wait(() => HasClass(className), description, timeout);
        }

        public void WaitClassAbsence(string className)
        {
            string description =
                FormatWithLocator(string.Format("Ожидание отсутствия класса '{0}' у элемента", className));
            string actualClasses;
            Assert.IsFalse(HasClass(className, out actualClasses),
                string.Format("{0}, актуальный класс: '{1}'", description, actualClasses));
        }

        public void WaitClassAbsenceWithRetries(string className, int? timeout = null)
        {
            string description =
                FormatWithLocator(string.Format("Ожидание отсутствия класса '{0}' у элемента", className));
            Waiter.Wait(() => !HasClass(className), description, timeout);
        }

        public void WaitAttributeValue(string attributeName, string expectedText)
        {
            string description =
                FormatWithLocator(string.Format("Ожидание аттрибута '{0}' со значением '{1}' в элементе", attributeName,
                    expectedText));
            Assert.AreEqual(expectedText, GetAttributeValue(attributeName), description);
        }

        public void WaitAttributeValueWithRetries(string attributeName, string expectedText, int? timeout = null)
        {
            string description =
                FormatWithLocator(string.Format("Ожидание аттрибута '{0}' со значением '{1}' в элементе", attributeName,
                    expectedText));
            Waiter.Wait(() => expectedText == GetAttributeValue(attributeName), description, timeout);
        }

        public virtual void Click()
        {
            element.Click();
        }

        public void ScrollDown()
        {
            element.ScrollDown();
        }

        public void ClickLeftUp()
        {
            element.ClickLeftUp();
        }

        public void ClickAndWaitTextError(int index = 0)
        {
            var alertClass = new StaticControl(BY.NthOfClass("alert-error", index));
            var errorClass = new StaticControl(BY.NthOfClass("help-inline", index));
            Click();
            var second = 0;
            while (errorClass.IsPresent == false && alertClass.IsPresent == false)
            {
                second = second + 1;
                Thread.Sleep(10);
                if (second >= 600) Assert.AreEqual(errorClass.IsPresent, true, "Время ожидание завершено. Не найден элемент содержаший ошибку");
                if (second >= 600) Assert.AreEqual(alertClass.IsPresent, true, "Время ожидание завершено. Не найден элемент содержаший ошибку");
            }
        }

        public void ClickAndWaitTextErrorAbsence(int index = 0)
        {
            var alertClass = new StaticControl(BY.NthOfClass("alert-error", index));
            var errorClass = new StaticControl(BY.NthOfClass("help-inline", index));
            Click();
            var second = 0;
            while (errorClass.IsPresent || alertClass.IsPresent)
            {
                second = second + 1;
                Thread.Sleep(10);
                if (second >= 600) Assert.AreEqual(errorClass.IsPresent, false, "Время ожидание завершено. Найден элемент содержаший ошибку");
                if (second >= 600) Assert.AreEqual(alertClass.IsPresent, false, "Время ожидание завершено. Найден элемент содержаший ошибку");
            }
        }

        public void Mouseover()
        {
            element.Mouseover();
        }

        public void SendKeysToBody(string text)
        {
            element.SendKeysToBody(text);
        }

        public virtual string GetText()
        {
            return element.Text.Trim();
        }

        public string GetAttributeValue(string attributeName)
        {
            return element.GetAttribute(attributeName);
        }

        public virtual void AssertNessesary()
        {
            WaitClassPresenceWithRetries("field__incorrect");
            WaitAttributeValue("title", "Поле должно быть заполнено");
        }

        public bool HasClass(string className)
        {
            string actualClasses;
            return HasClass(className, out actualClasses);
        }

        public string FormatWithLocator(string text)
        {
            return string.Format("{0} '{1}'", text, controlDescription);
        }

        public string GetParameterValue(string value)
        {
            string[] path = value.Split('.');
            string script = string.Format("return Configs['{0}']", GetAttributeValue("id"));
            script = path.Aggregate(script, (current, part) => current + string.Format("['{0}']", part));
            return WebDriverCache.WebDriver.ExecuteScript(script) as string;
        }

        private bool HasClass(string className, out string actualClasses)
        {
            actualClasses = GetAttributeValue("class");
            string[] actualClassesArray = actualClasses.Split(new[] {" ", "\r", "\n", "\t"},
                StringSplitOptions.RemoveEmptyEntries);
            string[] expectedClassesArray = className.Split(new[] {" ", "\r", "\n", "\t"},
                StringSplitOptions.RemoveEmptyEntries);
            return expectedClassesArray.All(actualClassesArray.Contains);
        }

        public string FormatControlDescription(string locatorString, HtmlControl container)
        {
            string description = string.Format("{0} ({1})", GetType().Name, locatorString);
            if (container == null)
                return description;
            return string.Format("{0} в контексте элемента {1}", description, container.controlDescription);
        }
    }
}