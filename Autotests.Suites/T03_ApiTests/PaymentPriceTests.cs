using System.Collections.Specialized;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class PaymentPriceTests : ConstVariablesTestBase
    {
        [Test, Description("Возможность НПП в городе или регионе")]
        public void PaymentPriceTest()
        {
            var cityPayment = "151185";
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var companiesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string companyId = companiesPage.Table.GetRow(0).ID.GetText();
            
            var paymentPricePage =
                LoadPage<PaymentPricePage>("/admin/paymentprice/?&filters[company]=" + companyName + "&filters[city]=" + cityPayment);
            while (paymentPricePage.Table.GetRow(0).Name.IsPresent)
            {
                paymentPricePage.Table.GetRow(0).ActionsDelete.Click();
                paymentPricePage.Aletr.Accept();
                paymentPricePage =
                LoadPage<PaymentPricePage>("/admin/paymentprice/?&filters[company]=" + companyName + "&filters[city]=" + cityPayment);
            }
            paymentPricePage.CompanyCreate.Click();
            var рaymentPriceCreatePage = paymentPricePage.GoTo<PaymentPriceCreatePage>();
            рaymentPriceCreatePage.Company.SetFirstValueSelect(companyName);
            рaymentPriceCreatePage.City.SetFirstValueSelect("Санкт-Петербург");
            рaymentPriceCreatePage.SaveButton.Click();
            paymentPricePage = рaymentPriceCreatePage.GoTo<PaymentPricePage>();

            var responsePaymentPrice = apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price.json",
                   new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"company", companyId},
                        {"city", "151185"}
                    });
            Assert.IsTrue(responsePaymentPrice.Success);

//            запрос на неправильный город
            responsePaymentPrice = apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price.json",
                   new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"company", companyId},
                        {"city", "151183"}
                    });
            Assert.IsFalse(responsePaymentPrice.Success);

//            запрос на неправильную компанию
            var companyIdError = "123456";
            responsePaymentPrice = apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price.json",
                   new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"company", companyIdError},
                        {"city", "151185"}
                    });
            Assert.IsFalse(responsePaymentPrice.Success);

//            удаляем наш НП
                paymentPricePage =
                LoadPage<PaymentPricePage>("/admin/paymentprice/?&filters[company]=" + companyName + "&filters[city]=" + cityPayment);
            while (paymentPricePage.Table.GetRow(0).Name.IsPresent)
            {
                paymentPricePage.Table.GetRow(0).ActionsDelete.Click();
                paymentPricePage.Aletr.Accept();
                paymentPricePage =
                LoadPage<PaymentPricePage>("/admin/paymentprice/?&filters[company]=" + companyName + "&filters[city]=" + cityPayment);
            }
//            Снова шлем запрос занашу организацию и город
            responsePaymentPrice = apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price.json",
                   new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"company", companyId},
                        {"city", "151185"}
                    });
            Assert.IsFalse(responsePaymentPrice.Success);
        }
    }
}