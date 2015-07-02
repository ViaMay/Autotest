using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class OrderCancelTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заказа на самовывоз и отмена заявки")]
        public void SupportCancelTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryPointsPage =
                LoadPage<AdminBaseListPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            var deliveryPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
            var deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            var deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "1"},
		        {"delivery_point", deliveryPoinId},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "true"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
		        {"items_count", "1"},
		        {"order_comment", "order_comment"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            
            LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");
            
            var defaultPage = shopsPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Orders.Click();
            var ordersPage = userPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Number.WaitText("test_userShops_via");
            ordersPage.Table.GetRow(0).Status.WaitText("На складе ИМ");
            ordersPage.Table.GetRow(0).Undo.Click();
            ordersPage.Aletr.WaitText("Ваш запрос на отмену заявки успешно отправлен. Он будет обработан в течение 8 рабочих часов");
            ordersPage.Aletr.Accept();
            ordersPage.Table.GetRow(0).ID.Click();
            var orderPage = ordersPage.GoTo<OrderPage>();
            orderPage.UndoButton.Click();
            orderPage.Aletr.WaitText("Ваш запрос на отмену заявки успешно отправлен. Он будет обработан в течение 8 рабочих часов");
            orderPage.Aletr.Accept();
         }
    }
}