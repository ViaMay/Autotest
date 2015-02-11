using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class СalculatorListControl : HtmlControl
    {
        public СalculatorListControl(By className)
            : base(className)
        {
            locator = className.ToString().Replace("By.XPath: ", "");;
        }

        public СalculatorRowControl GetRow(int index)
        {
            var row = new СalculatorRowControl(index + 1, locator);
            return row;
        }
        private readonly string locator;
    }
}