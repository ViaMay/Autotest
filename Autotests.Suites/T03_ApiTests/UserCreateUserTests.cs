using System.Collections.Specialized;
using System.Globalization;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class UserCreateUserTests : ConstVariablesTestBase
    {
        [Test, Description("Создание пользователя")]
        public void UserCreateTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.Users.Click();
            var usersPage = adminPage.GoTo<UsersPage>();
            usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();

            usersPage.UsersTable.GetRow(0).UserEmail.WaitText(userNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var userId = userEdiringPage.Key.GetAttributeValue("value");
            userEdiringPage.IsMaster.CheckAndWait();
            userEdiringPage.SaveButton.Click();
            userEdiringPage = userEdiringPage.GoTo<UserCreatePage>();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

            var emai1 = userNameAndPass + "u";
            var emai2 = userNameAndPass + "uu";
            var emai3 = userNameAndPass + "uuu";

            usersPage = LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass + "u");
            while (usersPage.UsersTable.GetRow(0).UserEmail.IsPresent)
            {
                usersPage.UsersTable.GetRow(0).ActionsDelete.Click();
                usersPage.Aletr.Accept();
                usersPage = usersPage.GoTo<UsersPage>();
                usersPage = LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass + "u");
            }

            var response01 =
                (ApiResponse.ResponseAddObject)apiRequest.POST("cabinet/" + userId + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "Наименование юр лица"},
                    {"email", emai1},
                    {"password", emai1},
                    {"name", "Контактное лицо"},
                    {"phone", "phone"},
                    {"director", "Ген. директор"},
                    {"on_basis", "Действует на основании"},
                    {"official_address", "Юридический адрес"},
                    {"address", "Фактический адрес"},
                    {"inn", "1234567890"},
                    {"ogrn", "1234567890123"},
                    {"bank_name", "Наименование банка"},
                    {"bank_bik", "123456789"},
                    {"bank_ks", "к/с банка"},
                    {"bank_rs", "12312312311231231231"}
                }
                );
            Assert.IsTrue(response01.Success);
            userEdiringPage = LoadPage<UserCreatePage>("admin/users/edit/" + response01.Response.Id);
            userEdiringPage.Name.WaitValue("Контактное лицо");
            userEdiringPage.Phone.WaitValue("phone");
            userEdiringPage.UserEmail.WaitValue(emai1);
            userEdiringPage.ResponsibleName.WaitValue(legalUserName);
            userEdiringPage.IsMaster.WaitUnchecked();
            userEdiringPage.OfficialName.WaitValue("Наименование юр лица");
            userEdiringPage.Director.WaitValue("Ген. директор");
            userEdiringPage.OnBasis.WaitValue("Действует на основании");
            userEdiringPage.Contract.WaitValue("");
            userEdiringPage.ContractDate.WaitValue(nowDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
            userEdiringPage.OfficialAddress.WaitValue("Юридический адрес");
            userEdiringPage.Address.WaitValue("Фактический адрес");
            userEdiringPage.Inn.WaitValue("1234567890");
            userEdiringPage.Ogrn.WaitValue("1234567890123");
            userEdiringPage.BankName.WaitValue("Наименование банка");
            userEdiringPage.BankBik.WaitValue("123456789");
            userEdiringPage.BankKs.WaitValue("к/с банка");
            userEdiringPage.BankRs.WaitValue("12312312311231231231");

            var response02 =
                (ApiResponse.ResponseAddObject)apiRequest.POST("cabinet/" + userId + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "Наименование юр лица2"},
                    {"email", emai2},
                    {"password", emai2},
                    {"inn", "123456789012"},
                    {"ogrn", "123456789012312"},
                }
                );
            Assert.IsTrue(response02.Success);
            userEdiringPage = LoadPage<UserCreatePage>("admin/users/edit/" + response02.Response.Id);
            userEdiringPage.Name.WaitValue("");
            userEdiringPage.Phone.WaitValue("");
            userEdiringPage.UserEmail.WaitValue(emai2);
            userEdiringPage.ResponsibleName.WaitValue(legalUserName);
            userEdiringPage.IsMaster.WaitUnchecked();
            userEdiringPage.OfficialName.WaitValue("Наименование юр лица2");
            userEdiringPage.Director.WaitValue("");
            userEdiringPage.OnBasis.WaitValue("");
            userEdiringPage.Contract.WaitValue("");
            userEdiringPage.ContractDate.WaitValue(nowDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
            userEdiringPage.OfficialAddress.WaitValue("");
            userEdiringPage.Address.WaitValue("");
            userEdiringPage.Inn.WaitValue("123456789012");
            userEdiringPage.Ogrn.WaitValue("123456789012312");
            userEdiringPage.BankName.WaitValue("");
            userEdiringPage.BankBik.WaitValue("");
            userEdiringPage.BankKs.WaitValue("");
            userEdiringPage.BankRs.WaitValue("");

            var response03 =
                (ApiResponse.ResponseAddObject)apiRequest.POST("cabinet/" + userId + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "Наименование юр лица3"},
                    {"email", emai3},
                    {"password", emai3},
                }
                );
            Assert.IsTrue(response03.Success);
            userEdiringPage = LoadPage<UserCreatePage>("admin/users/edit/" + response03.Response.Id);
            userEdiringPage.Inn.WaitValue("");
            userEdiringPage.Ogrn.WaitValue("");

            var defaultPage = userEdiringPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(emai3, emai3);
        }

        [Test, Description("Создание пользователя не успешное")]
        public void UserCreateErrorTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            var usersPage =  LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var userId = userEdiringPage.Key.GetAttributeValue("value");
            userEdiringPage.IsMaster.UncheckAndWait();
            userEdiringPage.SaveButton.Click();
            userEdiringPage = userEdiringPage.GoTo<UserCreatePage>();

            var emai1 = userNameAndPass + "u";

            usersPage = LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass + "u");
            while (usersPage.UsersTable.GetRow(0).UserEmail.IsPresent)
            {
                usersPage.UsersTable.GetRow(0).ActionsDelete.Click();
                usersPage.Aletr.Accept();
                usersPage = usersPage.GoTo<UsersPage>();
                usersPage = LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass + "u");
            }
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

            var responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userId + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "Наименование юр лица"},
                    {"email", emai1},
                    {"password", emai1},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "only master can access");

            usersPage = LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            userEdiringPage = usersPage.GoTo<UserCreatePage>();
            userEdiringPage.IsMaster.CheckAndWait();
            userEdiringPage.SaveButton.Click();
            userEdiringPage = userEdiringPage.GoTo<UserCreatePage>();
            adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userId + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", ""},
                    {"email", ""},
                    {"password", ""},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "email can not be empty; password can not be empty; official_name can not be empty");

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userId + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "a"},
                    {"email", "a"},
                    {"password", "a"},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "incorrect email");

            var responseFailObject =
                (ApiResponse.ResponseFailObject)apiRequest.POST("cabinet/" + userId + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "a"},
                    {"email", "3@f.ru3"},
                    {"password", "a"},
                }
                );
            Assert.IsFalse(responseFailObject.Success);
            Assert.AreEqual(responseFailObject.Response.Error.Username, "Email должно быть корректным адресом электронной почты");
            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userId + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "a"},
                    {"email", emai1},
                    {"password", "a"},
                    {"inn", "d"},
                    {"ogrn", "d"},
                    {"bank_bik", "d"},
                    {"bank_rs", "d"},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "inn can contains only digits; " +
                                                             "ogrn can contains only digits; " +
                                                             "bank_bik can contains only digits; " +
                                                             "bank_rs can contains only digits");
            
            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userId + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "a"},
                    {"email", emai1},
                    {"password", "a"},
                    {"inn", "1"},
                    {"ogrn", "1"},
                    {"bank_bik", "1"},
                    {"bank_rs", "1"},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "inn can contains only 10 or 12 symbol length; " +
                                                           "ogrn can contains only 15 or 13 symbol length; " +
                                                           "bank_bik can contains only 9 symbol length; " +
                                                           "bank_rs can contains only 20 symbol length");

            var response =
                (ApiResponse.ResponseAddObject)apiRequest.POST("cabinet/" + userId + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "Наименование юр лица3"},
                    {"email", emai1},
                    {"password", emai1},
                }
                );
            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("cabinet/" + userId + "/user_create.json",
                new NameValueCollection
                {                     
                {"official_name", "Наименование юр лица3"},
                {"email", emai1},
                {"password", emai1},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "email already exists");
        }
    }
}