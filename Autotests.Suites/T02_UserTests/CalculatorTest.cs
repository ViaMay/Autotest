using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class CalculatorTest : ConstantVariablesTestBase
    {
        [Test, Description("Создания Склада для тестов на калькулятор")]
        public void T01_CreateWarehouseTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserWarehouses.Click();
            var warehousesListPage = userPage.GoTo<UserWarehousesPage>();

            if (!warehousesListPage.Table.GetRow(0).Name.IsPresent)
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
                warehouseCreatePage.City.SetFirstValueSelect("Москва");

                for (int i = 0; i < 7; i++)
                {
                    warehouseCreatePage.GetDay(i).FromHour.SetValueAndWait("1:12");
                    warehouseCreatePage.GetDay(i).ToHour.SetValueAndWait("23:23");
                }

                warehouseCreatePage.CreateButton.Click();
                warehousesListPage = warehouseCreatePage.GoTo<UserWarehousesPage>();
            }
            warehousesListPage.Table.GetRow(0).Name.WaitPresence();
        }

        [Test, Description("Создания магазина для тестов на калькулятор")]
        public void T02_CreateShopTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();

            while (shopsListPage.Table.GetRow(0).Name.IsPresent)
            {
                shopsListPage.Table.GetRow(0).ActionsDelete.Click();
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
            shopsListPage.Table.GetRow(0).Name.WaitPresence();
        }

        [Test, Description("Калькулятор. Проверяем, что цена меняется в зависимости от указанного веса")]
        public void T03_CalculatorChangePriceTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var сalculatorPage = userPage.GoTo<СalculatorPage>();
            сalculatorPage.CityFrom.SetFirstValueSelect("Москва");
            сalculatorPage.Shop.SetFirstValueSelect(userShops);
            сalculatorPage.CityTo.SetFirstValueSelect("Москва");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();

            сalculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            сalculatorPage.TableFirst.GetRow(0).TimeDelivery.WaitText("1 - 2");
            сalculatorPage.TableFirst.GetRow(0).PriceDelivery.WaitText("16.00");
            сalculatorPage.TableFirst.GetRow(0).PricePickup.WaitText("200");

            сalculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);
            сalculatorPage.TableSecond.GetRow(0).TimeDelivery.WaitText("1 - 2");
            сalculatorPage.TableSecond.GetRow(0).PriceDelivery.WaitText("14.00");
            сalculatorPage.TableSecond.GetRow(0).PricePickup.WaitText("200");

            сalculatorPage.Weight.SetValueAndWait("9.1");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();

            сalculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            сalculatorPage.TableFirst.GetRow(0).TimeDelivery.WaitText("1 - 2");
            сalculatorPage.TableFirst.GetRow(0).PriceDelivery.WaitText("52.00");
            сalculatorPage.TableFirst.GetRow(0).PricePickup.WaitText("200");

            сalculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);
            сalculatorPage.TableSecond.GetRow(0).TimeDelivery.WaitText("1 - 2");
            сalculatorPage.TableSecond.GetRow(0).PriceDelivery.WaitText("41.00");
            сalculatorPage.TableSecond.GetRow(0).PricePickup.WaitText("200");
        }


        [Test, Description("Калькулятор. Проверяем, что не находит нашу компанию если вес превышает допустимый")]
        public void T04_CalculatorOverWeightTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var сalculatorPage = userPage.GoTo<СalculatorPage>();
            сalculatorPage.CityFrom.SetFirstValueSelect("Москва");
            сalculatorPage.Shop.SetFirstValueSelect(userShops);
            сalculatorPage.CityTo.SetFirstValueSelect("Москва");
            
            сalculatorPage.Weight.SetValueAndWait("9.1");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
            сalculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            сalculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            сalculatorPage.Weight.SetValueAndWait("10.0");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
            сalculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            сalculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            сalculatorPage.Weight.SetValueAndWait("10.01");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
//            проверяем что нет таблиц
            if (сalculatorPage.TableFirst.IsPresent)
