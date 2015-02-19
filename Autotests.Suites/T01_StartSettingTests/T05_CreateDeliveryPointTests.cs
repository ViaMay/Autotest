using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
{
    public class T05CreateDeliveryPointTests : ConstVariablesTestBase
    {
        [Test, Description("Создания точки доставки")]
        public void CreateDeliveryPointTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminСompanies.Click();
            adminPage.DeliveryPoints.Click();
            var deliveryPointsPage = adminPage.GoTo<DeliveryPointsPage>();
            deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
            deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();

            while (deliveryPointsPage.Table.GetRow(0).Name.IsPresent)
            {
                deliveryPointsPage.Table.GetRow(0).ActionsDelete.Click();
                deliveryPointsPage = deliveryPointsPage.GoTo<DeliveryPointsPage>();
                deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
                deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();
            }
            deliveryPointsPage.DeliveryPointCreate.Click();
            var deliveryPointCreatePage = deliveryPointsPage.GoTo<DeliveryPointCreatePage>();
            deliveryPointCreatePage.City.SetFirstValueSelect("Москва");
            deliveryPointCreatePage.DeliveryPointName.SetValueAndWait(deliveryPointName);
            deliveryPointCreatePage.CompanyName.SetFirstValueSelect(сompanyName);
            deliveryPointCreatePage.Address.SetValueAndWait("Ленинский проспект 127");
            deliveryPointCreatePage.Longitude.SetValueAndWait("37.477078814299");
            deliveryPointCreatePage.Latitude.SetValueAndWait("55.645872547535");
            deliveryPointCreatePage.HasFittingRoom.Click();
            deliveryPointCreatePage.IsCard.Click();
            deliveryPointCreatePage.IsCash.Click();
            deliveryPointCreatePage.SaveButton.Click();
            deliveryPointsPage = deliveryPointCreatePage.GoTo<DeliveryPointsPage>();

            deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
            deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();
            deliveryPointsPage.Table.GetRow(0).Name.WaitPresence();

            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/mongo_points");
            adminMaintenancePage.AlertText.WaitTextContains("Синхронизировано");
            adminMaintenancePage.AlertText.WaitTextContains("точек самовывоза");
        }
    }
}