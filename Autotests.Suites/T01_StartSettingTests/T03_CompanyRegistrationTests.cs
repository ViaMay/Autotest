using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
{
    public class T02CompanyRegistrationTests : ConstVariablesTestBase
    {
        [Test, Description("Создания компании")]
        public void T01_CreateCompatyTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminСompanies.Click();
            adminPage.Сompanies.Click();
            var companiesPage = adminPage.GoTo<СompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(сompanyName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(0).Name.IsPresent)
            {
                companiesPage.Table.GetRow(0).ActionsDelete.Click();
                companiesPage = companiesPage.GoTo<СompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(сompanyName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
            companiesPage.CompanyCreate.Click();
            var companyCreatePage = companiesPage.GoTo<СompanyCreatePage>();
            companyCreatePage.Name.SetValueAndWait(сompanyName);
            companyCreatePage.CompanyDriver.SelectValue("Boxberry");
            companyCreatePage.CompanyAddress.SetValueAndWait("Address");
            companyCreatePage.CompanyPickup.SetFirstValueSelect("FSD забор");
            companyCreatePage.CompanyPickupAddButton.Click();
            companyCreatePage.CompanyPickup.SetFirstValueSelect("Lenod");
            companyCreatePage.CompanyPickupAddButton.Click();
            companyCreatePage.ManagersLegalEntity.SetFirstValueSelect(legalEntityName);
            companyCreatePage.SaveButton.Click();
            companiesPage = companyCreatePage.GoTo<СompaniesPage>();

            companiesPage.Table.RowSearch.Name.SetValue(сompanyName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();
            companiesPage.Table.GetRow(0).Name.WaitText(сompanyName);

            companiesPage.Table.GetRow(0).ActionsEdit.Click();
            companyCreatePage = companiesPage.GoTo<СompanyCreatePage>();
            companyCreatePage.CompanyPickup.SetFirstValueSelect(сompanyName);
            companyCreatePage.CompanyPickupAddButton.Click();
            companyCreatePage.SaveButton.Click();
            companyCreatePage.GoTo<СompaniesPage>();
        }

        [Test, Description("Создания точки доставки")]
        public void T02_CreateDeliveryPointTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminСompanies.Click();
            adminPage.DeliveryPoints.Click();
            var deliveryPointsPage = adminPage.GoTo<DeliveryPointsPage>();
            deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
            deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();

            while (deliveryPointsPage.Table.GetRow(0).Name.IsPresent)
            {
                deliveryPointsPage.Table.GetRow(0).ActionsDelete.Click();
                deliveryPointsPage = deliveryPointsPage.GoTo<DeliveryPointsPage>();
                deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
                deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();
            }
            deliveryPointsPage.DeliveryPointCreate.Click();
            var deliveryPointCreatePage = deliveryPointsPage.GoTo<DeliveryPointCreatePage>();
            deliveryPointCreatePage.City.SetFirstValueSelect("Москва");
            deliveryPointCreatePage.DeliveryPointName.SetValueAndWait(deliveryPointName);
            deliveryPointCreatePage.CompanyName.SetFirstValueSelect(сompanyName);
            deliveryPointCreatePage.Address.SetValueAndWait("Москва");
            deliveryPointCreatePage.SaveButton.Click();
            deliveryPointsPage = deliveryPointCreatePage.GoTo<DeliveryPointsPage>();

            deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
            deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();
            deliveryPointsPage.Table.GetRow(0).Name.WaitPresence();
        }
    }
}