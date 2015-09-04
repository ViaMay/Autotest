using System;
using System.Collections.ObjectModel;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem.ByExtensions.ChangeId;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.TestSystem.ByExtensions
{
    public class ByModifiedIdOfHtmlContol : By
    {
        private readonly ChangeIdStrategy changeIdStrategy;
        private readonly HtmlControl control;

        internal ByModifiedIdOfHtmlContol(HtmlControl control, ChangeIdStrategy changeIdStrategy)
        {
            this.control = control;
            this.changeIdStrategy = changeIdStrategy;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            try
            {
                string id = control.Locator.FindElement(control.SearchContext).GetAttribute("id");
                string newId = changeIdStrategy.ChangeId(id);
                By newLocator = Id(newId);
                return newLocator.FindElement(context);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            try
            {
                string id = control.Locator.FindElement(control.SearchContext).GetAttribute("id");
                string newId = changeIdStrategy.ChangeId(id);
                By newLocator = Id(newId);
                return newLocator.FindElements(context);
            }
            catch (Exception)
            {
                return new ReadOnlyCollection<IWebElement>(new IWebElement[0]);
            }
        }

        public override string ToString()
        {
            return string.Format("ByModifiedIdOfHtmlContol: <{0}>[ChangeIdStrategy: {1}]", control, changeIdStrategy);
        }
    }
}