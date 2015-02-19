using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class DeliveryPointsPage : AdminPageBase
    {
        public DeliveryPointsPage()
        {
            LabelDirectoryDeliveryPoints = new StaticText(By.CssSelector("legend"));
            DeliveryPointCreate = new Link(By.LinkText("Создать"));

            Table = new BaseTableListControl(By.ClassName("table"));
        }


        public DeliveryPointsPage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<DeliveryPointsPage>();
        }

        public StaticText LabelDirectoryDeliveryPoints { get; set; }
        public Link DeliveryPointCreate { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryDeliveryPoints.WaitText(@"Справочник ""Пункты выдачи""");
            DeliveryPointCreate.WaitVisible();
        }
    }
}