using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class OrderSelfCreateSendTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заяки и отправление")]
        public void OrderCreateAndSendTest()
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
            orderCreateSelfPage.PaymentPrice.SetValueAndWait("0");
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
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();

            orderCreateSelfPage.PointDeliveryName.WaitText("Пункт выдачи: " + deliveryPointName);
            orderCreateSelfPage.PointDeliveryAddress.WaitText("Адрес: " + deliveryPointAddress);
            orderCreateSelfPage.PointDeliveryCompany.WaitText("Компания: " + companyName);
            orderCreateSelfPage.PointDeliveryPrice.WaitText("Цена: 28");

            orderCreateSelfPage.SendOrderButton.Click();
            var orderCourirsPage = orderCreateSelfPage.GoTo<OrderPage>();
            
            orderCourirsPage.StatusOrder.WaitText("Подтверждена");
            orderCourirsPage.BackOrders.Click();
            var ordersPage = orderCourirsPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Status.WaitText("Подтверждена");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Отменить");
        }
    }
}