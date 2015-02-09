using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ClearTests
{
    public class ClearTestTest : ConstantVariablesTestBase
    {
        public override void SetUp()
        {
            base.SetUp();
        }

        [Test, Description("Удаляем usera")]
        public void T01_DeleteUser()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.Users.Click();
            var usersPage = adminPage.GoTo<UsersPage>();
            usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();
            while (usersPage.UsersTable.GetRow(1).UserEmail.IsPresent)
            {
                usersPage.UsersTable.GetRow(1).ActionsDelete.Click();
                usersPage = usersPage.GoTo<UsersPage>();
                usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
                usersPage = usersPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаляем скалды usera")]
        public void T02_DeleteUserWarehouse()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersWarehouses.Click();
            var warehousesPage = adminPage.GoTo<UsersWarehousesPage>();
            warehousesPage.Table.RowSearch.Name.SetValue(userWarehouses);
            warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            while (warehousesPage.Table.GetRow(1).Name.IsPresent)
            {
                warehousesPage.Table.GetRow(1).ActionsDelete.Click();
                warehousesPage = warehousesPage.GoTo<UsersWarehousesPage>();
                warehousesPage.Table.RowSearch.Name.SetValue(userWarehouses);
                warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаляем магазины usera")]
        public void T03_DeleteUserShop()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersShops.Click();
            var shopsPage = adminPage.GoTo<UsersShopsPage>();
            shopsPage.Table.RowSearch.Name.SetValue(userShops);
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            while (shopsPage.Table.GetRow(1).Name.IsPresent)
            {
                shopsPage.Table.GetRow(1).ActionsDelete.Click();
                shopsPage = shopsPage.GoTo<UsersShopsPage>();
                shopsPage.Table.RowSearch.Name.SetValue(userShops);
                shopsPage = shopsPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("удаление юр лица")]
        public void T04_DeleteLegalEntities()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminReference.Click();
            adminPage.LegalEntities.Click();
            var legalEntitiesPage = adminPage.GoTo<LegalEntitiesPage>();

            legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
            legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();

            while (legalEntitiesPage.Table.GetRow(1).Name.IsPresent)
            {
                legalEntitiesPage.Table.GetRow(1).ActionsDelete.Click();
                legalEntitiesPage = legalEntitiesPage.GoTo<LegalEntitiesPage>();
                legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
                legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("удаление компании")]
        public void T05_DeleteCompany()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminСompanies.Click();
            adminPage.Сompanies.Click();
            var companiesPage = adminPage.GoTo<СompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(сompanyName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(1).Name.IsPresent)
            {
                companiesPage.Table.GetRow(1).ActionsDelete.Click();
                companiesPage = companiesPage.GoTo<СompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(сompanyName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("удаление точки доставки")]
        public void T06_DeleteDeliveryPoint()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminСompanies.Click();
            adminPage.DeliveryPoints.Click();
            var deliveryPointsPage = adminPage.GoTo<DeliveryPointsPage>();
            deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
            deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();

            while (deliveryPointsPage.Table.GetRow(1).Name.IsPresent)
            {
                deliveryPointsPage.Table.GetRow(1).ActionsDelete.Click();
                deliveryPointsPage = deliveryPointsPage.GoTo<DeliveryPointsPage>();
                deliveryPointsPage.Table.RowSearch.Name.SetValue(deliveryPointName);
                deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();
            }
        }
    }
}