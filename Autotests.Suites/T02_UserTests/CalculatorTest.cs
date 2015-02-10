using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class CalculatorTest : ConstantVariablesTestBase
    {
        [Test, Description("Создания Склада")]
        public void T01_CreateWarehouseTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserWarehouses.Click();
            var warehousesListPage = userPage.GoTo<UserWarehousesPage>();

            if (!warehousesListPage.Table.GetRow(1).Name.IsPresent)
            {
                warehousesListPage.WarehousesCreate.Click();
                var warehouseCreatePage = warehousesListPage.GoTo<UserWarehouseCreatePage>();
                warehouseCreatePage.Name.SetValueAndWait(userWarehouses);
                warehouseCreatePage.Street.SetValueAndWait("Улица");
                warehouseCreatePage.House.SetValueAndWait("Дом");
                warehouseCreatePage.Flat.SetValueAndWait("Квартира");
                warehouseCreatePage.ContactPerson.SetValueAndWait(legalEntityName);
                warehouseCreatePage.ContactPhone.SetValueAndWait("1111111111");
                warehouseCreatePage.ContactEmail.SetValueAndWait(userNameAndPass);
                warehouseCreatePage.City.SetValueAndSelect("Москва");

                for (int i = 1; i <= 7; i++)
                {
                    warehouseCreatePage.GetDay(i).FromHour.SetValueAndWait("1:12");
                    warehouseCreatePage.GetDay(i).ToHour.SetValueAndWait("23:23");
                }

                warehouseCreatePage.CreateButton.Click();
                warehousesListPage = warehouseCreatePage.GoTo<UserWarehousesPage>();
            }
            warehousesListPage.Table.GetRow(1).Name.WaitPresence();
        }

        [Test, Description("Создания магазина")]
        public void T02_CreateShopTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();

            while (shopsListPage.Table.GetRow(1).Name.IsPresent)
            {
                shopsListPage.Table.GetRow(1).ActionsDelete.Click();
                shopsListPage.AletrDelete.WaitText("Вы действительно хотите удалить магазин?");
                shopsListPage.AletrDelete.Accept();
                shopsListPage = shopsListPage.GoTo<UserShopsPage>();
            }

            shopsListPage.ShopsCreate.Click();
            var shopCreatePage = shopsListPage.GoTo<UserShopCreatePage>();
            shopCreatePage.Name.SetValueAndWait(userShops);
            shopCreatePage.Address.SetValueAndWait("Москва");
            shopCreatePage.Warehouse.SelectValue(userWarehouses);
            shopCreatePage.CreateButton.Click();
            shopsListPage = shopCreatePage.GoTo<UserShopsPage>();
            shopsListPage.Table.GetRow(1).Name.WaitPresence();
        }

        [Test, Description("Калькулятор")]
        public void T03_CalculatorChangePriceTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var сalculatorPage = userPage.GoTo<СalculatorPage>();
            сalculatorPage.CityFrom.SetValueAndSelect("Москва");
            сalculatorPage.Shop.SetValueAndSelect(userShops);
            сalculatorPage.CityTo.SetValueAndSelect("Москва");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();

            сalculatorPage.TableFirst.GetRow(1).Company.WaitText(сompanyName);
            сalculatorPage.TableFirst.GetRow(1).TimeDelivery.WaitText("1 - 2");
            сalculatorPage.TableFirst.GetRow(1).PriceDelivery.WaitText("16.00");
            сalculatorPage.TableFirst.GetRow(1).PricePickup.WaitText("200");

            сalculatorPage.TableSecond.GetRow(1).Company.WaitText(сompanyName);
            сalculatorPage.TableSecond.GetRow(1).TimeDelivery.WaitText("1 - 2");
            сalculatorPage.TableSecond.GetRow(1).PriceDelivery.WaitText("14.00");
            сalculatorPage.TableSecond.GetRow(1).PricePickup.WaitText("200");

            сalculatorPage.Weight.SetValueAndWait("9.1");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();

            сalculatorPage.TableFirst.GetRow(1).Company.WaitText(сompanyName);
            сalculatorPage.TableFirst.GetRow(1).TimeDelivery.WaitText("1 - 2");
            сalculatorPage.TableFirst.GetRow(1).PriceDelivery.WaitText("52.00");
            сalculatorPage.TableFirst.GetRow(1).PricePickup.WaitText("200");

            сalculatorPage.TableSecond.GetRow(1).Company.WaitText(сompanyName);
            сalculatorPage.TableSecond.GetRow(1).TimeDelivery.WaitText("1 - 2");
            сalculatorPage.TableSecond.GetRow(1).PriceDelivery.WaitText("41.00");
            сalculatorPage.TableSecond.GetRow(1).PricePickup.WaitText("200");
        }


        [Test, Description("Калькулятор")]
        public void T04_CalculatorOverWeightTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var сalculatorPage = userPage.GoTo<СalculatorPage>();
            сalculatorPage.CityFrom.SetValueAndSelect("Москва");
            сalculatorPage.Shop.SetValueAndSelect(userShops);
            сalculatorPage.CityTo.SetValueAndSelect("Москва");
            сalculatorPage.Weight.SetValueAndWait("9.1");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();

            сalculatorPage.TableFirst.GetRow(1).Company.WaitText(сompanyName);
            сalculatorPage.TableSecond.GetRow(1).Company.WaitText(сompanyName);

            сalculatorPage.Weight.SetValueAndWait("10.0");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();

            сalculatorPage.TableFirst.GetRow(1).Company.WaitText(сompanyName);
            сalculatorPage.TableSecond.GetRow(1).Company.WaitText(сompanyName);

            сalculatorPage.Weight.SetValueAndWait("10.01");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
