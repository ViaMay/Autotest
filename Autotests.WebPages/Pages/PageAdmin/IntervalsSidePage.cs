using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class IntervalsSidePage : AdminPageBase
    {
        public IntervalsSidePage()
        {
            LabelDirectoryIntervalsSide = new StaticText(By.CssSelector("legend"));
            IntervalSideCreate = new Link(By.LinkText("Создать"));
            Table = new BaseTableListControl(By.ClassName("table"));
        }

        public IntervalsSidePage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<IntervalsSidePage>();
        }

        public StaticText LabelDirectoryIntervalsSide { get; set; }
        public Link IntervalSideCreate { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryIntervalsSide.WaitText(@"Справочник ""Размеры""");
            IntervalSideCreate.WaitVisible();
        }
    }
}