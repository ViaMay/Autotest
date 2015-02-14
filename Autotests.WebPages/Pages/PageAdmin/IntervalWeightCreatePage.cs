using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class IntervalWeightCreatePage : AdminPageBase
    {
        public IntervalWeightCreatePage()
        {
            Name = new TextInput(By.Name("name"));
            Min = new TextInput(By.Name("min"));
            Max = new TextInput(By.Name("max"));

            SaveButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }

        public TextInput Name { get; set; }
        public TextInput Min { get; set; }
        public TextInput Max { get; set; }

        public ButtonInput SaveButton { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Name.WaitVisible();
        }
    }
}