using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class CalculatorTest : ConstantVariablesTestBase
    {
        [Test, Description("Создания Склада")]
        public void T01_CreateWarehouseTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
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
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
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

        [Test, Description("Калькулятор"), Repeat(10)]
        public void T03_CalculatorTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var сalculatorPage = userPage.GoTo<СalculatorPage>();
            сalculatorPage.CityFrom.SetValueAndSelect("Москва");
            сalculatorPage.Shop.SetValueAndSelect(userShops);
            сalculatorPage.CityTo.SetValueAndSelect("Москва");
            сalculatorPage.СountedButton.Click();

            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();

            сalculatorPage.TableSelf.GetRow(1).Company.WaitText(сompanyName);
            сalculatorPage.TableSelf.GetRow(1).TimeDelivery.WaitText("1 - 2");
            сalculatorPage.TableSelf.GetRow(1).PriceDelivery.WaitText("16.00");
            сalculatorPage.TableSelf.GetRow(1).PricePickup.WaitText("200");

            сalculatorPage.TableCourier.GetRow(1).Company.WaitText(сompanyName);
            сalculatorPage.TableCourier.GetRow(1).TimeDelivery.WaitText("1 - 2");
            сalculatorPage.TableCourier.GetRow(1).PriceDelivery.WaitText("14.00");
            сalculatorPage.TableCourier.GetRow(1).PricePickup.WaitText("200");

            сalculatorPage.Weight.SetValueAndWait("9.1");
            сalculatorPage.СountedButton.Click();

            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
            сalculatorPage.TableSelf.GetRow(1).Company.WaitText(сompanyName);
            сalculatorPage.TableSelf.GetRow(1).TimeDelivery.WaitText("1 - 2");
            сalculatorPage.TableSelf.GetRow(1).PriceDelivery.WaitText("52.00");
            сalculatorPage.TableSelf.GetRow(1).PricePickup.WaitText("200");

            сalculatorPage.TableCourier.GetRow(1).Company.WaitText(сompanyName);
            сalculatorPage.TableCourier.GetRow(1).TimeDelivery.WaitText("1 - 2");
            сalculatorPage.TableCourier.GetRow(1).PriceDelivery.WaitText("41.00");
            сalculatorPage.TableCourier.GetRow(1).PricePickup.WaitText("200");
        }


        [Test, Description("Калькулятор")]
        public void T03_CalculatorValidationTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.Calculator.Click();
            var сalculatorPage = userPage.GoTo<СalculatorPage>();
            сalculatorPage.CityFrom.SetValueAndSelect("Москва");
            сalculatorPage.Shop.SetValueAndSelect(userShops);
            сalculatorPage.CityTo.SetValueAndSelect("Москва");
            сalculatorPage.СountedButton.Click();

            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();

            сalculatorPage.СountedButton.Click();

            сalculatorPage = сalculatorPage.GoTo<СalculatorPage>();
            
        }
    }
}