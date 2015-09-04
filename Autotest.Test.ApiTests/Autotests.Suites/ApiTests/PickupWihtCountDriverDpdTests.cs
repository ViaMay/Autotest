using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class PickupWihtCountDriverDpdTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заказа на самовывоз с количеством мест подтверждения и отмена заявки")]
        public void PickupWihtCountDriverDpdTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryPointsPage =
                LoadPage<AdminBaseListPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            var deliveryPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
            var deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            var deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();
            deliveryCompaniesPage.Table.GetRow(0).ActionsEdit.Click();
            var companyCreatePage = deliveryCompaniesPage.GoTo<CompanyCreatePage>();
            companyCreatePage.CompanyDriver.SelectValue("Dpd");
            companyCreatePage.SaveButton.Click();
            deliveryCompaniesPage = companyCreatePage.GoTo<CompaniesPage>();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

            //            формируем документы чтобы не было списка заказов
            var usersPage = LoadPage<UsersPage>("/admin/users/?&filters[username]=" + pickupNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var pickupId = userEdiringPage.Key.GetValue();
            var responseDocumentsDeliveryError = apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_delivery.json",
               new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });

            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "1"},
		        {"delivery_point", deliveryPoinId},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "true"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_phone", "79999999999"},
		        {"to_add_phone", "71234567890"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
		        {"items_count", "3"},
		        {"is_cargo_volume", "true"},
		        {"order_comment", "order_comment"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");

            usersPage =
                LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            var userCreatePage = usersPage.GoTo<UserCreatePage>();
            var userKey = userCreatePage.Key.GetValue();

            var responseBarcodes = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", responseCreateOrders.Response.OrderId }, });

//            подтверждаем что заказ на складе
            var responseCreateFailOrder = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes.Response.Barcodes[0] }, });
            Assert.IsTrue(responseCreateFailOrder.Success);
            Assert.AreEqual(responseCreateFailOrder.Response.Message,
                "В заказе dd" + responseCreateOrders.Response.OrderId + " 3 мест, " +
                "вы отсканировали место " + responseBarcodes.Response.Barcodes[0] + "," +
                " остались места " + responseBarcodes.Response.Barcodes[1] + ", " + responseBarcodes.Response.Barcodes[2]);

            responseCreateFailOrder = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes.Response.Barcodes[1] }, });
            Assert.IsTrue(responseCreateFailOrder.Success);
            Assert.AreEqual(responseCreateFailOrder.Response.Message,
               "В заказе dd" + responseCreateOrders.Response.OrderId + " 3 мест, " +
               "вы отсканировали место " + responseBarcodes.Response.Barcodes[1] + "," +
               " остались места " + responseBarcodes.Response.Barcodes[2]);

            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes.Response.Barcodes[2] }, });
            Assert.IsTrue(responseConfirmDelivery.Success);
            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #" + responseBarcodes.Response.Barcodes[2]
                + " подтвержден. Заказ подтвержден у вас на складе и ожидает отправки в транспортную компанию");
            Assert.AreEqual(responseConfirmDelivery.Response.Status, "20");

//            Возврат
            responseCreateFailOrder = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseBarcodes.Response.Barcodes[0] }, });
            Assert.IsTrue(responseCreateFailOrder.Success);
            Assert.AreEqual(responseCreateFailOrder.Response.Message,
                "В заказе dd" + responseCreateOrders.Response.OrderId + " 3 мест, " +
                "вы отсканировали место " + responseBarcodes.Response.Barcodes[0] + "," +
                " остались места " + responseBarcodes.Response.Barcodes[1] + ", " + responseBarcodes.Response.Barcodes[2]);

            responseCreateFailOrder = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseBarcodes.Response.Barcodes[1] }, });
            Assert.IsTrue(responseCreateFailOrder.Success);
            Assert.AreEqual(responseCreateFailOrder.Response.Message,
               "В заказе dd" + responseCreateOrders.Response.OrderId + " 3 мест, " +
               "вы отсканировали место " + responseBarcodes.Response.Barcodes[1] + "," +
               " остались места " + responseBarcodes.Response.Barcodes[2]);

            var responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseBarcodes.Response.Barcodes[2] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message, "Заказ #" + responseBarcodes.Response.Barcodes[2]
                + " подтвержден для возврата. Заказ подтвержден для возврата и ожидает отправки в интернет-магазин");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");

            deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            deliveryCompaniesPage.Table.GetRow(0).ActionsEdit.Click();
            companyCreatePage = deliveryCompaniesPage.GoTo<CompanyCreatePage>();
            companyCreatePage.CompanyDriver.SelectValue("Boxberry");
            companyCreatePage.SaveButton.Click();
            deliveryCompaniesPage = companyCreatePage.GoTo<CompaniesPage>();
            adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
        }
    }
}