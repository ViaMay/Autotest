using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class LegalEntityCreatePage : AdminBaseListCreatePage
    {
        public LegalEntityCreatePage()
        {
            NameEntity = new TextInput(By.Name("name"));
        }

        public TextInput NameEntity { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            NameEntity.WaitVisible();
        }
    }
}