using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.StartSettingTests
{
    public class T05CreateDeliveryPointTests : ConstVariablesTestBase
    {
        [Test, Description("Создания точки доставки")]
        public void CreateDeliveryPointTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.DeliveryPoints.Click();
            var deliveryPointsPage = adminPage.GoTo<AdminBaseListPage>();
            deliveryPointsPage.LabelDirectory.WaitText(@"Справочник ""Пункты выдачи""");
            deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
            deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();

            while (deliveryPointsPage.Table.GetRow(0).Name.IsPresent)
            {
                deliveryPointsPage.Table.GetRow(0).ActionsDelete.Click();
                deliveryPointsPage.Aletr.Accept();
                deliveryPointsPage = deliveryPointsPage.GoTo<AdminBaseListPage>();
                deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
                deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();
            }
            deliveryPointsPage.Create.Click();
            var deliveryPointCreatePage = deliveryPointsPage.GoTo<DeliveryPointCreatePage>();
            deliveryPointCreatePage.City.SetFirstValueSelect("Москва");
            deliveryPointCreatePage.DeliveryPointName.SetValueAndWait(deliveryPointName);
            deliveryPointCreatePage.CompanyName.SetFirstValueSelect(companyName);
            deliveryPointCreatePage.Address.SetValueAndWait(deliveryPointAddress);
            deliveryPointCreatePage.Longitude.SetValueAndWait(deliveryPointLongitude);
            deliveryPointCreatePage.Latitude.SetValueAndWait(deliveryPointLatitude);
            deliveryPointCreatePage.HasFittingRoom.Click();
            deliveryPointCreatePage.IsCard.Click();
            deliveryPointCreatePage.IsCash.Click();
            deliveryPointCreatePage.SaveButton.Click();
            deliveryPointsPage = deliveryPointCreatePage.GoTo<AdminBaseListPage>();

            deliveryPointsPage.Create.Click();
            deliveryPointCreatePage = deliveryPointsPage.GoTo<DeliveryPointCreatePage>();
            deliveryPointCreatePage.City.SetFirstValueSelect("Санкт-Петербург");
            deliveryPointCreatePage.DeliveryPointName.SetValueAndWait(deliveryPointName + "2");
            deliveryPointCreatePage.CompanyName.SetFirstValueSelect(companyName);
            deliveryPointCreatePage.Address.SetValueAndWait(deliveryPointAddress2);
            deliveryPointCreatePage.Longitude.SetValueAndWait(deliveryPointLongitude2);
            deliveryPointCreatePage.Latitude.SetValueAndWait(deliveryPointLatitude2);
            deliveryPointCreatePage.HasFittingRoom.Click();
            deliveryPointCreatePage.IsCard.Click();
            deliveryPointCreatePage.IsCash.Click();
            deliveryPointCreatePage.SaveButton.Click();
            deliveryPointsPage = deliveryPointCreatePage.GoTo<AdminBaseListPage>();
            deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
            deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();
            deliveryPointsPage.Table.GetRow(1).Name.WaitPresence();

            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/mongo_points");
            adminMaintenancePage.AlertText.WaitTextContains("Синхронизировано");
            adminMaintenancePage.AlertText.WaitTextContains("точек самовывоза");
        }
    }
}