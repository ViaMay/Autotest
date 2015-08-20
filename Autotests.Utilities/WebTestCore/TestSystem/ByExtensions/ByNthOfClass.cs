using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.TestSystem.ByExtensions
{
    public class ByNthOfClass : By
    {
        private readonly string className;
        private readonly int index;

        internal ByNthOfClass(string className, int index)
        {
            this.className = className;
            this.index = index;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            ReadOnlyCollection<IWebElement> elements = ClassName(className).FindElements(context);
            if (index < 0 || index >= elements.Count)
                throw new NoSuchElementException(
                    string.Format("Element with class '{0}' and index '{1}' not found (actual count='{2}')", className,
                        index, elements.Count));
            return elements[index];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            ReadOnlyCollection<IWebElement> elements = ClassName(className).FindElements(context);
            if (index < 0 || index >= elements.Count)
                return new ReadOnlyCollection<IWebElement>(new IWebElement[0]);
            return new ReadOnlyCollection<IWebElement>(new[] {elements[index]});
        }

        public override string ToString()
        {
            return string.Format("ByNthOfClass: .{0}[{1}]", className, index);
        }
    }
}