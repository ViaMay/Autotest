using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class PickupCompaniesTests : ConstVariablesTestBase
    {
        [Test, Description("Список компаний, имеющих заказы на доставку на складе сортировки")]
        public void PickupCompaniesTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var usersPage =
                LoadPage<UsersPage>("/admin/users/?&filters[username]=" + pickupNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var pickupId = userEdiringPage.Key.GetValue();

            var companiesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            var companyId = companiesPage.Table.GetRow(0).ID.GetText();
            
            var responseCreateOrder =
                (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "2"},
                        {"to_city", "151184"},
                        {"delivery_company", "" + companyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "true"},
                        {"weight", "4"},
                        {"declared_price", "100"},
                        {"payment_price", "300"},
                        {"to_name", "Ургудан Рабат Мантов"},
                        {"to_street", "Барна"},
                        {"to_house", "3a"},
                        {"to_flat", "12"},
                        {"to_phone", "9999999999"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                    });
            Assert.IsTrue(responseCreateOrder.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");
            adminMaintenancePage.AlertText.WaitTextContains("Processed");

            var responseConfirmDelivery = apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection
                {
                    {"barcode", "dd-" + responseCreateOrder.Response.OrderId + "M01" },
                }
                );
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responsePickupCompanies =
                (ApiResponse.ResponseCompaniesOrShops) apiRequest.GET("api/v1/pickup/" + pickupId + "/companies.json",
                    new NameValueCollection {});
            Assert.IsTrue(responsePickupCompanies.Success);
            Assert.AreEqual(responsePickupCompanies.Response[0].Id, companyId);
            Assert.AreEqual(responsePickupCompanies.Response[0].Name, companyName);
        }
        
        [Test, Description("Список компаний, имеющих заказы на доставку на складе сортировки - не удачная отправка")]
        public void PickupCompaniesErrorTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var usersPage =
                LoadPage<UsersPage>("/admin/users/?&filters[username]=" + pickupNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var pickupId = userEdiringPage.Key.GetValue();


            var companiesPage = LoadPage<CompaniesPage>("admin/companies/?&filters[name]=" + companyName);
            var deliveryCompanyId = companiesPage.Table.GetRow(0).ID.GetText();
//            формируем документы чтобы не было списка заказов 
            var responseDocumentsDeliveryError = apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_delivery.json",
               new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });

//            Заказы не найден
            var responsePickupCompaniesError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupId + "/companies.json",
                new NameValueCollection {});
            Assert.IsFalse(responsePickupCompaniesError.Success);
            Assert.AreEqual(responsePickupCompaniesError.Response.ErrorText, "Заказы не найдены");

//            запрос на id без прав
//            usersPage =
//                LoadPage<UsersPage>("/admin/users/?&filters[username]=" + userNameAndPass);
//            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
//            userEdiringPage = usersPage.GoTo<UserCreatePage>();
//            var  userId = userEdiringPage.Key.GetValue();
//            var responsePickupCompaniesError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + userId + "/companies.json",
//                   new NameValueCollection {});
//            Assert.IsFalse(responsePickupCompaniesError.Success);
//            Assert.AreEqual(responsePickupCompaniesError.Response.ErrorText, "Пользователь должен иметь права pickup");
        }
    }
}