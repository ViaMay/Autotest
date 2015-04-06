using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class OrderCourirsCreateSendTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заяки и отправление")]
        public void OrderCreateCourirsAndSendTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.Table.FindRowByName(userShopName).OrdersCreateCourier.Click();
            var orderCreateCourirsPage = shopsListPage.GoTo<OrderCourirsCreatePage>();
            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Москва");
            orderCreateCourirsPage.DeclaredPrice.SetValueAndWait("10.1");
            orderCreateCourirsPage.Width.SetValueAndWait("10.1");
            orderCreateCourirsPage.Height.SetValueAndWait("11.1");
            orderCreateCourirsPage.Length.SetValueAndWait("12.1");
            orderCreateCourirsPage.Weight.SetValueAndWait("9.1");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();

            orderCreateCourirsPage.BuyerPostalCode.SetValueAndWait("123123");
            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");
            orderCreateCourirsPage.BuyerName.SetValueAndWait("Фамилия Имя Очество");
            orderCreateCourirsPage.BuyerPhone.SetValueAndWait("1111111111");

            orderCreateCourirsPage.GoodsDescription.SetValueAndWait("ok");
            orderCreateCourirsPage.SendOrderButton.Click();
            var orderCourirsPage = orderCreateCourirsPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("Подтверждена");
            orderCourirsPage.BackOrders.Click();
            var ordersPage = orderCourirsPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Status.WaitText("Подтверждена");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Отменить");
        }
    }
}