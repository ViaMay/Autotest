using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageFreshDesk;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.SupportFreshDeskTests
{
    public class SupportEditingSendCourirsTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заказа самовывоза и редактирование")]
        public void OrderSelfEditingTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryCompaniesPage =
                LoadPage<CompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();
            var deliveryPointsPage =
                LoadPage<AdminBaseListPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            string deliveryPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"delivery_point", deliveryPoinId},
                        {"to_city", "151184"},
                        {"delivery_company", deliveryCompanyId},
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
                        {"order_comment", "order_comment"}
                    });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");
            
            var responseEditOrders = (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/order_update/" +
                                                                                     responseCreateOrders.Response
                                                                                         .OrderId + ".json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"dimension_side1", "5"},
                    {"dimension_side2", "5"},
                    {"dimension_side3", "5"},
                    {"weight", "5"},
                    {"declared_price", "1100"},
                    {"payment_price", "1300"},
                    {"to_name", "to_name"},
                    {"to_street", "to_street"},
                    {"to_house", "to_house"},
                    {"to_flat", "to_flat"},
                    {"to_phone", "1199999999"},
                    {"goods_description", "goods_description"},
                    {"to_email", "2" + userNameAndPass},
                    {"order_comment", "order_comment2"}
                });
            Assert.IsTrue(responseEditOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateOrders.Response.OrderId, responseEditOrders.Response.Id);

            DefaultPage defaultPage = shopsPage.LoginOut();
            UserHomePage userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Support.Click();
            var pageFreshDesk = userPage.GoTo<SupportFreshDeskPage>();
            pageFreshDesk.LabelDirectory.WaitText("Служба поддержки");
            pageFreshDesk.TicketsLink.Click();
            var pageTickets = pageFreshDesk.GoTo<SupportTicketsPage>();
            pageTickets.TicketFilters.Click();
            pageTickets.TicketFiltersAll.Click();
            pageTickets.WaitTableVisible();
            pageTickets.Table.GetRow(0).TicketLink.Click();
            var pageTicket = pageFreshDesk.GoTo<SupportTicketOpenPage>();
//            pageTicket.TicketStatus.WaitTextContains("Начиная с");
            pageTicket.TicketHeading.WaitTextContains("Редактирование заказа dd-" + responseCreateOrders.Response.OrderId);
            pageTicket.TicketInfo.WaitTextContains("admin/orders/edit/" + responseCreateOrders.Response.OrderId);
            pageTicket.TicketInfo.WaitTextContains("admin/outgoingorders/edit/");
            pageTicket.TicketInfo.WaitTextContains("список измененных полей и новых значений:");
            pageTicket.TicketInfo.WaitTextContains("Оценочная стоимость = 1100");
            pageTicket.TicketInfo.WaitTextContains("Наложенный платеж = 1300");
            pageTicket.TicketInfo.WaitTextContains("Комментарий к заказу = order_comment2");
            pageTicket.TicketInfo.WaitTextContains("ФИО получателя = to_name");
            pageTicket.TicketInfo.WaitTextContains("Телефон получателя = 1199999999");
            pageTicket.TicketInfo.WaitTextContains("Email = 2tester@user.ru");
            pageTicket.TicketInfo.WaitTextContains("Улица получателя = to_street");
            pageTicket.TicketInfo.WaitTextContains("Дом получателя = to_house");
            pageTicket.TicketInfo.WaitTextContains("Квартира получателя = to_flat");
        }
    }
}