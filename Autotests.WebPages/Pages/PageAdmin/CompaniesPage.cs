using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class CompaniesPage : AdminBaseListPage
    {
        public CompaniesPage()
        {
            Table = new CompaniesListControl(By.ClassName("table"));
        }

        
        public CompaniesPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<CompaniesPage>();
        }

        public CompaniesListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Create.WaitVisible();
        }
    }
}