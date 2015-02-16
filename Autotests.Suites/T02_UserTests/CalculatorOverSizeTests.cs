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
                Assert.False(сompanyName == calculatorPage.TableFirst.GetRow(0).Company.GetText());
            if (calculatorPage.TableSecond.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableSecond.GetRow(0).Company.GetText());

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
//            проверяем что нет таблиц
            if (calculatorPage.TableFirst.IsPresent)
//    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == calculatorPage.TableFirst.GetRow(0).Company.GetText());
//            проверяем что нет таблиц
            if (calculatorPage.TableSecond.IsPresent)
//    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == calculatorPage.TableSecond.GetRow(0).Company.GetText());
        }

        [Test, Ignore, Description("Проверяем, что не находит нашу компанию если превышает max или меньше min")]
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
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableFirst.GetRow(0).Company.GetText());
            if (calculatorPage.TableSecond.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableSecond.GetRow(0).Company.GetText());

            calculatorPage.Width.SetValueAndWait((side1Min + 0.01).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Width.SetValueAndWait(side3Max.ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Width.SetValueAndWait((side3Max + 0.01).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableFirst.GetRow(0).Company.GetText());
            if (calculatorPage.TableSecond.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableSecond.GetRow(0).Company.GetText());
        }

        [Test, Ignore, Description("Проверяем, что не находит нашу компанию если превышает max или меньше min")]
        public void CalculatorOverHeightTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();
            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.Shop.SetFirstValueSelect(userShops);
            calculatorPage.CityTo.SetFirstValueSelect("Москва");
            calculatorPage.Weight.SetValueAndWait("3");

            calculatorPage.Height.SetValueAndWait(side1Min.ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableFirst.GetRow(0).Company.GetText());
            if (calculatorPage.TableSecond.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableSecond.GetRow(0).Company.GetText());

            calculatorPage.Height.SetValueAndWait((side1Min + 0.01).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Height.SetValueAndWait(side3Max.ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Height.SetValueAndWait((side3Max + 0.01).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableFirst.GetRow(0).Company.GetText());
            if (calculatorPage.TableSecond.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableSecond.GetRow(0).Company.GetText());
        }

        [Test, Ignore, Description("Проверяем, что не находит нашу компанию если превышает max или меньше min")]
        public void CalculatorOverLengthTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var calculatorPage = userPage.GoTo<СalculatorPage>();
            calculatorPage.CityFrom.SetFirstValueSelect("Москва");
            calculatorPage.Shop.SetFirstValueSelect(userShops);
            calculatorPage.CityTo.SetFirstValueSelect("Москва");
            calculatorPage.Weight.SetValueAndWait("3");

            calculatorPage.Length.SetValueAndWait(side1Min.ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableFirst.GetRow(0).Company.GetText());
            if (calculatorPage.TableSecond.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableSecond.GetRow(0).Company.GetText());

            calculatorPage.Length.SetValueAndWait((side1Min + 0.01).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Length.SetValueAndWait(side3Max.ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            calculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            calculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            calculatorPage.Length.SetValueAndWait((side3Max + 0.01).ToString());
            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<СalculatorPage>();
            if (calculatorPage.TableFirst.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableFirst.GetRow(0).Company.GetText());
            if (calculatorPage.TableSecond.IsPresent)
                Assert.False(сompanyName == calculatorPage.TableSecond.GetRow(0).Company.GetText());
        }
    }
}