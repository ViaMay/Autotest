using System.Collections.Specialized;
using Autotests.Tests.T04_AdminTests;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;
namespace Autotests.Tests.T03_ApiTests
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

//            подтверждаем что заказ на складе
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
//                new NameValueCollection { { "barcode", "dd-" + ordersId[0] + "M01" }, });
                new NameValueCollection { { "barcode", "dd-" + ordersId[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success);

            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
//                new NameValueCollection { { "barcode", "dd-" + ordersId[1] + "M01" }, });
                new NameValueCollection { { "barcode", "dd-" + ordersId[1] }, });
            Assert.IsTrue(responseConfirmDelivery.Success);

//            формируем  документы
            var companiesPage = LoadPage<CompaniesPage>("admin/companies/?&filters[name]=" + companyName);
            var deliveryCompanyId = companiesPage.Table.GetRow(0).ID.GetText();
            var responseDocumentsDelivery = apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_delivery.json",
               new NameValueCollection{ {"delivery_company_id", deliveryCompanyId},});
            Assert.IsTrue(responseDocumentsDelivery.Success);

//            делаем возврат
            var responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
//                new NameValueCollection { { "barcode", "dd-" + ordersId[0] + "M01" }, });
                new NameValueCollection { { "barcode", "dd-" + ordersId[0] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");

            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
//                new NameValueCollection { { "barcode", "dd-" + ordersId[1] + "M01" }, });
                new NameValueCollection { { "barcode", "dd-" + ordersId[1] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");

//            формируем документы на возврат
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string shopId = shopsPage.Table.GetRow(0).ID.GetText();
            var responseDocumentsReturn = (ApiResponse.ResponseDocumentPickup)apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_return.json",
               new NameValueCollection { { "shop_id", shopId }, });
            Assert.IsTrue(responseDocumentsReturn.Success);
            responseDocumentsReturn.Response.Confirm.Contains("http://s.");
            responseDocumentsReturn.Response.Confirm.Contains(".pdf?token=");

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