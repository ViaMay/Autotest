using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class OrderCreateToShopApiKeyTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заказа курьерской используя апи кей магазина")]
        public void OrderCourirsTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

           var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "true"},
		        {"weight", "4"},
                {"declared_price", "1000"},
		        {"payment_price", "10"},
                {"to_phone", "79999999999"},
		        {"to_add_phone", "71234567890"},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
		        {"items_count", "2"},
		        {"is_cargo_volume", "true"},
		        {"to_shop_api_key", keyShopPublic},
		        {"order_comment", "order_comment"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var defaultPage = shopsPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Orders.Click();
            var ordersPage = userPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).ID.WaitText(responseCreateOrders.Response.OrderId);
            ordersPage.Table.GetRow(0).Type.WaitText("Курьерская");
            ordersPage.Table.GetRow(0).Number.WaitText(userShopName);
            ordersPage.Table.GetRow(0).Status.WaitText("Подтверждена");
            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");

            ordersPage.Table.GetRow(0).Edit.Click();
            var orderCourirsEditingPage = ordersPage.GoTo<OrderCourirsEditingPage>();

            orderCourirsEditingPage.City.WaitValue("Москва");
            orderCourirsEditingPage.BuyerStreet.WaitValue("Улица");
            orderCourirsEditingPage.BuyerHouse.WaitValue("Дом");
            orderCourirsEditingPage.BuyerFlat.WaitValue("Квартира");
            orderCourirsEditingPage.BuyerName.WaitValue("test_legalEntity");
            orderCourirsEditingPage.BuyerPostalCode.WaitValue("555444");
            orderCourirsEditingPage.BuyerPhone.WaitValue("+7 (999)999-9999");
            orderCourirsEditingPage.BuyerPhoneAdd.WaitValue("71234567890");
            orderCourirsEditingPage.BuyerEmail.WaitValue(userNameAndPass);
        }

        [Test, Description("Создание заказа курьерской используя апи кей магазина")]
        public void OrderCourirsTest02()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

            //            Создания склада
            shopsPage.AdminUsers.Click();
            shopsPage.UsersWarehouses.Click();
            var warehousesPage =
                LoadPage<AdminBaseListPage>("/admin/warehouses/?&filters[name]=" + companyName + "_orders");
            while (warehousesPage.Table.GetRow(0).Name.IsPresent)
            {
                warehousesPage.Table.GetRow(0).ActionsDelete.Click();
                warehousesPage.Aletr.Accept();
                warehousesPage = LoadPage<AdminBaseListPage>("/admin/warehouses/?&filters[name]=" + companyName + "_orders"); ;
            }

            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_orders"},
                        {"city", "151185"},
                        {"contact_person", "contact_person"},
                        {"contact_phone", "+7 (999)999-3333"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "schedule"},
                        {"street", "street"},
                        {"postal_code", "444333"},
                        {"house", "house"},
                        {"flat", "flat"},
                    }
                    );
            Assert.IsTrue(responseWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var usersPage = LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var userId = userEdiringPage.Key.GetValue();

            //            удаление магазинов если они были
            shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_orders");
            while (shopsPage.Table.GetRow(0).Name.IsPresent)
            {
                shopsPage.Table.GetRow(0).ActionsDelete.Click();
                shopsPage.Aletr.Accept();
                shopsPage = shopsPage.GoTo<UsersShopsPage>();
                shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_orders");
            }

            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("cabinet/" + userId + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_orders"},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Санкт-Петербург"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                 new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"delivery_company", deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "1000"},
		        {"payment_price", "0"},
                {"to_phone", "79999999999"},
		        {"to_add_phone", "71234567890"},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
		        {"items_count", "2"},
		        {"is_cargo_volume", "true"},
		        {"to_shop_api_key", responseShop.Response.Key},
		        {"order_comment", "order_comment"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var defaultPage = shopsPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Orders.Click();
            var ordersPage = userPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).ID.WaitText(responseCreateOrders.Response.OrderId);
            ordersPage.Table.GetRow(0).Type.WaitText("Курьерская");
            ordersPage.Table.GetRow(0).Number.WaitText(userShopName);
            ordersPage.Table.GetRow(0).Status.WaitText("В обработке");
            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Санкт-Петербург");

            ordersPage.Table.GetRow(0).Edit.Click();
            var orderCourirsEditingPage = ordersPage.GoTo<OrderCourirsEditingPage>();

            orderCourirsEditingPage.City.WaitValue("Санкт-Петербург");
            orderCourirsEditingPage.BuyerStreet.WaitValue("street");
            orderCourirsEditingPage.BuyerHouse.WaitValue("house");
            orderCourirsEditingPage.BuyerFlat.WaitValue("flat");
            orderCourirsEditingPage.BuyerName.WaitValue("contact_person");
            orderCourirsEditingPage.BuyerPostalCode.WaitValue("444333");
            orderCourirsEditingPage.BuyerPhone.WaitValue("+7 (999)999-9999");
            orderCourirsEditingPage.BuyerPhoneAdd.WaitValue("71234567890");
            orderCourirsEditingPage.BuyerEmail.WaitValue(userNameAndPass);
        }

       

        [Test, Description("Создание заказа на самовывоз, запрос статусов, информации, подтверждения и отмена заявки")]
        public void OrderSelfTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryPointsPage =
                LoadPage<AdminBaseListPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName + "2");
            var deliveryPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
            var deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            var deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder) apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"delivery_point", deliveryPoinId},
                        {"delivery_company", deliveryCompanyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "false"},
                        {"weight", "4"},
                        {"declared_price", "100"},
                        {"payment_price", "300"},
                        {"to_phone", "79999999999"},
                        {"to_add_phone", "71234567890"},
                        {"goods_description", "Памперс"},
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"items_count", "2"},
                        {"to_shop_api_key", keyShopPublic},
                        {"is_cargo_volume", "true"},
                        {"order_comment", "order_comment"}
                    });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var defaultPage = shopsPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Orders.Click();
            var ordersPage = userPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).ID.WaitText(responseCreateOrders.Response.OrderId);
            ordersPage.Table.GetRow(0).Type.WaitText("Самовывоз");
            ordersPage.Table.GetRow(0).Status.WaitText("В обработке");
            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Санкт-Петербург");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Подтвердить");
            ordersPage.Table.GetRow(0).Edit.WaitText("Редактировать");
            ordersPage.Table.GetRow(0).Edit.Click();
            var orderSelfEditingPage = ordersPage.GoTo<OrderSelfEditingPage>();

            orderSelfEditingPage.Width.WaitValue("4");
            orderSelfEditingPage.Height.WaitValue("4");
            orderSelfEditingPage.Length.WaitValue("4");
            orderSelfEditingPage.Weight.WaitValue("4");

            orderSelfEditingPage.BuyerName.WaitValue("test_legalEntity");
            orderSelfEditingPage.BuyerPhone.WaitValue("+7 (999)999-9999");
            orderSelfEditingPage.BuyerPhoneAdd.WaitValue("71234567890");
            orderSelfEditingPage.BuyerEmail.WaitValue(userNameAndPass);
        }
    }
}