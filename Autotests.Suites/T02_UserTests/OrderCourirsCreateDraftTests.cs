using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class OrderCourirsCreateDraftTests : ConstVariablesTestBase
    {
        [Test, Description("Создание черновика заказа и проверка введенных значений")]
        public void OrderCreateCourirsCheckDraftTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.OrderNew.Click();
            userPage.OrderCreateCourirs.Click();
            var orderCreateCourirsPage = userPage.GoTo<OrderCourirsCreatePage>();
            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Москва");
            orderCreateCourirsPage.DeclaredPrice.SetValueAndWait("10.1");
            orderCreateCourirsPage.Width.SetValueAndWait("10.1");
            orderCreateCourirsPage.Height.SetValueAndWait("11.1");
            orderCreateCourirsPage.Length.SetValueAndWait("12.1");
            orderCreateCourirsPage.Weight.SetValueAndWait("9.1");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();

            orderCreateCourirsPage.DeliveryList[0].WaitTextContains("test_via, цена: 41.00 руб");
            orderCreateCourirsPage.DeliveryList[0].Click();

            orderCreateCourirsPage.BuyerPostalCode.SetValueAndWait("123123");
            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");
            orderCreateCourirsPage.BuyerName.SetValueAndWait("Фамилия Имя Очество");
            orderCreateCourirsPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateCourirsPage.BuyerEmail.SetValueAndWait(userNameAndPass);

            orderCreateCourirsPage.PaymentPrice.WaitValue("0");
            orderCreateCourirsPage.OrderNumber.SetValueAndWait("OrderNumber");
            orderCreateCourirsPage.GoodsDescription.SetValueAndWait("Хороший товар,годный");

            var rowArticle = orderCreateCourirsPage.GetArticleRow(0);
                rowArticle.Name.SetValueAndWait("Имя1");
                rowArticle.Article.SetValueAndWait("Article1");
                rowArticle.Count.SetValueAndWait("6");

            orderCreateCourirsPage.SaveDraftButton.Click();
            var orderCourirsPage = orderCreateCourirsPage.GoTo<OrderPage>();

            orderCourirsPage.TableSender.City.WaitText("Москва");
            orderCourirsPage.TableSender.Address.WaitText("ул.Улица, дом Дом, офис(квартира) Квартира");
            orderCourirsPage.TableSender.Name.WaitText(legalEntityName);
            orderCourirsPage.TableSender.Phone.WaitText("+7 (111)111-1111");
            orderCourirsPage.TableSender.Delivery.WaitText("Курьерская");

            orderCourirsPage.TableRecipient.City.WaitText("Москва");
            orderCourirsPage.TableRecipient.Address.WaitText("ул.Улица, дом Дом, офис(квартира) Квартира");
            orderCourirsPage.TableRecipient.Name.WaitText("Фамилия Имя Очество");
            orderCourirsPage.TableRecipient.Email.WaitText(userNameAndPass);
            orderCourirsPage.TableRecipient.Phone.WaitText("+7 (111)111-1111");
            orderCourirsPage.TableRecipient.Issue.WaitText("Ручная");
            orderCourirsPage.TableRecipient.PickupCompany.WaitText("FSD забор");
            orderCourirsPage.TableRecipient.DeliveryCompany.WaitText(сompanyName);

            orderCourirsPage.TablePrice.PaymentPrice.WaitText("0.00 руб.");
            orderCourirsPage.TablePrice.DeclaredPrice.WaitText("10.10 руб.");
            orderCourirsPage.TablePrice.Insurance.WaitText("0.00 руб.");
            orderCourirsPage.TablePrice.DeliveryPrice.WaitText("41.00 руб.");
            orderCourirsPage.TablePrice.PickupPrice.WaitText("200.00 руб.");

            orderCourirsPage.TableSize.Width.WaitText("10 см");
            orderCourirsPage.TableSize.Height.WaitText("11 см");
            orderCourirsPage.TableSize.Length.WaitText("12 см");
            orderCourirsPage.TableSize.Weight.WaitText("9.10 кг");

            orderCourirsPage.TableArticle.GetRow(0).Name.WaitText("Имя1");
            orderCourirsPage.TableArticle.GetRow(0).Article.WaitText("Article1");
            orderCourirsPage.TableArticle.GetRow(0).Count.WaitText("6");
        }

        [Test, Description("Проверка изменения статусов")]
        public void OrderCreateCourirsCheckStatusTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.OrderNew.Click();
            userPage.OrderCreateCourirs.Click();
            var orderCreateCourirsPage = userPage.GoTo<OrderCourirsCreatePage>();
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
            orderCreateCourirsPage.SaveDraftButton.Click();
            var orderCourirsPage = orderCreateCourirsPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("В обработке");

            orderCourirsPage.BackOrders.Click();
            var ordersPage = orderCourirsPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Status.WaitText("В обработке");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Подтвердить");
            ordersPage.Table.GetRow(0).ID.Click();
            orderCourirsPage = ordersPage.GoTo<OrderPage>();

            orderCourirsPage.СonfirmButton.Click();
            orderCourirsPage = orderCourirsPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("Подтверждена");

            orderCourirsPage.BackOrders.Click();
            ordersPage = orderCourirsPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Status.WaitText("Подтверждена");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Отменить");
            ordersPage.Table.GetRow(0).ID.Click();
            orderCourirsPage = ordersPage.GoTo<OrderPage>();

            orderCourirsPage.UndoButton.Click();
            orderCourirsPage = orderCourirsPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("В обработке");

            orderCourirsPage.BackOrders.Click();
            ordersPage = orderCourirsPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Status.WaitText("В обработке");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Подтвердить");
        }

        [Test, Description("Проверка работы кнопки пересчета и работы пересчета индекса")]
        public void OrderCreateCourirsCheckСountedAndIndexTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.OrderNew.Click();
            userPage.OrderCreateCourirs.Click();
            var orderCreateCourirsPage = userPage.GoTo<OrderCourirsCreatePage>();
            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Москва");
            orderCreateCourirsPage.DeclaredPrice.SetValueAndWait("10.1");
            orderCreateCourirsPage.Width.SetValueAndWait("10.1");
            orderCreateCourirsPage.Height.SetValueAndWait("11.1");
            orderCreateCourirsPage.Length.SetValueAndWait("12.1");

            orderCreateCourirsPage.Weight.SetValueAndWait("3.0");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains("test_via, цена: 20.00 руб");
            orderCreateCourirsPage.BuyerPostalCode.WaitValue("101000");

            orderCreateCourirsPage.Weight.SetValueAndWait("9.1");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains("test_via, цена: 41.00 руб");
            orderCreateCourirsPage.BuyerPostalCode.WaitValue("101000");

            orderCreateCourirsPage.Weight.SetValueAndWait("20.1");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextNotContains(сompanyName);
            
            orderCreateCourirsPage.BuyerPostalCode.WaitValue("101000");

            orderCreateCourirsPage.Weight.SetValueAndWait("10");
            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Санкт-Петербург");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextNotContains(сompanyName);
            orderCreateCourirsPage.BuyerPostalCode.WaitValue("190000");

            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Екатеринбург");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextNotContains(сompanyName);
            orderCreateCourirsPage.BuyerPostalCode.WaitValue("620000");

            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Питеримка");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextNotContains(сompanyName);
            orderCreateCourirsPage.BuyerPostalCode.WaitValue("162035");
        }
    }
}