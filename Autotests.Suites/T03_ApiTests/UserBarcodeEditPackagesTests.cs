using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;
namespace Autotests.Tests.T03_ApiTests
{
    public class UserBarcodeEditPackagesTests : ConstVariablesTestBase
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
            
            //            оправляем заказ используя три номера из списка
            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder) apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
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
                        {"items_count", "2"},
                        {"is_cargo_volume", "true"},
                        {"order_comment", "order_comment"}
                    });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            
//            получения шк заказа
            var responsePackagesOrder =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                    new NameValueCollection
                    {
                        {"order_id", responseCreateOrders.Response.OrderId},
                    });
            Assert.AreEqual(responsePackagesOrder.Response.Barcodes.Count(), 2);

//            получение шрикодовпула
            var responsePackages =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_barcodes.json",
                    new NameValueCollection
                    {});
            Assert.AreEqual(responsePackages.Response.Barcodes.Count(), 10);
            
            var response = apiRequest.POST("cabinet/" + userKey + "/edit_packages.json",
                    new NameValueCollection
                    {
                    {"type", "2"},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"barcodes", responsePackagesOrder.Response.Barcodes[0]},
                    });
            Assert.IsTrue(response.Success);
            
            response = apiRequest.POST("cabinet/" + userKey + "/edit_packages.json",
                    new NameValueCollection
                    {
                    {"type", "1"},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"barcodes", responsePackages.Response.Barcodes[0] + ", " + responsePackages.Response.Barcodes[1] },
                    });
            Assert.IsTrue(response.Success);

//            проверка что наши коды
            var responsePackagesOrder2 =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                    new NameValueCollection
                    {
                        {"order_id", responseCreateOrders.Response.OrderId},
                    });
            Assert.AreEqual(responsePackagesOrder2.Response.Barcodes.Count(), 3);
            Assert.AreEqual(responsePackagesOrder2.Response.Barcodes[0], responsePackages.Response.Barcodes[0]);
            Assert.AreEqual(responsePackagesOrder2.Response.Barcodes[1], responsePackages.Response.Barcodes[1]);
            Assert.AreEqual(responsePackagesOrder2.Response.Barcodes[2], responsePackagesOrder.Response.Barcodes[1]);

//            пробуем добавить больше чем нужно
            var responseFail = 
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userKey + "/edit_packages.json",
                new NameValueCollection
                    {
                    {"type", "1"},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"barcodes", responsePackages.Response.Barcodes[3]},
                    });
            Assert.AreEqual(responseFail.Response.ErrorText, "Число мест должно быть не больше 3");

//            удалить пробуем все
            response =
                apiRequest.POST("cabinet/" + userKey + "/edit_packages.json",
                    new NameValueCollection
                    {
                    {"type", "2"},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"barcodes",  responsePackagesOrder2.Response.Barcodes[0] + ", " + responsePackagesOrder2.Response.Barcodes[1]}
                    });
            Assert.IsTrue(response.Success);

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userKey + "/edit_packages.json",
                    new NameValueCollection
                    {
                    {"type", "2"},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"barcodes", responsePackagesOrder2.Response.Barcodes[2]}
                    });
            Assert.AreEqual(responseFail.Response.ErrorText, "Нельзя удалять последний штрих-код");

            usersPage =
                LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.BarcodeLimit.SetValueAndWait("0");
            userCreatePage.SaveButton.Click();

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userKey + "/edit_packages.json",
                    new NameValueCollection
                    {
                    {"type", "1"},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"barcodes", responsePackages.Response.Barcodes[5]}
                    });
            Assert.AreEqual(responseFail.Response.ErrorText, "Резервирование штрихкодов для данного Пользователя недоступно");
        }

        [Test, Description("get_barcodes.json Получить пул штрих-кодов")]
        public void UserGetBarcodesErrorTest()
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

            //            оправляем заказ используя три номера из списка
            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
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

            //            получение шрикодовпула
            var responsePackages =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_barcodes.json",
                    new NameValueCollection { });
            Assert.AreEqual(responsePackages.Response.Barcodes.Count(), 10);

           var responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userKey + "/edit_packages.json",
                    new NameValueCollection
                    {
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"barcodes", responsePackages.Response.Barcodes[5]}
                    });
           Assert.AreEqual(responseFail.Response.ErrorText, "Неправильный тип");

           responseFail =
               (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userKey + "/edit_packages.json",
                   new NameValueCollection
                    {
                    {"type", "1"},
                    {"barcodes", responsePackages.Response.Barcodes[5]}
                    });
           Assert.AreEqual(responseFail.Response.ErrorText, "Заказ не найден");

           responseFail =
               (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userKey + "/edit_packages.json",
                   new NameValueCollection
                    {
                    {"type", "1"},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    });
           Assert.AreEqual(responseFail.Response.ErrorText, "Введите штрих-коды через запятую");

           responseFail =
               (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userKey + "/edit_packages.json",
                   new NameValueCollection
                    {
                    {"type", "1"},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"barcodes", "123456"}
                    });
           Assert.AreEqual(responseFail.Response.ErrorText, "Передано недопустимое значение ШК");
        }
    }
}
