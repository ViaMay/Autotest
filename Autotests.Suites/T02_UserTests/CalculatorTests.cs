using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class CalculatorTests : ConstVariablesTestBase
    {
       [Test, Description("Проверяем, что цена меняется в зависимости от указанного веса")]
        public void CalculatorChangePriceTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();
            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.Shop.SetFirstValueSelect(userShops);
            calculatorPage.CityTo.SetFirstValueSelect("Москва");
            calculatorPage.Weight.SetValueAndWait("3");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();

            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
//            calculatorPage.TableFirst.GetRow(0).TimeDelivery.WaitText("1 - 2");
            calculatorPage.TableFirst.GetRow(0).PriceDelivery.WaitText("24.00");
            calculatorPage.TableFirst.GetRow(0).PricePickup.WaitText("200");

            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);
//            calculatorPage.TableSecond.GetRow(0).TimeDelivery.WaitText("1 - 2");
            calculatorPage.TableSecond.GetRow(0).PriceDelivery.WaitText("20.00");
            calculatorPage.TableSecond.GetRow(0).PricePickup.WaitText("200");

            calculatorPage.Weight.SetValueAndWait("9.1");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();

            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
//            calculatorPage.TableFirst.GetRow(0).TimeDelivery.WaitText("1 - 2");
            calculatorPage.TableFirst.GetRow(0).PriceDelivery.WaitText("52.00");
            calculatorPage.TableFirst.GetRow(0).PricePickup.WaitText("200");

            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);
//            calculatorPage.TableSecond.GetRow(0).TimeDelivery.WaitText("1 - 2");
            calculatorPage.TableSecond.GetRow(0).PriceDelivery.WaitText("41.00");
            calculatorPage.TableSecond.GetRow(0).PricePickup.WaitText("200");
        }

        [Test, Description("Проверяем автозаполнение полей стоимости и веса")]
        public void CalculatoraUpdatePriceAndTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();
            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.Shop.SetFirstValueSelect(userShops);
            calculatorPage.CityTo.SetFirstValueSelect("Москва");

            calculatorPage.DeclaredPrice.WaitValue("1500");
            calculatorPage.Weight.WaitValue("0.8");
            calculatorPage.Width.WaitValue("15");
            calculatorPage.Height.WaitValue("6");
            calculatorPage.Length.WaitValue("12");

            calculatorPage.DeclaredPrice.SetValue("0");
            calculatorPage.Weight.SetValue("0");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();

            calculatorPage.DeclaredPrice.WaitValue("1500");
            calculatorPage.Weight.WaitValue("0.8");
        }

        [Test, Description("Проверяем корректную работу с запятыми")]
        public void CalculatoraFormatFielInputTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();
            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.Shop.SetFirstValueSelect(userShops);
            calculatorPage.CityTo.SetFirstValueSelect("Москва");

            calculatorPage.DeclaredPrice.SetValueAndWait("15,1200");
            calculatorPage.Weight.SetValueAndWait("3,123123");
            calculatorPage.Width.SetValueAndWait("15,4444");
            calculatorPage.Height.SetValueAndWait("6,999999");
            calculatorPage.Length.SetValueAndWait("12,20");

            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();

            calculatorPage.DeclaredPrice.WaitValue("15,1200");
            calculatorPage.Weight.WaitValue("3,123123");
            calculatorPage.Width.WaitValue("15,4444");
            calculatorPage.Height.WaitValue("6,999999");
            calculatorPage.Length.WaitValue("12,20");

            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);
        }
    }
}