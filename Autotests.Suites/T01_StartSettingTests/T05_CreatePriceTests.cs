using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
{
    public class T04CreatePriceTests : ConstVariablesTestBase
    {
        [Test, Description("Создания цены забора")]
        public void T01_CreatePricePickupTest()
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
            pricePickupCreatePage.Weight.SetFirstValueSelect(weightName);
            pricePickupCreatePage.Dimension.SetFirstValueSelect(sizeName);
            pricePickupCreatePage.SaveButton.Click();
            pricesPickupPage = pricePickupCreatePage.GoTo<PricesPickupPage>();

            pricesPickupPage.Table.RowSearch.CompanyName.SetValue(сompanyName);
            pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();
            pricesPickupPage.Table.GetRow(0).CompanyName.WaitText(сompanyName);
        }

        [Test, Description("Создания цены курьера")]
        public void T02_CreatePriceCourierTest()
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
            priceCourierCreatePage.Weight.SetFirstValueSelect(weightName);
            priceCourierCreatePage.Dimension.SetFirstValueSelect(sizeName);
            priceCourierCreatePage.SaveButton.Click();
            pricesCourierPage = priceCourierCreatePage.GoTo<PricesCourierPage>();

            pricesCourierPage.Table.RowSearch.CompanyName.SetValue(сompanyName);
            pricesCourierPage = pricesCourierPage.SeachButtonRowClickAndGo();
            pricesCourierPage.Table.GetRow(0).CompanyName.WaitText(сompanyName);
        }

        [Test, Description("Создания цены самовывоза")]
        public void T03_CreateSelfPriceTest()
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
            priceSelfCreatePage.Weight.SetFirstValueSelect(weightName);
            priceSelfCreatePage.Dimension.SetFirstValueSelect(sizeName);
            priceSelfCreatePage.SaveButton.Click();
            pricesSelfPage = priceSelfCreatePage.GoTo<PricesSelfPage>();

            pricesSelfPage.Table.RowSearch.CompanyName.SetValue(сompanyName);
            pricesSelfPage = pricesSelfPage.SeachButtonRowClickAndGo();
            pricesSelfPage.Table.GetRow(0).CompanyName.WaitText(сompanyName);
        }
    }
}