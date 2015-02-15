using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class LegalEntitiesPage : AdminPageBase
    {
        public LegalEntitiesPage()
        {
            LabelDirectoryLegalEntities = new StaticText(By.CssSelector("legend"));
            LegalEntityCreate = new Link(By.LinkText("Создать"));
            Table = new BaseTableListControl(By.ClassName("table"));
        }

        public LegalEntitiesPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<LegalEntitiesPage>();
        }

        public StaticText LabelDirectoryLegalEntities { get; set; }
        public Link LegalEntityCreate { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryLegalEntities.WaitText(@"Справочник ""Юридическое лицо""");
            LegalEntityCreate.WaitVisible();
        }
    }
}