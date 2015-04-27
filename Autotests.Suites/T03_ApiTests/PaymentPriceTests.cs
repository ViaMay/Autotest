using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class PaymentPriceTests : ConstVariablesTestBase
    {
        [Test, Description("Возможность НПП в городе или регионе"), Ignore]
        public void Test()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var companiesPage =
                LoadPage<СompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string companyId = companiesPage.Table.GetRow(0).ID.GetText();

            var responseCompanyTerm =
               (ApiResponse.ResponseCompanyTerm)apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price.json",
                   new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"company", companyId},
                        {"city", "151184"}
                    });
            Assert.IsTrue(responseCompanyTerm.Success);
            Assert.AreEqual(responseCompanyTerm.Response.Id, companyId);
            Assert.AreEqual(responseCompanyTerm.Response.Term, "12");
            Assert.AreEqual(responseCompanyTerm.Response.Prolongation, true);
        }
    }
}