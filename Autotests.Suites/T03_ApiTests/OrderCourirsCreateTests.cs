using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class OrderCourirsCreateTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заказа курьерской")]
        public void OrderCourirsTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            string keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var deliveryCompaniesPage =
                LoadPage<СompaniesPage>("/admin/companies/?&filters[name]=" + companyName);
            string deliveryCompanyId = deliveryCompaniesPage.Table.GetRow(0).ID.GetText();

           var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST(keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
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
		        {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"},
		        {"items_count", "1"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var defaultPage = shopsPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Orders.Click();
            var ordersPage = userPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).ID.WaitText(responseCreateOrders.Response.OrderId);
            ordersPage.Table.GetRow(0).Type.WaitText("Курьерская");
            ordersPage.Table.GetRow(0).Number.WaitText(userShopName);
            ordersPage.Table.GetRow(0).Status.WaitText("В обработке");
            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Подтвердить");
            ordersPage.Table.GetRow(0).Edit.WaitText("Редактировать");

            ordersPage.Table.GetRow(0).Edit.Click();
            var orderCourirsEditingPage = ordersPage.GoTo<OrderCourirsEditingPage>();

            orderCourirsEditingPage.DeclaredPrice.WaitValue("100");
            orderCourirsEditingPage.Width.WaitValue("4");
            orderCourirsEditingPage.Height.WaitValue("4");
            orderCourirsEditingPage.Length.WaitValue("4");
            orderCourirsEditingPage.Weight.WaitValue("4");

            orderCourirsEditingPage.BuyerStreet.WaitValue("Барна");
            orderCourirsEditingPage.BuyerHouse.WaitValue("3a");
            orderCourirsEditingPage.BuyerFlat.WaitValue("12");
            orderCourirsEditingPage.BuyerName.WaitValue("Ургудан Рабат Мантов");
            orderCourirsEditingPage.BuyerPhone.WaitValue("+7 (999)999-9999");
            orderCourirsEditingPage.BuyerEmail.WaitValue(userNameAndPass);

            orderCourirsEditingPage.PaymentPrice.WaitValue("300");
            orderCourirsEditingPage.OrderNumber.WaitValue("test_userShops_via");
            orderCourirsEditingPage.GoodsDescription.WaitValue("Памперс");
            orderCourirsEditingPage.ItemsCount.WaitValue("1");
        }
    }
}