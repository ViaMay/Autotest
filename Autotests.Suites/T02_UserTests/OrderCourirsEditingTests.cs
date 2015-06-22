using System.Globalization;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class OrderCourirsEditingTests : ConstVariablesTestBase
    {
        [Test, Description("Создание черновика заказа а потом редактирование")]
        public void OrderCourirsDraftEditingTest()
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

            orderCreateCourirsPage.DeliveryList[0].WaitTextContains("test_via, цена: 53.00 руб");
            orderCreateCourirsPage.DeliveryList[0].Click();

            orderCreateCourirsPage.BuyerPostalCode.SetValueAndWait("123123");
            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");
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
            orderCourirsPage.StatusOrder.WaitText("В обработке");
            orderCourirsPage.BackOrders.Click();
            var ordersPage = orderCourirsPage.GoTo<OrdersListPage>();

            ordersPage.Table.GetRow(0).Number.WaitText("OrderNumber");
            ordersPage.Table.GetRow(0).Goods.WaitText("Хороший товар,годный");
            ordersPage.Table.GetRow(0).Edit.Click();
            var orderCourirsEditingPage = ordersPage.GoTo<OrderCourirsEditingPage>();

            orderCourirsEditingPage.City.WaitValue("Москва");
            orderCourirsEditingPage.DeclaredPrice.WaitValue("10.1");
            orderCourirsEditingPage.Width.WaitValue("10.1");
            orderCourirsEditingPage.Height.WaitValue("11.1");
            orderCourirsEditingPage.Length.WaitValue("12.1");
            orderCourirsEditingPage.Weight.WaitValue("9.1");
            orderCourirsEditingPage.ItemsCount.WaitValue("1");

            orderCourirsEditingPage.BuyerPostalCode.WaitValue("123123");
            orderCourirsEditingPage.BuyerStreet.WaitValue("Улица");
            orderCourirsEditingPage.BuyerHouse.WaitValue("Дом");
            orderCourirsEditingPage.BuyerFlat.WaitValue("Квартира");
            orderCourirsEditingPage.BuyerName.WaitValue("Фамилия Имя Очество");
            orderCourirsEditingPage.BuyerPhone.WaitValue("+7 (111)111-1111");
            orderCourirsEditingPage.BuyerEmail.WaitValue(userNameAndPass);

            orderCourirsEditingPage.PaymentPrice.WaitValue("1500");
            orderCourirsEditingPage.OrderNumber.WaitValue("OrderNumber");
            orderCourirsEditingPage.GoodsDescription.WaitValue("Хороший товар,годный");

            var rowArticleStatic = orderCourirsEditingPage.GetArticleRow(0);
            rowArticleStatic.Name.WaitValue("Имя1");
            rowArticleStatic.Article.WaitValue("Article1");
            rowArticleStatic.Count.WaitValue("6");

            orderCourirsEditingPage.DeclaredPrice.SetValue("10.2");
            orderCourirsEditingPage.Width.SetValue("11.2");
            orderCourirsEditingPage.Height.SetValue("12.2");
            orderCourirsEditingPage.Length.SetValue("13.2");
            orderCourirsEditingPage.Weight.SetValue("9.2");

            orderCourirsEditingPage.BuyerPostalCode.SetValue("123456");
            orderCourirsEditingPage.BuyerStreet.SetValue("Улица2");
            orderCourirsEditingPage.BuyerHouse.SetValue("Дом2");
            orderCourirsEditingPage.BuyerFlat.SetValue("Квартира2");
            orderCourirsEditingPage.BuyerName.SetValue("Фамилия2 Имя2 Очество2");
            orderCourirsEditingPage.BuyerPhone.SetValue("2222222222");
            orderCourirsEditingPage.BuyerEmail.SetValue("2"+ userNameAndPass);
            orderCourirsEditingPage.PaymentPrice.SetValue("1600");
            orderCourirsEditingPage.OrderNumber.SetValue("OrderNumber2");
            orderCourirsEditingPage.GoodsDescription.SetValue("Хороший товар,годный2");

            orderCourirsEditingPage.SaveChangeButton.Click();
            orderCourirsPage = orderCreateCourirsPage.GoTo<OrderPage>();

            orderCourirsPage.TableSender.City.WaitText("Москва");
            orderCourirsPage.TableSender.Address.WaitText("ул.Улица, дом Дом, офис(квартира) Квартира");
            orderCourirsPage.TableSender.Name.WaitText(legalEntityName);
            orderCourirsPage.TableSender.Phone.WaitText("+7 (111)111-1111");
            orderCourirsPage.TableSender.Delivery.WaitText("Курьерская");

            orderCourirsPage.TableRecipient.City.WaitText("Москва");
            orderCourirsPage.TableRecipient.PostCode.WaitText("123456");
            orderCourirsPage.TableRecipient.Address.WaitText("ул.Улица2, дом Дом2, офис(квартира) Квартира2");
            orderCourirsPage.TableRecipient.Name.WaitText("Фамилия2 Имя2 Очество2");
            orderCourirsPage.TableRecipient.Email.WaitText("2" + userNameAndPass);
            orderCourirsPage.TableRecipient.Phone.WaitText("+7 (222)222-2222");
            orderCourirsPage.TableRecipient.Issue.WaitText("Ручная");
            orderCourirsPage.TableRecipient.PickupCompany.WaitText(companyPickupName);
            orderCourirsPage.TableRecipient.DeliveryCompany.WaitText(companyName);

            orderCourirsPage.TablePrice.PaymentPrice.WaitText("1600.00 руб.");
            orderCourirsPage.TablePrice.DeclaredPrice.WaitText("10.20 руб.");
            orderCourirsPage.TablePrice.Insurance.WaitText("0.00 руб.");
            orderCourirsPage.TablePrice.DeliveryPrice.WaitText("53.00 руб.");
            orderCourirsPage.TablePrice.PickupPrice.WaitText("21.00 руб.");

            orderCourirsPage.TableSize.Width.WaitText("11 см");
            orderCourirsPage.TableSize.Height.WaitText("12 см");
            orderCourirsPage.TableSize.Length.WaitText("13 см");
            orderCourirsPage.TableSize.Weight.WaitText("9.20 кг");
            orderCourirsPage.TableSize.Count.WaitText("1");

            orderCourirsPage.TableArticle.GetRow(0).Name.WaitText("Имя1");
            orderCourirsPage.TableArticle.GetRow(0).Article.WaitText("Article1");
            orderCourirsPage.TableArticle.GetRow(0).Count.WaitText("6");

            orderCourirsPage.BackOrders.Click();
            ordersPage = orderCourirsPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Number.WaitText("OrderNumber2");
            ordersPage.Table.GetRow(0).Goods.WaitText("Хороший товар,годный 2");
        }

        [Test, Description("Отправка заказа, а потом редактирование")]
        public void OrderCourirsSendEditingTest()
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

            orderCreateCourirsPage.DeliveryList[0].WaitTextContains("test_via, цена: 53.00 руб");
            orderCreateCourirsPage.DeliveryList[0].Click();

            orderCreateCourirsPage.BuyerPostalCode.SetValueAndWait("123123");
            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");
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

            orderCreateCourirsPage.SendOrderButton.Click();
            var orderCourirsPage = orderCreateCourirsPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("Подтверждена");
            var dateDelivery = orderCourirsPage.DeliveryDate.GetText();
            orderCourirsPage.BackOrders.Click();
            var ordersPage = orderCourirsPage.GoTo<OrdersListPage>();

            ordersPage.Table.GetRow(0).Number.WaitText("OrderNumber");
            ordersPage.Table.GetRow(0).Goods.WaitText("Хороший товар,годный");
            
            ordersPage.Table.GetRow(0).Edit.Click();
            var orderCourirsEditingPage = ordersPage.GoTo<OrderCourirsEditingPage>();

            orderCourirsEditingPage.City.WaitValue("Москва");
            orderCourirsEditingPage.DeclaredPrice.WaitValue("10.1");
            orderCourirsEditingPage.Width.WaitValue("10.1");
            orderCourirsEditingPage.Height.WaitValue("11.1");
            orderCourirsEditingPage.Length.WaitValue("12.1");
            orderCourirsEditingPage.Weight.WaitValue("9.1");
            orderCourirsEditingPage.ItemsCount.WaitValue("1");

            orderCourirsEditingPage.BuyerPostalCode.WaitValue("123123");
            orderCourirsEditingPage.BuyerStreet.WaitValue("Улица");
            orderCourirsEditingPage.BuyerHouse.WaitValue("Дом");
            orderCourirsEditingPage.BuyerFlat.WaitValue("Квартира");
            orderCourirsEditingPage.BuyerName.WaitValue("Фамилия Имя Очество");
            orderCourirsEditingPage.BuyerPhone.WaitValue("+7 (111)111-1111");
            orderCourirsEditingPage.BuyerEmail.WaitValue(userNameAndPass);

            orderCourirsEditingPage.PaymentPrice.WaitValue("1500");
            orderCourirsEditingPage.OrderNumber.WaitValue("OrderNumber");
            orderCourirsEditingPage.GoodsDescription.WaitValue("Хороший товар,годный");
            orderCourirsEditingPage.DeliveryDate.WaitValue(dateDelivery);

            var rowArticleStatic = orderCourirsEditingPage.GetArticleRow(0);
            rowArticleStatic.Name.WaitValue("Имя1");
            rowArticleStatic.Article.WaitValue("Article1");
            rowArticleStatic.Count.WaitValue("6");

            orderCourirsEditingPage.DeclaredPrice.SetValue("10.2");
            orderCourirsEditingPage.Width.SetValue("11.2");
            orderCourirsEditingPage.Height.SetValue("12.2");
            orderCourirsEditingPage.Length.SetValue("13.2");
            orderCourirsEditingPage.Weight.SetValue("9.2");

            orderCourirsEditingPage.BuyerPostalCode.SetValue("123456");
            orderCourirsEditingPage.BuyerStreet.SetValue("Улица2");
            orderCourirsEditingPage.BuyerHouse.SetValue("Дом2");
            orderCourirsEditingPage.BuyerFlat.SetValue("Квартира2");
            orderCourirsEditingPage.BuyerName.SetValue("Фамилия2 Имя2 Очество2");
            orderCourirsEditingPage.BuyerPhone.SetValue("2222222222");
            orderCourirsEditingPage.BuyerEmail.SetValue("2" + userNameAndPass);

            orderCourirsEditingPage.PaymentPrice.SetValue("1600");
            orderCourirsEditingPage.OrderNumber.SetValue("OrderNumber2");
