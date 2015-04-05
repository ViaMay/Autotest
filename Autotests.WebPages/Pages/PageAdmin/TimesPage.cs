using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class TimesPage : AdminPageBase
    {
        public TimesPage()
        {
            LabelDirectoryTimesCourier = new StaticText(By.CssSelector("legend"));
            TimesCourierCreate = new Link(By.LinkText("Создать"));

            Table = new BaseTableListControl(By.ClassName("table"));
        }


        public TimesPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<TimesPage>();
        }

        public StaticText LabelDirectoryTimesCourier { get; set; }
        public Link TimesCourierCreate { get; set; }
        public BaseTableListControl Table { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryTimesCourier.WaitTextContains(@"Справочник ""Сроки ");
            TimesCourierCreate.WaitVisible();
        }
    }
}