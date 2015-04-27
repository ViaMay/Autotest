using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class DocumentsRequesAndStatusTests : ConstVariablesTestBase
    {
        [Test, Description("Запрос документов на генерацию и проверка статуса - не работает на стрейдж")]
        public void DocumentsTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var usersWarehousesPage = LoadPage<UsersWarehousesPage>("/admin/warehouses?&filters[name]=" + userWarehouseName);
            var usersWarehousId = usersWarehousesPage.Table.GetRow(0).ID.GetText();
            var deliveryPointsPage =
                LoadPage<DeliveryPointsPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            var deliveryPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
            var deliveryCompaniesPage =
                LoadPage<СompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            var deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

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
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responseCreateOrders2 = (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
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
		        {"to_street", "Барна"},
		        {"to_house", "3a"},
		        {"to_flat", "12"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                });
            Assert.IsTrue(responseCreateOrders2.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");
            adminMaintenancePage.AlertText.WaitTextContains("Processed");

//            генерация документов
            var responseDocumentsRequest =
                (ApiResponse.ResponseDocumentsRequest)apiRequest.GET("api/v1/" + keyShopPublic + "/documents_request.json",
                new NameValueCollection
                {
                {"order_id", responseCreateOrders.Response.OrderId + "," + responseCreateOrders2.Response.OrderId }
                });

            Assert.IsTrue(responseDocumentsRequest.Success, "Ожидался ответ true на отправленный запрос Get по API");
            Assert.IsFalse(responseDocumentsRequest.Response.Completed);

//            спим минуту ждем генерации
           Thread.Sleep(60000);

            var responseDocumentsStatus =
                (ApiResponse.ResponseDocumentsRequest)apiRequest.GET("api/v1/" + keyShopPublic +
                 "/documents_status/" + responseDocumentsRequest.Response.RequestId + ".json", new NameValueCollection{});

            Assert.IsTrue(responseDocumentsStatus.Success, "Ожидался ответ true на отправленный запрос Get по API");
            Assert.IsTrue(responseDocumentsStatus.Response.Completed);
            Assert.AreEqual(responseDocumentsRequest.Response.RequestId, responseDocumentsStatus.Response.RequestId);
            Assert.AreEqual(responseDocumentsStatus.Response.Documents.Count(), 3);

            foreach (var document in responseDocumentsStatus.Response.Documents)
            {
                document.DeliveryCompany.Equals(deliveryCompanyId);
                document.PickupCompany.Equals(deliveryCompanyId);
                document.Warehouse.Equals(usersWarehousId);
            }
        }

        [Test, Description("Запрос документов на генерацию когда не правильный Id заказа")]
        public void DocumentsErrorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            //            генерация документов с не правильным Id заказа
            var responseDocumentsError =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/documents_request.json",
                new NameValueCollection
                {
                {"order_id", "123456" }
                });
            Assert.IsFalse(responseDocumentsError.Success, "Ожидался ответ false на отправленный запрос Get по API");
            Assert.AreEqual(responseDocumentsError.Response.ErrorText, "Order not found");

            //            проверка статуса документа с неправильным id RequestId
            var responseDocumentsStatus =
                (ApiResponse.ResponseDocumentsRequest)apiRequest.GET("api/v1/" + keyShopPublic +
                 "/documents_status/" + "a_23456" + ".json", new NameValueCollection { });

            Assert.IsTrue(responseDocumentsStatus.Success, "Ожидался ответ true на отправленный запрос Get по API");
            Assert.IsFalse(responseDocumentsStatus.Response.Completed);
        }
    }
}