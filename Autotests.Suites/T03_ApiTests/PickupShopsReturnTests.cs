using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;
namespace Autotests.Tests.T03_ApiTests
{
    public class PickupShopsReturnTests : SendOrdersBasePage
    {
        [Test, Description("Список интернет-магазинов, имеющих заказы на возврат на складе сортировки")]
        public void PickupShopsReturnTest()
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

            usersPage =
                LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            var userCreatePage = usersPage.GoTo<UserCreatePage>();
            var userKey = userCreatePage.Key.GetValue();

            var responseBarcodes01 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[0] }, });
            var responseBarcodes02 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[1] }, });

//            подтверждаем что заказы на складе
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes01.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");
            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes02.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            формируем документы
            var responseDocumentsDelivery = apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_delivery.json",
               new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });
            Assert.IsTrue(responseDocumentsDelivery.Success);

            var ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[0]);
            ordersEditPage.PickupStatus.WaitText("Передан в компанию Доставки");
            ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[1]);
            ordersEditPage.PickupStatus.WaitText("Передан в компанию Доставки");

//            делаем возврат
            var responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseBarcodes01.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");
            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseBarcodes02.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");
            
            ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[0]);
            ordersEditPage.PickupStatus.WaitText("Возврат со склада Сортировки");
            ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[1]);
            ordersEditPage.PickupStatus.WaitText("Возврат со склада Сортировки");

//            запрашиваем магазины 
            var responsePickupShop =
                (ApiResponse.ResponseCompaniesOrShops)apiRequest.GET("api/v1/pickup/" + pickupId + "/shops_return.json",
                    new NameValueCollection {});
            Assert.IsTrue(responsePickupShop.Success);
            Assert.AreEqual(responsePickupShop.Response.Count(), 1);
            Assert.AreEqual(responsePickupShop.Response[0].Name, userShopName);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string shopId = shopsPage.Table.GetRow(0).ID.GetText();
            Assert.AreEqual(responsePickupShop.Response[0].Id, shopId);

//            формируем документы на возврат
            var responseDocumentsReturn = (ApiResponse.ResponseDocumentPickup)apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_return.json",
               new NameValueCollection { { "shop_id", shopId }, });
            Assert.IsTrue(responseDocumentsReturn.Success);

//            снова запрашиваем магазин когда нет никого
            var responseError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupId + "/shops_return.json",
                new NameValueCollection {});
            Assert.IsFalse(responseError.Success);
            Assert.AreEqual(responseError.Response.ErrorText, "Заказы не найдены");
        }
    }
}