using System.Threading;
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
            orderCreateSelfPage.Weight.SetValueAndWait("4");
            orderCreateSelfPage.Width.SetValueAndWait("4");
            orderCreateSelfPage.Height.SetValueAndWait("4");
            orderCreateSelfPage.Length.SetValueAndWait("4");

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

            Thread.Sleep(1000);
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(1).Name.Click();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(2).Name.Click();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(3).Name.Click();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(4).Name.Click();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(5).Name.Click();
            orderCreateSelfPage.MapOrders.ImageLocator.Click();
            orderCreateSelfPage.MapOrders.TakeHere.Click();
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();
            
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
            orderPage.TableRecipient.DeliveryCompany.WaitText(сompanyName);

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
    }
}