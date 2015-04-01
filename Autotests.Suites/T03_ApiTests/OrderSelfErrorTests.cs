using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class OrderSelfErrorTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заказа на самовывоз,ошибки при создании")]
        public void OrderSelfErrorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var deliveryPointsPage =
                LoadPage<DeliveryPointsPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            string deliveriPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
            var deliveryCompaniesPage =
                LoadPage<СompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string deliveriCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

            // ошибка в delivery_point
            var responseCreateFailOrders = (ApiResponse.ResponseFailOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "1"},
		        {"delivery_point", "123456789"},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveriCompanyId},
		        {"shop_refnum", ""},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                });
            Assert.IsFalse(responseCreateFailOrders.Success, "Ожидался ответ Fail на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateFailOrders.Message.Error.ToCity, "Город получения обязательно к заполнению");
            Assert.AreEqual(responseCreateFailOrders.Message.Error.DeliveryPoint, "ПВЗ не работает. Пожалуйста, свяжитесь с администратором системы.");
            Assert.AreEqual(responseCreateFailOrders.Message.Error.DeliveryCompany, "Компания доставки обязательно к заполнению");

            // ошибка в dimension_side1 = 0
            responseCreateFailOrders = (ApiResponse.ResponseFailOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "1"},
		        {"delivery_point", deliveriPoinId},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveriCompanyId},
		        {"shop_refnum", ""},
		        {"dimension_side1", "0"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                });
            Assert.IsFalse(responseCreateFailOrders.Success, "Ожидался ответ Fail на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateFailOrders.Message.Error.CalculateOrder, "Ошибка просчета цены, или маршрут недоступен");
            Assert.AreEqual(responseCreateFailOrders.Message.Error.DimensionSide1, "Значение поля &laquo;Сторона 1&raquo; должно быть положительным числом");
        }
    }
}