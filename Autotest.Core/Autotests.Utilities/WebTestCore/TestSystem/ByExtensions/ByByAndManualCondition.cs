using System;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.TestSystem.ByExtensions
{
    public class ByByAndManualCondition : By
    {
        private readonly By by;
        private readonly Func<IWebElement, bool> condition;

        internal ByByAndManualCondition(By by, Func<IWebElement, bool> condition)
        {
            this.condition = condition;
            this.by = by;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            ReadOnlyCollection<IWebElement> elements = by.FindElements(context);
            return elements.Where(condition).Single();
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            ReadOnlyCollection<IWebElement> elements = by.FindElements(context);
            return new ReadOnlyCollection<IWebElement>(elements.Where(condition).ToArray());
        }
    }
}