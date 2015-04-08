using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class CalculatorPickupDateTests : ConstVariablesTestBase
    {
        [Test, Description("Расчитать цену самовывоза c датой предполагаемого забора таска 732"), Ignore ]
        public void CalculatorPickupDateTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var deliveryPointsPage =
                LoadPage<DeliveryPointsPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            string deliveriPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();

//            Заполены все значения. Расчитать цену самомвывоза (по городу доставки)
            var responseCalculator =
                (ApiResponse.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                    new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "151184"},
                        {"delivery_point", deliveriPoinId},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"},
                        {"pickup_date", "02.04.2015"}
                    });
            Assert.AreEqual(responseCalculator.Message.Count(), 6);
            Assert.AreEqual(responseCalculator.Message[0].DeliveryCompanyName, companyName);


        }
    }
}