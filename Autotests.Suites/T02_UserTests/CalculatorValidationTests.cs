using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class CalculatorValidationTests : ConstVariablesTestBase
    {
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
            calculatorPage.CityFrom.SetFirstValueSelect("Питер");
            calculatorPage.СountedButton.ClickAndWaitTextError();
            calculatorPage.AlertErrorText[0].WaitText("Магазин обязательно к заполнению");
            calculatorPage.AlertErrorText[1].WaitText("Город получения обязательно к заполнению");
            calculatorPage.AlertErrorText[2].WaitAbsence();

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

            calculatorPage.СountedButton.ClickAndWaitTextErrorAbsence();

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
    }
}