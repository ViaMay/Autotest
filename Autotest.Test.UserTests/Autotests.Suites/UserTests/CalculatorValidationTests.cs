using System;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
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
            calculatorPage.ActionErrorText[0].WaitText("Магазин обязательно к заполнению");
            calculatorPage.ActionErrorText[1].WaitText("Город получения обязательно к заполнению");
            calculatorPage.ActionErrorText[2].WaitText("Город отправления обязательно к заполнению");

            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.СountedButton.ClickAndWaitTextError();
            calculatorPage.ActionErrorText[0].WaitText("Город получения обязательно к заполнению");
            calculatorPage.ActionErrorText[1].WaitAbsence();

            calculatorPage.CityFromConbobox.Remove.Click();
            calculatorPage.CityFrom.SetFirstValueSelect("Екатеринбург");
            calculatorPage.СountedButton.ClickAndWaitTextError();
            calculatorPage.ActionErrorText[0].WaitText("Магазин обязательно к заполнению");
            calculatorPage.ActionErrorText[1].WaitText("Город получения обязательно к заполнению");
            calculatorPage.ActionErrorText[2].WaitAbsence();

            calculatorPage.CityFromConbobox.Remove.Click();
            calculatorPage.CityTo.SetFirstValueSelect("Москва");
            calculatorPage.СountedButton.ClickAndWaitTextError();
            calculatorPage.ActionErrorText[0].WaitText("Магазин обязательно к заполнению");
            calculatorPage.ActionErrorText[1].WaitText("Город отправления обязательно к заполнению");
            calculatorPage.ActionErrorText[2].WaitAbsence();

            calculatorPage.Shop.SetFirstValueSelect(userShopName);
            calculatorPage.СountedButton.ClickAndWaitTextError();
            calculatorPage.ActionErrorText[0].WaitText("Город отправления обязательно к заполнению");
            calculatorPage.ActionErrorText[1].WaitAbsence();
        }

        [Test, Description("Провека сообщений об ошибках для размера")]
        public void CalculatorValidationSizeTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();

            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.CityTo.SetFirstValueSelect("Москва");
            calculatorPage.Shop.SetFirstValueSelect(userShopName);
            calculatorPage.DeclaredPrice.SetValueAndWait("");
            calculatorPage.Weight.SetValueAndWait("");
            calculatorPage.Width.SetValueAndWait("");
            calculatorPage.Height.SetValueAndWait("");
            calculatorPage.Length.SetValueAndWait("");

            calculatorPage.СountedButton.ClickAndWaitTextError();

            calculatorPage.Width.WaitText("");
            calculatorPage.ErrorText[0].WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            calculatorPage.Height.WaitText("");
            calculatorPage.ErrorText[1].WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            calculatorPage.Length.WaitText("");
            calculatorPage.ErrorText[2].WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");

            calculatorPage.Width.SetValueAndWait("0");
            calculatorPage.Height.SetValueAndWait("0");
            calculatorPage.Length.SetValueAndWait("0");

            calculatorPage.СountedButton.ClickAndWaitTextError();

            calculatorPage.ErrorText[0].WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            calculatorPage.ErrorText[1].WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            calculatorPage.ErrorText[2].WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");

            calculatorPage.Width.SetValueAndWait("249");
            calculatorPage.Height.SetValueAndWait("249");
            calculatorPage.Length.SetValueAndWait("249");

            calculatorPage.СountedButton.ClickAndWaitTextErrorAbsence();

            calculatorPage.ErrorText[0].WaitAbsence();
            calculatorPage.ErrorText[1].WaitAbsence();
            calculatorPage.ErrorText[2].WaitAbsence();

            calculatorPage.Width.SetValueAndWait("250");
            calculatorPage.Height.SetValueAndWait("250");
            calculatorPage.Length.SetValueAndWait("250");

            calculatorPage.СountedButton.ClickAndWaitTextError();

            calculatorPage.ErrorText[0].WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            calculatorPage.ErrorText[1].WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            calculatorPage.ErrorText[2].WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
        }
    }
}