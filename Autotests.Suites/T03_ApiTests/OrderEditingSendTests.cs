using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class OrderEditingSendTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заказа курьерски и редактирование")]
        public void OrderCourirsEditingTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder) apiRequest.POST(keyShopPublic + "/order_create.json",
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
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"order_comment", "order_comment"}
                    });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responseEditOrders = (ApiResponse.ResponseAddObject) apiRequest.POST(keyShopPublic + "/order_update/" +
                                                                                     responseCreateOrders.Response
                                                                                         .OrderId + ".json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"dimension_side1", "5"},
                    {"dimension_side2", "5"},
                    {"dimension_side3", "5"},
                    {"weight", "5"},
                    {"declared_price", "1100"},
                    {"payment_price", "1300"},
                    {"to_name", "to_name"},
                    {"to_street", "to_street"},
                    {"to_house", "to_house"},
                    {"to_flat", "to_flat"},
                    {"to_phone", "1199999999"},
                    {"goods_description", "goods_description"},
                    {"to_email", "2" + userNameAndPass},
                    {"order_comment", "order_comment2"}
                });
            Assert.IsTrue(responseEditOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateOrders.Response.OrderId, responseEditOrders.Response.Id);

            DefaultPage defaultPage = shopsPage.LoginOut();
            UserHomePage userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Orders.Click();
            var ordersPage = userPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).ID.WaitText(responseCreateOrders.Response.OrderId);
            ordersPage.Table.GetRow(0).Type.WaitText("Курьерская");
            ordersPage.Table.GetRow(0).Number.WaitText(userShopName);
            ordersPage.Table.GetRow(0).Status.WaitText("Подтверждена");
            ordersPage.Table.GetRow(0).Recipient.WaitText("to_name");
            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");

            ordersPage.Table.GetRow(0).Edit.Click();
            var orderCourirsEditingPage = ordersPage.GoTo<OrderCourirsEditingPage>();

            orderCourirsEditingPage.DeclaredPrice.WaitValue("1100");
            orderCourirsEditingPage.Width.WaitValue("5");
            orderCourirsEditingPage.Height.WaitValue("5");
            orderCourirsEditingPage.Length.WaitValue("5");
            orderCourirsEditingPage.Weight.WaitValue("5");

            orderCourirsEditingPage.BuyerStreet.WaitValue("to_street");
            orderCourirsEditingPage.BuyerHouse.WaitValue("to_house");
            orderCourirsEditingPage.BuyerFlat.WaitValue("to_flat");
            orderCourirsEditingPage.BuyerName.WaitValue("to_name");
            orderCourirsEditingPage.BuyerPhone.WaitValue("+7 (119)999-9999");
            orderCourirsEditingPage.BuyerEmail.WaitValue("2" + userNameAndPass);

            orderCourirsEditingPage.PaymentPrice.WaitValue("1300");
            orderCourirsEditingPage.OrderNumber.WaitValue("test_userShops_via");
            orderCourirsEditingPage.GoodsDescription.WaitValue("goods_description");
            orderCourirsEditingPage.OrderComment.WaitValue("order_comment2");
            orderCourirsEditingPage.ItemsCount.WaitValue("1");
        }

        [Test, Description("Создание заказа самовывоза и редактирование")]
        public void OrderSelfEditingTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();
            var deliveryPointsPage =
                LoadPage<AdminBaseListPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            string deliveryPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
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
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"order_comment", "order_comment"}
                    });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

         var responseEditOrders = (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/order_update/" +
                                                                                     responseCreateOrders.Response
                                                                                         .OrderId + ".json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"dimension_side1", "5"},
                    {"dimension_side2", "5"},
                    {"dimension_side3", "5"},
                    {"weight", "5"},
                    {"declared_price", "1100"},
                    {"payment_price", "1300"},
                    {"to_name", "to_name"},
                    {"to_street", "to_street"},
                    {"to_house", "to_house"},
                    {"to_flat", "to_flat"},
                    {"to_phone", "1199999999"},
                    {"goods_description", "goods_description"},
                    {"to_email", "2" + userNameAndPass},
                    {"order_comment", "order_comment2"}
                });
            Assert.IsTrue(responseEditOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateOrders.Response.OrderId, responseEditOrders.Response.Id);

            DefaultPage defaultPage = shopsPage.LoginOut();
            UserHomePage userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Orders.Click();
            var ordersPage = userPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).ID.WaitText(responseCreateOrders.Response.OrderId);
            ordersPage.Table.GetRow(0).Type.WaitText("Самовывоз");
            ordersPage.Table.GetRow(0).Number.WaitText(userShopName);
            ordersPage.Table.GetRow(0).Status.WaitText("Подтверждена");
            ordersPage.Table.GetRow(0).Recipient.WaitText("to_name");
            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");

            ordersPage.Table.GetRow(0).Edit.Click();
            var orderSelfEditingPage = ordersPage.GoTo<OrderSelfEditingPage>();

            orderSelfEditingPage.Width.WaitValue("5");
            orderSelfEditingPage.Height.WaitValue("5");
            orderSelfEditingPage.Length.WaitValue("5");
            orderSelfEditingPage.Weight.WaitValue("5");

            orderSelfEditingPage.BuyerName.WaitValue("to_name");
            orderSelfEditingPage.BuyerPhone.WaitValue("+7 (119)999-9999");
            orderSelfEditingPage.BuyerEmail.WaitValue("2" + userNameAndPass);
            orderSelfEditingPage.DeclaredPrice.WaitValue("1100");
            orderSelfEditingPage.PaymentPrice.WaitValue("1300");
            orderSelfEditingPage.OrderNumber.WaitValue("test_userShops_via");
            orderSelfEditingPage.GoodsDescription.WaitValue("goods_description");
            orderSelfEditingPage.ItemsCount.WaitValue("1");
        }

        [Test, Description("Создание заказа курьерски и редактирование не корректное")]
        public void OrderErrorEditingTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder) apiRequest.POST(keyShopPublic + "/order_create.json",
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
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            string orderIdError = "123456";
            var responseFail = (ApiResponse.ResponseFail) apiRequest.POST(keyShopPublic + "/order_update/" +
                                                                          orderIdError + ".json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", orderIdError},
                    {"dimension_side1", "5"},
                    {"dimension_side2", "5"},
                    {"dimension_side3", "5"},
                    {"weight", "5"},
                    {"declared_price", "1100"},
                    {"payment_price", "1300"},
                    {"to_name", "to_name"},
                    {"to_street", "to_street"},
                    {"to_house", "to_house"},
                    {"to_flat", "to_flat"},
                    {"to_phone", "1199999999"},
                    {"goods_description", "goods_description"},
                    {"to_email", userNameAndPass}
                });
            Assert.IsFalse(responseFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseFail.Response.ErrorText, "Order not found");

            var responseOrderFail = (ApiResponse.ResponseFailObject) apiRequest.POST(keyShopPublic + "/order_update/" +
                                                                      responseCreateOrders.Response.OrderId
                                                                      + ".json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"confirmed", "true"},
                    {"weight", "5"},
                    {"declared_price", "1100"},
                    {"payment_price", "1300"},
                    {"to_name", "to_name"},
                    {"to_street", "to_street"},
                    {"to_house", "to_house"},
                    {"to_flat", "to_flat"},
                    {"to_phone", "1199999999"},
                    {"goods_description", "goods_description"},
                    {"to_email", "123"}
                });
            Assert.IsFalse(responseOrderFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseOrderFail.Response.Error.Email, "Email должно быть корректным адресом электронной почты");

            responseOrderFail = (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/order_update/" +
                                                          responseCreateOrders.Response.OrderId
                                                          + ".json",
                                                          new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"dimension_side1", "500"},
                });
            Assert.IsFalse(responseOrderFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseOrderFail.Response.Error.CalculateOrder, "Ошибка просчета цены, или маршрут недоступен");
        }
    }
}