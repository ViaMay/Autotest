using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class WarehouseInfoTests : ConstVariablesTestBase
    {
        [Test, Description("Получить информацию о текущем складе магазина Api админа")]
        public void WarehousesInfoTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            shopsPage.AdminUsers.Click();
            shopsPage.UsersWarehouses.Click();
            var warehousesPage = shopsPage.GoTo<UsersWarehousesPage>();
            warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName + "_Api");
            warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            while (warehousesPage.Table.GetRow(0).Name.IsPresent)
            {
                warehousesPage.Table.GetRow(0).ActionsDelete.Click();
                warehousesPage.Aletr.Accept();
                warehousesPage = warehousesPage.GoTo<UsersWarehousesPage>();
                warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName + "_ApiAdmin");
                warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            }
//            Создания склада
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

            shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_ApiAdmin");
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
//            Получения информации о текущем складе магазина
            var responseInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/warehouse_info.json",
                new NameValueCollection { }
                );

            Assert.IsTrue(responseInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseInfo.Response.Id, responseWarehouse.Response.Id);
            Assert.AreEqual(responseInfo.Response.Name, userWarehouseName + "_ApiAdmin");
            Assert.AreEqual(responseInfo.Response.Street, "street");
            Assert.AreEqual(responseInfo.Response.House, "house");
            Assert.AreEqual(responseInfo.Response.Flat, "138");
            Assert.AreEqual(responseInfo.Response.ContactEmail, userNameAndPass);
            Assert.AreEqual(responseInfo.Response.ContactPerson, "contact_person");
            Assert.AreEqual(responseInfo.Response.ContactPhone, "contact_phone");
            Assert.AreEqual(responseInfo.Response.Schedule, "10:00-19:00,10:00-19:00,10:00-19:00,10:00-19:00,10:00-19:00,NODAY,NODAY");
        }

        [Test, Description("Получить информацию о текущем складе магазина Api админа не удачное")]
        public void WarehousesInfoErrorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            shopsPage.AdminUsers.Click();
            shopsPage.UsersWarehouses.Click();
            var warehousesPage = shopsPage.GoTo<UsersWarehousesPage>();
            warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName + "_Api");
            warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            while (warehousesPage.Table.GetRow(0).Name.IsPresent)
            {
                warehousesPage.Table.GetRow(0).ActionsDelete.Click();
                warehousesPage.Aletr.Accept();
                warehousesPage = warehousesPage.GoTo<UsersWarehousesPage>();
                warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName + "_ApiAdmin");
                warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            }
            //            Создания склада
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
            shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_ApiAdmin");
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
//            удаления склада 
            shopsPage.AdminUsers.Click();
            shopsPage.UsersWarehouses.Click();
            warehousesPage = shopsPage.GoTo<UsersWarehousesPage>();
            warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName + "_Api");
            warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            while (warehousesPage.Table.GetRow(0).Name.IsPresent)
            {
                warehousesPage.Table.GetRow(0).ActionsDelete.Click();
                warehousesPage.Aletr.Accept();
                warehousesPage = warehousesPage.GoTo<UsersWarehousesPage>();
                warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName + "_ApiAdmin");
                warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            }
            //            Получения информации о текущем складе магазина
            var responseErrorWarehouse = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/warehouse_info.json",
                new NameValueCollection { }
                );
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Warehouse not found");
}
    }
}