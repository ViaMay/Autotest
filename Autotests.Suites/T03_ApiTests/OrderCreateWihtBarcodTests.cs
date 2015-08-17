using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class OrderCreateWihtBarcod : ConstVariablesTestBase
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
            var companyCreatePage = LoadPage<CompanyCreatePage>("admin/companies/edit/" + deliveryCompanyId);
            companyCreatePage.BarcodePull.CheckAndWait();
            companyCreatePage.SaveButton.Click();
            deliveryCompaniesPage = companyCreatePage.GoTo<CompaniesPage>();
            var usersPage =
               LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            var userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.BarcodeLimit.SetValueAndWait("10");
            var userKey = userCreatePage.Key.GetValue();
            userCreatePage.SaveButton.Click();
            userCreatePage = userCreatePage.GoTo<UserCreatePage>();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
            //            получение шрикодов
            var response1 =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_barcodes.json",
                    new NameValueCollection { });
            Assert.AreEqual(response1.Response.Barcodes.Count(), 10);

            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/pro_order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "2"},
                        {"barcodes", response1.Response.Barcodes[1] + ", " + response1.Response.Barcodes[0]},
                        {"to_city", "151184"},
                        {"delivery_company", "" + deliveryCompanyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "fasle"},
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
                        {"items_count", "1"},
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
                    {"weight", "5"},
                    {"items_count", "3"},
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
            ordersPage.Table.GetRow(0).Status.WaitText("В обработке");
            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");

            ordersPage.Table.GetRow(0).ID.Click();
            var orderPage = ordersPage.GoTo<OrderPage>();

            orderPage.TableSize.Weight.WaitText("5.00 кг");
            orderPage.TableSize.Count.WaitText("2");
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
            var companyCreatePage = LoadPage<CompanyCreatePage>("admin/companies/edit/" + deliveryCompanyId);
            companyCreatePage.BarcodePull.CheckAndWait();
            companyCreatePage.SaveButton.Click();
            deliveryCompaniesPage = companyCreatePage.GoTo<CompaniesPage>();
            var deliveryPointsPage =
                LoadPage<AdminBaseListPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            string deliveryPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();

            var usersPage =
               LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            var userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.BarcodeLimit.SetValueAndWait("10");
            var userKey = userCreatePage.Key.GetValue();
            userCreatePage.SaveButton.Click();
            userCreatePage = userCreatePage.GoTo<UserCreatePage>();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
            //            получение шрикодов
            var response1 =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_barcodes.json",
                    new NameValueCollection { });
            Assert.AreEqual(response1.Response.Barcodes.Count(), 10);

            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"barcodes", response1.Response.Barcodes[1] + ", " + response1.Response.Barcodes[0]},
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
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"items_count", "1"},
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
                    {"weight", "5"},
                    {"items_count", "3"},
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
            ordersPage.Table.GetRow(0).Status.WaitText("В обработке");
            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");

            ordersPage.Table.GetRow(0).ID.Click();
            var orderPage = ordersPage.GoTo<OrderPage>();

            orderPage.TableSize.Weight.WaitText("5.00 кг");
            orderPage.TableSize.Count.WaitText("2");
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
            var deliveryPointsPage =
                LoadPage<AdminBaseListPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            string deliveryPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();

            var usersPage =
               LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            var userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.BarcodeLimit.SetValueAndWait("10");
            var userKey = userCreatePage.Key.GetValue();
            userCreatePage.SaveButton.Click();
            userCreatePage = userCreatePage.GoTo<UserCreatePage>();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
            //            получение шрикодов
            var response1 =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_barcodes.json",
                    new NameValueCollection { });
            Assert.AreEqual(response1.Response.Barcodes.Count(), 10);

            usersPage =
               LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.BarcodeLimit.SetValueAndWait("0");
            userCreatePage.SaveButton.Click();
            userCreatePage = userCreatePage.GoTo<UserCreatePage>();

            var responseCreateOrders =
                (ApiResponse.ResponseFail)apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"barcodes", response1.Response.Barcodes[1] + ", " + response1.Response.Barcodes[0]},
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
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"order_comment", "order_comment"}
                    });
            Assert.AreEqual(responseCreateOrders.Response.ErrorText, "Резервирование штрихкодов, для данного Пользователя недоступно.");

            usersPage =
               LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.BarcodeLimit.SetValueAndWait("10");
            userCreatePage.SaveButton.Click();
            userCreatePage = userCreatePage.GoTo<UserCreatePage>();

            var companyCreatePage = LoadPage<CompanyCreatePage>("admin/companies/edit/" + deliveryCompanyId);
            companyCreatePage.BarcodePull.UncheckAndWait();
            companyCreatePage.SaveButton.Click();
            deliveryCompaniesPage = companyCreatePage.GoTo<CompaniesPage>();
            adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

            responseCreateOrders =
                (ApiResponse.ResponseFail)apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"barcodes", response1.Response.Barcodes[1] + ", " + response1.Response.Barcodes[0]},
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
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"order_comment", "order_comment"}
                    });
            Assert.AreEqual(responseCreateOrders.Response.ErrorText, "Резервирование штрих-кодов недоступно для указанной ТК");

            companyCreatePage = LoadPage<CompanyCreatePage>("admin/companies/edit/" + deliveryCompanyId);
            companyCreatePage.BarcodePull.CheckAndWait();
            companyCreatePage.SaveButton.Click();
            deliveryCompaniesPage = companyCreatePage.GoTo<CompaniesPage>();
            adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

            responseCreateOrders =
                (ApiResponse.ResponseFail)apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"barcodes", response1.Response.Barcodes[1] + ", 123456, " + response1.Response.Barcodes[0]},
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
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"order_comment", "order_comment"}
                    });
            Assert.AreEqual(responseCreateOrders.Response.ErrorText, "Передано недопустимое значение ШК");

            var responseCreateOrders2 =
                (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {
                            "barcodes", response1.Response.Barcodes[4] + ", " + response1.Response.Barcodes[5]
                         + ", " + response1.Response.Barcodes[6] + ", " + response1.Response.Barcodes[7]
                        },
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
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"order_comment", "order_comment"}
                    });
            Assert.AreEqual(responseCreateOrders2.Response.Error.ItemsCount, "Количество мест в заказе превышает допустимое для данной ТК");
        }
    }
}