using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class OrderCourirsCkeckSizeCountedTests : ConstVariablesTestBase
    {
       [Test, Description("Проверка что нашей компании нету при привешении допустимых размеров")]
        public void OrderCreateCourirsCheckOverWeightTest()
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
            orderCreateCourirsPage.Weight.SetValueAndWait("3.0");

            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains("test_via, цена: 32.00 руб");

            orderCreateCourirsPage.Weight.SetValueAndWait(weightMin.ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.DeliveryList[0].WaitAbsence();

            orderCreateCourirsPage.Weight.SetValueAndWait((weightMin + 0.1).ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains(companyName);

            orderCreateCourirsPage.Weight.SetValueAndWait(weightMax.ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains(companyName);

            orderCreateCourirsPage.Weight.SetValueAndWait((weightMax + 0.1).ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.DeliveryList[0].WaitAbsence();
        }

        [Test, Description("Проверка что нашей компании нету при привешении допустимых размеров")]
        public void OrderCreateCourirsCheckOverSideTest()
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
            orderCreateCourirsPage.Weight.SetValueAndWait("3.0");

            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");

            orderCreateCourirsPage.Width.SetValueAndWait(side1Min.ToString());
            orderCreateCourirsPage.Height.SetValueAndWait(side3Min.ToString());
            orderCreateCourirsPage.Length.SetValueAndWait(side2Min.ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.DeliveryList[0].WaitAbsence();

            orderCreateCourirsPage.Width.SetValueAndWait((side3Min + 0.01).ToString());
            orderCreateCourirsPage.Height.SetValueAndWait((side1Min + 0.01).ToString());
            orderCreateCourirsPage.Length.SetValueAndWait((side2Min + 0.01).ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains(companyName);

            orderCreateCourirsPage.Width.SetValueAndWait(side2Max.ToString());
            orderCreateCourirsPage.Height.SetValueAndWait(side1Max.ToString());
            orderCreateCourirsPage.Length.SetValueAndWait(side3Max.ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains(companyName);

            orderCreateCourirsPage.Width.SetValueAndWait((side2Max + 0.01).ToString());
            orderCreateCourirsPage.Height.SetValueAndWait((side1Max + 0.01).ToString());
            orderCreateCourirsPage.Length.SetValueAndWait((side3Max + 0.01).ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.DeliveryList[0].WaitAbsence();
        }
    }
}