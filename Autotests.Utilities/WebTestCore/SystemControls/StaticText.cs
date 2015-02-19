using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class StaticText : HtmlControl
    {
        public StaticText(By locator, HtmlControl container = null)
            : base(locator, container)
        {
        }

        public StaticText(string idLocator, HtmlControl container = null)
            : base(idLocator, container)
        {
        }

        public void GetSearchContext111()
        {
            WebDriverCache.WebDriver.driver.FindElement(By.XPath("//li[2]/a/span")).Click();
            WebDriverCache.WebDriver.driver.FindElement(By.XPath("//li[3]/a/span")).Click();
            WebDriverCache.WebDriver.driver.FindElement(By.XPath("//li[4]/a/span")).Click();
            WebDriverCache.WebDriver.driver.FindElement(By.XPath("//li[5]/a/span[2]")).Click();
            WebDriverCache.WebDriver.driver.FindElement(By.XPath("//li[6]/a/span[2]")).Click();
            WebDriverCache.WebDriver.driver.FindElement(By.CssSelector("ymaps.ymaps-image")).Click();
            WebDriverCache.WebDriver.driver.FindElement(By.LinkText("Заберу здесь")).Click();
        }

        public void WaitValue(string value)
        {
            string description = FormatWithLocator(string.Format("Ожидание value '{0}' в элементе", value));
            Waiter.Wait(() => GetAttributeValue("value") == value, description);
        }
    }
}