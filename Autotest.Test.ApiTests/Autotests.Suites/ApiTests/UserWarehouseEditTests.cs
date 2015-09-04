using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserWarehouseEditTests : ConstVariablesTestBase
    {
        [Test, Description("Редактирование склада через Api"), Ignore]
        public void WarehousesEditTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.Users.Click();
            var usersPage = adminPage.GoTo<UsersPage>();
            usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();

            if (!usersPage.UsersTable.GetRow(0).UserEmail.IsPresent)
            {
                usersPage.Create.Click();
                var userCreatePage = usersPage.GoTo<UserCreatePage>();
                userCreatePage.UserEmail.SetValueAndWait(userNameAndPass);
                userCreatePage.UserPassword.SetValueAndWait(userNameAndPass);
                userCreatePage.UserGroups.SetFirstValueSelect("user");
                userCreatePage.UserGroupsAddButton.Click();
                userCreatePage.OfficialName.SetValueAndWait(legalEntityName);
                userCreatePage.SaveButton.Click();
                usersPage = userCreatePage.GoTo<UsersPage>();
                usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
                usersPage = usersPage.SeachButtonRowClickAndGo();
            }
            usersPage.UsersTable.GetRow(0).UserEmail.WaitText(userNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var userId = userEdiringPage.Key.GetAttributeValue("value");
            
//            удаление скалад если он был до этого
            userEdiringPage.AdminUsers.Click();
            userEdiringPage.UsersWarehouses.Click();
            var warehousesPage = userEdiringPage.GoTo<AdminBaseListPage>();
            warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName + "_Api");
            warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            while (warehousesPage.Table.GetRow(0).Name.IsPresent)
            {
                warehousesPage.Table.GetRow(0).ActionsDelete.Click();
                warehousesPage.Aletr.Accept();
                warehousesPage = warehousesPage.GoTo<AdminBaseListPage>();
                warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName + "_Api");
                warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            }

            //            Создания склада
            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST("cabinet/" + userId + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_Api"},
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
                (ApiResponse.ResponseAddObject)apiRequest.POST("cabinet/" + userId + "/warehouse_edit/"
                + responseWarehouse.Response.Id+".json",
                new NameValueCollection
                    {
                        {"name", userWarehouseName + "_Api2"},
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
            var row = warehousesListPage.Table.FindRowByName(userWarehouseName + "_Api2");
            row.Name.WaitText(userWarehouseName + "_Api2");
            row.City.WaitText("Кедровый");
            row.Address.WaitText("street2, house2 flat139");
            row.Contact.WaitText("contact_person2 (contact_phone2 tester@user.ru)");
            row.TimeWork.WaitText("1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23");
        }

        [Test, Description("Редактирвоание склада через Api неудачное"), Ignore]
        public void WarehousesEditErrorTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.Users.Click();
            var usersPage = adminPage.GoTo<UsersPage>();
            usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();

            if (!usersPage.UsersTable.GetRow(0).UserEmail.IsPresent)
            {
                usersPage.Create.Click();
                var userCreatePage = usersPage.GoTo<UserCreatePage>();
                userCreatePage.UserEmail.SetValueAndWait(userNameAndPass);
                userCreatePage.UserPassword.SetValueAndWait(userNameAndPass);
                userCreatePage.UserGroups.SetFirstValueSelect("user");
                userCreatePage.UserGroupsAddButton.Click();
                userCreatePage.OfficialName.SetValueAndWait(legalEntityName);
                userCreatePage.SaveButton.Click();
                usersPage = userCreatePage.GoTo<UsersPage>();
                usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
                usersPage = usersPage.SeachButtonRowClickAndGo();
            }
            usersPage.UsersTable.GetRow(0).UserEmail.WaitText(userNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var userId = userEdiringPage.Key.GetAttributeValue("value");

//            удаление скалад если он был до этого           
            userEdiringPage.AdminUsers.Click();
            userEdiringPage.UsersWarehouses.Click();
            var warehousesPage = userEdiringPage.GoTo<AdminBaseListPage>();
            warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName + "_Api");
            warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            while (warehousesPage.Table.GetRow(0).Name.IsPresent)
            {
                warehousesPage.Table.GetRow(0).ActionsDelete.Click();
                warehousesPage.Aletr.Accept();
                warehousesPage = warehousesPage.GoTo<AdminBaseListPage>();
                warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName + "_Api");
                warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            }

//            Создания склада
            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST("cabinet/" + userId + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_Api"},
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
            
//            Редктирование склада неудачное пустое id склада
            var responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userId + "/warehouse_edit.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_Api"},
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

//            Редктирование склада неудачное
            responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userId + "/warehouse_edit/"
                + responseWarehouse.Response.Id + ".json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_Api"},
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

//            Редктирование склада неудачное
            var responseErrorWarehouse2 =
                (ApiResponse.ResponseFailObject)apiRequest.POST("cabinet/" + userId + "/warehouse_edit/"
                + responseWarehouse.Response.Id + ".json",
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