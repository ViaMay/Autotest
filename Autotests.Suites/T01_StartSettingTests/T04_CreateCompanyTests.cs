using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
{
    public class T04CreateCompanyTests : ConstVariablesTestBase
    {
        [Test, Description("Создания компании")]
        public void CreateCompatyTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminСompanies.Click();
            adminPage.Сompanies.Click();
            var companiesPage = adminPage.GoTo<СompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(0).Name.IsPresent)
            {
                companiesPage.Table.GetRow(0).ActionsDelete.Click();
                companiesPage = companiesPage.GoTo<СompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
            companiesPage.CompanyCreate.Click();
            var companyCreatePage = companiesPage.GoTo<СompanyCreatePage>();
            companyCreatePage.Name.SetValueAndWait(companyName);
            companyCreatePage.CompanyDriver.SelectValue("Boxberry");
            companyCreatePage.CompanyAddress.SetValueAndWait("Address");
            companyCreatePage.CompanyPickup.SetFirstValueSelect("FSD забор");
            companyCreatePage.CompanyPickupAddButton.Click();
            companyCreatePage.CompanyPickup.SetFirstValueSelect("Lenod");
            companyCreatePage.CompanyPickupAddButton.Click();
            companyCreatePage.ManagersLegalEntity.SetFirstValueSelect(legalEntityName);
            companyCreatePage.SaveButton.Click();
            companiesPage = companyCreatePage.GoTo<СompaniesPage>();

            companiesPage.Table.RowSearch.Name.SetValue(companyName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();
            companiesPage.Table.GetRow(0).Name.WaitText(companyName);

            companiesPage.Table.GetRow(0).ActionsEdit.Click();
            companyCreatePage = companiesPage.GoTo<СompanyCreatePage>();
            companyCreatePage.CompanyPickup.SetFirstValueSelect(companyName);
            companyCreatePage.CompanyPickupAddButton.Click();
            companyCreatePage.SaveButton.Click();
            companyCreatePage.GoTo<СompaniesPage>();

            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
        }
    }
}