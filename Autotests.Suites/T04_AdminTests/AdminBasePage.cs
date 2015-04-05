using System;
using System.Collections.Specialized;
using System.Globalization;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T04_AdminTests
{
    public class AdminBasePage : ConstVariablesTestBase
    {
        public string[] SendOrersRequest()
        {
//            LoginAsAdmin(adminName, adminPass);
            var companiesPage = LoadPage<СompaniesPage>("admin/companies/?&filters[name]=" + companyName);
            var deliveriCompanyId = companiesPage.Table.GetRow(0).ID.GetText();

            var shopsPage = LoadPage<ShopsPage>("admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryPointsPage =
                 LoadPage<DeliveryPointsPage>("admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            var deliveriPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();

            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

            var responseCreateOrder1 = (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                 new NameValueCollection
                {
                {"api_key", keyShopPublic},
//                {"pickup_date", nowDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveriCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "true"},
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
            Assert.IsTrue(responseCreateOrder1.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responseCreateOrder2 = (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
//                {"pickup_date", nowDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)},
		        {"type", "1"},
		        {"delivery_point", deliveriPoinId},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveriCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "true"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                });
            Assert.IsTrue(responseCreateOrder2.Success, "Ожидался ответ true на отправленный запрос POST по API");
            return new[] {responseCreateOrder1.Message.OrderId, responseCreateOrder2.Message.OrderId};
        }


    }
}