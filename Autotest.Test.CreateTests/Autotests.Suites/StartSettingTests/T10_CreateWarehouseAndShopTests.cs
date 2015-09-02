using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;
//using MySql.Data.MySqlClient; 

namespace Autotests.Tests.StartSettingTests
{
    public class T09CreateWarehouseAndShopTests : ConstVariablesTestBase
    {
        [Test, Description("Создания Склада для тестов на калькулятор")]
        public void T01_CreateWarehouseTest()
        {
//            MySqlConnectionStringBuilder mysqlCSB;
//            mysqlCSB = new MySqlConnectionStringBuilder();
//
//            mysqlCSB.Server = "http://stage.ddelivery.ru/phpmyadmin/index.php";
//            mysqlCSB.Database = "ddelivery_staging";
//            mysqlCSB.UserID = "dd_user";
//            mysqlCSB.Password = "'apron6lousy5foil'";
//            string queryString = @"SSELECT * 
//FROM  shops 
//WHERE  name LIKE  'test_userShops_via'
//LIMIT 0 , 30";
//            MySqlConnection con = new MySqlConnection();
//            con.ConnectionString = mysqlCSB.ConnectionString;
//            con.Open();
//            MySqlCommand com = new MySqlCommand(queryString, con);
//            using (MySqlDataReader dr = com.ExecuteReader())
//            {
//                //есть записи?
//                if (dr.HasRows)
//                {
//                    //заполняем объект DataTable
//                    var d = dr;
//                }
//            }
//                     
//        
//        
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersWarehouses.Click();
            var warehousesPage = adminPage.GoTo<AdminBaseListPage>();
            warehousesPage.LabelDirectory.WaitText(@"Справочник ""Склады""");
            warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName);
            warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            while (warehousesPage.Table.GetRow(0).Name.IsPresent)
            {
                warehousesPage.Table.GetRow(0).ActionsDelete.Click();
                warehousesPage.Aletr.Accept();
                warehousesPage = warehousesPage.GoTo<AdminBaseListPage>();
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
            warehouseCreatePage.PostalCode.SetValueAndWait("555444");
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

            var defaultPage = warehousesListPage.LoginOut();
            adminPage = defaultPage.LoginAsAdmin(adminName, adminPass);
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
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
                shopsPage.Aletr.Accept();
                shopsPage = shopsPage.GoTo<UsersShopsPage>();
                shopsPage.Table.RowSearch.Name.SetValue(userShopName);
                shopsPage = shopsPage.SeachButtonRowClickAndGo();
            }
            var defaultPage = shopsPage.LoginOut();
            var userPage = defaultPage.LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.ShopsCreate.Click();
            var shopCreatePage = shopsListPage.GoTo<UserShopCreatePage>();
            shopCreatePage.Name.SetValueAndWait(userShopName);
            shopCreatePage.Address.SetValueAndWait("Москва");
            shopCreatePage.Warehouse.SelectValue(userWarehouseName);
            shopCreatePage.CreateButton.Click();
            shopsListPage = shopCreatePage.GoTo<UserShopsPage>();
            var row = shopsListPage.Table.FindRowByName(userShopName);

            defaultPage = shopsListPage.LoginOut();
            adminPage = defaultPage.LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersShops.Click();
            shopsPage = adminPage.GoTo<UsersShopsPage>();
            shopsPage = shopCreatePage.GoTo<UsersShopsPage>();
            shopsPage.Table.RowSearch.Name.SetValue(userShopName);
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            shopsPage.Table.GetRow(0).Name.WaitPresence();
            shopsPage.Table.GetRow(0).ActionsEdit.Click();

            var shopEditPage = shopsPage.GoTo<UserAdminShopCreatePage>();
            shopEditPage.CompanyPickup.SetFirstValueSelect(companyPickupName);
            shopEditPage.CreateButton.Click();
            shopsPage = shopEditPage.GoTo<UsersShopsPage>();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
        }
    }
}