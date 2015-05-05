using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class CompaniesPage : AdminPageBase
    {
        public CompaniesPage()
        {
            LabelDirectoryCompanies = new StaticText(By.CssSelector("legend"));
            CompanyCreate = new Link(By.LinkText("Создать"));

            Table = new CompaniesListControl(By.ClassName("table"));
        }

        
        public CompaniesPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<CompaniesPage>();
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