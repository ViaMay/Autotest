using Autotests.WebPages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
{
    public class OrderCourirsValidationTests : ConstVariablesTestBase
    {
        [Test, Description("Проверка валидаций в параметры заявки до перерасчета")]
        public void TestAlertValidationСounted()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.Table.FindRowByName(userShopName).OrdersCreateCourier.Click();
            var orderCreateCourirsPage = shopsListPage.GoTo<OrderCourirsCreatePage>();

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.Aletr.WaitText("Сначала нужно указать размеры, вес, оценочную стоимость и выбрать город");
            orderCreateCourirsPage.Aletr.Сancel();

            orderCreateCourirsPage.DeclaredPrice.SetValueAndWait("10.1");
            orderCreateCourirsPage.Width.SetValueAndWait("10.1");
            orderCreateCourirsPage.Height.SetValueAndWait("11.1");
            orderCreateCourirsPage.Length.SetValueAndWait("20.1");
            orderCreateCourirsPage.Weight.SetValueAndWait("9.1");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.Aletr.WaitText("Сначала нужно указать размеры, вес, оценочную стоимость и выбрать город");
            orderCreateCourirsPage.Aletr.Accept();

            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Москва");
            orderCreateCourirsPage.DeclaredPrice.SetValueAndWait(" ");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.Aletr.WaitText("Сначала нужно указать размеры, вес, оценочную стоимость и выбрать город");
            orderCreateCourirsPage.Aletr.Accept();

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.Aletr.WaitText("Сначала нужно указать размеры, вес, оценочную стоимость и выбрать город");
            orderCreateCourirsPage.Aletr.Accept();

            orderCreateCourirsPage.DeclaredPrice.SetValueAndWait("100");
            orderCreateCourirsPage.Weight.SetValueAndWait(" ");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.Aletr.WaitText("Сначала нужно указать размеры, вес, оценочную стоимость и выбрать город");
            orderCreateCourirsPage.Aletr.Accept();

            orderCreateCourirsPage.Height.SetValueAndWait(" ");
            orderCreateCourirsPage.Length.SetValueAndWait(" ");
            orderCreateCourirsPage.Weight.SetValueAndWait(" ");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.Aletr.WaitText("Сначала нужно указать размеры, вес, оценочную стоимость и выбрать город");
            orderCreateCourirsPage.Aletr.Accept();

            orderCreateCourirsPage.Height.SetValueAndWait("0");
            orderCreateCourirsPage.Length.SetValueAndWait("0");
            orderCreateCourirsPage.Weight.SetValueAndWait("0");
            orderCreateCourirsPage.СountedButton.Click();

            orderCreateCourirsPage.Aletr.WaitText("Необходимо заполнить все поля из блока Дополнительная информация");
            orderCreateCourirsPage.Aletr.Accept();

            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");
            orderCreateCourirsPage.СountedButton.Click();

            orderCreateCourirsPage.Aletr.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            orderCreateCourirsPage.Aletr.Accept();

            orderCreateCourirsPage.WaitTextRadioButtonError("Отсутствуют варианты доставки, соответствующие указанным параметрам заказа");
            orderCreateCourirsPage.Height.SetValueAndWait("10");
            orderCreateCourirsPage.Length.SetValueAndWait("10");
            orderCreateCourirsPage.Weight.SetValueAndWait("10");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitVisible();    
        }

        [Test, Description("Проверка валидации у Buyer")]
        public void TestValidationBuyer()
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
            orderCreateCourirsPage.Length.SetValueAndWait("20.1");
            orderCreateCourirsPage.Weight.SetValueAndWait("9.1");

            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();

            orderCreateCourirsPage.DeliveryList[0].WaitVisible();

            orderCreateCourirsPage.BuyerPhoneAdd.SetValueAndWait("+74444444444, +75555555555,  +75555555555");
            orderCreateCourirsPage.BuyerEmail.SetValueAndWait("+7");

            orderCreateCourirsPage.SendOrderButton.ClickAndWaitTextError(3);
            orderCreateCourirsPage.ErrorText[0].WaitText("ФИО получателя обязательно к заполнению");
            orderCreateCourirsPage.ErrorText[1].WaitText("Телефон получателя обязательно к заполнению");
            orderCreateCourirsPage.ErrorText[2].WaitText("Длина поля «Дополнительный телефон получателя» должна быть не более 30 символов");
            orderCreateCourirsPage.ErrorText[3].WaitText("Email должно быть корректным адресом электронной почты"); 
            orderCreateCourirsPage.ErrorText[4].WaitTextContains("Максимальное количество мест");
            orderCreateCourirsPage.ErrorText[5].WaitText("( Если по вашим оценкам ваше отправление превышает 0,5 м3)");
            orderCreateCourirsPage.ErrorText[6].WaitText("Внимание! Калькулятор произвел расчет по параметрам, не учитывающим кол-во мест в заказе");
            orderCreateCourirsPage.ErrorText[7].WaitText("Описание посылки обязательно к заполнению");
            
            orderCreateCourirsPage.BuyerPostalCode.SetValueAndWait("123123");
            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");
            orderCreateCourirsPage.BuyerName.SetValueAndWait("Фамилия Имя Очество");
            orderCreateCourirsPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateCourirsPage.GoodsDescription.SetValueAndWait("ok");
            orderCreateCourirsPage.BuyerPhoneAdd.SetValueAndWait("");
            orderCreateCourirsPage.BuyerEmail.SetValueAndWait("");

            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Санкт-Петербург");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitVisible();

            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("");
            orderCreateCourirsPage.SendOrderButton.ClickAndWaitTextError(3);
            orderCreateCourirsPage.ErrorText[0].WaitText("Улица получателя обязательно к заполнению");
            orderCreateCourirsPage.ErrorText[1].WaitText("Дом получателя обязательно к заполнению");
            orderCreateCourirsPage.ErrorText[2].WaitText("Квартира получателя обязательно к заполнению");
            
            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");
            orderCreateCourirsPage.SendOrderButton.Click();
            var orderCourirsPage = orderCreateCourirsPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("Подтверждена");
        }

        [Test, Description("Проверка валидации маршрута после появления полей для Buyer-a")]
        public void TestValidationRouter()
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
            orderCreateCourirsPage.Length.SetValueAndWait("20.1");
            orderCreateCourirsPage.Weight.SetValueAndWait("9.1");

            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitVisible();

            orderCreateCourirsPage.CityToConbobox.Remove.Click();

            orderCreateCourirsPage.BuyerPostalCode.SetValueAndWait("123123");
            orderCreateCourirsPage.BuyerName.SetValueAndWait("Фамилия Имя Очество");
            orderCreateCourirsPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateCourirsPage.GoodsDescription.SetValueAndWait("ok");

            orderCreateCourirsPage.SendOrderButton.ClickAndWaitTextError(1);
            orderCreateCourirsPage.ActionErrorText[0].WaitText("Ошибка просчета цены, или маршрут недоступен");
            orderCreateCourirsPage.ActionErrorText[1].WaitText("Город получения обязательно к заполнению");

            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Москва");
            orderCreateCourirsPage.Weight.SetValueAndWait("0");
            orderCreateCourirsPage.SendOrderButton.ClickAndWaitTextError();
            orderCreateCourirsPage.ActionErrorText[0].WaitText("Ошибка просчета цены, или маршрут недоступен");

            orderCreateCourirsPage.Weight.SetValueAndWait("10");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();

            orderCreateCourirsPage.SendOrderButton.Click();
            var orderCourirsPage = orderCreateCourirsPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("Подтверждена");
        }
    }
}