﻿using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser.Controls;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class ShopDeliveryCompanyTests : ConstVariablesTestBase
    {
        [Test, Description("Получить список доступных компаний доставки если указана компания забора и если не указана")]
        public void ShopDeliveryCompanyTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var companiesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            var companyId = companiesPage.Table.GetRow(0).ID.GetText();

//           Порверка если компания забора указана
            var responseDeliveryCompanу = (ApiResponse.ResponseCompaniesOrShops)apiRequest.GET("api/v1/" + keyShopPublic + "/shop_delivery_companies.json",
                new NameValueCollection { });
            Assert.IsTrue(responseDeliveryCompanу.Success);
            Assert.AreEqual(responseDeliveryCompanу.Response[0].Id, companyId);
            Assert.AreEqual(responseDeliveryCompanу.Response[0].Name, companyName);

//            проверка если нету компании забора
            var usersWarehousesPage =
                LoadPage<UsersWarehousesPage>("/admin/warehouses/?&filters[name]=" + userWarehouseName);
            var warehousesId = companiesPage.Table.GetRow(0).ID.GetText();

            shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_ApiAdmin");
            while (shopsPage.Table.GetRow(0).Name.IsPresent)
            {
                shopsPage.Table.GetRow(0).ActionsDelete.Click();
                shopsPage.Aletr.Accept();
                shopsPage = shopsPage.GoTo<UsersShopsPage>();
                shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_ApiAdmin");
            }

//            Создание магазина
            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiAdmin"},
                    {"warehouse", warehousesId},
                    {"address", "Москва"}
                }
                );
            Assert.IsTrue(responseShop.Success);
            
//            удаление компании забора
            shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_ApiAdmin");
            shopsPage.Table.GetRow(0).ActionsEdit.Click();

            var shopEditPage = shopsPage.GoTo<UserAdminShopCreatePage>();
            shopEditPage.CompanyPickup.SetValueAndWait("  ");
            shopEditPage.CreateButton.Click();
            shopsPage = shopEditPage.GoTo<UsersShopsPage>();

//            отправляем запрос так что в магазин с пустой компанией забора
            responseDeliveryCompanу = (ApiResponse.ResponseCompaniesOrShops)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/shop_delivery_companies.json",
                new NameValueCollection { });

            Assert.IsTrue(responseDeliveryCompanу.Success);

            var responseRowDeliveryCompanу = FindRowByName(companyName, responseDeliveryCompanу);
            Assert.AreEqual(responseRowDeliveryCompanу.Id, companyId);
            Assert.AreEqual(responseRowDeliveryCompanу.Name, companyName);
        }

        [Test, Description("Получить список доступных компаний доставки")]
        public void ShopDeliveryCompanyErrorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiAdmin"},
                        {"flat", "138"},
                        {"city", "151184"},
                        {"contact_person", "contact_person"},
                        {"contact_phone", "contact_phone"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "schedule"},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsTrue(responseWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");

            shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_ApiAdmin");
            while (shopsPage.Table.GetRow(0).Name.IsPresent)
            {
                shopsPage.Table.GetRow(0).ActionsDelete.Click();
                shopsPage.Aletr.Accept();
                shopsPage = shopsPage.GoTo<UsersShopsPage>();
                shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_ApiAdmin");
            }

//            Создание магазина
            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiAdmin"},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Москва"}
                }
                );
            Assert.IsTrue(responseShop.Success);

//             меняем компании забора чтобы компания не имела цены забора и удаляем цену
            shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_ApiAdmin");
            shopsPage.Table.GetRow(0).ActionsEdit.Click();

            var shopEditPage = shopsPage.GoTo<UserAdminShopCreatePage>();
            shopEditPage.CompanyPickup.SetFirstValueSelect(companyName);
            shopEditPage.CreateButton.Click();
            shopsPage = shopEditPage.GoTo<UsersShopsPage>();
            var pricesPickupPage = LoadPage<PricesPickupPage>("/admin/pickupprices/?&filters[company]=" + companyName);
            while (pricesPickupPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesPickupPage.Table.GetRow(0).ActionsDelete.Click();
                pricesPickupPage.Aletr.Accept();
                pricesPickupPage = pricesPickupPage.GoTo<PricesPickupPage>();
                pricesPickupPage = LoadPage<PricesPickupPage>("/admin/pickupprices/?&filters[company]=" + companyName);
            }
//            отправляем запрос так что в магазин с пустой компанией забора
            var responseDeliveryCompanу = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/shop_delivery_companies.json",
                new NameValueCollection { });
            Assert.IsFalse(responseDeliveryCompanу.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseDeliveryCompanу.Response.ErrorText, "У компании Забора нет забора в городе, к которому привязан склад.");

//            создаем цену забора
            pricesPickupPage.PricePickupCreate.Click();
            var pricePickupCreatePage = pricesPickupPage.GoTo<PricePickupCreatePage>();
            pricePickupCreatePage.CompanyName.SetFirstValueSelect(companyName);
            pricePickupCreatePage.City.SetFirstValueSelect("Москва");
            pricePickupCreatePage.Price.SetValueAndWait("10");
            pricePickupCreatePage.PriceOverFlow.SetValueAndWait("2");
            pricePickupCreatePage.Weight.SetFirstValueSelect(weightName);
            pricePickupCreatePage.Dimension.SetFirstValueSelect(sideName);
            pricePickupCreatePage.SaveButton.Click();
            pricesPickupPage = pricePickupCreatePage.GoTo<PricesPickupPage>();

            //            отправляем запрос так что в магазин с пустой компанией забора
            responseDeliveryCompanу = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/shop_delivery_companies.json",
                new NameValueCollection { });
            Assert.IsFalse(responseDeliveryCompanу.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseDeliveryCompanу.Response.ErrorText, "Компании не найдены");

//            удаление склада
            var warehousesPage = LoadPage<UsersWarehousesPage>("/admin/warehouses/?&filters[name]=" + userWarehouseName + "_Api");

            warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName + "_Api");
            warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            while (warehousesPage.Table.GetRow(0).Name.IsPresent)
            {
                warehousesPage.Table.GetRow(0).ActionsDelete.Click();
                warehousesPage.Aletr.Accept();
                warehousesPage = warehousesPage.GoTo<UsersWarehousesPage>();
                warehousesPage = LoadPage<UsersWarehousesPage>("/admin/warehouses/?&filters[name]=" + userWarehouseName + "_Api");
            }

            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

            responseDeliveryCompanу = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/shop_delivery_companies.json",
                new NameValueCollection { });
            Assert.IsFalse(responseDeliveryCompanу.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseDeliveryCompanу.Response.ErrorText, "У компании Забора нет забора в городе, к которому привязан склад.");
        }

        public ApiResponse.MessageCompaniesOrShops FindRowByName(string name, ApiResponse.ResponseCompaniesOrShops responseDeliveryCompanу)
        {
            for (var i = 0; i < responseDeliveryCompanу.Response.Count(); i++)
            {
                if (responseDeliveryCompanу.Response[i].Name == name)
                    return responseDeliveryCompanу.Response[i];
            }
            throw new Exception(string.Format("не найдена компания с именем {0} в списке всех компаний доставки", companyName));
        }
    }
}