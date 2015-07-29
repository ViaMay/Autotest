using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class PickupWihtCountTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заказа на самовывоз с количеством мест подтверждения и отмена заявки"), Ignore]
        public void OrderWihtCountTest()
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
		        {"confirmed", "false"},
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
            Assert.AreEqual(responseCreateOrders.Response.Barcodes[0], "dd-" + responseCreateOrders.Response.OrderId + "m01");
            Assert.AreEqual(responseCreateOrders.Response.Barcodes[1], "dd-" + responseCreateOrders.Response.OrderId + "m02");
            Assert.AreEqual(responseCreateOrders.Response.Barcodes[2], "dd-" + responseCreateOrders.Response.OrderId + "m03");
            
//           Подтверждение заявки
            var responseConfirmationOrders = apiRequest.POST(keyShopPublic + "/order_confirm.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", responseCreateOrders.Response.OrderId},
                });
            Assert.IsTrue(responseConfirmationOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");

//            подтверждаем что заказ на складе
            var responseConfirmPart = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseCreateOrders.Response.Barcodes[0] }, });
//                new NameValueCollection { { "barcode", "dd-" + responseCreateOrders.Response.OrderId }, });
            Assert.IsTrue(responseConfirmPart.Success);
            Assert.AreEqual(responseConfirmPart.Response.Message, "В заказе dd-" + responseCreateOrders.Response.OrderId
                + " 3 мест, вы отсканировали место m01, остались места m02, m03");

            responseConfirmPart = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseCreateOrders.Response.Barcodes[1] }, });
//                new NameValueCollection { { "barcode", "dd-" + responseCreateOrders.Response.OrderId + "m02" }, });
            Assert.IsTrue(responseConfirmPart.Success);
            Assert.AreEqual(responseConfirmPart.Response.Message, "В заказе dd-" + responseCreateOrders.Response.OrderId
                + " 3 мест, вы отсканировали место m02, остались места m03");

            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseCreateOrders.Response.Barcodes[2] }, });
//                new NameValueCollection { { "barcode", "dd-" + responseCreateOrders.Response.OrderId + "m03" }, });
            Assert.IsTrue(responseConfirmDelivery.Success);
            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #" + responseCreateOrders.Response.Barcodes[2]
                + " подтвержден. Заказ подтвержден у вас на складе и ожидает отправки в транспортную компанию");

//            формируем  документы
            var responseDocumentsDelivery = apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_delivery.json",
               new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });
            Assert.IsTrue(responseDocumentsDelivery.Success);
        }

        [Test, Description("Создание заказа на самовывоз с количеством мест подтверждения и отмена заявки"), Ignore]
        public void OrderWihtCountTest02()
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
            Assert.AreEqual(responseCreateOrders.Response.Barcodes[0], "dd-" + responseCreateOrders.Response.OrderId + "m01");
            Assert.AreEqual(responseCreateOrders.Response.Barcodes[1], "dd-" + responseCreateOrders.Response.OrderId + "m02");
            Assert.AreEqual(responseCreateOrders.Response.Barcodes[2], "dd-" + responseCreateOrders.Response.OrderId + "m03");
            
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");

//            подтверждаем что заказ на складе
            var responseConfirmPart = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseCreateOrders.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmPart.Success);
            Assert.AreEqual(responseConfirmPart.Response.Message, "В заказе dd-" + responseCreateOrders.Response.OrderId
                + " 3 мест, вы отсканировали место m01, остались места m02, m03");

            responseConfirmPart = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseCreateOrders.Response.Barcodes[1] }, });
            Assert.IsTrue(responseConfirmPart.Success);
            Assert.AreEqual(responseConfirmPart.Response.Message, "В заказе dd-" + responseCreateOrders.Response.OrderId
                + " 3 мест, вы отсканировали место m02, остались места m03");

            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseCreateOrders.Response.Barcodes[2] }, });
            Assert.IsTrue(responseConfirmDelivery.Success);
            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #" + responseCreateOrders.Response.Barcodes[2]
                + " подтвержден. Заказ подтвержден у вас на складе и ожидает отправки в транспортную компанию");

            //            Возврат 
            responseConfirmPart = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseCreateOrders.Response.Barcodes[2] }, });
            Assert.IsTrue(responseConfirmPart.Success);
            Assert.AreEqual(responseConfirmPart.Response.Message, "В заказе dd-" + responseCreateOrders.Response.OrderId
                + " 3 мест, вы отсканировали место m03, остались места m01, m02");

            responseConfirmPart = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseCreateOrders.Response.Barcodes[1] }, });
            Assert.IsTrue(responseConfirmPart.Success);
            Assert.AreEqual(responseConfirmPart.Response.Message, "В заказе dd-" + responseCreateOrders.Response.OrderId
                + " 3 мест, вы отсканировали место m02, остались места m01");

            var responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseCreateOrders.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message, "Заказ #" + responseCreateOrders.Response.Barcodes[0]
                + " подтвержден для возврата. Заказ подтвержден для возврата и ожидает отправки в интернет-магазин");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");

            //            при повторной отправке на возврат с другим шрихкодам
            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseCreateOrders.Response.Barcodes[1] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message, "Заказ #" + responseCreateOrders.Response.Barcodes[1]
                + " уже ожидает возврата. Заказ подтвержден для возврата и ожидает отправки в интернет-магазин");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");
        }
    }
}