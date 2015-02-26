using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class CalculatorOverSizeTests : ConstVariablesTestBase
    {
        [Test, Description("Проверяем, что не находит нашу компанию если вес превышает max или меньше min")]
        public void CalculatorOverWeightTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();
            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.Shop.SetFirstValueSelect(userShops);
            calculatorPage.CityTo.SetFirstValueSelect("Москва");

            calculatorPage.Weight.SetValueAndWait(weightMin.ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                calculatorPage.TableFirst.GetRow(0).Company.WaitTextNotContains(сompanyName);
            if (calculatorPage.TableSecond.IsPresent)
                calculatorPage.TableSecond.GetRow(0).Company.WaitTextNotContains(сompanyName);

            calculatorPage.Weight.SetValueAndWait((weightMin + 0.1).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Weight.SetValueAndWait(weightMax.ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Weight.SetValueAndWait((weightMax + 0.1).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                calculatorPage.TableFirst.GetRow(0).Company.WaitTextNotContains(сompanyName);

            if (calculatorPage.TableSecond.IsPresent)
                calculatorPage.TableSecond.GetRow(0).Company.WaitTextNotContains(сompanyName);
        }

        [Test, Description("Проверяем, что не находит нашу компанию если превышает max или меньше min")]
        public void CalculatorOverWidthTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();
            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.Shop.SetFirstValueSelect(userShops);
            calculatorPage.CityTo.SetFirstValueSelect("Москва");
            calculatorPage.Weight.SetValueAndWait("3");

            calculatorPage.Width.SetValueAndWait(side1Min.ToString());
            calculatorPage.Height.SetValueAndWait(side3Min.ToString());
            calculatorPage.Length.SetValueAndWait(side2Min.ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                calculatorPage.TableFirst.GetRow(0).Company.WaitTextNotContains(сompanyName);
            if (calculatorPage.TableSecond.IsPresent)
                calculatorPage.TableSecond.GetRow(0).Company.WaitTextNotContains(сompanyName);

            calculatorPage.Width.SetValueAndWait(side2Min.ToString());
            calculatorPage.Height.SetValueAndWait(side3Min.ToString());
            calculatorPage.Length.SetValueAndWait(side1Min.ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                calculatorPage.TableFirst.GetRow(0).Company.WaitTextNotContains(сompanyName);
            if (calculatorPage.TableSecond.IsPresent)
                calculatorPage.TableSecond.GetRow(0).Company.WaitTextNotContains(сompanyName);

            calculatorPage.Width.SetValueAndWait((side3Min + 0.01).ToString());
            calculatorPage.Height.SetValueAndWait((side1Min + 0.01).ToString());
            calculatorPage.Length.SetValueAndWait((side2Min + 0.01).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Width.SetValueAndWait((side1Min + 0.01).ToString());
            calculatorPage.Height.SetValueAndWait((side2Min + 0.01).ToString());
            calculatorPage.Length.SetValueAndWait((side3Min + 0.01).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Width.SetValueAndWait(side3Max.ToString());
            calculatorPage.Height.SetValueAndWait(side2Max.ToString());
            calculatorPage.Length.SetValueAndWait(side1Max.ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Width.SetValueAndWait(side2Max.ToString());
            calculatorPage.Height.SetValueAndWait(side1Max.ToString());
            calculatorPage.Length.SetValueAndWait(side3Max.ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Width.SetValueAndWait((side3Max + 0.01).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                calculatorPage.TableFirst.GetRow(0).Company.WaitTextNotContains(сompanyName);
            if (calculatorPage.TableSecond.IsPresent)
                calculatorPage.TableSecond.GetRow(0).Company.WaitTextNotContains(сompanyName);

            calculatorPage.Width.SetValueAndWait((side3Max).ToString());
            calculatorPage.Height.SetValueAndWait((side1Max + 0.01).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                calculatorPage.TableFirst.GetRow(0).Company.WaitTextNotContains(сompanyName);
            if (calculatorPage.TableSecond.IsPresent)
                calculatorPage.TableSecond.GetRow(0).Company.WaitTextNotContains(сompanyName);


            calculatorPage.Height.SetValueAndWait((side1Max).ToString());
            calculatorPage.Length.SetValueAndWait((side2Max + 0.01).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                calculatorPage.TableFirst.GetRow(0).Company.WaitTextNotContains(сompanyName);
            if (calculatorPage.TableSecond.IsPresent)
                calculatorPage.TableSecond.GetRow(0).Company.WaitTextNotContains(сompanyName);
        }
    }
}