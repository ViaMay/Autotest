using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;
namespace Autotests.Tests.ApiTests
{
    public class PickupDocumentsReturnTests : SendOrdersBasePage
    {
        [Test, Description("Запрос на получение документов на возврат")]
        public void PickupDocumentsReturnTest()
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
            usersPage =
                LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            var userCreatePage = usersPage.GoTo<UserCreatePage>();
            var userKey = userCreatePage.Key.GetValue();

            var responseBarcodes01 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[0] }, });
            var responseBarcodes02 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[1] }, });

//            подтверждаем что заказ на складе
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes01.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success);
            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes02.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success);

//            делаем возврат
            var responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseBarcodes01.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");
            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseBarcodes02.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");

            var ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[0]);
            ordersEditPage.PickupStatus.WaitText("Возврат со склада Сортировки");
            ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[1]);
            ordersEditPage.PickupStatus.WaitText("Возврат со склада Сортировки");

//            формируем документы на возврат
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string shopId = shopsPage.Table.GetRow(0).ID.GetText();
            var responseDocumentsReturn = (ApiResponse.ResponseDocumentPickup)apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_return.json",
               new NameValueCollection { { "shop_id", shopId }, });
            Assert.IsTrue(responseDocumentsReturn.Success);
            responseDocumentsReturn.Response.Confirm.Contains("http://s.");
            responseDocumentsReturn.Response.Confirm.Contains(".pdf?token=");

            ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[0]);
            ordersEditPage.PickupStatus.WaitText("На складе ИМ");
            ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[1]);
            ordersEditPage.PickupStatus.WaitText("На складе ИМ");

//            формируем документы - нет заказов
            var responseDocumentsError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_return.json",
               new NameValueCollection { { "shop_id", shopId }, });
            Assert.IsFalse(responseDocumentsError.Success);
            Assert.AreEqual(responseDocumentsError.Response.ErrorText, "Заказы не найдены");

//            формируем документы - нет id
            responseDocumentsError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_return.json",
               new NameValueCollection { });
            Assert.IsFalse(responseDocumentsError.Success);
            Assert.AreEqual(responseDocumentsError.Response.ErrorText, "Укажите ID интернет-магазина");
        }
    }
}