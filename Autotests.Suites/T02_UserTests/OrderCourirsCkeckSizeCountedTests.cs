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

            orderCreateCourirsPage.Weight.SetValueAndWait(weightMin.ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextNotContains(сompanyName);

            orderCreateCourirsPage.Weight.SetValueAndWait((weightMin + 0.1).ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains(сompanyName);

            orderCreateCourirsPage.Weight.SetValueAndWait(weightMax.ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains(сompanyName);

            orderCreateCourirsPage.Weight.SetValueAndWait((weightMax + 0.1).ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextNotContains(сompanyName);
        }

        [Test, Description("Проверка что нашей компании нету при привешении допустимых размеров")]
        public void OrderCreateCourirsCheckOverSideTest()
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

            orderCreateCourirsPage.Width.SetValueAndWait(side1Min.ToString());
            orderCreateCourirsPage.Height.SetValueAndWait(side3Min.ToString());
            orderCreateCourirsPage.Length.SetValueAndWait(side2Min.ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextNotContains(сompanyName);

            orderCreateCourirsPage.Width.SetValueAndWait((side3Min + 0.01).ToString());
            orderCreateCourirsPage.Height.SetValueAndWait((side1Min + 0.01).ToString());
            orderCreateCourirsPage.Length.SetValueAndWait((side2Min + 0.01).ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains(сompanyName);

            orderCreateCourirsPage.Width.SetValueAndWait(side2Max.ToString());
            orderCreateCourirsPage.Height.SetValueAndWait(side1Max.ToString());
            orderCreateCourirsPage.Length.SetValueAndWait(side3Max.ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains(сompanyName);

            orderCreateCourirsPage.Width.SetValueAndWait((side2Max + 0.01).ToString());
            orderCreateCourirsPage.Height.SetValueAndWait((side1Max + 0.01).ToString());
            orderCreateCourirsPage.Length.SetValueAndWait((side3Max + 0.01).ToString());
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextNotContains(сompanyName);
        }
    }
}