//            проверяем что нет таблиц
            if (сalculatorPage.TableFirst.IsPresent)
//    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == сalculatorPage.TableFirst.GetRow(1).Company.GetText());

            //            проверяем что нет таблиц
            if (сalculatorPage.TableSecond.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == сalculatorPage.TableSecond.GetRow(1).Company.GetText());
        }

        [Test, Description("Калькулятор")]
        public void T05_CalculatorOverSideTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var сalculatorPage = userPage.GoTo<СalculatorPage>();
            сalculatorPage.CityFrom.SetValueAndSelect("Москва");
            сalculatorPage.Shop.SetValueAndSelect(userShops);
            сalculatorPage.CityTo.SetValueAndSelect("Москва");
            сalculatorPage.DimensionSide1.SetValueAndWait("9.1");
            сalculatorPage.DimensionSide2.SetValueAndWait("9.1");
            сalculatorPage.DimensionSide3.SetValueAndWait("9.1");

            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();

            сalculatorPage.TableFirst.GetRow(1).Company.WaitText(сompanyName);
            сalculatorPage.TableSecond.GetRow(1).Company.WaitText(сompanyName);

            сalculatorPage.DimensionSide1.SetValueAndWait("80.0");
            сalculatorPage.DimensionSide2.SetValueAndWait("100.0");
            сalculatorPage.DimensionSide3.SetValueAndWait("50.0");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();

            сalculatorPage.TableFirst.GetRow(1).Company.WaitText(сompanyName);
            сalculatorPage.TableSecond.GetRow(1).Company.WaitText(сompanyName);

            сalculatorPage.DimensionSide1.SetValueAndWait("80.1");
            сalculatorPage.DimensionSide2.SetValueAndWait("100.0");
            сalculatorPage.DimensionSide3.SetValueAndWait("50.0");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
            //            проверяем что нет таблиц
            if (сalculatorPage.TableFirst.IsPresent)
            //    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == сalculatorPage.TableFirst.GetRow(1).Company.GetText());

            //            проверяем что нет таблиц
            if (сalculatorPage.TableSecond.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == сalculatorPage.TableSecond.GetRow(1).Company.GetText());

            сalculatorPage.DimensionSide1.SetValueAndWait("80.0");
            сalculatorPage.DimensionSide2.SetValueAndWait("100.1");
            сalculatorPage.DimensionSide3.SetValueAndWait("50.0");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
            //            проверяем что нет таблиц
            if (сalculatorPage.TableFirst.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == сalculatorPage.TableFirst.GetRow(1).Company.GetText());

            //            проверяем что нет таблиц
            if (сalculatorPage.TableSecond.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == сalculatorPage.TableSecond.GetRow(1).Company.GetText());

            сalculatorPage.DimensionSide1.SetValueAndWait("80.0");
            сalculatorPage.DimensionSide2.SetValueAndWait("100.0");
            сalculatorPage.DimensionSide3.SetValueAndWait("50.1");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
            //            проверяем что нет таблиц
            if (сalculatorPage.TableFirst.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == сalculatorPage.TableFirst.GetRow(1).Company.GetText());

            //            проверяем что нет таблиц
            if (сalculatorPage.TableSecond.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == сalculatorPage.TableSecond.GetRow(1).Company.GetText());
        }
    }
}