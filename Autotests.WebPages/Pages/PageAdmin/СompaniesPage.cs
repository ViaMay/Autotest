using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class СompaniesPage : AdminPageBase
    {
        public СompaniesPage()
        {
            LabelDirectoryCompanies = new StaticText(By.CssSelector("legend"));
            CompanyCreate = new Link(By.LinkText("Создать"));

            Table = new CompaniesListControl(By.ClassName("table"));
        }

        
        public СompaniesPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<СompaniesPage>();
        }

        public StaticText LabelDirectoryCompanies { get; set; }
        public Link CompanyCreate { get; set; }
        public CompaniesListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryCompanies.WaitText(@"Справочник ""Компании""");
            CompanyCreate.WaitVisible();
        }
    }
}