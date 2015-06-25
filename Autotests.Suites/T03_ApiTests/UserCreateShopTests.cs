﻿using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class UserCreateShopTests : ConstVariablesTestBase
    {
        [Test, Description("Создание магазина")]
        public void ShopCreateTest()
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
                userCreatePage.AdminUsers.Click();
                userCreatePage.Users.Click();
                usersPage = adminPage.GoTo<UsersPage>();
                usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
                usersPage = usersPage.SeachButtonRowClickAndGo();
            }
            usersPage.UsersTable.GetRow(0).UserEmail.WaitText(userNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            userId = userEdiringPage.Key.GetAttributeValue("value");

            var responseWarehouse = (ApiResponse.ResponseAddObject)apiRequest.POST("cabinet/" + userId + "/warehouse_create.json",
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

//            удаление магазинов если они были
            userEdiringPage.AdminUsers.Click();
            userEdiringPage.UsersShops.Click();
            var shopsPage = userEdiringPage.GoTo<UsersShopsPage>();
            shopsPage.Table.RowSearch.Name.SetValue(userShopName + "_Api");
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            while (shopsPage.Table.GetRow(0).Name.IsPresent)
            {
                shopsPage.Table.GetRow(0).ActionsDelete.Click();
                shopsPage.Aletr.Accept();
                shopsPage = shopsPage.GoTo<UsersShopsPage>();
                shopsPage.Table.RowSearch.Name.SetValue(userShopName + "_Api");
                shopsPage = shopsPage.SeachButtonRowClickAndGo();
            }

            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("cabinet/" + userId + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_Api"},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Квебек"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var defaultPage = shopsPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            var row = shopsListPage.Table.FindRowByName(userShopName + "_Api");
            row.Name.WaitTextContains(userShopName + "_Api\r\nAPI ключ для модуля: " + responseShop.Response.Key);
            row.Address.WaitTextContains("Квебек");

//            повторное создание магазина
            responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("cabinet/" + userId + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_Api2"},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Квебек2"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");
        }
        private string userId;
    }
}