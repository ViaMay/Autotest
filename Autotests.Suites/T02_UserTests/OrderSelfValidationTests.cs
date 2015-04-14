using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class OrderSelfValidationTests : ConstVariablesTestBase
    {
        [Test, Description("Проверка валидаций перерасчета")]
        public void TestAlertValidationСounted()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.Table.FindRowByName(userShopName).OrdersCreateSelf.Click();
            var orderCreateSelfPage = shopsListPage.GoTo<OrderSelfCreatePage>();

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.AletrError.WaitText("Сначала нужно указать размеры, вес, оценочную стоимость и наложенный платеж");
            orderCreateSelfPage.AletrError.Сancel();

            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("10.1");
            orderCreateSelfPage.Width.SetValueAndWait("10.1");
            orderCreateSelfPage.Height.SetValueAndWait("11.1");
            orderCreateSelfPage.Length.SetValueAndWait("20.1");
            orderCreateSelfPage.Weight.SetValueAndWait("9.1");

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.AletrError.WaitText("Сначала нужно указать размеры, вес, оценочную стоимость и наложенный платеж");
            orderCreateSelfPage.AletrError.Accept();

            orderCreateSelfPage.PaymentPrice.SetValueAndWait("0");
            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("");

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.AletrError.WaitText("Сначала нужно указать размеры, вес, оценочную стоимость и наложенный платеж");
            orderCreateSelfPage.AletrError.Accept();

            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("100");
            orderCreateSelfPage.Weight.SetValueAndWait("");

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.AletrError.WaitText("Сначала нужно указать размеры, вес, оценочную стоимость и наложенный платеж");
            orderCreateSelfPage.AletrError.Accept();

            orderCreateSelfPage.Height.SetValueAndWait("");
            orderCreateSelfPage.Length.SetValueAndWait("");
            orderCreateSelfPage.Weight.SetValueAndWait("");

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.AletrError.WaitText("Сначала нужно указать размеры, вес, оценочную стоимость и наложенный платеж");
            orderCreateSelfPage.AletrError.Accept();

            orderCreateSelfPage.Height.SetValueAndWait("0");
            orderCreateSelfPage.Length.SetValueAndWait("0");
            orderCreateSelfPage.Weight.SetValueAndWait("0");
            orderCreateSelfPage.СountedButton.Click();
 }

        [Test, Description("Проверка валидации при отправке")]
        public void TestValidationSend()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.Table.FindRowByName(userShopName).OrdersCreateSelf.Click();
            var orderCreateSelfPage = shopsListPage.GoTo<OrderSelfCreatePage>();

            orderCreateSelfPage.SendOrderButton.ClickAndWaitTextError(1);
            orderCreateSelfPage.ErrorText[0].WaitText("ФИО получателя обязательно к заполнению");
            orderCreateSelfPage.ErrorText[1].WaitText("Телефон получателя обязательно к заполнению");
            orderCreateSelfPage.ErrorText[2].WaitText("Описание посылки обязательно к заполнению");
            orderCreateSelfPage.ErrorText[3].WaitText("Сторона 1 обязательно к заполнению");
            orderCreateSelfPage.ErrorText[4].WaitText("Сторона 2 обязательно к заполнению");
            orderCreateSelfPage.ErrorText[5].WaitText("Сторона 3 обязательно к заполнению");
            orderCreateSelfPage.ErrorText[6].WaitText("Вес обязательно к заполнению");

            orderCreateSelfPage.ActionErrorText[0].WaitText("Ошибка просчета цены, или маршрут недоступен");
            orderCreateSelfPage.ActionErrorText[1].WaitText("Компания доставки обязательно к заполнению");
            orderCreateSelfPage.ActionErrorText[2].WaitText("Не выбран пункт выдачи");
            orderCreateSelfPage.ActionErrorText[3].WaitText("Город получения обязательно к заполнению");

            orderCreateSelfPage.BuyerName.SetValueAndWait("Фамилия Имя Очество");
            orderCreateSelfPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateSelfPage.GoodsDescription.SetValueAndWait("ok");
            orderCreateSelfPage.Width.SetValueAndWait("4");
            orderCreateSelfPage.Height.SetValueAndWait("4");
            orderCreateSelfPage.Length.SetValueAndWait("4");
            orderCreateSelfPage.Weight.SetValueAndWait("4");

            orderCreateSelfPage.SendOrderButton.ClickAndWaitTextError();
            orderCreateSelfPage.ActionErrorText[0].WaitText("Ошибка просчета цены, или маршрут недоступен");
            orderCreateSelfPage.ActionErrorText[1].WaitText("Компания доставки обязательно к заполнению");
            orderCreateSelfPage.ActionErrorText[2].WaitText("Не выбран пункт выдачи");
            orderCreateSelfPage.ActionErrorText[3].WaitText("Город получения обязательно к заполнению");
            orderCreateSelfPage.ErrorText[0].WaitText("Внимание! Калькулятор произвел расчет по параметрам, не учитывающим кол-во мест в заказе");

            orderCreateSelfPage.MapOrders.SwitchToFrame();
            orderCreateSelfPage.MapOrders.City.SelectValueFirst("Москва");
            orderCreateSelfPage.MapOrders.ImageLocator.Click();
            orderCreateSelfPage.MapOrders.TakeHere.Click();
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();

            orderCreateSelfPage.SendOrderButton.Click();
            var orderCourirsPage = orderCreateSelfPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("Подтверждена");
        }
    }
}