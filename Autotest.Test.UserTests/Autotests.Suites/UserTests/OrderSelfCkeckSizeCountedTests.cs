using Autotests.WebPages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
{
    public class OrderSelfCkeckSizeCountedTests : ConstVariablesTestBase
    {
        [Test, Description("Проверка что нашей компании нету при привешении допустимых размеров")]
        public void OrderCreateSelfCheckOverSideTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.Table.FindRowByName(userShopName).OrdersCreateSelf.Click();
            var orderCreateSelfPage = shopsListPage.GoTo<OrderSelfCreatePage>();
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
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(0).Name.WaitAbsence();
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
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(0).Name.WaitAbsence();
        }

        [Test, Description("Проверка что нашей компании нету при привешении допустимых размеров")]
        public void OrderCreateSelfCheckOverWeightTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.Table.FindRowByName(userShopName).OrdersCreateSelf.Click();
            var orderCreateSelfPage = shopsListPage.GoTo<OrderSelfCreatePage>();
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
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(0).Name.WaitAbsence();
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
            orderCreateSelfPage.MapOrders.GetMapCompanyRow(0).Name.WaitAbsence();
        }
    }
}