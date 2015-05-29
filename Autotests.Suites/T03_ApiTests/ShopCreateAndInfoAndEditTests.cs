using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class ShopCreateAndInfoAndEditTests : ConstVariablesTestBase
    {
        [Test, Description("Создание магазина через Api админа")]
        public void ShopCreateAndInfoAndEditTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiAdmin"},
                        {"flat", "138"},
                        {"city", "416"},
                        {"contact_person", "contact_person"},
                        {"contact_phone", "contact_phone"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "schedule"},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsTrue(responseWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");

            shopsPage.Table.RowSearch.Name.SetValue(userShopName + "_ApiAdmin");
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            while (shopsPage.Table.GetRow(0).Name.IsPresent)
            {
                shopsPage.Table.GetRow(0).ActionsDelete.Click();
                shopsPage.Aletr.Accept();
                shopsPage = shopsPage.GoTo<UsersShopsPage>();
                shopsPage.Table.RowSearch.Name.SetValue(userShopName + "_ApiAdmin");
                shopsPage = shopsPage.SeachButtonRowClickAndGo();
            }

//            Создание магазина
            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiAdmin"},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Москва"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");
            var defaultPage = shopsPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            var row = shopsListPage.Table.FindRowByName(userShopName + "_ApiAdmin");
            row.Name.WaitTextContains(userShopName + "_ApiAdmin\r\nAPI ключ для модуля: " + responseShop.Response.Key);
            row.Address.WaitTextContains("Москва");

//            Получения информации о магазине
            var responseShopInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/shop_info.json",
                new NameValueCollection {}
                );
            Assert.IsTrue(responseShopInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseShopInfo.Response.Id, responseShop.Response.Id);
            Assert.AreEqual(responseShopInfo.Response.Name, userShopName + "_ApiAdmin");
            Assert.AreEqual(responseShopInfo.Response.Warehouse, responseWarehouse.Response.Id);
            Assert.AreEqual(responseShopInfo.Response.Address, "Москва");

//            редактирование магазина
            responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/shop_edit.json",
                new NameValueCollection
                {
                    {"_id", responseShop.Response.Id},
                    {"name", userShopName + "_ApiAdmin2"},
                    {"address", "Санкт-Питербург"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");
            shopsListPage.UseProfile.Click();
            shopsListPage.UserShops.Click();
            shopsListPage = shopsListPage.GoTo<UserShopsPage>();
            row = shopsListPage.Table.FindRowByName(userShopName + "_ApiAdmin2");
            row.Name.WaitTextContains(userShopName + "_ApiAdmin2\r\nAPI ключ для модуля: " + responseShop.Response.Key);
            row.Address.WaitTextContains("Санкт-Питербург");

            defaultPage = shopsListPage.LoginOut();
            var adminPage = defaultPage.LoginAsAdmin(adminName, adminPass);
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

//            Получения информации о магазине
            responseShopInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/shop_info.json",
                new NameValueCollection { }
                );
            Assert.IsTrue(responseShopInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseShopInfo.Response.Id, responseShop.Response.Id);
            Assert.AreEqual(responseShopInfo.Response.Name, userShopName + "_ApiAdmin2");
            Assert.AreEqual(responseShopInfo.Response.Warehouse, responseWarehouse.Response.Id);
            Assert.AreEqual(responseShopInfo.Response.Address, "Санкт-Питербург");

 //            повторное создание магазина
            responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/shop_create.json",
                new NameValueCollection
                {
                    {"_id", responseShop.Response.Id},
                    {"name", userShopName + "_ApiAdmin3"},
                    {"address", "Санкт-Питербург"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");
        }

        [Test, Description("Создание магазина через Api админа неудачное")]
        public void ShopCreateErrorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiAdmin"},
                        {"flat", "138"},
                        {"city", "416"},
                        {"contact_person", "contact_person"},
                        {"contact_phone", "contact_phone"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "schedule"},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsTrue(responseWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");

            //            Создание магазина - ошибка пусто адрес
            var responseShop = (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiAdmin"},
                    {"warehouse", responseWarehouse.Response.Id},
                }
                );
            Assert.IsFalse(responseShop.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseShop.Response.Error.Address, "Адрес обязательно к заполнению");

            //            Создание магазина - ошибка пусто склад
            responseShop = (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiAdmin"},
                    {"address", "Санкт-Питербург"}
                }
                );
            Assert.IsFalse(responseShop.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseShop.Response.Error.Warehouse, "Поле склад обязательно к заполнению");

//            пустое имя магазина
            responseShop = (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/shop_create.json",
                new NameValueCollection
                {
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Санкт-Питербург"}
                }
                );
            Assert.IsFalse(responseShop.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseShop.Response.Error.Name, "Название обязательно к заполнению");

//            такое имя уже было
            responseShop = (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Санкт-Питербург"}
                }
                );
            Assert.IsFalse(responseShop.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseShop.Response.Error.Name, "Такое имя уже существует");
        }

        [Test, Description("Редактирвоание магазина через Api админа неудачное")]
        public void ShopEditErrorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            string shopId = shopsPage.Table.GetRow(0).ID.GetText();

//            не правильный id магазина
            var errorId = "123456";
            var responseError = (ApiResponse.ResponseFail)apiRequest.POST(keyShopPublic + "/shop_edit.json",
                new NameValueCollection
                {
                    {"_id", errorId},
                    {"name", userShopName + "_ApiAdmin2"},
                    {"address", "Санкт-Питербург"}
                }
                );
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Shop not found");

//            не правильно указаны имя и адрес
            var responseError2 = (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/shop_edit.json",
                new NameValueCollection
                {
                    {"_id", shopId},
                    {"name", ""},
                    {"address", ""}
                }
                );
            Assert.IsFalse(responseError2.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError2.Response.Error.Name, "Название обязательно к заполнению");
            Assert.AreEqual(responseError2.Response.Error.Address, "Адрес обязательно к заполнению");
        }
    }
}