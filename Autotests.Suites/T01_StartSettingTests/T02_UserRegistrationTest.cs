using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
{
    public class T01UserRegistrationTest : ConstVariablesTestBase
    {
        [Test, Description("Создания пользователя - user")]
        public void CreateUserTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.Users.Click();
            var usersPage = adminPage.GoTo<UsersPage>();

//            Проверяем нет ли пользователя с таким именем, если есть удаляем
            usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();
            while (usersPage.UsersTable.GetRow(0).UserEmail.IsPresent)
            {
                usersPage.UsersTable.GetRow(0).ActionsDelete.Click();
                usersPage = usersPage.GoTo<UsersPage>();
                usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
                usersPage = usersPage.SeachButtonRowClickAndGo();
            }
            usersPage.UsersCreate.Click();
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
            usersPage.UsersTable.GetRow(0).UserEmail.WaitText(userNameAndPass);

//            Проверяем что не осталось не использованных складов
            usersPage.AdminUsers.Click();
            usersPage.UsersWarehouses.Click();
            var warehousesPage = usersPage.GoTo<UsersWarehousesPage>();
            warehousesPage.Table.RowSearch.Name.SetValue(userWarehouses);
            warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            while (warehousesPage.Table.GetRow(0).Name.IsPresent)
            {
                warehousesPage.Table.GetRow(0).ActionsDelete.Click();
                warehousesPage = warehousesPage.GoTo<UsersWarehousesPage>();
                warehousesPage.Table.RowSearch.Name.SetValue(userWarehouses);
                warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            }

 //            Проверяем что не осталось не использованных магазинов
            warehousesPage.AdminUsers.Click();
            warehousesPage.UsersShops.Click();
            var shopsPage = warehousesPage.GoTo<UsersShopsPage>();
            shopsPage.Table.RowSearch.Name.SetValue(userShops);
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            while (shopsPage.Table.GetRow(0).Name.IsPresent)
            {
                shopsPage.Table.GetRow(0).ActionsDelete.Click();
                shopsPage = shopsPage.GoTo<UsersShopsPage>();
                shopsPage.Table.RowSearch.Name.SetValue(userShops);
                shopsPage = shopsPage.SeachButtonRowClickAndGo();
            }
        }
    }
}