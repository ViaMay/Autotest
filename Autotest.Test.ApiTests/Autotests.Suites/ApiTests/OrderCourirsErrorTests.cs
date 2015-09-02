using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class OrderCourirsErrorTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заказа курьерской")]
        public void OrderCourirsErrorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

//            delivery_company = ""
            var responseCreateFailOrder = (ApiResponse.ResponseFail)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", "" + ""},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_street", "Барна"},
		        {"to_house", "3a"},
		        {"to_flat", "12"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                });
            Assert.IsFalse(responseCreateFailOrder.Success, "Ожидался ответ Fail на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateFailOrder.Response.ErrorText, "Компания доставки обязательна к заполнению");
            
//            Вес пустой weight=0
            var responseCreateFailObjectOrder = (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "0"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_street", "Барна"},
		        {"to_house", "3a"},
		        {"to_flat", "12"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                });
            Assert.IsFalse(responseCreateFailObjectOrder.Success, "Ожидался ответ Fail на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateFailObjectOrder.Response.Error.CalculateOrder, "Ошибка просчета цены, или маршрут недоступен");
            Assert.AreEqual(responseCreateFailObjectOrder.Response.Error.Weight, "Вес обязательно к заполнению");
        
//            to_city=""
            responseCreateFailObjectOrder = (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"to_city", ""},
		        {"delivery_company", "" + deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_street", "Барна"},
		        {"to_house", "3a"},
		        {"to_flat", "12"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                });
            Assert.IsFalse(responseCreateFailObjectOrder.Success, "Ожидался ответ Fail на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateFailObjectOrder.Response.Error.CalculateOrder, "Ошибка просчета цены, или маршрут недоступен");
            Assert.AreEqual(responseCreateFailObjectOrder.Response.Error.ToCity, "Город получения обязательно к заполнению");
        }
    }
}