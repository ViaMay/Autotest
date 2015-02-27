using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class OrderSelfCreateDraftTests : ConstVariablesTestBase
    {
        [Test, Description("Создание черновика заказа и проверка введенных значений")]
        public void OrderSelfCreateCheckDraftTest()
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
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();

            orderCreateSelfPage.PointDeliveryName.WaitText("Пункт выдачи: " + deliveryPointName);
            orderCreateSelfPage.PointDeliveryAddress.WaitText("Адрес: " + deliveryPointAddress);
            orderCreateSelfPage.PointDeliveryCompany.WaitText("Компания: " + companyName);
            orderCreateSelfPage.PointDeliveryPrice.WaitText("Цена: 28");

            orderCreateSelfPage.SaveDraftButton.Click();
            var orderPage = orderCreateSelfPage.GoTo<OrderPage>();

            orderPage.TableSender.City.WaitText("Москва");
            orderPage.TableSender.Address.WaitText("ул.Улица, дом Дом, офис(квартира) Квартира");
            orderPage.TableSender.Name.WaitText(legalEntityName);
            orderPage.TableSender.Phone.WaitText("+7 (111)111-1111");
            orderPage.TableSender.Delivery.WaitText("Самовывоз");

            orderPage.TableRecipient.City.WaitText("Москва");
            orderPage.TableRecipient.Address.WaitText("Ленинский проспект 127");
            orderPage.TableRecipient.Name.WaitText("Фамилия Имя Отчество");
            orderPage.TableRecipient.Email.WaitText(userNameAndPass);
            orderPage.TableRecipient.Phone.WaitText("+7 (111)111-1111");
            orderPage.TableRecipient.Issue.WaitText("Ручная");
            orderPage.TableRecipient.PickupCompany.WaitText("FSD забор");
            orderPage.TableRecipient.DeliveryCompany.WaitText(companyName);

            orderPage.TablePrice.PaymentPrice.WaitText("4.00 руб.");
            orderPage.TablePrice.DeclaredPrice.WaitText("4.00 руб.");
            orderPage.TablePrice.Insurance.WaitText("0.00 руб.");
            orderPage.TablePrice.DeliveryPrice.WaitText("28.00 руб.");
            orderPage.TablePrice.PickupPrice.WaitText("200.00 руб.");

            orderPage.TableSize.Width.WaitText("4 см");
            orderPage.TableSize.Height.WaitText("4 см");
            orderPage.TableSize.Length.WaitText("4 см");
            orderPage.TableSize.Weight.WaitText("4.00 кг");

            orderPage.TableArticle.GetRow(0).Name.WaitText("Имя1");
            orderPage.TableArticle.GetRow(0).Article.WaitText("Article1");
            orderPage.TableArticle.GetRow(0).Count.WaitText("6");
        }

        [Test, Description("Проверка изменения статусов")]
        public void OrderCreateCourirsCheckStatusTest()
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

            orderCreateSelfPage.MapOrders.HasFittingRoom.Click();
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

            orderCreateSelfPage.SaveDraftButton.Click();
            var orderPage = orderCreateSelfPage.GoTo<OrderPage>();
            orderPage.StatusOrder.WaitText("В обработке");

            orderPage.BackOrders.Click();
            var ordersPage = orderPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Status.WaitText("В обработке");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Подтвердить");
            ordersPage.Table.GetRow(0).ID.Click();
            orderPage = ordersPage.GoTo<OrderPage>();

            orderPage.СonfirmButton.Click();
            orderPage = orderPage.GoTo<OrderPage>();
            orderPage.StatusOrder.WaitText("Подтверждена");

            orderPage.BackOrders.Click();
            ordersPage = orderPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Status.WaitText("Подтверждена");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Отменить");
            ordersPage.Table.GetRow(0).ID.Click();
            orderPage = ordersPage.GoTo<OrderPage>();

            orderPage.UndoButton.Click();
            orderPage = orderPage.GoTo<OrderPage>();
            orderPage.StatusOrder.WaitText("В обработке");

            orderPage.BackOrders.Click();
            ordersPage = orderPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Status.WaitText("В обработке");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Подтвердить");
        }
    }
}