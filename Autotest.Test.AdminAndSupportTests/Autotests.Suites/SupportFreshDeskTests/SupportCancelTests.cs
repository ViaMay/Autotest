using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageFreshDesk;
using NUnit.Framework;

namespace Autotests.Tests.SupportFreshDeskTests
{
    public class SupportCancelTests : ConstVariablesTestBase
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

            var responseOrderCancel = (ApiResponse.ResponseTrueCancel)apiRequest.GET("api/v1/" + keyShopPublic + "/order_cancel.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", responseCreateOrders.Response.OrderId}
                });
            Assert.AreEqual(responseOrderCancel.Response.OrderId, responseCreateOrders.Response.OrderId);

            var defaultPage = shopsPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Support.Click();
            var pageFreshDesk = userPage.GoTo<SupportFreshDeskPage>();
            pageFreshDesk.LabelDirectory.WaitText("Служба поддержки");
            pageFreshDesk.TicketsLink.Click();
            var pageTickets = pageFreshDesk.GoTo<SupportTicketsPage>();
            pageTickets.TicketFilters.Click();
            pageTickets.TicketFiltersAll.Click();
            pageTickets.WaitTableVisible();
            pageTickets.Table.FindRowByName(responseOrderCancel.Response.OrderId).TicketLink.Click();
            var pageTicket = pageFreshDesk.GoTo<SupportTicketOpenPage>();
            pageTicket.TicketHeading.WaitTextContains("Отмена заказа dd-" + responseCreateOrders.Response.OrderId);
            pageTicket.TicketInfo.WaitTextContains("admin/orders/edit/" + responseCreateOrders.Response.OrderId);
            pageTicket.TicketInfo.WaitTextContains("admin/outgoingorders/edit/");
            pageTicket.TicketInfo.WaitTextContains("Сообщение на отмену заказа dd-" + responseCreateOrders.Response.OrderId);
         }
    }
}