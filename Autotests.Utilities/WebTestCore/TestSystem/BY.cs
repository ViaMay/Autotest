using System;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem.ByExtensions;
using Autotests.Utilities.WebTestCore.TestSystem.ByExtensions.ChangeId;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.TestSystem
{
    public static class BY
    {
        public static By Self(HtmlControl control)
        {
            return new BySelf(control);
        }

        public static By ByAndManualCondition(By by, Func<IWebElement, bool> condition)
        {
            return new ByByAndManualCondition(by, condition);
        }

        public static By NthOfBy(By by, int index)
        {
            return new ByNthOfBy(by, index);
        }

        public static By NthOfClass(string className, int index)
        {
            return new ByNthOfClass(className, index);
        }

        public static By ModifiedIdOfHtmlContol(HtmlControl control, ChangeIdStrategy changeIdStrategy)
        {
            return new ByModifiedIdOfHtmlContol(control, changeIdStrategy);
        }
    }
}