using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class CalculatorTests : ConstantVariablesTestBase
    {
       [Test, Description("Калькулятор. Проверяем, что цена меняется в зависимости от указанного веса")]
        public void CalculatorChangePriceTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();
            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.Shop.SetFirstValueSelect(userShops);
            calculatorPage.CityTo.SetFirstValueSelect("Москва");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();

            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableFirst.GetRow(0).TimeDelivery.WaitText("1 - 2");
            calculatorPage.TableFirst.GetRow(0).PriceDelivery.WaitText("16.00");
            calculatorPage.TableFirst.GetRow(0).PricePickup.WaitText("200");

            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).TimeDelivery.WaitText("1 - 2");
            calculatorPage.TableSecond.GetRow(0).PriceDelivery.WaitText("14.00");
            calculatorPage.TableSecond.GetRow(0).PricePickup.WaitText("200");

            calculatorPage.Weight.SetValueAndWait("9.1");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();

            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableFirst.GetRow(0).TimeDelivery.WaitText("1 - 2");
            calculatorPage.TableFirst.GetRow(0).PriceDelivery.WaitText("52.00");
            calculatorPage.TableFirst.GetRow(0).PricePickup.WaitText("200");

            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).TimeDelivery.WaitText("1 - 2");
            calculatorPage.TableSecond.GetRow(0).PriceDelivery.WaitText("41.00");
            calculatorPage.TableSecond.GetRow(0).PricePickup.WaitText("200");
        }


        [Test, Description("Калькулятор. Проверяем, что не находит нашу компанию если вес превышает допустимый")]
        public void CalculatorOverWeightTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();
            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.Shop.SetFirstValueSelect(userShops);
            calculatorPage.CityTo.SetFirstValueSelect("Москва");

            calculatorPage.Weight.SetValueAndWait("9.1");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Weight.SetValueAndWait("10.0");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Weight.SetValueAndWait("10.01");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
//            проверяем что нет таблиц
            if (calculatorPage.TableFirst.IsPresent)