//    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == сalculatorPage.TableFirst.GetRow(0).Company.GetText());

            //            проверяем что нет таблиц
            if (сalculatorPage.TableSecond.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.False(сompanyName == сalculatorPage.TableSecond.GetRow(0).Company.GetText());
        }

        [Test, Description("Калькулятор. Проверяем, что не находит нашу компанию если размеры превышают допустимые")]
        public void T05_CalculatorOverSideTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var сalculatorPage = userPage.GoTo<СalculatorPage>();
            сalculatorPage.CityFrom.SetFirstValueSelect("Москва");
            сalculatorPage.Shop.SetFirstValueSelect(userShops);
            сalculatorPage.CityTo.SetFirstValueSelect("Москва");
            
            сalculatorPage.Width.SetValueAndWait("9.1");
            сalculatorPage.Height.SetValueAndWait("9.1");
            сalculatorPage.Length.SetValueAndWait("9.1");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
            сalculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            сalculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            сalculatorPage.Width.SetValueAndWait("80.0");
            сalculatorPage.Height.SetValueAndWait("100.0");
            сalculatorPage.Length.SetValueAndWait("50.0");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
            сalculatorPage.TableFirst.GetRow(0).Company.WaitText(сompanyName);
            сalculatorPage.TableSecond.GetRow(0).Company.WaitText(сompanyName);

            сalculatorPage.Width.SetValueAndWait("80.1");
            сalculatorPage.Height.SetValueAndWait("100.0");
            сalculatorPage.Length.SetValueAndWait("50.0");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
            //            проверяем что нет таблиц
            if (сalculatorPage.TableFirst.IsPresent)
            //    если все таки есть, то проверяем что нет нашей компании
                Assert.True(сompanyName != сalculatorPage.TableFirst.GetRow(0).Company.GetText());
            //            проверяем что нет таблиц
            if (сalculatorPage.TableSecond.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.True(сompanyName != сalculatorPage.TableSecond.GetRow(0).Company.GetText());

            сalculatorPage.Width.SetValueAndWait("80.0");
            сalculatorPage.Height.SetValueAndWait("100.1");
            сalculatorPage.Length.SetValueAndWait("50.0");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
            //            проверяем что нет таблиц
            if (сalculatorPage.TableFirst.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.True(сompanyName != сalculatorPage.TableFirst.GetRow(0).Company.GetText());
            //            проверяем что нет таблиц
            if (сalculatorPage.TableSecond.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.True(сompanyName != сalculatorPage.TableSecond.GetRow(0).Company.GetText());

            сalculatorPage.Width.SetValueAndWait("80.0");
            сalculatorPage.Height.SetValueAndWait("100.0");
            сalculatorPage.Length.SetValueAndWait("50.1");
            сalculatorPage.СountedButton.Click();
            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
            //            проверяем что нет таблиц
            if (сalculatorPage.TableFirst.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.True(сompanyName != сalculatorPage.TableFirst.GetRow(0).Company.GetText());
            //            проверяем что нет таблиц
            if (сalculatorPage.TableSecond.IsPresent)
                //    если все таки есть, то проверяем что нет нашей компании
                Assert.True(сompanyName != сalculatorPage.TableSecond.GetRow(0).Company.GetText());
        }

        [Test, Description("Провека сообщений об ошибках для автокомплитов стран и магазина")]
        public void T06_CalculatorValidationCityAndShopTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var сalculatorPage = userPage.GoTo<СalculatorPage>();

            сalculatorPage.СountedButton.Click();

            сalculatorPage.AlertErrorText[0].WaitText("Магазин обязательно к заполнению");
            сalculatorPage.AlertErrorText[1].WaitText("Город получения обязательно к заполнению");
            сalculatorPage.AlertErrorText[2].WaitText("Город отправления обязательно к заполнению");

            сalculatorPage.CityFrom.SetFirstValueSelect("Москва");
            сalculatorPage.СountedButton.Click();
            сalculatorPage.AlertErrorText[0].WaitText("Город получения обязательно к заполнению");
            сalculatorPage.AlertErrorText[1].WaitAbsence();

            сalculatorPage.CityFromConbobox.Remove.Click();
            сalculatorPage.CityTo.SetFirstValueSelect("Москва");
            сalculatorPage.СountedButton.Click();
            сalculatorPage.AlertErrorText[0].WaitText("Магазин обязательно к заполнению");
            сalculatorPage.AlertErrorText[1].WaitText("Город отправления обязательно к заполнению");
            сalculatorPage.AlertErrorText[2].WaitAbsence();

            сalculatorPage.Shop.SetFirstValueSelect(userShops);
            сalculatorPage.СountedButton.Click();
            сalculatorPage.AlertErrorText[0].WaitText("Город отправления обязательно к заполнению");
            сalculatorPage.AlertErrorText[1].WaitAbsence();
        }

        [Test, Description("Провека сообщений об ошибках для размера")]
        public void T07_CalculatorValidationSizeTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var сalculatorPage = userPage.GoTo<СalculatorPage>();

            сalculatorPage.CityFrom.SetFirstValueSelect("Москва");
            сalculatorPage.CityTo.SetFirstValueSelect("Москва");
            сalculatorPage.Shop.SetFirstValueSelect(userShops);
            сalculatorPage.DeclaredPrice.SetValueAndWait("");
            сalculatorPage.Weight.SetValueAndWait("");
            сalculatorPage.Width.SetValueAndWait("");
            сalculatorPage.Height.SetValueAndWait("");
            сalculatorPage.Length.SetValueAndWait("");

            сalculatorPage.СountedButton.Click();

            сalculatorPage.DeclaredPrice.WaitText("");
            сalculatorPage.Weight.WaitText("");
            сalculatorPage.Width.WaitText("");
            сalculatorPage.WidthErrorText.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            сalculatorPage.Height.WaitText("");
            сalculatorPage.HeightErrorText.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
            сalculatorPage.Length.WaitText("");
            сalculatorPage.LengthErrorText.WaitText("Превышены возможные размеры или вес отправления для данного ПВЗ");
        }
    }
}