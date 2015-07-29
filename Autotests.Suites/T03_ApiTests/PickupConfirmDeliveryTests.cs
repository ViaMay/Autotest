using System.Collections.Specialized;
using Autotests.Tests.T04_AdminTests;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;
namespace Autotests.Tests.T03_ApiTests
{
    public class PickupConfirmDeliveryTests : SendOrdersBasePage
    {
        [Test, Description("Принять заказ на склад по штрих-коду")]
        public void PickupConfirmDeliveryTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
//            отправляем две заявки
            string[] ordersId = SendOrdersRequest();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");
            adminMaintenancePage.AlertText.WaitTextContains("Processed");
            var usersPage =
                LoadPage<UsersPage>("/admin/users/?&filters[username]=" + pickupNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var pickupId = userEdiringPage.Key.GetValue();

//            шлем запрос потверить их
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection
                {
//                    {"barcode", "dd-" + ordersId[0] + "M01" },
                    {"barcode", "dd-" + ordersId[0] },
                }
                );
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");
//            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #dd-" + ordersId[0] + "M01" + 
            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #dd-" + ordersId[0] + 
                " подтвержден. Заказ подтвержден у вас на складе и ожидает отправки в транспортную компанию");
            Assert.AreEqual(responseConfirmDelivery.Response.Status, "20");

            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection
                {
//                    {"barcode", "dd-" + ordersId[1] + "M01" },
                    {"barcode", "dd-" + ordersId[1]},
                }
                );
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");
//            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #dd-" + ordersId[1] + "M01" +
            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #dd-" + ordersId[1] +
                " подтвержден. Заказ подтвержден у вас на складе и ожидает отправки в транспортную компанию");
            Assert.AreEqual(responseConfirmDelivery.Response.Status, "20");
        }

        [Test, Description("Принять заказ на склад по штрих-коду - неудачный запрос")]
        public void PickupConfirmDeliveryErrorTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            var usersPage =
                LoadPage<UsersPage>("/admin/users/?&filters[username]=" + pickupNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var pickupId = userEdiringPage.Key.GetValue();
            var errorOrderId = "dd-123456";

//            отправляем запрос на подтверждение с некорректным номером заявки
            var responseConfirmDelivery = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
               new NameValueCollection
                {
                    {"barcode", errorOrderId},
                }
               );
            Assert.IsFalse(responseConfirmDelivery.Success);
//            Assert.AreEqual(responseConfirmDelivery.Response.ErrorText, "Заказ не найден. Сверьте номер на штрих-коде, соответствует ли он коду " + errorOrderId);
            Assert.AreEqual(responseConfirmDelivery.Response.ErrorText, "Заказ не найден");

//            отправляем запрос с пустым номером заявки
            responseConfirmDelivery = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
               new NameValueCollection
                {}
               );
            Assert.IsFalse(responseConfirmDelivery.Success);
            Assert.AreEqual(responseConfirmDelivery.Response.ErrorText, "Укажите штрих-код");
        }
    }
}