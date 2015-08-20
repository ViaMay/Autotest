using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
{
    public class T06CreatePriceTests : ConstVariablesTestBase
    {
        [Test, Description("Создания цены забора")]
        public void CreatePricePickupTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Prices.Mouseover();
            adminPage.PricesPickup.Click();
            var pricesPickupPage = adminPage.GoTo<PricesPickupPage>();
            pricesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();

            while (pricesPickupPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesPickupPage.Table.GetRow(0).ActionsDelete.Click();
                pricesPickupPage.Aletr.Accept();
                pricesPickupPage = pricesPickupPage.GoTo<PricesPickupPage>();
                pricesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();
            }
            pricesPickupPage.Create.Click();
            var pricePickupCreatePage = pricesPickupPage.GoTo<PricePickupCreatePage>();
            pricePickupCreatePage.CompanyName.SetFirstValueSelect(companyPickupName);
            pricePickupCreatePage.City.SetFirstValueSelect("Москва");
            pricePickupCreatePage.Price.SetValueAndWait("10");
            pricePickupCreatePage.PriceOverFlow.SetValueAndWait("2");
            pricePickupCreatePage.Weight.SetFirstValueSelect(weightName);
            pricePickupCreatePage.Dimension.SetFirstValueSelect(sideName);
            pricePickupCreatePage.SaveButton.Click();
            pricesPickupPage = pricePickupCreatePage.GoTo<PricesPickupPage>();

            pricesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();
            pricesPickupPage.Table.GetRow(0).CompanyName.WaitText(companyPickupName);
        }

        [Test, Description("Создания цены курьера")]
        public void CreatePriceCourierTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Prices.Mouseover();
            adminPage.PricesCourier.Click();
            var pricesCourierPage = adminPage.GoTo<PricesPage>();
            pricesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
            pricesCourierPage = pricesCourierPage.SeachButtonRowClickAndGo();

            while (pricesCourierPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesCourierPage.Table.GetRow(0).ActionsDelete.Click();
                pricesCourierPage.Aletr.Accept();
                pricesCourierPage = pricesCourierPage.GoTo<PricesPage>();
                pricesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
                pricesCourierPage = pricesCourierPage.SeachButtonRowClickAndGo();
            }

            pricesCourierPage.Create.Click();
            var priceCourierCreatePage = pricesCourierPage.GoTo<PriceCreatePage>();
            priceCourierCreatePage.Price.SetValueAndWait("11");
            priceCourierCreatePage.PriceOverFlow.SetValueAndWait("3");
            priceCourierCreatePage.Route.SetFirstValueSelect("2", "г. Москва #151184 - г. Москва #151184");
            priceCourierCreatePage.CompanyName.SetFirstValueSelect(companyName);
            priceCourierCreatePage.Weight.SetFirstValueSelect(weightName);
            priceCourierCreatePage.Dimension.SetFirstValueSelect(sideName);
            priceCourierCreatePage.SaveButton.Click();
            pricesCourierPage = priceCourierCreatePage.GoTo<PricesPage>();
            
            pricesCourierPage.Create.Click();
            priceCourierCreatePage = pricesCourierPage.GoTo<PriceCreatePage>();
            priceCourierCreatePage.Price.SetValueAndWait("11");
            priceCourierCreatePage.PriceOverFlow.SetValueAndWait("3");
            priceCourierCreatePage.Route.SetFirstValueSelect("3", "г. Москва #151184 - г. Санкт-Петербург #151185");
            priceCourierCreatePage.CompanyName.SetFirstValueSelect(companyName);
            priceCourierCreatePage.Weight.SetFirstValueSelect(weightName);
            priceCourierCreatePage.Dimension.SetFirstValueSelect(sideName);
            priceCourierCreatePage.SaveButton.Click();

            pricesCourierPage = priceCourierCreatePage.GoTo<PricesPage>();
            pricesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
            pricesCourierPage = pricesCourierPage.SeachButtonRowClickAndGo();
            pricesCourierPage.Table.GetRow(1).CompanyName.WaitText(companyName);
        }

        [Test, Description("Создания цены самовывоза")]
        public void CreateSelfPriceTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Prices.Mouseover();
            adminPage.PricesSelf.Click();
            var pricesSelfPage = adminPage.GoTo<PricesPage>();
            pricesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
            pricesSelfPage = pricesSelfPage.SeachButtonRowClickAndGo();

            while (pricesSelfPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesSelfPage.Table.GetRow(0).ActionsDelete.Click();
                pricesSelfPage.Aletr.Accept();
                pricesSelfPage = pricesSelfPage.GoTo<PricesPage>();
                pricesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
                pricesSelfPage = pricesSelfPage.SeachButtonRowClickAndGo();
            }

            pricesSelfPage.Create.Click();
            var priceSelfCreatePage = pricesSelfPage.GoTo<PriceCreatePage>();
            priceSelfCreatePage.Price.SetValueAndWait("12");
            priceSelfCreatePage.PriceOverFlow.SetValueAndWait("4");
            priceSelfCreatePage.Route.SetFirstValueSelect("2", "г. Москва #151184 - г. Москва #151184");
            priceSelfCreatePage.CompanyName.SetFirstValueSelect(companyName);
            priceSelfCreatePage.Weight.SetFirstValueSelect(weightName);
            priceSelfCreatePage.Dimension.SetFirstValueSelect(sideName);
            priceSelfCreatePage.SaveButton.Click();
            pricesSelfPage = priceSelfCreatePage.GoTo<PricesPage>();
            
            pricesSelfPage.Create.Click();
            priceSelfCreatePage = pricesSelfPage.GoTo<PriceCreatePage>();
            priceSelfCreatePage.Price.SetValueAndWait("12");
            priceSelfCreatePage.PriceOverFlow.SetValueAndWait("3");
            priceSelfCreatePage.Route.SetFirstValueSelect("3", "г. Москва #151184 - г. Санкт-Петербург #151185");
            priceSelfCreatePage.CompanyName.SetFirstValueSelect(companyName);
            priceSelfCreatePage.Weight.SetFirstValueSelect(weightName);
            priceSelfCreatePage.Dimension.SetFirstValueSelect(sideName);
            priceSelfCreatePage.SaveButton.Click();
            pricesSelfPage = priceSelfCreatePage.GoTo<PricesPage>();

            pricesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
            pricesSelfPage = pricesSelfPage.SeachButtonRowClickAndGo();
            pricesSelfPage.Table.GetRow(1).CompanyName.WaitText(companyName);
        }
    }
}