//            orderCourirsEditingPage.DeliveryDate.SetValueAndWait(nowDate.AddDays(5).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture));
            orderCourirsEditingPage.GoodsDescription.SetValue("Хороший товар,годный2");

            orderCourirsEditingPage.SaveChangeButton.Click();
            orderCourirsPage = orderCreateCourirsPage.GoTo<OrderPage>();

            orderCourirsPage.TableSender.City.WaitText("Москва");
            orderCourirsPage.TableSender.Address.WaitText("ул.Улица, дом Дом, офис(квартира) Квартира");
            orderCourirsPage.TableSender.Name.WaitText(legalEntityName);
            orderCourirsPage.TableSender.Phone.WaitText("+7 (111)111-1111");
            orderCourirsPage.TableSender.Delivery.WaitText("Курьерская");

            orderCourirsPage.TableRecipient.City.WaitText("Москва");
            orderCourirsPage.TableRecipient.PostCode.WaitText("123456");
            orderCourirsPage.TableRecipient.Address.WaitText("ул.Улица2, дом Дом2, офис(квартира) Квартира2");
            orderCourirsPage.TableRecipient.Name.WaitText("Фамилия2 Имя2 Очество2");
            orderCourirsPage.TableRecipient.Email.WaitText("2" + userNameAndPass);
            orderCourirsPage.TableRecipient.Phone.WaitText("+7 (222)222-2222");
            orderCourirsPage.TableRecipient.Issue.WaitText("Ручная");
            orderCourirsPage.TableRecipient.PickupCompany.WaitText(companyPickupName);
            orderCourirsPage.TableRecipient.DeliveryCompany.WaitText(companyName);

