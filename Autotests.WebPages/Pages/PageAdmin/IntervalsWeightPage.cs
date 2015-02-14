using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class IntervalsWeightPage : AdminPageBase
    {
        public IntervalsWeightPage()
        {
            LabelDirectoryIntervalsWeight = new StaticText(By.CssSelector("legend"));
            IntervalWeightCreate = new Link(By.LinkText("Создать"));
            Table = new BaseTableListControl(By.ClassName("table"));
        }

        public IntervalsWeightPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<IntervalsWeightPage>();
        }

        public StaticText LabelDirectoryIntervalsWeight { get; set; }
        public Link IntervalWeightCreate { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryIntervalsWeight.WaitText(@"Справочник ""Веса""");
            IntervalWeightCreate.WaitVisible();
        }
    }
}