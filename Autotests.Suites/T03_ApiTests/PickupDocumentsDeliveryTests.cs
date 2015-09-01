using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;
namespace Autotests.Tests.T03_ApiTests
{
    public class PickupDocumentsDeliveryTests : SendOrdersBasePage
    {
        [Test, Description("Запрос на получение документов на доставку")]
        public void PickupDocumentsDeliveryTest()
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

            var companiesPage = LoadPage<CompaniesPage>("admin/companies/?&filters[name]=" + companyName);
            var deliveryCompanyId = companiesPage.Table.GetRow(0).ID.GetText();

            var ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[0]);
            ordersEditPage.PickupStatus.WaitText("На складе ИМ");
            ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[1]);
            ordersEditPage.PickupStatus.WaitText("На складе ИМ");

            var responseBarcodes01 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[0] }, });
            var responseBarcodes02 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[1] }, });

//            подтверждения заявок
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes01.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #" + responseBarcodes01.Response.Barcodes[0] + 
                " подтвержден. Заказ подтвержден у вас на складе и ожидает отправки в транспортную компанию");
            Assert.AreEqual(responseConfirmDelivery.Response.Status, "20");

            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes02.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #" + responseBarcodes02.Response.Barcodes[0] +
                " подтвержден. Заказ подтвержден у вас на складе и ожидает отправки в транспортную компанию");
            Assert.AreEqual(responseConfirmDelivery.Response.Status, "20");

            ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[0]);
            ordersEditPage.PickupStatus.WaitText("На складе сортировки");
            ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[1]);
            ordersEditPage.PickupStatus.WaitText("На складе сортировки");

//            формирование документов
            var responseDocumentsDelivery = (ApiResponse.ResponseDocumentPickup)apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_delivery.json",
                new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });
            Assert.IsTrue(responseDocumentsDelivery.Success);
            responseDocumentsDelivery.Response.View.Contains("http://s.");
            responseDocumentsDelivery.Response.View.Contains(".pdf?token=");
            responseDocumentsDelivery.Response.Confirm.Contains("http://s.");
            responseDocumentsDelivery.Response.Confirm.Contains(".pdf?token=");

            ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[0]);
            ordersEditPage.PickupStatus.WaitText("Передан в компанию Доставки");
            ordersEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersId[1]);
            ordersEditPage.PickupStatus.WaitText("Передан в компанию Доставки");

//            формируем документы - нет заказов
            var responseDocumentsDeliveryError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_delivery.json",
               new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });
            Assert.IsFalse(responseDocumentsDeliveryError.Success);
            Assert.AreEqual(responseDocumentsDeliveryError.Response.ErrorText, "Заказы не найдены");

//            формируем документы - нет id
            responseDocumentsDeliveryError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_delivery.json",
               new NameValueCollection { });
            Assert.IsFalse(responseDocumentsDeliveryError.Success);
            Assert.AreEqual(responseDocumentsDeliveryError.Response.ErrorText, "Укажите ID транспортной компании");
        }
    }
}