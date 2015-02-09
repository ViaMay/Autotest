using System.Collections.Generic;
using System.Collections.ObjectModel;
using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.TestSystem
{
    public class BySelf : By
    {
        private readonly HtmlControl control;

        public BySelf(HtmlControl control)
        {
            this.control = control;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return control.element;
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> list = control.element != null
                ? new List<IWebElement> {control.element}
                : new List<IWebElement>();
            return new ReadOnlyCollection<IWebElement>(list);
        }

        public override string ToString()
        {
            return "BySelf";
        }
    }
}