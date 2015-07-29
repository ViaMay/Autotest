using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class UserBarcodeTests : ConstVariablesTestBase
    {
        [Test, Description("get_barcodes.json Получить пул штрих-кодов"), Ignore]
        public void UserGetBarcodesTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryPointsPage =
                LoadPage<AdminBaseListPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            var deliveryPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
            var deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            var deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

            var usersPage =
                LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            var userCreatePage = usersPage.GoTo<UserCreatePage>();
            userCreatePage.BarcodeLimit.SetValueAndWait("10");
            var userKey = userCreatePage.Key.GetValue();
            userCreatePage.SaveButton.Click();
            userCreatePage = userCreatePage.GoTo<UserCreatePage>();

//            получение шрикодов
            var response =
                (ApiResponse.ResponseUserBarcodes) apiRequest.POST("cabinet/" + userKey + "/get_barcodes.json",
                    new NameValueCollection
                    {});
            Assert.AreEqual(response.Response.List.Count(), 10);

            var responseFail =
                (ApiResponse.ResponseFail) apiRequest.POST("cabinet/" + userKey + "/get_barcodes.json",
                    new NameValueCollection {});
            Assert.AreEqual(responseFail.Response.ErrorText, "Too frequent queries");

            //            оправляем заказ используя один из номеров

            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder) apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"_id", response.Response.List[0]},
                        {"delivery_point", deliveryPoinId},
                        {"to_city", "151184"},
                        {"delivery_company", "" + deliveryCompanyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "true"},
                        {"weight", "4"},
                        {"declared_price", "100"},
                        {"payment_price", "300"},
                        {"to_name", "Ургудан Рабат Мантов"},
                        {"to_phone", "79999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
                        {"items_count", "3"},
                        {"is_cargo_volume", "true"},
                        {"order_comment", "order_comment"}
                    });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateOrders.Response.OrderId, response.Response.List[0]);

            response =
                (ApiResponse.ResponseUserBarcodes) apiRequest.POST("cabinet/" + userKey + "/get_barcodes.json",
                    new NameValueCollection
                    {});
            Assert.AreEqual(response.Response.List.Count(), 10);
        }
    }
}