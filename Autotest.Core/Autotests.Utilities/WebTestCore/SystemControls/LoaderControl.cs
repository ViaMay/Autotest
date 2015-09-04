using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class LoaderControl : HtmlControl
    {
        public LoaderControl()
            : base(By.ClassName("loading"), null)
        {
        }
    }
}