using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class WarehouseAdminEditTests : ConstVariablesTestBase
    {
        [Test, Description("Редактирование склада через Api админа"), Ignore]
        public void WarehousesEditTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
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

            var responseEditWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/warehouse_edit.json",
                new NameValueCollection
                    {
                        {"_id", responseWarehouse.Response.Id},
                        {"name", userWarehouseName + "_ApiAdmin2"},
                        {"flat", "flat139"},
                        {"city", "417"},
                        {"contact_person", "contact_person2"},
                        {"contact_phone", "contact_phone2"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23"},
                        {"street", "street2"},
                        {"house", "house2"}
                    }
        );
            Assert.IsTrue(responseEditWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var defaultPage = warehousesPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserWarehouses.Click();
            var warehousesListPage = userPage.GoTo<UserWarehousesPage>();
            var row = warehousesListPage.Table.FindRowByName(userWarehouseName + "_ApiAdmin2");
            row.Name.WaitText(userWarehouseName + "_ApiAdmin2");
            row.City.WaitText("Кедровый");
            row.Address.WaitText("street2, house2 flat139");
            row.Contact.WaitText("contact_person2 (contact_phone2 tester@user.ru)");
            row.TimeWork.WaitText("1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23");
        }

        [Test, Description("Редактирвоание склада через Api админа неудачное"), Ignore]
        public void WarehousesEditErrorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
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
            
            //            Создания склада неудачное пустое id склада
            var responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST(keyShopPublic + "/warehouse_edit.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiAdmin"},
                        {"flat", ""},
                        {"city", ""},
                        {"contact_person", "contact_person"},
                        {"contact_phone", "contact_phone"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "schedule"},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Warehouse not found");

            responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST(keyShopPublic + "/warehouse_edit.json",
                    new NameValueCollection
                    {
                        {"_id", responseWarehouse.Response.Id},
                        {"name", userWarehouseName + "_ApiAdmin"},
                        {"flat", ""},
                        {"city", ""},
                        {"contact_person", "contact_person"},
                        {"contact_phone", "contact_phone"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "schedule"},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "City not found");

//            Создания склада неудачное
            var responseErrorWarehouse2 =
                (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/warehouse_edit.json",
                    new NameValueCollection
                    {
                        {"_id", responseWarehouse.Response.Id},
                        {"name", ""},
                        {"flat", ""},
                        {"city", "715"},
                        {"contact_person", ""},
                        {"contact_phone", ""},
                        {"contact_email", ""},
                        {"schedule", ""},
                        {"street", ""},
                        {"house", ""}
                    }
                    );
            Assert.IsFalse(responseErrorWarehouse2.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse2.Response.Error.Flat, "Номер помещения обязательно к заполнению");
            Assert.AreEqual(responseErrorWarehouse2.Response.Error.House, "Дом обязательно к заполнению");
            Assert.AreEqual(responseErrorWarehouse2.Response.Error.Street, "Улица обязательно к заполнению");
            Assert.AreEqual(responseErrorWarehouse2.Response.Error.Name, "Название обязательно к заполнению");
            Assert.AreEqual(responseErrorWarehouse2.Response.Error.ContactPerson, "Контактное лицо обязательно к заполнению");
            Assert.AreEqual(responseErrorWarehouse2.Response.Error.ContactPhone, "Контактный телефон обязательно к заполнению");
        }
    }

}