//    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == calculatorPage.TableFirst.GetRow(0).Company.GetText());

            //            проверяем что нет таблиц
            if (calculatorPage.TableSecond.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == calculatorPage.TableSecond.GetRow(0).Company.GetText());
        }

        [Test, Description("Калькулятор. Проверяем, что не находит нашу компанию если размеры превышают допустимые")]
        public void CalculatorOverSideTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();
            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.Shop.SetFirstValueSelect(userShops);
            calculatorPage.CityTo.SetFirstValueSelect("Москва");

            calculatorPage.Width.SetValueAndWait("9.1");
            calculatorPage.Height.SetValueAndWait("9.1");
            calculatorPage.Length.SetValueAndWait("9.1");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Width.SetValueAndWait("80.0");
            calculatorPage.Height.SetValueAndWait("100.0");
            calculatorPage.Length.SetValueAndWait("50.0");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Width.SetValueAndWait("80.1");
            calculatorPage.Height.SetValueAndWait("100.0");
            calculatorPage.Length.SetValueAndWait("50.0");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            //            проверяем что нет таблиц
            if (calculatorPage.TableFirst.IsPresent)
            //    если все таки есть, то проверяем что нет нашей компании
                Assert.True(сompanyName != calculatorPage.TableFirst.GetRow(0).Company.GetText());
            //            проверяем что нет таблиц
            if (calculatorPage.TableSecond.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.True(сompanyName != calculatorPage.TableSecond.GetRow(0).Company.GetText());

            calculatorPage.Width.SetValueAndWait("80.0");
            calculatorPage.Height.SetValueAndWait("100.1");
            calculatorPage.Length.SetValueAndWait("50.0");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            //            проверяем что нет таблиц
            if (calculatorPage.TableFirst.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.True(сompanyName != calculatorPage.TableFirst.GetRow(0).Company.GetText());
            //            проверяем что нет таблиц
            if (calculatorPage.TableSecond.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.True(сompanyName != calculatorPage.TableSecond.GetRow(0).Company.GetText());

            calculatorPage.Width.SetValueAndWait("80.0");
            calculatorPage.Height.SetValueAndWait("100.0");
            calculatorPage.Length.SetValueAndWait("50.1");
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            //            проверяем что нет таблиц
            if (calculatorPage.TableFirst.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.True(сompanyName != calculatorPage.TableFirst.GetRow(0).Company.GetText());
            //            проверяем что нет таблиц
            if (calculatorPage.TableSecond.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.True(сompanyName != calculatorPage.TableSecond.GetRow(0).Company.GetText());
        }

        [Test, Description("Провека сообщений об ошибках для автокомплитов стран и магазина")]
        public void CalculatorValidationCityAndShopTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();

            calculatorPage.СountedButton.ClickAndWaitTextError();
            calculatorPage.AlertErrorText[0].WaitText("Магазин обязательно к заполнению");
            calculatorPage.AlertErrorText[1].WaitText("Город получения обязательно к заполнению");
            calculatorPage.AlertErrorText[2].WaitText("Город отправления обязательно к заполнению");

            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.СountedButton.ClickAndWaitTextError();
            calculatorPage.AlertErrorText[0].WaitText("Город получения обязательно к заполнению");
            calculatorPage.AlertErrorText[1].WaitAbsence();

            calculatorPage.CityFromConbobox.Remove.Click();
            calculatorPage.CityTo.SetFirstValueSelect("Москва");
            calculatorPage.СountedButton.ClickAndWaitTextError();
            calculatorPage.AlertErrorText[0].WaitText("Магазин обязательно к заполнению");
            calculatorPage.AlertErrorText[1].WaitText("Город отправления обязательно к заполнению");
            calculatorPage.AlertErrorText[2].WaitAbsence();

            calculatorPage.Shop.SetFirstValueSelect(userShops);
            calculatorPage.СountedButton.ClickAndWaitTextError();
            calculatorPage.AlertErrorText[0].WaitText("Город отправления обязательно к заполнению");
            calculatorPage.AlertErrorText[1].WaitAbsence();
        }

        [Test, Description("Провека сообщений об ошибках для размера")]
        public void CalculatorValidationSizeTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();

            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.CityTo.SetFirstValueSelect("Москва");
            calculatorPage.Shop.SetFirstValueSelect(userShops);
            calculatorPage.DeclaredPrice.SetValueAndWait("");
            calculatorPage.Weight.SetValueAndWait("");
            calculatorPage.Width.SetValueAndWait("");
            calculatorPage.Height.SetValueAndWait("");
            calculatorPage.Length.SetValueAndWait("");

            calculatorPage.СountedButton.ClickAndWaitTextError();

            calculatorPage.Width.WaitText("");
            calculatorPage.WidthErrorText.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            calculatorPage.Height.WaitText("");
            calculatorPage.HeightErrorText.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            calculatorPage.Length.WaitText("");
            calculatorPage.LengthErrorText.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");

            calculatorPage.Width.SetValueAndWait("0");
            calculatorPage.Height.SetValueAndWait("0");
            calculatorPage.Length.SetValueAndWait("0");

            calculatorPage.СountedButton.ClickAndWaitTextError();

            calculatorPage.WidthErrorText.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            calculatorPage.HeightErrorText.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            calculatorPage.LengthErrorText.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");

            calculatorPage.Width.SetValueAndWait("249");
            calculatorPage.Height.SetValueAndWait("249");
            calculatorPage.Length.SetValueAndWait("249");

            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();

            calculatorPage.WidthErrorText.WaitAbsence();
            calculatorPage.HeightErrorText.WaitAbsence();
            calculatorPage.LengthErrorText.WaitAbsence();

            calculatorPage.Width.SetValueAndWait("250");
            calculatorPage.Height.SetValueAndWait("250");
            calculatorPage.Length.SetValueAndWait("250");

            calculatorPage.СountedButton.ClickAndWaitTextError();

            calculatorPage.WidthErrorText.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            calculatorPage.HeightErrorText.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            calculatorPage.LengthErrorText.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
        }

        [Test, Description("Автозаполнение полей стоимости и весе")]
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
            calculatorPage.Weight.SetValueAndWait("0,123123");
            calculatorPage.Width.SetValueAndWait("15,4444");
            calculatorPage.Height.SetValueAndWait("6,999999");
            calculatorPage.Length.SetValueAndWait("12,20");

            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();

            calculatorPage.DeclaredPrice.WaitValue("15,1200");
            calculatorPage.Weight.WaitValue("0,123123");
            calculatorPage.Width.WaitValue("15,4444");
            calculatorPage.Height.WaitValue("6,999999");
            calculatorPage.Length.WaitValue("12,20");

            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);
        }
    }
}