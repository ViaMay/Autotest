using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class MarginsValueCreatePage : AdminPageBase
    {
        public MarginsValueCreatePage()
        {
            Value = new TextInput(By.Name("value"));
            Mode = new Select(By.Name("mode"));

            SaveButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }

        public TextInput Value { get; set; }
        public Select Mode { get; set; }

        public ButtonInput SaveButton { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Value.WaitVisible();
        }
    }
}