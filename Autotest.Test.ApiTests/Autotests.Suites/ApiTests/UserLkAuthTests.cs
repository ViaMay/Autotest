using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserLkAuthTests : ConstVariablesTestBase
    {
        [Test, Description("логирование под пользователем")]
        public void UserLkAuthTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders?&filters[shop]=" + userShopName + "&filters[status]=30");
            var orderId = оrdersInputPage.Table.GetRow(0).ID.GetText();

            var responseLkAuth01 =
                (ApiResponse.ResponseLkAuth)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                new NameValueCollection
                {
                    {"login", userNameAndPass},
                    {"password", userNameAndPass}
                });

            var responseLkAuth02 =
                (ApiResponse.ResponseLkAuth)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                new NameValueCollection
                {
                    {"login", userNameAndPass},
                    {"password", userNameAndPass}
                });

            var response =
                apiRequest.GET("api/v1/cabinet/" + responseLkAuth01.Response.Token + "/lk_request.json",
                new NameValueCollection
                {
                    {"method", "get_shops"},
                });
            Assert.IsTrue(response.Success);

            response =
                apiRequest.GET("api/v1/cabinet/" + responseLkAuth02.Response.Token + "/lk_request.json",
                new NameValueCollection
                {
                    {"method", "get_warehouses"},
                });
            Assert.IsTrue(response.Success);

            var responseDocumentsRequest =
                (ApiResponse.ResponseDocumentsRequest)apiRequest.GET("api/v1/cabinet/" + responseLkAuth01.Response.Token + "/lk_request.json",
                new NameValueCollection
                {
                    {"method", "documents_request"},
                    {"order_id", orderId},
                });
            Assert.IsTrue(response.Success);
            Assert.IsFalse(responseDocumentsRequest.Response.Completed);
        }

         [Test, Description("логирование под пользователем не корректное")]
        public void UserLkAuthErrorTest()
        {
            var responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                new NameValueCollection{});
            Assert.AreEqual(responseFail.Response.ErrorText, "password:Пароль обязательно к заполнению;login:login обязательно к заполнению;");

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                new NameValueCollection
                {
                    {"login", userNameAndPass}
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "password:Пароль обязательно к заполнению;");

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                new NameValueCollection
                {
                    {"password", userNameAndPass}
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "login:login обязательно к заполнению;");

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                new NameValueCollection
                {
                    {"login", adminName},
                    {"password", adminPass}
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "Некорректный логин или пароль");
            responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                new NameValueCollection
                {
                    {"login", pickupNameAndPass},
                    {"password", pickupNameAndPass}
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "Некорректный логин или пароль");
        }

         [Test, Description("не корректное использование метода")]
         public void UserLkAuthmethodErrorTest()
         {
             var responseLkAuth =
                 (ApiResponse.ResponseLkAuth)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                 new NameValueCollection
                {
                    {"login", userNameAndPass},
                    {"password", userNameAndPass}
                });
             
             var responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + responseLkAuth.Response.Token + "/lk_request.json",
                 new NameValueCollection{});
             Assert.AreEqual(responseFail.Response.ErrorText, "method:method обязательно к заполнению;");

             responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + responseLkAuth.Response.Token + "/lk_request.json",
                 new NameValueCollection
                {
                    {"method", ""},
                });
             Assert.AreEqual(responseFail.Response.ErrorText, "method:method обязательно к заполнению;");

             responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + responseLkAuth.Response.Token + "/lk_request.json",
                 new NameValueCollection
                {
                    {"method", "documents_request"},
                });
             Assert.AreEqual(responseFail.Response.ErrorText, "Empty id field");

             responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + responseLkAuth.Response.Token + "/lk_request.json",
                 new NameValueCollection
                {
                    {"method", "shop_create"},
                });
             Assert.AreEqual(responseFail.Response.ErrorText, "HTTP-метод не корректен");
         }
    }
}