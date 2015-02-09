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
            var row = new СalculatorRowControl(index, locator);
            return row;
        }

//        public СalculatorRowControl FindRowByName(string value)
//        {
//
//            var row = new СalculatorRowControl(index, locator);
//            return row;
//        }

        private readonly string locator;
    }
}