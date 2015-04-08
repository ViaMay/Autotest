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
            companyCreatePage.ManagersLegalEntity.SetFirstValueSelect(legalEntityName);
            companyCreatePage.SaveButton.Click();
            companiesPage = companyCreatePage.GoTo<СompaniesPage>();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

            adminMaintenancePage.AdminСompanies.Click();
            adminMaintenancePage.Сompanies.Click();
            companiesPage = adminPage.GoTo<СompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();
            companiesPage.Table.GetRow(0).Name.WaitText(companyName);

            companiesPage.Table.GetRow(0).ActionsEdit.Click();
            companyCreatePage = companiesPage.GoTo<СompanyCreatePage>();
            companyCreatePage.CompanyPickup.SetFirstValueSelect(companyName);
            companyCreatePage.CompanyPickupAddButton.Click();
            companyCreatePage.SaveButton.Click();
            companiesPage = companyCreatePage.GoTo<СompaniesPage>();
            
//            удаление календаря если он был
            companiesPage.AdminСompanies.Click();
            companiesPage.Calendars.Click();
            var calendarsPage = companyCreatePage.GoTo<CalendarsPage>();
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
            adminPage.AdminСompanies.Click();
            adminPage.PaymentPrice.Click();
            var рaymentPricePage = adminPage.GoTo<PaymentPricePage>();
            рaymentPricePage.Table.RowSearch.CompanyName.SetValue(companyName);
            рaymentPricePage = рaymentPricePage.SeachButtonRowClickAndGo();

            while (рaymentPricePage.Table.GetRow(0).Name.IsPresent)
            {
                рaymentPricePage.Table.GetRow(0).ActionsDelete.Click();
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
            adminPage.AdminСompanies.Click();
            adminPage.PickupTimetable.Click();
            var pickupTimetablePage = adminPage.GoTo<PickupTimetablePage>();
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
            pickupTimetableCreatePage.Company.SetFirstValueSelect(companyName);
            pickupTimetableCreatePage.PickupTime.SelectByText("23:45");
            pickupTimetableCreatePage.PickupPeriod.SelectByText("Сегодня");
            pickupTimetableCreatePage.SaveButton.Click();
            pickupTimetablePage = pickupTimetableCreatePage.GoTo<PickupTimetablePage>();

            pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyName);
            pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            pickupTimetablePage.Table.GetRow(0).Name.WaitText(companyName);
        }
    }
}