using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class WarehouseCreateTests : ConstVariablesTestBase
    {
        [Test, Description("Создание склада")]
        public void WarehouseCreateTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.Users.Click();
            var usersPage = adminPage.GoTo<UsersPage>();
            usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();

            if (!usersPage.UsersTable.GetRow(0).UserEmail.IsPresent)
            {
                usersPage.UsersCreate.Click();
                var userCreatePage = usersPage.GoTo<UserCreatePage>();
                userCreatePage.UserEmail.SetValueAndWait(userNameAndPass);
                userCreatePage.UserPassword.SetValueAndWait(userNameAndPass);
                userCreatePage.UserGroups.SetFirstValueSelect("user");
                userCreatePage.UserGroupsAddButton.Click();
                userCreatePage.OfficialName.SetValueAndWait(legalEntityName);
                userCreatePage.SaveButton.Click();
                usersPage = userCreatePage.GoTo<UsersPage>();
            }
            usersPage.UsersTable.GetRow(0).UserEmail.WaitText(userNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            userId = userEdiringPage.Key.GetAttributeValue("value");
            var response = Api.POST("cabinet/" + userId + "/warehouse_create.json",
                new NameValueCollection
                {
                    {"name", userWarehouses + "_Api"},
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
            Assert.IsTrue(response.Success, "Ожидался ответ true на отправленный запрос POST по API" );

            var defaultPage = userEdiringPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserWarehouses.Click();
            var warehousesListPage = userPage.GoTo<UserWarehousesPage>();
            var row = warehousesListPage.Table.FindRowByName(userWarehouses + "_Api");
            row.Name.WaitText(userWarehouses + "_Api");
            row.City.WaitText("Томск");
            row.Address.WaitText("street, house 138");
            row.Contact.WaitText("contact_person (contact_phone tester@user.ru)");
            row.TimeWork.WaitText("10:00-19:00,10:00-19:00,10:00-19:00,10:00-19:00,10:00-19:00,Выходной,Выходной");
        }
        private string userId;
    }
}