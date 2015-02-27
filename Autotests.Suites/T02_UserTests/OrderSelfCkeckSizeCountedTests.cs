using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class OrderSelfCkeckSizeCountedTests : ConstVariablesTestBase
    {
        [Test, Description("Проверка что нашей компании нету при привешении допустимых размеров")]
        public void OrderCreateSelfCheckOverSideTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.OrderNew.Click();
            userPage.OrderCreateSelf.Click();
            var orderCreateSelfPage = userPage.GoTo<OrderSelfCreatePage>();
            orderCreateSelfPage.BuyerName.SetValueAndWait("Фамилия Имя Очество");
            orderCreateSelfPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateSelfPage.GoodsDescription.SetValueAndWait("ok");
            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("4");
            orderCreateSelfPage.PaymentPrice.SetValueAndWait("4");
            orderCreateSelfPage.Weight.SetValueAndWait("3.0");
            
            orderCreateSelfPage.Width.SetValueAndWait(side1Min.ToString());
            orderCreateSelfPage.Height.SetValueAndWait(side3Min.ToString());
            orderCreateSelfPage.Length.SetValueAndWait(side2Min.ToString());
            orderCreateSelfPage.СountedButton.Click();

            orderCreateSelfPage.MapOrders.SwitchToFrame();
            orderCreateSelfPage.MapOrders.City.SelectValueFirst("Москва");
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(0).Name.WaitTextNotContains(companyName);
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();

            orderCreateSelfPage.Width.SetValueAndWait((side3Min + 0.01).ToString());
            orderCreateSelfPage.Height.SetValueAndWait((side1Min + 0.01).ToString());
            orderCreateSelfPage.Length.SetValueAndWait((side2Min + 0.01).ToString());
            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.MapOrders.SwitchToFrame();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(0).Name.WaitText(companyName);
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();

            orderCreateSelfPage.Width.SetValueAndWait(side2Max.ToString());
            orderCreateSelfPage.Height.SetValueAndWait(side1Max.ToString());
            orderCreateSelfPage.Length.SetValueAndWait(side3Max.ToString());
            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.MapOrders.SwitchToFrame();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(0).Name.WaitText(companyName);
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();

            orderCreateSelfPage.Width.SetValueAndWait((side2Max + 0.01).ToString());
            orderCreateSelfPage.Height.SetValueAndWait((side1Max + 0.01).ToString());
            orderCreateSelfPage.Length.SetValueAndWait((side3Max + 0.01).ToString());
            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.MapOrders.SwitchToFrame();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(0).Name.WaitTextNotContains(companyName);
        }

        [Test, Description("Проверка что нашей компании нету при привешении допустимых размеров")]
        public void OrderCreateSelfCheckOverWeightTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.OrderNew.Click();
            userPage.OrderCreateSelf.Click();
            var orderCreateSelfPage = userPage.GoTo<OrderSelfCreatePage>();
            orderCreateSelfPage.BuyerName.SetValueAndWait("Фамилия Имя Очество");
            orderCreateSelfPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateSelfPage.GoodsDescription.SetValueAndWait("ok");
            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("4");
            orderCreateSelfPage.PaymentPrice.SetValueAndWait("4");
            orderCreateSelfPage.Width.SetValueAndWait("10.1");
            orderCreateSelfPage.Height.SetValueAndWait("11.1");
            orderCreateSelfPage.Length.SetValueAndWait("12.1");

            orderCreateSelfPage.Weight.SetValueAndWait(weightMin.ToString());
            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.MapOrders.SwitchToFrame();
            orderCreateSelfPage.MapOrders.City.SelectValueFirst("Москва");
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(0).Name.WaitTextNotContains(companyName);
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();
            
            orderCreateSelfPage.Weight.SetValueAndWait((weightMin + 0.1).ToString());
            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.MapOrders.SwitchToFrame();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(0).Name.WaitText(companyName);
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();

            orderCreateSelfPage.Weight.SetValueAndWait(weightMax.ToString());
            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.MapOrders.SwitchToFrame();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(0).Name.WaitText(companyName);
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();

            orderCreateSelfPage.Weight.SetValueAndWait((weightMax + 0.1).ToString());
            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.MapOrders.SwitchToFrame();
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(0).Name.WaitTextNotContains(companyName);
        }
    }
}