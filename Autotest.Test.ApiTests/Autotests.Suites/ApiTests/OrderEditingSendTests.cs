using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
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
                        {"to_postal_code", "123123"},
                        {"to_street", "Барна"},
                        {"to_house", "3a"},
                        {"to_flat", "12"},
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"is_cargo_volume", "true"},
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
                    {"to_postal_code", "333333"},
                    {"to_street", "to_street"},
                    {"to_house", "to_house"},
                    {"to_flat", "to_flat"},
                    {"to_phone", "1199999999"},
                    {"to_add_phone", "74444444444"},
                    {"goods_description", "goods_description"},
                    {"to_email", "2" + userNameAndPass},
                    {"order_comment", "order_comment2"},
                    {"is_cargo_volume", "false"},
                    {"items_count", "3"},
                });
            Assert.IsTrue(responseEditOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateOrders.Response.OrderId, responseEditOrders.Response.Id);

            DefaultPage defaultPage = shopsPage.LoginOut();
            UserHomePage userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            var ordersPage = LoadPage<OrdersListPage>("user/?search=" + responseCreateOrders.Response.OrderId);
            ordersPage.Table.GetRow(0).ID.WaitText(responseCreateOrders.Response.OrderId);
            ordersPage.Table.GetRow(0).Type.WaitText("Курьерская");
            ordersPage.Table.GetRow(0).Number.WaitText(userShopName);
            ordersPage.Table.GetRow(0).Status.WaitText("Подтверждена");
            ordersPage.Table.GetRow(0).Recipient.WaitText("to_name");
            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");

            ordersPage.Table.GetRow(0).ID.Click();
            var orderPage = ordersPage.GoTo<OrderPage>();

            orderPage.TableSender.City.WaitText("Москва");
            orderPage.TableSender.Address.WaitText("ул.Улица, дом Дом, офис(квартира) Квартира");
            orderPage.TableSender.Name.WaitText(legalEntityName);
            orderPage.TableSender.Phone.WaitText("+7 (111)111-1111");
            orderPage.TableSender.Delivery.WaitText("Курьерская");
            orderPage.TableSender.OrderComment.WaitText("order_comment2");
            orderPage.TableSender.IsCargoVolume.WaitText("да");

            orderPage.TableRecipient.City.WaitText("Москва");
            orderPage.TableRecipient.PostCode.WaitText("333333");
            orderPage.TableRecipient.Address.WaitText("ул.to_street, дом to_house, офис(квартира) to_flat");
            orderPage.TableRecipient.Name.WaitText("to_name");
            orderPage.TableRecipient.Email.WaitText("2" + userNameAndPass);
            orderPage.TableRecipient.Phone.WaitText("1199999999");
            orderPage.TableRecipient.PhoneAdd.WaitText("74444444444");
            orderPage.TableRecipient.Issue.WaitText("Ручная");
            orderPage.TableRecipient.PickupCompany.WaitText(companyPickupName);
            orderPage.TableRecipient.DeliveryCompany.WaitText(companyName);

            orderPage.TablePrice.PaymentPrice.WaitText("1300.00 руб.");
            orderPage.TablePrice.DeclaredPrice.WaitText("1100.00 руб.");
            orderPage.TablePrice.Insurance.WaitText("0.00 руб.");
            orderPage.TablePrice.DeliveryPrice.WaitText("38.00 руб.");
            orderPage.TablePrice.PickupPrice.WaitText("21.00 руб.");

            orderPage.TableSize.Width.WaitText("5 см");
            orderPage.TableSize.Height.WaitText("5 см");
            orderPage.TableSize.Length.WaitText("5 см");
            orderPage.TableSize.Weight.WaitText("5.00 кг");
            orderPage.TableSize.Count.WaitText("1");
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
                        {"to_add_phone", "1234567891234567890123456789001"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"is_cargo_volume", "true"},
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
                    {"to_add_phone", "74444444444"},
                    {"goods_description", "goods_description"},
                    {"to_email", "2" + userNameAndPass},
                    {"order_comment", "order_comment2"},
                    {"is_cargo_volume", "false"},
                    {"items_count", "3"},
                });
            Assert.IsTrue(responseEditOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateOrders.Response.OrderId, responseEditOrders.Response.Id);

            DefaultPage defaultPage = shopsPage.LoginOut();
            UserHomePage userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            var ordersPage = LoadPage<OrdersListPage>("user/?search=" + responseCreateOrders.Response.OrderId);
            ordersPage.Table.GetRow(0).ID.WaitText(responseCreateOrders.Response.OrderId);
            ordersPage.Table.GetRow(0).Type.WaitText("Самовывоз");
            ordersPage.Table.GetRow(0).Number.WaitText(userShopName);
            ordersPage.Table.GetRow(0).Status.WaitText("Подтверждена");
            ordersPage.Table.GetRow(0).Recipient.WaitText("to_name");
            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");

            ordersPage.Table.GetRow(0).ID.Click();
            var orderPage = ordersPage.GoTo<OrderPage>();

            orderPage.TableSender.City.WaitText("Москва");
            orderPage.TableSender.Address.WaitText("ул.Улица, дом Дом, офис(квартира) Квартира");
            orderPage.TableSender.Name.WaitText(legalEntityName);
            orderPage.TableSender.Phone.WaitText("+7 (111)111-1111");
            orderPage.TableSender.Delivery.WaitText("Самовывоз");
            orderPage.TableSender.OrderComment.WaitText("order_comment2");
            orderPage.TableSender.IsCargoVolume.WaitText("да");

            orderPage.TableRecipient.City.WaitText("Москва");
            orderPage.TableRecipient.PostCode.WaitText("");
            orderPage.TableRecipient.Address.WaitText("Ленинский проспект 127");
            orderPage.TableRecipient.Name.WaitText("to_name");
            orderPage.TableRecipient.Email.WaitText("2" + userNameAndPass);
            orderPage.TableRecipient.Phone.WaitText("1199999999");
            orderPage.TableRecipient.PhoneAdd.WaitText("74444444444");
            orderPage.TableRecipient.Issue.WaitText("Ручная");
            orderPage.TableRecipient.PickupCompany.WaitText(companyPickupName);
            orderPage.TableRecipient.DeliveryCompany.WaitText(companyName);

            orderPage.TablePrice.PaymentPrice.WaitText("1300.00 руб.");
            orderPage.TablePrice.DeclaredPrice.WaitText("1100.00 руб.");
            orderPage.TablePrice.Insurance.WaitText("0.00 руб.");
            orderPage.TablePrice.DeliveryPrice.WaitText("45.00 руб.");
            orderPage.TablePrice.PickupPrice.WaitText("21.00 руб.");

            orderPage.TableSize.Width.WaitText("5 см");
            orderPage.TableSize.Height.WaitText("5 см");
            orderPage.TableSize.Length.WaitText("5 см");
            orderPage.TableSize.Weight.WaitText("5.00 кг");
            orderPage.TableSize.Count.WaitText("1");
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


            responseOrderFail = (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/order_update/" +
                                                          responseCreateOrders.Response.OrderId
                                                          + ".json",
                                                          new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"order_comment", "1231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111112"},
                });
            Assert.IsFalse(responseOrderFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseOrderFail.Response.Error.OrderComment, @"Длина поля &laquo;Комментарий к заказу&raquo; должна быть не более 300 символов");
            responseOrderFail = (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/order_update/" +
                                                          responseCreateOrders.Response.OrderId
                                                          + ".json",
                                                          new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"to_add_phone", "1234567890123456789012345678901"},
                });
            Assert.IsFalse(responseOrderFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseOrderFail.Response.Error.ToAddPhone, @"Длина поля &laquo;Дополнительный телефон получателя&raquo; должна быть не более 30 символов");
        
        }
    }
}