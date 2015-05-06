using System.Collections.Specialized;
using System.Linq;
using Autotests.Tests.T04_AdminTests;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;
namespace Autotests.Tests.T03_ApiTests
{
    public class PickupShopsTests : SendOrdersBasePage
    {
        [Test, Description("Список интернет-магазинов, имеющих заказы на складе сортировки")]
        public void PickupShopsTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            string[] ordersId = SendOrdersRequest();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");
            adminMaintenancePage.AlertText.WaitTextContains("Processed");
            var usersPage =
                LoadPage<UsersPage>("/admin/users/?&filters[username]=" + pickupNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var pickupId = userEdiringPage.Key.GetValue();
            var companiesPage = LoadPage<CompaniesPage>("admin/companies/?&filters[name]=" + companyName);
            var deliveryCompanyId = companiesPage.Table.GetRow(0).ID.GetText();

//            подтверждаем что заказы на складе
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection{{"barcode", "dd-" + ordersId[0] },});
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");

            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection{{"barcode", "dd-" + ordersId[1] },});
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            снова запрашиваем магазины
            var responsePickupShop =
                (ApiResponse.ResponsePickupCompaniesOrShops)apiRequest.GET("api/v1/pickup/" + pickupId + "/shops.json",
                    new NameValueCollection {});
            Assert.IsTrue(responsePickupShop.Success);
            Assert.AreEqual(responsePickupShop.Response.Count(), 1);
            Assert.AreEqual(responsePickupShop.Response[0].Name, userShopName);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string shopId = shopsPage.Table.GetRow(0).ID.GetText();
            Assert.AreEqual(responsePickupShop.Response[0].Id, shopId);
        }
    }
}