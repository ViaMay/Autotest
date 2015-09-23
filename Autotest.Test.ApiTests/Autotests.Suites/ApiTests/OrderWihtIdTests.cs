using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class OrderWihtIdTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заказа на самовывоз c указанием ID, запрос статусов, информации, подтверждения и отмена заявки"), Ignore]
        public void OrderWihtIdTest()
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

            var usersPage =
                LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            var userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.BarcodeLimit.SetValueAndWait("10");
            var userKey = userCreatePage.Key.GetValue();
            userCreatePage.SaveButton.Click();
            userCreatePage = userCreatePage.GoTo<UserCreatePage>();

//            получение шрикодов
            var response =
                (ApiResponse.ResponseUserBarcodes)apiRequest.POST("cabinet/" + userKey + "/get_barcodes.json",
                    new NameValueCollection
                    {});

            //            формируем документы чтобы не было списка заказов
            usersPage = LoadPage<UsersPage>("/admin/users/?&filters[username]=" + pickupNameAndPass);
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
		        {"_id", response.Response.Barcodes[0]},
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
            Assert.AreEqual(responseCreateOrders.Response.OrderId, response.Response.Barcodes[0]);
            Assert.AreEqual(responseCreateOrders.Response.Barcodes[0], "dd-" + response.Response.Barcodes[0] + "m01");
            Assert.AreEqual(responseCreateOrders.Response.Barcodes[1], "dd-" + response.Response.Barcodes[0] + "m02");
            Assert.AreEqual(responseCreateOrders.Response.Barcodes[2], "dd-" + response.Response.Barcodes[0] + "m03");
            
            var defaultPage = shopsPage.LoginOut();
            var homePage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            
            var ordersPage = LoadPage<OrdersListPage>("user/?search=" + responseCreateOrders.Response.OrderId);
            ordersPage.Table.GetRow(0).ID.WaitText(responseCreateOrders.Response.OrderId);

//           Подтверждение заявки
            var responseConfirmationOrders = apiRequest.POST(keyShopPublic + "/order_confirm.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", responseCreateOrders.Response.OrderId},
                });
            Assert.IsTrue(responseConfirmationOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            
            defaultPage = ordersPage.LoginOut();
            var adminPage = defaultPage.LoginAsAdmin(adminName, adminPass);
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

//            формируем  документы
            var responseDocumentsDelivery = apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_delivery.json",
               new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });
            Assert.IsTrue(responseDocumentsDelivery.Success);

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

        [Test, Description("Создание заказа на курьерку c указанием ID, не корректный"), Ignore]
        public void OrderWihtIdErrorTest()
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

            var usersPage =
                LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            var userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.BarcodeLimit.SetValueAndWait("10");
            var userKey = userCreatePage.Key.GetValue();
            userCreatePage.SaveButton.Click();
            userCreatePage = userCreatePage.GoTo<UserCreatePage>();

            //            получение штрихкодов
            var response =
                (ApiResponse.ResponseUserBarcodes) apiRequest.POST("cabinet/" + userKey + "/get_barcodes.json",
                    new NameValueCollection {});

            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder) apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "2"},
                        {"_id", response.Response.Barcodes[0]},
                        {"to_city", "151184"},
                        {"delivery_company", deliveryCompanyId},
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
                        {"to_phone", "79999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"items_count", "2"},
                        {"is_cargo_volume", "true"},
                        {"order_comment", "order_comment"}
                    });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateOrders.Response.OrderId, response.Response.Barcodes[0]);
            Assert.AreEqual(responseCreateOrders.Response.Barcodes[0], "dd-" + response.Response.Barcodes[0] + "m01");
            Assert.AreEqual(responseCreateOrders.Response.Barcodes[1], "dd-" + response.Response.Barcodes[0] + "m02");

