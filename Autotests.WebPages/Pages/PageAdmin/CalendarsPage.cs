using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class CalendarsPage : AdminPageBase
    {
        public CalendarsPage()
        {
            LabelDirectoryCalendars = new StaticText(By.CssSelector("legend"));
            CalendarsCreate = new Link(By.LinkText("Создать"));
            Table = new BaseTableListControl(By.ClassName("table"));
        }

        public CalendarsPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<CalendarsPage>();
        }

        public StaticText LabelDirectoryCalendars { get; set; }
        public Link CalendarsCreate { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryCalendars.WaitText(@"Справочник ""Календарь выходных дней компании""");
            CalendarsCreate.WaitVisible();
        }
    }
}