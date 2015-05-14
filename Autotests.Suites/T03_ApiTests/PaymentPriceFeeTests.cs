using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class PaymentPriceFeeTests : ConstVariablesTestBase
    {
        [Test, Description("Комиссия за наложенный платеж")]
        public void PaymentPriceFeeTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var companiesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string companyId = companiesPage.Table.GetRow(0).ID.GetText();

            var paymentPriceFeePage =
                LoadPage<PaymentPriceFeePage>("/admin/paymentpricefee/?&filters[company]=" + companyName);
            while (paymentPriceFeePage.Table.GetRow(0).Name.IsPresent)
            {
                paymentPriceFeePage.Table.GetRow(0).ActionsDelete.Click();
                paymentPriceFeePage.Aletr.Accept();
                paymentPriceFeePage =
                    LoadPage<PaymentPriceFeePage>("/admin/paymentpricefee/?&filters[company]=" + companyName);
            }
            paymentPriceFeePage.Create.Click();
            var рaymentPriceFeeCreatePage = paymentPriceFeePage.GoTo<PaymentPriceCreateFeePage>();
            рaymentPriceFeeCreatePage.Company.SetFirstValueSelect(companyName);
            рaymentPriceFeeCreatePage.From.SetValueAndWait("1.1");
            рaymentPriceFeeCreatePage.MinCommission.SetValueAndWait("47.2");
            рaymentPriceFeeCreatePage.Percent.SetValueAndWait("1.9");
            рaymentPriceFeeCreatePage.PercentCard.SetValueAndWait("2.6");
            рaymentPriceFeeCreatePage.SaveButton.Click();
            paymentPriceFeePage = рaymentPriceFeeCreatePage.GoTo<PaymentPriceFeePage>();

            var responsePaymentFeePrice = (ApiResponse.ResponsePaymentPriceFee)
                apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price_fee.json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"company", companyId},
                    {"city", "151185"}
                });
            Assert.IsTrue(responsePaymentFeePrice.Success);
            Assert.AreEqual(responsePaymentFeePrice.Response.From, "1.1");
            Assert.AreEqual(responsePaymentFeePrice.Response.Min, "47.2");
            Assert.AreEqual(responsePaymentFeePrice.Response.Percent, "1.9");
            Assert.AreEqual(responsePaymentFeePrice.Response.PercentCard, "2.6");

            paymentPriceFeePage =
                LoadPage<PaymentPriceFeePage>("/admin/paymentpricefee/?&filters[company]=" + companyName);
            paymentPriceFeePage.Table.GetRow(0).ActionsDelete.Click();
            paymentPriceFeePage.Aletr.Accept();
            //            запрос на компанию у которой нет коммисии
            var responseError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price_fee.json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"company", companyId},
                }
                );
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Не найдено комиссий для указанной компании");

            //            запрос на неправильную компанию            
            responseError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price_fee.json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"company", "123456"},
                }
                );
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Указанная компания не существует");
        }
    }
}
