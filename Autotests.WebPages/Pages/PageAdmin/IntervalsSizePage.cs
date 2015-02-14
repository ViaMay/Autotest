using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class IntervalsSizePage : AdminPageBase
    {
        public IntervalsSizePage()
        {
            LabelDirectoryIntervalsSize = new StaticText(By.CssSelector("legend"));
            IntervalSizeCreate = new Link(By.LinkText("Создать"));
            Table = new BaseTableListControl(By.ClassName("table"));
        }

        public IntervalsSizePage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<IntervalsSizePage>();
        }

        public StaticText LabelDirectoryIntervalsSize { get; set; }
        public Link IntervalSizeCreate { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryIntervalsSize.WaitText(@"Справочник ""Размеры""");
            IntervalSizeCreate.WaitVisible();
        }
    }
}