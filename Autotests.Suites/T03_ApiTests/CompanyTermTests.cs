using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class CompanyTermTests : ConstVariablesTestBase
    {
        [Test, Description("Получить информацию о сроке хранения на ПВЗ для определенной компании")]
        public void CompanyTermTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var companiesPage =
                LoadPage<СompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string companyId = companiesPage.Table.GetRow(0).ID.GetText();
            var companyEditPage =
                LoadPage<СompanyCreatePage>("admin/companies/edit/" + companyId);
            companyEditPage.Term.SetValue("0");
            companyEditPage.Prolongation.UncheckAndWait();
            companyEditPage.SaveButton.Click();

            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

//            Term  = 0, возвращаем 0 и false
            var responseCompanyTerm =
               (ApiResponse.ResponseCompanyTerm)apiRequest.GET("api/v1/" + keyShopPublic + "/company_term.json",
                   new NameValueCollection
                    {
                        {"company", companyId}
                    });
            Assert.IsTrue(responseCompanyTerm.Success);
            Assert.AreEqual(responseCompanyTerm.Response.Id, companyId);
            Assert.AreEqual(responseCompanyTerm.Response.Term, "0");
            Assert.AreEqual(responseCompanyTerm.Response.Prolongation, false);

            companyEditPage =
                LoadPage<СompanyCreatePage>("admin/companies/edit/" + companyId);
            companyEditPage.Term.SetValue("12");
            companyEditPage.Prolongation.CheckAndWait();
            companyEditPage.SaveButton.Click();

            adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

            //            Term  = 12, возвращаем 12 и true
            responseCompanyTerm =
               (ApiResponse.ResponseCompanyTerm)apiRequest.GET("api/v1/" + keyShopPublic + "/company_term.json",
                   new NameValueCollection
                    {
                        {"company", companyId}
                    });
            Assert.IsTrue(responseCompanyTerm.Success);
            Assert.AreEqual(responseCompanyTerm.Response.Id, companyId);
            Assert.AreEqual(responseCompanyTerm.Response.Term, "12");
            Assert.AreEqual(responseCompanyTerm.Response.Prolongation, true);
        }

        [Test, Description("Получить информацию о сроке хранения на ПВЗ для определенной компании")]
        public void CompanyTermErrorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var responseFail =
               (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/company_term.json",
                   new NameValueCollection
                    {
                        {"company", "company"}
                    });
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.Message, "Company not found");

        }
    }
}