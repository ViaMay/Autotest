﻿using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class OrderSelfErrorTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заказа на самовывоз,ошибки при создании")]
        public void OrderSelfErrorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var deliveryPointsPage =
                LoadPage<AdminBaseListPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            string deliveryPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
            var deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

            // ошибка в delivery_point
            var responseCreateFailOrder = (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "1"},
		        {"delivery_point", "123456789"},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveryCompanyId},
		        {"shop_refnum", ""},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                });
            Assert.IsFalse(responseCreateFailOrder.Success, "Ожидался ответ Fail на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateFailOrder.Response.Error.ToCity, "Город получения обязательно к заполнению");
            Assert.AreEqual(responseCreateFailOrder.Response.Error.DeliveryPoint, "ПВЗ не работает. Пожалуйста, свяжитесь с администратором системы.");
            Assert.AreEqual(responseCreateFailOrder.Response.Error.DeliveryCompany, "Компания доставки обязательно к заполнению");

            // ошибка в dimension_side1 = 0
            responseCreateFailOrder = (ApiResponse.ResponseFailObject)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "1"},
		        {"delivery_point", deliveryPoinId},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveryCompanyId},
		        {"shop_refnum", ""},
		        {"dimension_side1", "0"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                });
            Assert.IsFalse(responseCreateFailOrder.Success, "Ожидался ответ Fail на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateFailOrder.Response.Error.CalculateOrder, "Ошибка просчета цены, или маршрут недоступен");
            Assert.AreEqual(responseCreateFailOrder.Response.Error.DimensionSide1, "Значение поля &laquo;Сторона 1&raquo; должно быть положительным числом");
        }
    }
}