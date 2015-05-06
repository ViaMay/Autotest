using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class DocumentsPage : AdminPageBase
    {
        public DocumentsPage()
        {
            LabelDirectory = new StaticText(By.CssSelector("legend"));
            Table = new BaseTableListControl(By.ClassName("table"));
        }

        public DocumentsPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<DocumentsPage>();
        }

        public StaticText LabelDirectory { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitText(@"Справочник ""Документы""");
        }
    }
}