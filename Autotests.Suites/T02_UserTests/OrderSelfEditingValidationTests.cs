using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class OrderSelfEditingValidationTests : ConstVariablesTestBase
    {
        [Test, Description("Создание черновика заказа, проверка валидации при редактировании")]
        public void OrderSelfEditingValidationTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.OrderNew.Click();
            userPage.OrderCreateSelf.Click();
            var orderCreateSelfPage = userPage.GoTo<OrderSelfCreatePage>();
            orderCreateSelfPage.Width.SetValueAndWait("4");
            orderCreateSelfPage.Height.SetValueAndWait("4");
            orderCreateSelfPage.Length.SetValueAndWait("4");
            orderCreateSelfPage.Weight.SetValueAndWait("4");
            orderCreateSelfPage.OrderNumber.SetValue("14");

            orderCreateSelfPage.BuyerName.SetValueAndWait("Фамилия Имя Отчество");
            orderCreateSelfPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateSelfPage.BuyerEmail.SetValueAndWait(userNameAndPass);
            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("4");
            orderCreateSelfPage.PaymentPrice.SetValueAndWait("4");
            orderCreateSelfPage.GoodsDescription.SetValueAndWait("4");

            var rowArticle = orderCreateSelfPage.GetArticleRow(0);
            rowArticle.Name.SetValueAndWait("Имя1");
            rowArticle.Article.SetValueAndWait("Article1");
            rowArticle.Count.SetValueAndWait("6");

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.MapOrders.SwitchToFrame();

            orderCreateSelfPage.MapOrders.City.SelectValueFirst("Москва");

            orderCreateSelfPage.MapOrders.GetMapCompanyRow(1).Name.Click();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(2).Name.Click();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(3).Name.Click();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(4).Name.Click();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(5).Name.Click();
            orderCreateSelfPage.MapOrders.ImageLocator.Click();
            orderCreateSelfPage.MapOrders.TakeHere.Click();
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();;

            orderCreateSelfPage.SaveDraftButton.Click();
            var orderPage = orderCreateSelfPage.GoTo<OrderPage>();

            orderPage.BackOrders.Click();
            var ordersPage = orderPage.GoTo<OrdersListPage>();

            ordersPage.Table.GetRow(0).Edit.Click();
            var orderSelfEditingPage = ordersPage.GoTo<OrderSelfEditingPage>();

            orderSelfEditingPage.Width.SetValue("");
            orderSelfEditingPage.Height.SetValue("");
            orderSelfEditingPage.Length.SetValue("");
            orderSelfEditingPage.Weight.SetValue("");

            orderSelfEditingPage.BuyerName.SetValue("");
            orderSelfEditingPage.BuyerPhone.SetValueAndWait(" ");
            orderSelfEditingPage.BuyerEmail.SetValue("");
            orderSelfEditingPage.DeclaredPrice.SetValue("");
            orderSelfEditingPage.PaymentPrice.SetValue("");

            orderSelfEditingPage.SaveChangeButton.ClickAndWaitTextError();
            orderSelfEditingPage.ErrorText[0].WaitText("ФИО получателя обязательно к заполнению");
            orderSelfEditingPage.ErrorText[1].WaitText("Телефон получателя обязательно к заполнению");
            orderSelfEditingPage.ErrorText[2].WaitText("Значение поля «Сторона 1» должно быть положительным числом");
            orderSelfEditingPage.ErrorText[3].WaitText("Значение поля «Сторона 2» должно быть положительным числом");
            orderSelfEditingPage.ErrorText[4].WaitText("Значение поля «Сторона 3» должно быть положительным числом");
            orderSelfEditingPage.ErrorText[5].WaitText("Значение поля «Вес» должно быть положительным числом");
            orderSelfEditingPage.ActionErrorText[0].WaitText("Ошибка просчета цены, или маршрут недоступен");

            orderSelfEditingPage.Width.SetValue("11.2");
            orderSelfEditingPage.Height.SetValue("12.2");
            orderSelfEditingPage.Length.SetValue("13.2");
            orderSelfEditingPage.Weight.SetValue("9.2");

            orderSelfEditingPage.BuyerName.SetValue("Фамилия2 Имя2 Очество2");
            orderSelfEditingPage.BuyerPhone.SetValue("2222222222");
            orderSelfEditingPage.SaveChangeButton.Click();
            orderPage = orderSelfEditingPage.GoTo<OrderPage>();
            orderPage.StatusOrder.WaitText("В обработке");
        }
    }
}