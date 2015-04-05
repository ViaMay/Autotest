using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class OrderSelfCreateAndSendTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заказа на самовывоз, запрос статусов, информации, подтверждения и отмена заявки")]
        public void OrderSelfTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryPointsPage =
                LoadPage<DeliveryPointsPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            var deliveriPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
            var deliveryCompaniesPage =
                LoadPage<СompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            var deliveriCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "1"},
		        {"delivery_point", deliveriPoinId},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveriCompanyId},
		        {"shop_refnum", userShopName},
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
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var defaultPage = shopsPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Orders.Click();
            var ordersPage = userPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).ID.WaitText(responseCreateOrders.Message.OrderId);
            ordersPage.Table.GetRow(0).Type.WaitText("Самовывоз");
            ordersPage.Table.GetRow(0).Status.WaitText("В обработке");
            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Подтвердить");
            ordersPage.Table.GetRow(0).Edit.WaitText("Редактировать");

//           Подтверждение заявки
            var responseConfirmationOrders = apiRequest.POST(keyShopPublic + "/order_confirm.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", responseCreateOrders.Message.OrderId},
                });
            Assert.IsTrue(responseConfirmationOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            ordersPage.Orders.Click();
            ordersPage = ordersPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).ID.WaitText(responseCreateOrders.Message.OrderId);
            ordersPage.Table.GetRow(0).Status.WaitText("Подтверждена");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Отменить");
            ordersPage.Table.GetRow(0).Edit.WaitText("Редактировать");

//           Порверка статуса заявки
            var responseOrderStatus = (ApiResponse.ResponseStatus)apiRequest.GET("api/v1/" + keyShopPublic + "/order_status.json",
                new NameValueCollection
                {
                {"order", responseCreateOrders.Message.OrderId}
                });
            Assert.AreEqual(responseOrderStatus.Message.StatusDescription, "Подтверждена");

//           Инфо заявки 
            var responseOrderInfo = (ApiResponse.ResponseOrderInfo)apiRequest.GET("api/v1/" + keyShopPublic
                + "/order_info/" + responseCreateOrders.Message.OrderId + ".json",
                new NameValueCollection {});
            Assert.AreEqual(responseOrderInfo.Message.ToEmail, userNameAndPass);
            Assert.AreEqual(responseOrderInfo.Message.ToName, "Ургудан Рабат Мантов");
							        
//         Отмена ордера (неудачная)
            var responseOrderCancelFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/order_cancel.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", responseCreateOrders.Message.OrderId}
                });
            Assert.AreEqual(responseOrderCancelFail.Message.Message, "This order can not be canceled");

            defaultPage = ordersPage.LoginOut();
            var adminPage = defaultPage.LoginAsAdmin(adminName, adminPass);
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");
            defaultPage = adminMaintenancePage.LoginOut();
            userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Orders.Click();
            ordersPage = userPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).ID.WaitText(responseCreateOrders.Message.OrderId);
            ordersPage.Table.GetRow(0).Status.WaitText("На складе ИМ");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Отменить");
            ordersPage.Table.GetRow(0).Edit.WaitText("Редактировать");
            var responseOrderCancel = (ApiResponse.ResponseTrueCancel)apiRequest.GET("api/v1/" + keyShopPublic + "/order_cancel.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", responseCreateOrders.Message.OrderId}
                });
            Assert.AreEqual(responseOrderCancel.Message.OrderId, responseCreateOrders.Message.OrderId);
            
            ordersPage.Support.Click();
            ordersPage.SupportList.Click();
            var supportListPage = userPage.GoTo<SupportListPage>();
            supportListPage.Table.GetRow(0).TicketId.WaitText(responseOrderCancel.Message.TicketId);
            supportListPage.Table.GetRow(0).TicketText.WaitText("Изменение заявок");
            supportListPage.Table.GetRow(0).Content.WaitText("Отмена заказа");
            supportListPage.Table.GetRow(0).Status.WaitText("Открыт");
        }
    }
}