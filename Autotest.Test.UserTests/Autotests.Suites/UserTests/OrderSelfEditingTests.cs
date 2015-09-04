using Autotests.WebPages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
{
    public class OrderSelfEditingTests : ConstVariablesTestBase
    {
        [Test, Description("Создание черновика заказа а потом редактирование")]
        public void OrdeSelfDraftEditingTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.Table.FindRowByName(userShopName).OrdersCreateSelf.Click();
             var orderCreateSelfPage = shopsListPage.GoTo<OrderSelfCreatePage>();
            orderCreateSelfPage.Width.SetValueAndWait("4");
            orderCreateSelfPage.Height.SetValueAndWait("4");
            orderCreateSelfPage.Length.SetValueAndWait("4");
            orderCreateSelfPage.Weight.SetValueAndWait("4");
            orderCreateSelfPage.OrderNumber.SetValue("14");

            orderCreateSelfPage.BuyerName.SetValueAndWait("Фамилия Имя Отчество");
            orderCreateSelfPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateSelfPage.BuyerPhoneAdd.SetValueAndWait("+71234567890, +71234567890");
            orderCreateSelfPage.BuyerEmail.SetValueAndWait(userNameAndPass);
            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("0");
            orderCreateSelfPage.PaymentPrice.SetValueAndWait("4");
            orderCreateSelfPage.GoodsDescription.SetValueAndWait("4");
            orderCreateSelfPage.OrderComment.SetValueAndWait("OrderComment");
            orderCreateSelfPage.IsCargoVolume.CheckAndWait();
            orderCreateSelfPage.ItemsCount.SetValueAndWait("2");

            var rowArticle = orderCreateSelfPage.GetArticleRow(0);
            rowArticle.Name.SetValueAndWait("Имя1");
            rowArticle.Article.SetValueAndWait("Article1");
            rowArticle.Count.SetValueAndWait("6");

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.MapOrders.SwitchToFrame();

            orderCreateSelfPage.MapOrders.City.SelectValueFirst("Москва");
            orderCreateSelfPage.MapOrders.ImageLocator.Click();
            orderCreateSelfPage.MapOrders.TakeHere.Click();
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();;

            orderCreateSelfPage.SaveDraftButton.Click();
            var orderPage = orderCreateSelfPage.GoTo<OrderPage>();
            orderPage.StatusOrder.WaitText("В обработке");

            orderPage.BackOrders.Click();
            var ordersPage = orderPage.GoTo<OrdersListPage>();

            ordersPage.Table.GetRow(0).Number.WaitText("14");
            ordersPage.Table.GetRow(0).Goods.WaitText("4");
            ordersPage.Table.GetRow(0).Edit.Click();
            var orderSelfEditingPage = ordersPage.GoTo<OrderSelfEditingPage>();

            orderSelfEditingPage.Width.WaitValue("4");
            orderSelfEditingPage.Height.WaitValue("4");
            orderSelfEditingPage.Length.WaitValue("4");
            orderSelfEditingPage.Weight.WaitValue("4");
            orderSelfEditingPage.OrderNumber.WaitValue("14");

            orderSelfEditingPage.BuyerName.WaitValue("Фамилия Имя Отчество");
            orderSelfEditingPage.BuyerPhone.WaitValue("+7 (111)111-1111");
            orderSelfEditingPage.BuyerPhoneAdd.WaitValue("+71234567890, +71234567890");
            orderSelfEditingPage.BuyerEmail.WaitValue(userNameAndPass);
            orderSelfEditingPage.DeclaredPrice.WaitValue("0");
            orderSelfEditingPage.PaymentPrice.WaitValue("4");
            orderSelfEditingPage.GoodsDescription.WaitValue("4");
            orderSelfEditingPage.OrderComment.WaitValue("OrderComment");
            orderSelfEditingPage.IsCargoVolume.WaitChecked();
            orderSelfEditingPage.ItemsCount.WaitValue("2");

            var rowArticleStatic = orderSelfEditingPage.GetArticleRow(0);
            rowArticleStatic.Name.WaitValue("Имя1");
            rowArticleStatic.Article.WaitValue("Article1");
            rowArticleStatic.Count.WaitValue("6");

            orderSelfEditingPage.DeclaredPrice.SetValue("10.2");
            orderSelfEditingPage.Width.SetValue("11.2");
            orderSelfEditingPage.Height.SetValue("12.2");
            orderSelfEditingPage.Length.SetValue("13.2");
            orderSelfEditingPage.Weight.SetValue("9.2");

            orderSelfEditingPage.BuyerName.SetValue("Фамилия2 Имя2 Очество2");
            orderSelfEditingPage.BuyerPhone.SetValue("2222222222");
            orderSelfEditingPage.BuyerPhoneAdd.SetValue("+74444444444, +75555555555");
            orderSelfEditingPage.BuyerEmail.SetValue("2" + userNameAndPass);
            orderSelfEditingPage.ItemsCount.SetValue("3");

            orderSelfEditingPage.PaymentPrice.SetValue("1500");
            orderSelfEditingPage.DeclaredPrice.SetValue("1600");

            orderSelfEditingPage.GoodsDescription.SetValue("24");
            orderSelfEditingPage.OrderComment.SetValue("OrderComment2");
            orderSelfEditingPage.IsCargoVolume.UncheckAndWait();
            orderSelfEditingPage.OrderNumber.SetValue("44");

            orderSelfEditingPage.SaveChangeButton.Click();
            orderPage = orderSelfEditingPage.GoTo<OrderPage>();

            orderPage.TableSender.City.WaitText("Москва");
            orderPage.TableSender.Address.WaitText("ул.Улица, дом Дом, офис(квартира) Квартира");
            orderPage.TableSender.Name.WaitText(legalEntityName);
            orderPage.TableSender.Phone.WaitText("+7 (111)111-1111");
            orderPage.TableSender.Delivery.WaitText("Самовывоз");
            orderPage.TableSender.OrderComment.WaitText("OrderComment2");
            orderPage.TableSender.IsCargoVolume.WaitText("нет");

            orderPage.TableRecipient.City.WaitText("Москва");
            orderPage.TableRecipient.Address.WaitText("Ленинский проспект 127");
            orderPage.TableRecipient.Name.WaitText("Фамилия2 Имя2 Очество2");
            orderPage.TableRecipient.Email.WaitText("2" + userNameAndPass);
            orderPage.TableRecipient.Phone.WaitText("+7 (222)222-2222");
            orderPage.TableRecipient.PhoneAdd.WaitText("+74444444444, +75555555555");
            orderPage.TableRecipient.Issue.WaitText("Ручная");
            orderPage.TableRecipient.PickupCompany.WaitText(companyPickupName);
            orderPage.TableRecipient.DeliveryCompany.WaitText(companyName);

            orderPage.TablePrice.PaymentPrice.WaitText("1500.00 руб.");
            orderPage.TablePrice.DeclaredPrice.WaitText("1600.00 руб.");
            orderPage.TablePrice.Insurance.WaitText("0.00 руб.");
            orderPage.TablePrice.DeliveryPrice.WaitText("65.00 руб.");
            orderPage.TablePrice.PickupPrice.WaitText("21.00 руб.");

            orderPage.TableSize.Width.WaitText("11 см");
            orderPage.TableSize.Height.WaitText("12 см");
            orderPage.TableSize.Length.WaitText("13 см");
            orderPage.TableSize.Weight.WaitText("9.20 кг");
            orderPage.TableSize.Count.WaitText("3");

            orderPage.TableArticle.GetRow(0).Name.WaitText("Имя1");
            orderPage.TableArticle.GetRow(0).Article.WaitText("Article1");
            orderPage.TableArticle.GetRow(0).Count.WaitText("6");

            orderPage.BackOrders.Click();
            ordersPage = orderPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Number.WaitText("44");
            ordersPage.Table.GetRow(0).Goods.WaitText("24");
        }

        [Test, Description("Отправка заказа а потом редактирование")]
        public void OrdeSelfSendEditingTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.Table.FindRowByName(userShopName).OrdersCreateSelf.Click();
            var orderCreateSelfPage = shopsListPage.GoTo<OrderSelfCreatePage>();
            orderCreateSelfPage.Width.SetValueAndWait("4");
            orderCreateSelfPage.Height.SetValueAndWait("4");
            orderCreateSelfPage.Length.SetValueAndWait("4");
            orderCreateSelfPage.Weight.SetValueAndWait("4");
            orderCreateSelfPage.OrderNumber.SetValue("14");

            orderCreateSelfPage.BuyerName.SetValueAndWait("Фамилия Имя Отчество");
            orderCreateSelfPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateSelfPage.BuyerPhoneAdd.SetValueAndWait("+71234567890, +71234567890");
            orderCreateSelfPage.BuyerEmail.SetValueAndWait(userNameAndPass);
            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("0");
            orderCreateSelfPage.PaymentPrice.SetValueAndWait("5");
            orderCreateSelfPage.GoodsDescription.SetValueAndWait("4");
            orderCreateSelfPage.OrderComment.SetValueAndWait("OrderComment");
            orderCreateSelfPage.IsCargoVolume.CheckAndWait();

            var rowArticle = orderCreateSelfPage.GetArticleRow(0);
            rowArticle.Name.SetValueAndWait("Имя1");
            rowArticle.Article.SetValueAndWait("Article1");
            rowArticle.Count.SetValueAndWait("6");

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.MapOrders.SwitchToFrame();

            orderCreateSelfPage.MapOrders.City.SelectValueFirst("Москва");
            orderCreateSelfPage.MapOrders.ImageLocator.Click();
            orderCreateSelfPage.MapOrders.TakeHere.Click();
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();

            orderCreateSelfPage.SendOrderButton.Click();
            var orderPage = orderCreateSelfPage.GoTo<OrderPage>();
            orderPage.StatusOrder.WaitText("Подтверждена");
            orderPage.BackOrders.Click();
            var ordersPage = orderPage.GoTo<OrdersListPage>();

            ordersPage.Table.GetRow(0).Number.WaitText("14");
            ordersPage.Table.GetRow(0).Goods.WaitText("4");
            ordersPage.Table.GetRow(0).Edit.Click();
            var orderSelfEditingPage = ordersPage.GoTo<OrderSelfEditingPage>();

            orderSelfEditingPage.Width.WaitValue("4");
            orderSelfEditingPage.Height.WaitValue("4");
            orderSelfEditingPage.Length.WaitValue("4");
            orderSelfEditingPage.Weight.WaitValue("4");
            orderSelfEditingPage.OrderNumber.WaitValue("14");

            orderSelfEditingPage.BuyerName.WaitValue("Фамилия Имя Отчество");
            orderSelfEditingPage.BuyerPhone.WaitValue("+7 (111)111-1111");
            orderSelfEditingPage.BuyerEmail.WaitValue(userNameAndPass);
            orderSelfEditingPage.BuyerPhoneAdd.WaitValue("+71234567890, +71234567890");
            orderSelfEditingPage.DeclaredPrice.WaitValue("0");
            orderSelfEditingPage.PaymentPrice.WaitValue("5");
            orderSelfEditingPage.GoodsDescription.WaitValue("4");
            orderSelfEditingPage.OrderComment.WaitValue("OrderComment");
            orderSelfEditingPage.ItemsCount.WaitValue("1");
            orderSelfEditingPage.IsCargoVolume.WaitChecked();

            var rowArticleStatic = orderSelfEditingPage.GetArticleRow(0);
            rowArticleStatic.Name.WaitValue("Имя1");
            rowArticleStatic.Article.WaitValue("Article1");
            rowArticleStatic.Count.WaitValue("6");

            orderSelfEditingPage.DeclaredPrice.SetValue("10.2");
            orderSelfEditingPage.Width.SetValue("11.2");
            orderSelfEditingPage.Height.SetValue("12.2");
            orderSelfEditingPage.Length.SetValue("13.2");
            orderSelfEditingPage.Weight.SetValue("9.2");

            orderSelfEditingPage.BuyerName.SetValue("Фамилия2 Имя2 Очество2");
            orderSelfEditingPage.BuyerPhone.SetValue("2222222222");
            orderSelfEditingPage.BuyerPhoneAdd.SetValue("+74444444444, +75555555555");
            orderSelfEditingPage.BuyerEmail.SetValue("2" + userNameAndPass);

            orderSelfEditingPage.PaymentPrice.SetValue("1500");
            orderSelfEditingPage.DeclaredPrice.SetValue("1600");

            orderSelfEditingPage.GoodsDescription.SetValue("24");
            orderSelfEditingPage.OrderComment.SetValue("OrderComment2");
            orderSelfEditingPage.IsCargoVolume.Click();
            orderSelfEditingPage.OrderNumber.SetValue("44");

            orderSelfEditingPage.SaveChangeButton.Click();
            orderSelfEditingPage.Aletr.WaitText("Уважаемый клиент! ДДеливери.ру делает все возможное, чтобы редактирование заказа прошло успешно, но не гарантирует внесение изменений в заказ, так как уровень автоматизации работы служб доставки не всегда позволяет это сделать. Надеемся на понимание.");
            orderSelfEditingPage.Aletr.Accept();
            orderPage = orderSelfEditingPage.GoTo<OrderPage>();

            orderPage.TableSender.City.WaitText("Москва");
            orderPage.TableSender.Address.WaitText("ул.Улица, дом Дом, офис(квартира) Квартира");
            orderPage.TableSender.Name.WaitText(legalEntityName);
            orderPage.TableSender.Phone.WaitText("+7 (111)111-1111");
            orderPage.TableSender.Delivery.WaitText("Самовывоз");
            orderPage.TableSender.OrderComment.WaitText("OrderComment2");
            orderPage.TableSender.IsCargoVolume.WaitText("да");


            orderPage.TableRecipient.City.WaitText("Москва");
            orderPage.TableRecipient.Address.WaitText("Ленинский проспект 127");
            orderPage.TableRecipient.Name.WaitText("Фамилия2 Имя2 Очество2");
            orderPage.TableRecipient.Email.WaitText("2" + userNameAndPass);
            orderPage.TableRecipient.Phone.WaitText("+7 (222)222-2222");
            orderPage.TableRecipient.PhoneAdd.WaitText("+74444444444, +75555555555");
            orderPage.TableRecipient.Issue.WaitText("Ручная");
            orderPage.TableRecipient.PickupCompany.WaitText(companyPickupName);
            orderPage.TableRecipient.DeliveryCompany.WaitText(companyName);

            orderPage.TablePrice.PaymentPrice.WaitText("1500.00 руб.");
            orderPage.TablePrice.DeclaredPrice.WaitText("1600.00 руб.");
            orderPage.TablePrice.Insurance.WaitText("0.00 руб.");
            orderPage.TablePrice.DeliveryPrice.WaitText("65.00 руб.");
            orderPage.TablePrice.PickupPrice.WaitText("21.00 руб.");

            orderPage.TableSize.Width.WaitText("11 см");
            orderPage.TableSize.Height.WaitText("12 см");
            orderPage.TableSize.Length.WaitText("13 см");
            orderPage.TableSize.Weight.WaitText("9.20 кг");
            orderPage.TableSize.Count.WaitText("1");

            orderPage.TableArticle.GetRow(0).Name.WaitText("Имя1");
            orderPage.TableArticle.GetRow(0).Article.WaitText("Article1");
            orderPage.TableArticle.GetRow(0).Count.WaitText("6");

            orderPage.BackOrders.Click();
            ordersPage = orderPage.GoTo<OrdersListPage>();
            ordersPage.Table.GetRow(0).Number.WaitText("44");
            ordersPage.Table.GetRow(0).Goods.WaitText("24");
        }
    }
}