using System.Collections.Specialized;
using Autotests.Tests.T04_AdminTests;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;
namespace Autotests.Tests.T03_ApiTests
{
    public class PickupConfirmReturnTests : SendOrdersBasePage
    {
        [Test, Description("Принять заказ для возврата по штрих-коду или почтовому идентификатору")]
        public void PickupConfirmReturnTest()
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

//            запрашиваем документы на возврат - ошибка
            var responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection{{"barcode", "dd-" + ordersId[0] },});
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message,"Заказ #dd-" + ordersId[0]
                + " невозможно вернуть. Заказ еще не отправлялся и находится на складе интернет-магазина");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "10");

            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection{{"barcode", "dd-" + ordersId[1] },});
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message, "Заказ #dd-" + ordersId[1]
                + " невозможно вернуть. Заказ еще не отправлялся и находится на складе интернет-магазина");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "10");

//            подтверждаем что заказ на складе  - ошибка
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection{{"barcode", "dd-" + ordersId[0] },});
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");

            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection{{"barcode", "dd-" + ordersId[1] },}
                );
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            снова проверяем статус
            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection{{"barcode", "dd-" + ordersId[0] },});
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message,"Заказ #dd-" + ordersId[0]
                + " невозможно вернуть. Заказ находится на складе Забора и ожидает отправки в транспортную компанию.");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "20");

            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection{{"barcode", "dd-" + ordersId[1] },});
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message, "Заказ #dd-" + ordersId[1]
                + " невозможно вернуть. Заказ находится на складе Забора и ожидает отправки в транспортную компанию.");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "20");

//            формируем  документы
            var companiesPage = LoadPage<CompaniesPage>("admin/companies/?&filters[name]=" + companyName);
            var deliveryCompanyId = companiesPage.Table.GetRow(0).ID.GetText();
            var responseDocumentsDelivery = apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_delivery.json",
               new NameValueCollection{ {"delivery_company_id", deliveryCompanyId},});
            Assert.IsTrue(responseDocumentsDelivery.Success);

//            проверяем теперь
            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection{{"barcode", "dd-" + ordersId[0] },});
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message,"Заказ #dd-" + ordersId[0]
                + " подтвержден для возврата. Заказ подтвержден для возврата и ожидает отправки в интернет-магазин");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");

            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection{{"barcode", "dd-" + ordersId[1] },});
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message, "Заказ #dd-" + ordersId[1]
                + " подтвержден для возврата. Заказ подтвержден для возврата и ожидает отправки в интернет-магазин");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");

//            при повторной отправке на возврат
            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection{{"barcode", "dd-" + ordersId[0] },});
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message,"Заказ #dd-" + ordersId[0]
                + " уже ожидает возврата. Заказ подтвержден для возврата и ожидает отправки в интернет-магазин");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");

            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection { {"barcode", "dd-" + ordersId[1] },});
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message, "Заказ #dd-" + ordersId[1]
                + " уже ожидает возврата. Заказ подтвержден для возврата и ожидает отправки в интернет-магазин");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");
        }

        [Test, Description("Принять заказ для возврата по штрих-коду или почтовому идентификатору - неудачный запрос")]
        public void PickupConfirmReturnErrorTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            var usersPage =
                LoadPage<UsersPage>("/admin/users/?&filters[username]=" + pickupNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var pickupId = userEdiringPage.Key.GetValue();
            var errorOrderId = "dd-123456";
            var responseConfirmDelivery = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
               new NameValueCollection
                {
                    {"barcode", errorOrderId},
                }
               );
            Assert.IsFalse(responseConfirmDelivery.Success);
            Assert.AreEqual(responseConfirmDelivery.Response.ErrorText, "Заказ не найден. Сверьте номер на штрих-коде, соответствует ли он коду " + errorOrderId);

            responseConfirmDelivery = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
               new NameValueCollection
                {}
               );
            Assert.IsFalse(responseConfirmDelivery.Success);
            Assert.AreEqual(responseConfirmDelivery.Response.ErrorText, "Укажите штрих-код или почтовый идентификатор");
        }
    }
}