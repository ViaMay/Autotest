﻿using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;
namespace Autotests.Tests.ApiTests
{
    public class PickupOrdersTests : SendOrdersBasePage
    {
        [Test, Description("Список заказов на складе сортировки")]
        public void PickupOrdersTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            string[] ordersId = SendOrdersRequest();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");
            adminMaintenancePage.AlertText.WaitTextContains("Processed");
            var usersPage =
                LoadPage<UsersPage>("/admin/users/?&filters[username]=" + pickupNameAndPass);
            usersPage.UsersTable.GetRow(0).ActionsEdit.Click();
            var userEdiringPage = usersPage.GoTo<UserCreatePage>();
            var pickupId = userEdiringPage.Key.GetValue();
            var companiesPage = LoadPage<CompaniesPage>("admin/companies/?&filters[name]=" + companyName);
            var deliveryCompanyId = companiesPage.Table.GetRow(0).ID.GetText();
            usersPage =
                LoadPage<UsersPage>("admin/users?&filters[username]=" + userNameAndPass);
            usersPage.Table.GetRow(0).ActionsEdit.Click();
            var userCreatePage = usersPage.GoTo<UserCreatePage>();
            var userKey = userCreatePage.Key.GetValue();

            var responseBarcodes01 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[0] }, });
            var responseBarcodes02 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[1] }, });

//            формируем документы чтобы не было списка заказов 
            var responseDocumentsDeliveryError = apiRequest.GET("api/v1/pickup/" + pickupId + "/documents_delivery.json",
               new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });

//            запрос списка когда нет его
            var responseError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupId + "/orders.json",
               new NameValueCollection{ {"delivery_company_id", deliveryCompanyId},});
            Assert.IsFalse(responseError.Success);
            Assert.AreEqual(responseError.Response.ErrorText, "Заказы не найдены");

//            подтверждаем что заказ на складе
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes01.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success);
            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupId + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes02.Response.Barcodes[0]} });
            Assert.IsTrue(responseConfirmDelivery.Success);

//            запрос списка
            var responseOrdersPickup = (ApiResponse.ResponsePickupOrders)apiRequest.GET("api/v1/pickup/" + pickupId + "/orders.json",
               new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });
            Assert.IsTrue(responseOrdersPickup.Success);
            Assert.AreEqual(responseOrdersPickup.Response.Count(), 2);
            Assert.AreEqual(responseOrdersPickup.Response[0].DeliveryCompanyId, deliveryCompanyId);
            Assert.AreEqual(responseOrdersPickup.Response[1].DeliveryCompanyId, deliveryCompanyId);
            if (responseOrdersPickup.Response[0].Id == ordersId[0])
                Assert.AreEqual(responseOrdersPickup.Response[1].Id, ordersId[1]);
            else
            {
                Assert.AreEqual(responseOrdersPickup.Response[0].Id, ordersId[1]);
                Assert.AreEqual(responseOrdersPickup.Response[1].Id, ordersId[0]);
            }
        }
    }
}