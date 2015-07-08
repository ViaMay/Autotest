﻿using Autotests.WebPages.Pages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T04_AdminTests
{
    public class CheckCompanyApiCalculatorTests : SendOrdersBasePage
    {
        [Test, Description("Проверка компании у поторой драйвер pochtarossii - что калькулятор для нее работает")]
//        то как считается на этом сайте http://www.postcalc.ru/
        public void CheckCompanyApiCalculatorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var companiesPage = LoadPage<CompaniesPage>("admin/companies/?&filters[driver]=pochtarossii&filters[enabled_delivery]=1");
            var companyName = companiesPage.Table.GetRow(0).Name.GetText();

            companiesPage.Orders.Click();
            companiesPage.Сalculator.Click();
            var calculatorPage = companiesPage.GoTo<CalculatorPage>();
            calculatorPage.LabelDirectory.WaitText("Маршрут");
            calculatorPage.RadioButtonList[1].Click();

            calculatorPage.CityFrom.SetFirstValueSelect("Санкт-Петербург");
            calculatorPage.CityTo.SetFirstValueSelect("Москва");
            calculatorPage.Weight.SetValueAndWait("4");
            calculatorPage.Width.SetValueAndWait("4");
            calculatorPage.Height.SetValueAndWait("4");
            calculatorPage.Length.SetValueAndWait("4");

            calculatorPage.PriceDeclared.SetValueAndWait("0");
            calculatorPage.PricePayment.SetValueAndWait("0");

            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<CalculatorPage>();
            var row = calculatorPage.Table.FindRowByName(companyName);
            var priceDeliveryBase = row.PriceDeliveryBase.GetText();
        
            var postcalcPage =LoadPageUrl<PostcalcPage>("http://test.postcalc.ru/?f=190000&t=101000&w=4000&v=0");
            var priceParcel = postcalcPage.PriceParcel.GetText();
            priceParcel = priceParcel.Replace("Array\r\n(\r\n", "");
            priceParcel = priceParcel.Replace(")", "");
            priceParcel = priceParcel.Replace(" ", "");
            priceParcel = priceParcel.Replace("\r", "");
            var cells = priceParcel.Split('\n');
            var priceDeliveryParcel = cells[4];
            Assert.AreEqual("[Доставка]=>" + priceDeliveryBase + ".00", priceDeliveryParcel);
            
        }
    }
}