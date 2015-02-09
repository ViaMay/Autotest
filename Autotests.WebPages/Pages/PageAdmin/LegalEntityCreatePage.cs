using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class LegalEntityCreatePage : AdminPageBase
    {
        public LegalEntityCreatePage()
        {
            NameEntity = new TextInput(By.Name("name"));

            SaveButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }

        public TextInput NameEntity { get; set; }

        public ButtonInput SaveButton { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            NameEntity.WaitVisible();
        }
    }
}