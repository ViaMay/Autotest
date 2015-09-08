using Autotests.WebPages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
{
    public class OrderCourirsEditingValidationTests : ConstVariablesTestBase
    {
        [Test, Description("Создание черновика заказа, проверка валидации при редактировании")]

        public void OrderCourirsEditingValidationTest()
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

            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();

            orderCreateCourirsPage.DeliveryList[0].WaitTextContains("test_via, цена: 53.00 руб");
            orderCreateCourirsPage.DeliveryList[0].Click();

            orderCreateCourirsPage.BuyerPostalCode.SetValueAndWait("123123");
            orderCreateCourirsPage.BuyerName.SetValueAndWait("Фамилия Имя Очество");
            orderCreateCourirsPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateCourirsPage.BuyerEmail.SetValueAndWait(userNameAndPass);

            orderCreateCourirsPage.PaymentPrice.SetValueAndWait("1500");
            orderCreateCourirsPage.OrderNumber.SetValueAndWait("OrderNumber");
            orderCreateCourirsPage.GoodsDescription.SetValueAndWait("Хороший товар,годный");

            var rowArticle = orderCreateCourirsPage.GetArticleRow(0);
            rowArticle.Name.SetValueAndWait("Имя1");
            rowArticle.Article.SetValueAndWait("Article1");
            rowArticle.Count.SetValueAndWait("6");

            orderCreateCourirsPage.SaveDraftButton.Click();
            var orderCourirsPage = orderCreateCourirsPage.GoTo<OrderPage>();
            orderCourirsPage.BackOrders.Click();
            var ordersPage = orderCourirsPage.GoTo<OrdersListPage>();

            ordersPage.Table.GetRow(0).Edit.Click();
            var orderCourirsEditingPage = ordersPage.GoTo<OrderCourirsEditingPage>();

            orderCourirsEditingPage.DeclaredPrice.SetValue("");
            orderCourirsEditingPage.Width.SetValue("");
            orderCourirsEditingPage.Height.SetValue("");
            orderCourirsEditingPage.Length.SetValue("");
            orderCourirsEditingPage.Weight.SetValue("");

            orderCourirsEditingPage.BuyerName.SetValue("");
            orderCourirsEditingPage.BuyerPhone.SetValue(" ");
            orderCourirsEditingPage.BuyerEmail.SetValue("");
            orderCourirsEditingPage.PaymentPrice.SetValue("");
            orderCourirsEditingPage.BuyerPhoneAdd.SetValueAndWait("+74444444444, +75555555555,  +75555555555");
            orderCourirsEditingPage.BuyerEmail.SetValueAndWait("+7");

            orderCourirsEditingPage.SaveChangeButton.ClickAndWaitTextError(3);
            orderCourirsEditingPage.ErrorText[0].WaitText("Значение поля «Сторона 1» должно быть положительным числом");
            orderCourirsEditingPage.ErrorText[1].WaitText("Значение поля «Сторона 2» должно быть положительным числом");
            orderCourirsEditingPage.ErrorText[2].WaitText("Значение поля «Сторона 3» должно быть положительным числом");
            orderCourirsEditingPage.ErrorText[3].WaitText("Значение поля «Вес» должно быть положительным числом");
            orderCourirsEditingPage.ErrorText[4].WaitText("ФИО получателя обязательно к заполнению");
            orderCourirsEditingPage.ErrorText[5].WaitText("Телефон получателя обязательно к заполнению");
            orderCourirsEditingPage.ErrorText[6].WaitText("Длина поля «Дополнительный телефон получателя» должна быть не более 30 символов");
            orderCourirsEditingPage.ErrorText[7].WaitText("Email должно быть корректным адресом электронной почты");
            orderCourirsEditingPage.ActionErrorText[0].WaitText("Ошибка просчета цены, или маршрут недоступен");

            orderCourirsEditingPage.Width.SetValue("11.2");
            orderCourirsEditingPage.Height.SetValue("12.2");
            orderCourirsEditingPage.Length.SetValue("13.2");
            orderCourirsEditingPage.Weight.SetValue("9.2");
            orderCourirsEditingPage.DeclaredPrice.SetValueAndWait("10.1");
            
            orderCourirsEditingPage.BuyerName.SetValue("Фамилия2 Имя2 Очество2");
            orderCourirsEditingPage.BuyerPhone.SetValue("2222222222");
            orderCourirsEditingPage.BuyerPhoneAdd.SetValueAndWait("");
            orderCourirsEditingPage.BuyerEmail.SetValueAndWait("");

            orderCourirsEditingPage.CityTo.SetFirstValueSelect("Санкт-Петербург");
            orderCourirsEditingPage.СountedButton.Click();
            orderCourirsEditingPage.WaitCounted();

            orderCourirsEditingPage.BuyerStreet.SetValueAndWait("");
            orderCourirsEditingPage.BuyerHouse.SetValueAndWait("");
            orderCourirsEditingPage.BuyerFlat.SetValueAndWait("");
            orderCourirsEditingPage.SaveChangeButton.ClickAndWaitTextError(6);
            orderCourirsEditingPage.ErrorText[0].WaitText("Улица получателя обязательно к заполнению");
            orderCourirsEditingPage.ErrorText[1].WaitText("Дом получателя обязательно к заполнению");
            orderCourirsEditingPage.ErrorText[2].WaitText("Квартира получателя обязательно к заполнению");

            orderCourirsEditingPage.BuyerStreet.SetValueAndWait("Улица");
            orderCourirsEditingPage.BuyerHouse.SetValueAndWait("Дом");
            orderCourirsEditingPage.BuyerFlat.SetValueAndWait("Квартира");
            orderCourirsEditingPage.SaveChangeButton.Click();
            orderCourirsPage = orderCourirsEditingPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("В обработке");
        }
    }
}