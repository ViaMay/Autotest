using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class CalculatorPriceSelfTests : ConstVariablesTestBase
    {
        private string userId;

        [Test, Description("Расчитать цену самовывоза")]
        public void CalculatorPriceSelfTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var deliveryPointsPage =
                LoadPage<DeliveryPointsPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            string deliveriPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();

 //            Не заполен город city_to. Расчитать цену самомвывоза (по пункут выдачи)
            var responseCalculator =
                (Api.ResponseCalculation) apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
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
                        {"payment_price", "1000"}
                    });
            Assert.AreEqual(responseCalculator.MessageCalculation.Count(), 1);
            Assert.AreEqual(responseCalculator.MessageCalculation[0].DeliveryCompanyName, companyName);

//            Заполены все значения. Расчитать цену самомвывоза (по городу доставки)
            responseCalculator =
                (Api.ResponseCalculation) apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
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
            Assert.AreEqual(responseCalculator.MessageCalculation.Count(), 6);
            Assert.AreEqual(responseCalculator.MessageCalculation[0].DeliveryCompanyName, companyName);

//            delivery_point пустое. Расчитать цену самомвывоза (по городу доставки)
            responseCalculator =
                (Api.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                    new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "151184"},
                        {"delivery_point", ""},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.AreEqual(responseCalculator.MessageCalculation.Count(), 6);
            Assert.AreEqual(responseCalculator.MessageCalculation[0].DeliveryCompanyName, companyName);

//            Заполены все значения, Город заполнен текстом. Расчитать цену самомвывоза (по городу доставки)
            responseCalculator =
                (Api.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                    new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "Москва"},
                        {"delivery_point", deliveriPoinId},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.AreEqual(responseCalculator.MessageCalculation.Count(), 6);
            Assert.AreEqual(responseCalculator.MessageCalculation[0].DeliveryCompanyName, companyName);

//            Одна из сторон равна нулю. Возврат ошибки
            var responseFailCalculator =
                (Api.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "151184"},
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

//           declared_price равна нулю. Возврат ошибки
            responseFailCalculator =
                (Api.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "151184"},
                        {"delivery_point", deliveriPoinId},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"payment_price", "1000"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.ResponseMessage.Message, "declared price обязательно к заполнению");

 //           payment_price равна нулю. Расчитываем стоимость.
            responseCalculator =
                 (Api.ResponseCalculation) apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
               new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "151184"},
                        {"delivery_point", deliveriPoinId},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", ""},
                        {"payment_price", "1000"}
                    });
           Assert.AreEqual(responseCalculator.MessageCalculation.Count(), 6);
           Assert.AreEqual(responseCalculator.MessageCalculation[0].DeliveryCompanyName, companyName);

//           Проверка корректного to_city, не корректый город. Возврат ошибки
           responseFailCalculator =
               (Api.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
               new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "Оклахома"},
                        {"delivery_point", deliveriPoinId},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
           Assert.IsFalse(responseFailCalculator.Success);
           Assert.AreEqual(responseFailCalculator.ResponseMessage.Message, "City not found (city to)");
        }
    }
}