using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartTests
{
    public class T03CreateWarehouseAndShopTest : ConstantVariablesTestBase
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
    }
}