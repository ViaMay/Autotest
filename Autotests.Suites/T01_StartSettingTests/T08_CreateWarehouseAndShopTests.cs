using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
{
    public class T08CreateWarehouseAndShopTests : ConstVariablesTestBase
    {
        [Test, Description("Создания Склада для тестов на калькулятор")]
        public void T01_CreateWarehouseTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersWarehouses.Click();
            var warehousesPage = adminPage.GoTo<UsersWarehousesPage>();
            warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName);
            warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            while (warehousesPage.Table.GetRow(0).Name.IsPresent)
            {
                warehousesPage.Table.GetRow(0).ActionsDelete.Click();
                warehousesPage = warehousesPage.GoTo<UsersWarehousesPage>();
                warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName);
                warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            }
            var defaulPage = warehousesPage.LoginOut();
            UserHomePage userPage = defaulPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserWarehouses.Click();
            var warehousesListPage = userPage.GoTo<UserWarehousesPage>();

            warehousesListPage.WarehousesCreate.Click();
            var warehouseCreatePage = warehousesListPage.GoTo<UserWarehouseCreatePage>();
            warehouseCreatePage.Name.SetValueAndWait(userWarehouseName);
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

            warehousesListPage.Table.GetRow(0).Name.WaitPresence();
        }

        [Test, Description("Создания магазина для тестов на калькулятор")]
        public void T02_CreateShopTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersShops.Click();
            var shopsPage = adminPage.GoTo<UsersShopsPage>();
            shopsPage.Table.RowSearch.Name.SetValue(userShopName);
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            while (shopsPage.Table.GetRow(0).Name.IsPresent)
            {
                shopsPage.Table.GetRow(0).ActionsDelete.Click();
                shopsPage = shopsPage.GoTo<UsersShopsPage>();
                shopsPage.Table.RowSearch.Name.SetValue(userShopName);
                shopsPage = shopsPage.SeachButtonRowClickAndGo();
            }
            shopsPage.ShopsCreate.Click();
            var shopCreatePage = shopsPage.GoTo<UserAdminShopCreatePage>();
            shopCreatePage.Name.SetValueAndWait(userShopName);
            shopCreatePage.Address.SetValueAndWait("Москва");
            shopCreatePage.CompanyPickup.SetFirstValueSelect(companyName);
            shopCreatePage.Warehouse.SetFirstValueSelect(userWarehouseName);
            shopCreatePage.ManagersLegalEntity.SetFirstValueSelect(legalEntityName);
            shopCreatePage.CreateButton.Click();
            shopsPage = shopCreatePage.GoTo<UsersShopsPage>();
            shopsPage.Table.RowSearch.Name.SetValue(userShopName);
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            shopsPage.Table.GetRow(0).Name.WaitPresence();
            shopsPage.Table.GetRow(0).ActionsEdit.Click();

            shopCreatePage = shopsPage.GoTo<UserAdminShopCreatePage>();
            shopCreatePage.CompanyPickup.SetFirstValueSelect(companyName);
            shopCreatePage.CreateButton.Click();
            shopsPage = shopCreatePage.GoTo<UsersShopsPage>();
        }
    }
}