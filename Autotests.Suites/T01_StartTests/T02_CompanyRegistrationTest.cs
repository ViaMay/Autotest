using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartTests
{
    public class T02CompanyRegistrationTest : ConstantVariablesTestBase
    {
        [Test, Description("Создания юредического лица для Компании")]
        public void T01_CreateLegalEntityTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminReference.Click();
            adminPage.LegalEntities.Click();
            var legalEntitiesPage = adminPage.GoTo<LegalEntitiesPage>();

            legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
            legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();

            while (legalEntitiesPage.Table.GetRow(0).Name.IsPresent)
            {
                legalEntitiesPage.Table.GetRow(0).ActionsDelete.Click();
                legalEntitiesPage = legalEntitiesPage.GoTo<LegalEntitiesPage>();
                legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
                legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();
            }
            legalEntitiesPage.LegalEntityCreate.Click();
            var legalEntityCreatePage = legalEntitiesPage.GoTo<LegalEntityCreatePage>();
            legalEntityCreatePage.NameEntity.SetValueAndWait(legalEntityName);
            legalEntityCreatePage.SaveButton.Click();
            legalEntitiesPage = legalEntityCreatePage.GoTo<LegalEntitiesPage>();

            legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
            legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();
            legalEntitiesPage.Table.GetRow(0).Name.WaitText(legalEntityName);
        }

        [Test, Description("Создания компании")]
        public void T02_CreateCompatyTest()
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
            companiesPage = companyCreatePage.GoTo<СompaniesPage>();
        }

        [Test, Description("Создания точки доставки")]
        public void T03_CreateDeliveryPointTest()
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

        [Test, Description("Создания цены забора")]
        public void T04_CreatePricePickupTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminСompanies.Click();
            adminPage.Prices.Mouseover();
            adminPage.PricesPickup.Click();
            var pricesPickupPage = adminPage.GoTo<PricesPickupPage>();
            pricesPickupPage.Table.RowSearch.CompanyName.SetValue(сompanyName);
            pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();

            while (pricesPickupPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesPickupPage.Table.GetRow(0).ActionsDelete.Click();
                pricesPickupPage = pricesPickupPage.GoTo<PricesPickupPage>();
                pricesPickupPage.Table.RowSearch.CompanyName.SetValue(сompanyName);
                pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();
            }
            pricesPickupPage.PricePickupCreate.Click();
            var pricePickupCreatePage = pricesPickupPage.GoTo<PricePickupCreatePage>();
            pricePickupCreatePage.CompanyName.SetFirstValueSelect(сompanyName);
            pricePickupCreatePage.City.SetFirstValueSelect("Москва");
            pricePickupCreatePage.Price.SetValueAndWait("10");
            pricePickupCreatePage.PriceOverFlow.SetValueAndWait("2");
            pricePickupCreatePage.Weight.SetFirstValueSelect("Pick (Max)");
            pricePickupCreatePage.Dimension.SetFirstValueSelect("Boxberry (Max)");
            pricePickupCreatePage.SaveButton.Click();
            pricesPickupPage = pricePickupCreatePage.GoTo<PricesPickupPage>();

            pricesPickupPage.Table.RowSearch.CompanyName.SetValue(сompanyName);
            pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();
            pricesPickupPage.Table.GetRow(0).CompanyName.WaitText(сompanyName);
        }

        [Test, Description("Создания цены курьера")]
        public void T05_CreatePriceCourierTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminСompanies.Click();
            adminPage.Prices.Mouseover();
            adminPage.PricesCourier.Click();
            var pricesCourierPage = adminPage.GoTo<PricesCourierPage>();
            pricesCourierPage.Table.RowSearch.CompanyName.SetValue(сompanyName);
            pricesCourierPage = pricesCourierPage.SeachButtonRowClickAndGo();

            while (pricesCourierPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesCourierPage.Table.GetRow(0).ActionsDelete.Click();
                pricesCourierPage = pricesCourierPage.GoTo<PricesCourierPage>();
                pricesCourierPage.Table.RowSearch.CompanyName.SetValue(сompanyName);
                pricesCourierPage = pricesCourierPage.SeachButtonRowClickAndGo();
            }

            pricesCourierPage.PriceCourierCreate.Click();
            var priceCourierCreatePage = pricesCourierPage.GoTo<PriceCreatePage>();
            priceCourierCreatePage.Price.SetValueAndWait("11");
            priceCourierCreatePage.PriceOverFlow.SetValueAndWait("3");
            priceCourierCreatePage.Route.SetFirstValueSelect("2");
            priceCourierCreatePage.CompanyName.SetFirstValueSelect(сompanyName);
            priceCourierCreatePage.Weight.SetFirstValueSelect("Pick (Max)");
            priceCourierCreatePage.Dimension.SetFirstValueSelect("Boxberry (Max)");
            priceCourierCreatePage.SaveButton.Click();
            pricesCourierPage = priceCourierCreatePage.GoTo<PricesCourierPage>();

            pricesCourierPage.Table.RowSearch.CompanyName.SetValue(сompanyName);
            pricesCourierPage = pricesCourierPage.SeachButtonRowClickAndGo();
            pricesCourierPage.Table.GetRow(0).CompanyName.WaitText(сompanyName);
        }

        [Test, Description("Создания цены самовывоза")]
        public void T06_CreateSelfPriceTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminСompanies.Click();
            adminPage.Prices.Mouseover();
            adminPage.PricesSelf.Click();
            var pricesSelfPage = adminPage.GoTo<PricesSelfPage>();
            pricesSelfPage.Table.RowSearch.CompanyName.SetValue(сompanyName);
            pricesSelfPage = pricesSelfPage.SeachButtonRowClickAndGo();

            while (pricesSelfPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesSelfPage.Table.GetRow(0).ActionsDelete.Click();
                pricesSelfPage = pricesSelfPage.GoTo<PricesSelfPage>();
                pricesSelfPage.Table.RowSearch.CompanyName.SetValue(сompanyName);
                pricesSelfPage = pricesSelfPage.SeachButtonRowClickAndGo();
            }

            pricesSelfPage.PriceSelfCreate.Click();
            var priceSelfCreatePage = pricesSelfPage.GoTo<PriceCreatePage>();
            priceSelfCreatePage.Price.SetValueAndWait("12");
            priceSelfCreatePage.PriceOverFlow.SetValueAndWait("4");
            priceSelfCreatePage.Route.SetFirstValueSelect("2");
            priceSelfCreatePage.CompanyName.SetFirstValueSelect(сompanyName);
            priceSelfCreatePage.Weight.SetFirstValueSelect("Pick (Max)");
            priceSelfCreatePage.Dimension.SetFirstValueSelect("Boxberry (Max)");
            priceSelfCreatePage.SaveButton.Click();
            pricesSelfPage = priceSelfCreatePage.GoTo<PricesSelfPage>();

            pricesSelfPage.Table.RowSearch.CompanyName.SetValue(сompanyName);
            pricesSelfPage = pricesSelfPage.SeachButtonRowClickAndGo();
            pricesSelfPage.Table.GetRow(0).CompanyName.WaitText(сompanyName);
        }
    }
}