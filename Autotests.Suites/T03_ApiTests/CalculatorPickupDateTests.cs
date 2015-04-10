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
        [Test, Description("Расчитать цену самовывоза c датой предполагаемого забора таска 732")]
        public void CalculatorPickupDateTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var deliveryPointsPage =
                LoadPage<DeliveryPointsPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            string deliveriPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();

//            поле pickup_date нет - считается что оно заполненно тукущей датой
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
                        {"payment_price", "1000"}
                    });
            Assert.AreEqual(responseCalculator.Message.Count(), 1);
            Assert.AreEqual(responseCalculator.Message[0].DeliveryCompanyName, companyName);
//            на два дня вперед от PickupDate
            Assert.AreEqual(responseCalculator.Message[0].DeliveryDate,
                nowDate.AddDays(2).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
//            близайщая от даты забора к PickupDate
            Assert.AreEqual(responseCalculator.Message[0].ConfirmDate,
                nowDate.AddDays(-1).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) + " 23:45");
//            возвращается текущая дата
            Assert.AreEqual(responseCalculator.Message[0].PickupDate,
                nowDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));

//            поле pickup_date - заполненно текущей датой
            var pickupDate = nowDate;
            responseCalculator =
                (ApiResponse.ResponseCalculation) apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
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
                        {"pickup_date", pickupDate.ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture)}
                    });
            Assert.AreEqual(responseCalculator.Message.Count(), 1);
//            на два дня вперед от PickupDate
            Assert.AreEqual(responseCalculator.Message[0].DeliveryCompanyName, companyName);
            Assert.AreEqual(responseCalculator.Message[0].DeliveryDate,
                pickupDate.AddDays(2).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
//            близайщая от даты забора к PickupDate
            Assert.AreEqual(responseCalculator.Message[0].ConfirmDate,
                pickupDate.AddDays(-1).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) + " 23:45");
//            возвращается текущая PickupDate
            Assert.AreEqual(responseCalculator.Message[0].PickupDate,
                pickupDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));

//            поле pickup_date - заполненно на два дня вперед
            pickupDate = nowDate.AddDays(2);
            responseCalculator =
                (ApiResponse.ResponseCalculation) apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                    new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", ""},
                        {"delivery_point", deliveriPoinId},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"},
                        {
                            "pickup_date", pickupDate.ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                        }
                    });

            Assert.AreEqual(responseCalculator.Message.Count(), 1);
            Assert.AreEqual(responseCalculator.Message[0].DeliveryCompanyName, companyName);
//            на два дня вперед от PickupDate
            Assert.AreEqual(responseCalculator.Message[0].DeliveryCompanyName, companyName);
            Assert.AreEqual(responseCalculator.Message[0].DeliveryDate,
                pickupDate.AddDays(2).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
//            близайщая от даты забора к PickupDate
            Assert.AreEqual(responseCalculator.Message[0].ConfirmDate,
                pickupDate.AddDays(-1).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) + " 23:45");
//            возвращается текущая PickupDate
            Assert.AreEqual(responseCalculator.Message[0].PickupDate,
                pickupDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
        }
    }
}