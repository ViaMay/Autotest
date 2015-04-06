using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T05_ClearTests
{
    public class ClearTestTest : ConstVariablesTestBase
    {
        [Test, Description("Удаляем usera")]
        public void T01_DeleteUserTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.Users.Click();
            var usersPage = adminPage.GoTo<UsersPage>();
            usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();
            while (usersPage.UsersTable.GetRow(0).UserEmail.IsPresent)
            {
                usersPage.UsersTable.GetRow(0).ActionsDelete.Click();
                usersPage = usersPage.GoTo<UsersPage>();
                usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
                usersPage = usersPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаляем скалды usera")]
        public void T02_DeleteUserWarehouseTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
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
        }

        [Test, Description("Удаляем магазины usera")]
        public void T03_DeleteUserShopTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
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
        }

        [Test, Description("удаление юр лица")]
        public void T04_DeleteLegalEntitiesTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminReference.Click();
            adminPage.LegalEntities.Click();
            var legalEntitiesPage = adminPage.GoTo<LegalEntitiesPage>();

            legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
            legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();

            while (legalEntitiesPage.Table.GetRow(0).Name.IsPresent)
            {
                legalEntitiesPage.Table.GetRow(0).ActionsDelete.Click();
                legalEntitiesPage = legalEntitiesPage.GoTo<LegalEntitiesPage>();
                legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
                legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("удаление компании")]
        public void T05_DeleteCompanyTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminСompanies.Click();
            adminPage.Сompanies.Click();
            var companiesPage = adminPage.GoTo<СompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(0).Name.IsPresent)
            {
                companiesPage.Table.GetRow(0).ActionsDelete.Click();
                companiesPage = companiesPage.GoTo<СompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("удаление точки доставки")]
        public void T06_DeleteDeliveryPointTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminСompanies.Click();
            adminPage.DeliveryPoints.Click();
            var deliveryPointsPage = adminPage.GoTo<DeliveryPointsPage>();
            deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
            deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();

            while (deliveryPointsPage.Table.GetRow(0).Name.IsPresent)
            {
                deliveryPointsPage.Table.GetRow(0).ActionsDelete.Click();
                deliveryPointsPage = deliveryPointsPage.GoTo<DeliveryPointsPage>();
                deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
                deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Создания веса")]
        public void T07_DeleteWeightTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminReference.Click();
            adminPage.Intervals.Mouseover();
            adminPage.IntervalsWeight.Click();
            var intervalsWeightPage = adminPage.GoTo<IntervalsWeightPage>();
            intervalsWeightPage.Table.RowSearch.Name.SetValue(weightName);
            intervalsWeightPage = intervalsWeightPage.SeachButtonRowClickAndGo();

            while (intervalsWeightPage.Table.GetRow(0).Name.IsPresent)
            {
                intervalsWeightPage.Table.GetRow(0).ActionsDelete.Click();
                intervalsWeightPage = intervalsWeightPage.GoTo<IntervalsWeightPage>();
                intervalsWeightPage.Table.RowSearch.Name.SetValue(weightName);
                intervalsWeightPage = intervalsWeightPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаление размера")]
        public void T08_DeleteSizeTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminReference.Click();
            adminPage.Intervals.Mouseover();
            adminPage.IntervalsSize.Click();
            var intervalsSizePage = adminPage.GoTo<IntervalsSidePage>();
            intervalsSizePage.Table.RowSearch.Name.SetValue(sideName);
            intervalsSizePage = intervalsSizePage.SeachButtonRowClickAndGo();

            while (intervalsSizePage.Table.GetRow(0).Name.IsPresent)
            {
                intervalsSizePage.Table.GetRow(0).ActionsDelete.Click();
                intervalsSizePage = intervalsSizePage.GoTo<IntervalsSidePage>();
                intervalsSizePage.Table.RowSearch.Name.SetValue(sideName);
                intervalsSizePage = intervalsSizePage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаление заявок")]
        public void T09_DeleteOrdersTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.Orders.Click();
            adminPage.OrderInput.Click();
            var оrdersInputPage = adminPage.GoTo<OrdersInputPage>();
            оrdersInputPage.Table.RowSearch.ShopName.SetValue(userShopName);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            while (оrdersInputPage.Table.GetRow(0).ID.IsPresent)
            {
                var id = оrdersInputPage.Table.GetRow(0).ID.GetText();
                оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/delete/" + id);
                оrdersInputPage.Table.RowSearch.ShopName.SetValue(userShopName);
                оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаление календаря")]
        public void T09_DeleteCalendarsTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            //            удаление календаря если он был
            adminPage.AdminСompanies.Click();
            adminPage.Calendars.Click();
            var calendarsPage = adminPage.GoTo<CalendarsPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            while (calendarsPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                calendarsPage.Table.GetRow(0).ActionsDelete.Click();
                calendarsPage = calendarsPage.GoTo<CalendarsPage>();
                calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
                calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            }
        }
    }
}