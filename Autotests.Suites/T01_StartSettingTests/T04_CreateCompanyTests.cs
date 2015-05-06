using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
{
    public class T04CreateCompanyTests : ConstVariablesTestBase
    {
        [Test, Description("Создания компании Pickup")]
        public void CreateCompatyPickupTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<CompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(0).Name.IsPresent)
            {
                companiesPage.Table.GetRow(0).ActionsDelete.Click();
                companiesPage.Aletr.Accept();
                companiesPage = companiesPage.GoTo<CompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
            companiesPage.CompanyCreate.Click();
            var companyCreatePage = companiesPage.GoTo<CompanyCreatePage>();
            companyCreatePage.Name.SetValueAndWait(companyPickupName);
            companyCreatePage.CompanyDriver.SelectValue("Boxberry");
            companyCreatePage.CompanyAddress.SetValueAndWait("Address");
            companyCreatePage.ManagersLegalEntity.SetFirstValueSelect(legalEntityName);
            companyCreatePage.ManagersPickup.SetFirstValueSelect(legalPickupName);
            companyCreatePage.SaveButton.Click();
            companiesPage = companyCreatePage.GoTo<CompaniesPage>();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
        }

        [Test, Description("Создания компании")]
        public void CreateCompatyTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<CompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(0).Name.IsPresent)
            {
                companiesPage.Table.GetRow(0).ActionsDelete.Click();
                companiesPage.Aletr.Accept();
                companiesPage = companiesPage.GoTo<CompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
            companiesPage.CompanyCreate.Click();
            var companyCreatePage = companiesPage.GoTo<CompanyCreatePage>();
            companyCreatePage.Name.SetValueAndWait(companyName);
            companyCreatePage.CompanyPickup.SetFirstValueSelect(companyPickupName);
            companyCreatePage.CompanyPickupAddButton.Click();
            companyCreatePage.CompanyDriver.SelectValue("Boxberry");
            companyCreatePage.CompanyAddress.SetValueAndWait("Address");
            companyCreatePage.ManagersLegalEntity.SetFirstValueSelect(legalEntityName);
            companyCreatePage.SaveButton.Click();
            companiesPage = companyCreatePage.GoTo<CompaniesPage>();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
           
//            удаление календаря если он был
            adminMaintenancePage.AdminCompanies.Click();
            adminMaintenancePage.Calendars.Click();
            var calendarsPage = adminMaintenancePage.GoTo<CalendarsPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            while (calendarsPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                calendarsPage.Table.GetRow(0).ActionsDelete.Click();
                calendarsPage = calendarsPage.GoTo<CalendarsPage>();
                calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
                calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            }

            adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
        }
        [Test, Description("Создания наложенного платежа")]
        public void CreatePaymentPriceTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.PaymentPrice.Click();
            var рaymentPricePage = adminPage.GoTo<PaymentPricePage>();
            рaymentPricePage.Table.RowSearch.CompanyName.SetValue(companyName);
            рaymentPricePage = рaymentPricePage.SeachButtonRowClickAndGo();

            while (рaymentPricePage.Table.GetRow(0).Name.IsPresent)
            {
                рaymentPricePage.Table.GetRow(0).ActionsDelete.Click();
                рaymentPricePage.Aletr.Accept();
                рaymentPricePage = рaymentPricePage.GoTo<PaymentPricePage>();
                рaymentPricePage.Table.RowSearch.CompanyName.SetValue(companyName);
                рaymentPricePage = рaymentPricePage.SeachButtonRowClickAndGo();
            }
            рaymentPricePage.CompanyCreate.Click();
            var рaymentPriceCreatePage = рaymentPricePage.GoTo<PaymentPriceCreatePage>();
            рaymentPriceCreatePage.Company.SetFirstValueSelect(companyName);
            рaymentPriceCreatePage.City.SetFirstValueSelect("Москва");
            рaymentPriceCreatePage.SaveButton.Click();
            рaymentPricePage = рaymentPriceCreatePage.GoTo<PaymentPricePage>();

            рaymentPricePage.Table.RowSearch.CompanyName.SetValue(companyName);
            рaymentPricePage = рaymentPricePage.SeachButtonRowClickAndGo();
            рaymentPricePage.Table.GetRow(0).Name.WaitText(companyName);
        }

        [Test, Description("Создание графика забора")]
        public void PickupTimetableTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.PickupTimetable.Click();
            var pickupTimetablePage = adminPage.GoTo<PickupTimetablePage>();
            pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();

            while (pickupTimetablePage.Table.GetRow(0).Name.IsPresent)
            {
                pickupTimetablePage.Table.GetRow(0).ActionsDelete.Click();
                pickupTimetablePage = pickupTimetablePage.GoTo<PickupTimetablePage>();
                pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            }

            pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyName);
            pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            while (pickupTimetablePage.Table.GetRow(0).Name.IsPresent)
            {
                pickupTimetablePage.Table.GetRow(0).ActionsDelete.Click();
                pickupTimetablePage = pickupTimetablePage.GoTo<PickupTimetablePage>();
                pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyName);
                pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            }

            pickupTimetablePage.Create.Click();
            var pickupTimetableCreatePage = pickupTimetablePage.GoTo<PickupTimetableCreatePage>();
            pickupTimetableCreatePage.Company.SetFirstValueSelect(companyPickupName);
            pickupTimetableCreatePage.PickupTime.SelectByText("23:45");
            pickupTimetableCreatePage.PickupPeriod.SelectByText("Сегодня");
            pickupTimetableCreatePage.SaveButton.Click();
            pickupTimetablePage = pickupTimetableCreatePage.GoTo<PickupTimetablePage>();

            pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            pickupTimetablePage.Table.GetRow(0).Name.WaitText(companyPickupName);
        }
    }
}