using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
{
    public class T02UserRegistrationTest : ConstVariablesTestBase
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
                usersPage.Aletr.Accept();
                usersPage = usersPage.GoTo<UsersPage>();
                usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
                usersPage = usersPage.SeachButtonRowClickAndGo();
            }
            usersPage.Create.Click();
            var userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.UserEmail.SetValueAndWait(userNameAndPass);
            userCreatePage.UserPassword.SetValueAndWait(userNameAndPass);
            userCreatePage.UserGroups.SetFirstValueSelect("user");
            userCreatePage.UserGroupsAddButton.Click();
            userCreatePage.OfficialName.SetValueAndWait(legalUserName);
            userCreatePage.SaveButton.Click();
            userCreatePage.AdminUsers.Click();
            userCreatePage.Users.Click();
            usersPage = adminPage.GoTo<UsersPage>();

            usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();
            usersPage.UsersTable.GetRow(0).UserEmail.WaitText(userNameAndPass);
        }

        [Test, Description("Создания пользователя - pickup")]
        public void CreatePickupTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.Users.Click();
            var usersPage = adminPage.GoTo<UsersPage>();

            //            Проверяем нет ли пользователя с таким именем, если есть удаляем
            usersPage.UsersTable.RowSearch.UserEmail.SetValue(pickupNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();
            while (usersPage.UsersTable.GetRow(0).UserEmail.IsPresent)
            {
                usersPage.UsersTable.GetRow(0).ActionsDelete.Click();
                usersPage.Aletr.Accept();
                usersPage = usersPage.GoTo<UsersPage>();
                usersPage.UsersTable.RowSearch.UserEmail.SetValue(pickupNameAndPass);
                usersPage = usersPage.SeachButtonRowClickAndGo();
            }
            usersPage.Create.Click();
            var userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.UserEmail.SetValueAndWait(pickupNameAndPass);
            userCreatePage.UserPassword.SetValueAndWait(pickupNameAndPass);
            userCreatePage.UserGroups.SetFirstValueSelect("pickup");
            userCreatePage.UserGroupsAddButton.Click();
            userCreatePage.OfficialName.SetValueAndWait(legalPickupName);
            userCreatePage.SaveButton.Click();
            userCreatePage.AdminUsers.Click();
            userCreatePage.Users.Click();
            usersPage = adminPage.GoTo<UsersPage>();

            usersPage.UsersTable.RowSearch.UserEmail.SetValue(pickupNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();
            usersPage.UsersTable.GetRow(0).UserEmail.WaitText(pickupNameAndPass);
        }
    }
}