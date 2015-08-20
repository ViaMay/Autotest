﻿using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class CalculatorPriceCourirsTests : ConstVariablesTestBase
    {
        [Test, Description("Расчитать цену курьерская")]
        public void CalculatorPriceSelfTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

//            Все поля заполнены. Расчитать цену курьерки
            var responseCalculator =
                (ApiResponse.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                    new NameValueCollection
                    {
                        {"type", "2"},
                        {"city_to", "151184"},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.AreEqual(responseCalculator.Response.Count(), 1);
            Assert.AreEqual(responseCalculator.Response[0].DeliveryCompanyName, companyName);

//          city_to заполнен некорректно. Возврат ошибки
            var responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                new NameValueCollection
                    {
                        {"type", "2"},
                        {"city_to", "33030303030"},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "City not found (city to)");

//            Одна из сторон в запросе равна нулю. Возврат ошибки
            responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                new NameValueCollection
                    {
                        {"type", "2"},
                        {"city_to", "151184"},
                        {"dimension_side1", "0"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "Превышены возможные размеры или вес отправления для данного ПВЗ");

//          Проверка сторон по справочнику, ввод не корретной стороны. Возврат ошибки
            responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                new NameValueCollection
                    {
                        {"type", "2"},
                        {"city_to", "151184"},
                        {"dimension_side", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "Превышены возможные размеры или вес отправления для данного ПВЗ");

//          Превышение веса. Возврат Ошибки
            responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                new NameValueCollection
                    {
                        {"type", "2"},
                        {"city_to", "151184"},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "411111111111111"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "Превышены возможные размеры или вес отправления для данного ПВЗ");

//          Проверка отсутстие declared_price. Возврат ошибки
            responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                new NameValueCollection
                    {
                        {"type", "2"},
                        {"city_to", "151184"},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"payment_price", "1000"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "declared price обязательно к заполнению");
        }
    }
}