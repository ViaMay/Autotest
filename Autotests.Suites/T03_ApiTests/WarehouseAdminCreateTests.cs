using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class WarehouseAdminCreateTests : ConstVariablesTestBase
    {
        [Test, Description("Создание склада через Api админа")]
        public void WarehousesCreateTest()
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
                (ApiResponse.ResponseAddObject) apiRequest.POST(keyShopPublic + "/warehouse_create.json",
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

            var defaultPage = warehousesPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserWarehouses.Click();
            var warehousesListPage = userPage.GoTo<UserWarehousesPage>();
            var row = warehousesListPage.Table.FindRowByName(userWarehouseName + "_ApiAdmin");
            row.Name.WaitText(userWarehouseName + "_ApiAdmin");
            row.City.WaitText("Томск");
            row.Address.WaitText("street, house 138");
            row.Contact.WaitText("contact_person (contact_phone tester@user.ru)");
            row.TimeWork.WaitText("10:00-19:00,10:00-19:00,10:00-19:00,10:00-19:00,10:00-19:00,Выходной,Выходной");
        }

        [Test, Description("Создание склада через Api админа неудачное")]
        public void WarehousesCreateErrorTest()
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

            //            Создания склада неудачное пустой город
            var responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST(keyShopPublic + "/warehouse_create.json",
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
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "City not found");

            //            Создания склада неудачное все поля пустые
            var responseErrorWarehouse2 =
                (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/warehouse_create.json",
                    new NameValueCollection
                    {
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