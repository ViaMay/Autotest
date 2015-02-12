using Autotests.Utilities.WebTestCore.TestSystem;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class TextInput : HtmlControl
    {
        public TextInput(By locator, HtmlControl container = null)
            : base(locator, container)
        {
        }

        public virtual void SetValueAndWait(string value)
        {
            Clear();
            element.SendKeys(value + Keys.Tab);
        }

        public void SetValue(string value)
        {
            Clear();
            element.SendKeys(value);
        }

        public void SendKeys(string keys)
        {
            element.SendKeys(keys);
        }

        public void WaitValue(string value)
        {
            string description = FormatWithLocator(string.Format("Ожидание value '{0}' в элементе", value));
            Waiter.Wait(() => GetAttributeValue("value") == value, description);
        }

        public void Clear()
        {
            element.Clear();
        }

        public void Copy()
        {
            element.SendKeys(Keys.Control + "a" + "c" + Keys.Control + Keys.Tab);
        }

        public void Paste()
        {
            Clear();
            element.SendKeys(Keys.Control + "v" + Keys.Control + Keys.Tab);
        }

        public void AssertMaxLength(int maxLength)
        {
            var longText = new string('1', 500);
            SetValueAndWait(longText);
            string description =
                FormatWithLocator(string.Format("Ожидание максимально допустимой длины {0} в элементе", maxLength));
            Assert.AreEqual(maxLength, GetText().Length, description);
        }
    }
}