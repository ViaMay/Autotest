using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class TimesPickupPage : AdminPageBase
    {
        public TimesPickupPage()
        {
            LabelDirectoryTimesPickup = new StaticText(By.CssSelector("legend"));
            TimesPickupCreate = new Link(By.LinkText("Создать"));

            Table = new BaseTableListControl(By.ClassName("table"));
        }


        public TimesPickupPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<TimesPickupPage>();
        }

        public StaticText LabelDirectoryTimesPickup { get; set; }
        public Link TimesPickupCreate { get; set; }
        public BaseTableListControl Table { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryTimesPickup.WaitText(@"Справочник ""Время забора""");
            TimesPickupCreate.WaitVisible();
        }
    }
}