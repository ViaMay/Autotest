using System.Collections.Specialized;
using System.Globalization;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class OrderStatusDatePredictTests : ConstVariablesTestBase
    {
        [Test, Description("Передача планируемой даты доставки от ТК при запросе статуса 754")]
        public void OrderStatusDatePredictTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryPointsPage =
                LoadPage<DeliveryPointsPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            var deliveriPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
            var deliveryCompaniesPage =
                LoadPage<СompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            var deliveriCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

//            не указана pickup_date то есть текущая дата
            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveriCompanyId},
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
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

//           Порверка статуса заявки
            var responseOrderStatus = (ApiResponse.ResponseStatus)apiRequest.GET("api/v1/" + keyShopPublic + "/order_status.json",
                new NameValueCollection
                {
                {"order", responseCreateOrders.Message.OrderId}
                });
//            доставка равна на два дня вперед от pickup_date
            Assert.AreEqual(responseOrderStatus.Message.DeliveryDate,
                nowDate.AddDays(2).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
//            pickup_date проставилась текущая дата
            Assert.AreEqual(responseOrderStatus.Message.PickupDate,
                nowDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));

//            отправляем новый запрос в нем указываем pickup_date равную после завтра nowDate.AddDays(2)
            responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "1"},
                {"pickup_date", nowDate.AddDays(2).ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture)},
		        {"delivery_point", deliveriPoinId},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveriCompanyId},
		        {"shop_refnum", userShopName},
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
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            //           Порверка статуса заявки
             responseOrderStatus = (ApiResponse.ResponseStatus)apiRequest.GET("api/v1/" + keyShopPublic + "/order_status.json",
                new NameValueCollection
                {
                {"order", responseCreateOrders.Message.OrderId}
                });
//            доставка равна на два дня вперед от pickup_date
            Assert.AreEqual(responseOrderStatus.Message.DeliveryDate,
                nowDate.AddDays(4).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
//            pickup_date равна отправляемой pickup_date в заявке
            Assert.AreEqual(responseOrderStatus.Message.PickupDate,
                nowDate.AddDays(2).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
        }
    }
}