//            orderCourirsPage.DeliveryDate.WaitText(nowDate.AddDays(5).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture));

            orderCourirsPage.TablePrice.PaymentPrice.WaitText("1600.00 руб.");
            orderCourirsPage.TablePrice.DeclaredPrice.WaitText("10.20 руб.");
            orderCourirsPage.TablePrice.Insurance.WaitText("0.00 руб.");
            orderCourirsPage.TablePrice.DeliveryPrice.WaitText("53.00 руб.");
            orderCourirsPage.TablePrice.PickupPrice.WaitText("21.00 руб.");

            orderCourirsPage.TableSize.Width.WaitText("11 см");
            orderCourirsPage.TableSize.Height.WaitText("12 см");
            orderCourirsPage.TableSize.Length.WaitText("13 см");
            orderCourirsPage.TableSize.Weight.WaitText("9.20 кг");
            orderCourirsPage.TableSize.Count.WaitText("1");

            orderCourirsPage.TableArticle.GetRow(0).Name.WaitText("Имя1");
            orderCourirsPage.TableArticle.GetRow(0).Article.WaitText("Article1");
            orderCourirsPage.TableArticle.GetRow(0).Count.WaitText("6");

            orderCourirsPage.BackOrders.Click();
            ordersPage = orderCourirsPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Number.WaitText("OrderNumber2");
            ordersPage.Table.GetRow(0).Goods.WaitText("Хороший товар,годный 2");
        }
    }
}