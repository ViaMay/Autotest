using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class OrderNotFoundTests : ConstVariablesTestBase
    {
        [Test, Description("Отправка запросов по id не существующего order")]
        public void OrderErrorTest()
        {
            var orderErrorId = "123123";
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            //           Подтверждение заявки c неправильным id заявки
            var responseFail = (ApiResponse.ResponseFail)apiRequest.POST(keyShopPublic + "/order_confirm.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", orderErrorId},
                });
            Assert.IsFalse(responseFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseFail.Response.ErrorText, "Order not found!");

            //           Порверка статуса заявки id заявки
            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/order_status.json",
                new NameValueCollection
                {
                {"order",  orderErrorId}
                });
            Assert.IsFalse(responseFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseFail.Response.ErrorText, "Order not found");

            //           Инфо заявки id заявки
            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic
                + "/order_info/" + orderErrorId + ".json",
                new NameValueCollection { });
            Assert.IsFalse(responseFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseFail.Response.ErrorText, "Order not found");

            //         Отмена ордера id заявки
            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/order_cancel.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", orderErrorId}
                });
            Assert.IsFalse(responseFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseFail.Response.ErrorText, "Order not found");

//            проверка полного статуса 
            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/order_full_status.json",
                 new NameValueCollection
                {
                {"order", orderErrorId}
                });
            Assert.IsFalse(responseFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseFail.Response.ErrorText, "Order not found");
        }
    }
}