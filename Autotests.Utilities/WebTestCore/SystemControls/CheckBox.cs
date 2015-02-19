using Autotests.Utilities.WebTestCore.TestSystem;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class CheckBox : HtmlControl
    {
        public CheckBox(By locator, HtmlControl container = null)
            : base(locator, container)
        {
        }
    }
}