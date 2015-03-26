﻿using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class CheckApiKeyTests : ConstVariablesTestBase
    {
        private string userId;

        [Test, Description("проверка api-key. Возврат ошибки"), Ignore]
        public void CalculatorPriceSelfTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var deliveryPointsPage =
                LoadPage<DeliveryPointsPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            string deliveriPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
    
           var responseFailCalculator =
               (Api.ResponseFail)apiRequest.GET("api/v1/" + "1234567890" + "/calculator.json",
               new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "Москва"},
                        {"delivery_point", deliveriPoinId},
                        {"dimension_side1", "0"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
           Assert.IsFalse(responseFailCalculator.Success);
           Assert.AreEqual(responseFailCalculator.ResponseMessage.Message, "Превышены возможные размеры или вес отправления для данного ПВЗ");
        }
    }
}