//            повторное создание  - получаем ошибку:
            var responseCreateOrdersError =
                (ApiResponse.ResponseFail)apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "2"},
                        {"_id", response.Response.Barcodes[0]},
                        {"to_city", "151184"},
                        {"delivery_company", deliveryCompanyId},
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
                        {"to_phone", "79999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"items_count", "2"},
                        {"is_cargo_volume", "true"},
                        {"order_comment", "order_comment"}
                    });
            Assert.AreEqual(responseCreateOrdersError.Response.ErrorText, "Нельзя использовать этот номер");

//             создание с неправильным id - получаем ошибку:
            responseCreateOrdersError =
                (ApiResponse.ResponseFail)apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "2"},
                        {"_id", "dfgdfg"},
                        {"to_city", "151184"},
                        {"delivery_company", deliveryCompanyId},
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
                        {"to_phone", "79999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"items_count", "2"},
                        {"is_cargo_volume", "true"},
                        {"order_comment", "order_comment"}
                    });
            Assert.AreEqual(responseCreateOrdersError.Response.ErrorText, "Номер заказа не существует в вашем пуле");
        }


//        [Test, Description("Создание 3 заказов одновременно с одним ID")]
//        public void OrderWihtIdErrorTest02()
//        {
//            LoginAsAdmin(adminName, adminPass);
//            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
//            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
//            var deliveryPointsPage =
//                LoadPage<AdminBaseListPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
//            var deliveryPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
//            var deliveryCompaniesPage =
//                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
//            var deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();
//
//            var usersPage =
//                LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
//            usersPage.Table.GetRow(0).ActionsEdit.Click();
//            var userCreatePage = usersPage.GoTo<UserCreatePage>();
//            userCreatePage.BarcodeLimit.SetValueAndWait("10");
//            var userKey = userCreatePage.Key.GetValue();
//            userCreatePage.SaveButton.Click();
//            userCreatePage = userCreatePage.GoTo<UserCreatePage>();
//
////            получение штрихкодов
//            var response =
//                (ApiResponse.ResponseUserBarcodes)apiRequest.POST("cabinet/" + userKey + "/get_barcodes.json",
//                    new NameValueCollection { });
//            var barcode =response.Response.List[0];
//
//
//            Parallel.Invoke(
//                
//                () => SendOrders(new[] { keyShopPublic, barcode, deliveryCompanyId, "2400" }),
//                () => SendOrders(new[] { keyShopPublic, barcode, deliveryCompanyId, "2300" }))
//                ; 
//
//
//            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/");
//            оrdersInputPage.Table.RowSearch.ID.SetValue(response.Response.List[0]);
//            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
//            оrdersInputPage.Table.GetRow(0).ID.WaitText(response.Response.List[0]);
//            оrdersInputPage.Table.GetRow(1).ID.WaitAbsence();
//        }
//
//        void SendOrders(object o)
//        {
//            var b = o as string[];
//            var responseCreateOrders =
//                apiRequest.POST(b[0] + "/order_create.json",
//                    new NameValueCollection
//                    {
//                        {"api_key", b[0]},
//                        {"type", "2"},
//                        {"_id", b[1]},
//                        {"to_city", "151184"},
//                        {"delivery_company", b[2]},
//                        {"shop_refnum", userShopName},
//                        {"dimension_side1", "4"},
//                        {"dimension_side2", "4"},
//                        {"dimension_side3", "4"},
//                        {"confirmed", "true"},
//                        {"weight", "4"},
//                        {"declared_price", "100"},
//                        {"payment_price", "300"},
//                        {"to_name", "Ургудан Рабат Мантов"},
//                        {"to_street", "Барна"},
//                        {"to_house", "3a"},
//                        {"to_flat", "12"},
//                        {"to_phone", "79999999999"},
//                        {"to_add_phone", "71234567890"},
//                        {"to_email", userNameAndPass},
//                        {"goods_description", "Памперс"},
//                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
//                        {"items_count", "2"},
//                        {"is_cargo_volume", "true"},
//                        {"order_comment", b[3]}
//                        
//                    });
//        }
    }
}