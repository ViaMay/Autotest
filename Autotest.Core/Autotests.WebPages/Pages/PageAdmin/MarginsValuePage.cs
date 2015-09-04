using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class MarginsValuePage : AdminPageBase
    {
        public MarginsValuePage()
        {
            LabelDirectory = new StaticText(By.CssSelector("legend"));

            Table = new BaseTableListControl(By.ClassName("table"));
        }


        public MarginsValuePage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<MarginsValuePage>();
        }

        public StaticText LabelDirectory { get; set; }
        public BaseTableListControl Table { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitText(@"Справочник ""Наценки""");
        }
    }
}