using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class UserBarcodeGetTests : ConstVariablesTestBase
    {
        [Test, Description("get_barcodes.json Получить пул штрих-кодов")]
        public void UserGetBarcodesTest()
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
                    new NameValueCollection
                    {});
            Assert.AreEqual(response1.Response.Barcodes.Count(), 10);
            
            //            оправляем заказ используя три номера из списка
            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder) apiRequest.POST(keyShopPublic + "/pro_order_create.json",
                    new NameValueCollection
                    {
                       {"api_key", keyShopPublic},
                        {"type", "2"},
                        {"barcodes", response1.Response.Barcodes[2]+ 
                        ", " + response1.Response.Barcodes[1] + ", " + response1.Response.Barcodes[0]},
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

            var response2 =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_barcodes.json",
                    new NameValueCollection
                    {});
            Assert.AreEqual(response2.Response.Barcodes.Count(), 10);
            foreach (var code in response2.Response.Barcodes)
            {
                Assert.AreNotEqual(code, response1.Response.Barcodes[0]);
                Assert.AreNotEqual(code, response1.Response.Barcodes[1]);
                Assert.AreNotEqual(code, response1.Response.Barcodes[2]);
            }

            usersPage =
                LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.BarcodeLimit.SetValueAndWait("0");
            userCreatePage.SaveButton.Click();

            var responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userKey + "/edit_packages.json",
                    new NameValueCollection
                    {
                    {"type", "1"},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"barcodes", response2.Response.Barcodes[5]}
                    });
            Assert.AreEqual(responseFail.Response.ErrorText, "Резервирование штрихкодов для данного Пользователя недоступно");
        }

        [Test, Description("get_packages_by_order Получить ШК по номеру заказа")]
        public void UserGetPackagesByOrderTest()
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

            //            оправляем заказ используя три номера из списка
            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/pro_order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"barcodes", response1.Response.Barcodes[2]+ 
                        ", " + response1.Response.Barcodes[1] + ", " + response1.Response.Barcodes[0]},
                        {"delivery_point", deliveryPoinId},
                        {"to_city", "151184"},
                        {"delivery_company", deliveryCompanyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "fasle"},
                        {"weight", "4"},
                        {"declared_price", "100"},
                        {"payment_price", "300"},
                        {"to_name", "Ургудан Рабат Мантов"},
                        {"to_phone", "79999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},
                        {"order_comment", "order_comment"}
                    });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responsePackagesOrder =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                    new NameValueCollection
                    {
                        {"order_id", responseCreateOrders.Response.OrderId},
                    });
            Assert.AreEqual(responsePackagesOrder.Response.Barcodes.Count(), 3);
            Assert.AreEqual(response1.Response.Barcodes[0], responsePackagesOrder.Response.Barcodes[0]);
            Assert.AreEqual(response1.Response.Barcodes[1], responsePackagesOrder.Response.Barcodes[1]);
            Assert.AreEqual(response1.Response.Barcodes[2], responsePackagesOrder.Response.Barcodes[2]);
            
            var responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                    new NameValueCollection
                    {
                    {"order_id", "123456"},
                    });
            Assert.AreEqual(responseFail.Response.ErrorText, "Заказ не найден");
        }


        [Test, Description("get_order_by_packages Получить номер заказа по шк")]
        public void UserGetOrderByPackagesTest()
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

            //            оправляем заказ используя три номера из списка
            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/pro_order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"barcodes", response1.Response.Barcodes[2]+ 
                        ", " + response1.Response.Barcodes[1] + ", " + response1.Response.Barcodes[0]},
                        {"delivery_point", deliveryPoinId},
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
                        {"to_phone", "79999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},
                        {"order_comment", "order_comment"}
                    });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responsePackagesOrder =
                (ApiResponse.ResponseTrueCancel)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_order_by_packages.json",
                    new NameValueCollection
                    {
                        {"barcode", response1.Response.Barcodes[1]},
                    });
            Assert.AreEqual(responsePackagesOrder.Response.OrderId, responseCreateOrders.Response.OrderId);

            var responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_order_by_packages.json",
                    new NameValueCollection
                    {
                    {"barcode", "123456"},
                    });
            Assert.AreEqual(responseFail.Response.ErrorText, "Передано недопустимое значение ШК");

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_order_by_packages.json",
                    new NameValueCollection
                    {
                    {"barcode", response1.Response.Barcodes[5]},
                    });
            Assert.AreEqual(responseFail.Response.ErrorText, "ШК не использован ни в одном заказе");

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_order_by_packages.json",
                    new NameValueCollection{});
            Assert.AreEqual(responseFail.Response.ErrorText, "Передано недопустимое значение ШК");
            usersPage =
                LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.BarcodeLimit.SetValueAndWait("0");
            userCreatePage.SaveButton.Click();

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_order_by_packages.json",
                    new NameValueCollection
                    {
                    {"barcode", response1.Response.Barcodes[5]},
                    });
            Assert.AreEqual(responseFail.Response.ErrorText, "Резервирование штрихкодов для данного Пользователя недоступно");
        }
    }
}