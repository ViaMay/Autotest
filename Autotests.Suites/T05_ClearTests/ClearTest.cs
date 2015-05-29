using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T05_ClearTests
{
    public class ClearTestTest : ConstVariablesTestBase
    {

        [Test, Description("Удаление документов")]
        public void T01_DeleteDocumentsTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            var documentsPage = LoadPage<DocumentsPage>("admin/documents/?&filters[warehouse]=" + userWarehouseName);
            while (documentsPage.Table.GetRow(0).ID.IsPresent)
            {
                var id = documentsPage.Table.GetRow(0).ID.GetText();
                documentsPage = LoadPage<DocumentsPage>("admin/documents/delete/" + id);
                documentsPage = LoadPage<DocumentsPage>("admin/documents/?&filters[warehouse]=" + userWarehouseName);
            }
        }

        [Test, Description("Удаление заявок")]
        public void T02_DeleteOrdersTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders?&filters[shop]=" + userShopName);
            while (оrdersInputPage.Table.GetRow(0).ID.IsPresent)
            {
                var id = оrdersInputPage.Table.GetRow(0).ID.GetText();
                оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/delete/" + id);
                оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders?&filters[shop]=" + userShopName);
            }

            var оrdersOutputPage = LoadPage<OrdersOutputPage>("admin/outgoingorders?&filters[company]=" + companyName);
            while (оrdersOutputPage.Table.GetRow(0).ID.IsPresent)
            {
                var id = оrdersOutputPage.Table.GetRow(0).ID.GetText();
                оrdersOutputPage = LoadPage<OrdersOutputPage>("admin/outgoingorders/delete/" + id);
                оrdersOutputPage = LoadPage<OrdersOutputPage>("admin/outgoingorders?&filters[company]=" + companyName);
            }

            var оrdersPickupPage = LoadPage<OrdersPickupPage>("admin/pickuporders?&filters[pickup_company]=" + companyPickupName);
            while (оrdersPickupPage.Table.GetRow(0).ID.IsPresent)
            {
                var id = оrdersPickupPage.Table.GetRow(0).ID.GetText();
                оrdersPickupPage = LoadPage<OrdersPickupPage>("admin/pickuporders/delete/" + id);
                оrdersPickupPage = LoadPage<OrdersPickupPage>("admin/pickuporders?&filters[pickup_company]=" + companyPickupName);
            }
        }

        [Test, Description("Удаляем склады ценны")]
        public void T03_DeletePriceTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Prices.Mouseover();
            adminPage.PricesPickup.Click();
            var pricesPickupPage = adminPage.GoTo<PricesPickupPage>();
            pricesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();

            while (pricesPickupPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesPickupPage.Table.GetRow(0).ActionsDelete.Click();
                pricesPickupPage.Aletr.Accept();
                pricesPickupPage = pricesPickupPage.GoTo<PricesPickupPage>();
                pricesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();
            }

            pricesPickupPage.Table.RowSearch.CompanyName.SetValue(companyName);
            pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();

            while (pricesPickupPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesPickupPage.Table.GetRow(0).ActionsDelete.Click();
                pricesPickupPage.Aletr.Accept();
                pricesPickupPage = pricesPickupPage.GoTo<PricesPickupPage>();
                pricesPickupPage.Table.RowSearch.CompanyName.SetValue(companyName);
                pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();
            }
            pricesPickupPage.AdminCompanies.Click();
            pricesPickupPage.Prices.Mouseover();
            pricesPickupPage.PricesCourier.Click();
            var pricesCourierPage = adminPage.GoTo<PricesCourierPage>();
            pricesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
            pricesCourierPage = pricesCourierPage.SeachButtonRowClickAndGo();

            while (pricesCourierPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesCourierPage.Table.GetRow(0).ActionsDelete.Click();
                pricesCourierPage.Aletr.Accept();
                pricesCourierPage = pricesCourierPage.GoTo<PricesCourierPage>();
                pricesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
                pricesCourierPage = pricesCourierPage.SeachButtonRowClickAndGo();
            }

            pricesCourierPage.AdminCompanies.Click();
            pricesCourierPage.Prices.Mouseover();
            pricesCourierPage.PricesSelf.Click();
            var pricesSelfPage = adminPage.GoTo<PricesSelfPage>();
            pricesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
            pricesSelfPage = pricesSelfPage.SeachButtonRowClickAndGo();

            while (pricesSelfPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesSelfPage.Table.GetRow(0).ActionsDelete.Click();
                pricesSelfPage.Aletr.Accept();
                pricesSelfPage = pricesSelfPage.GoTo<PricesSelfPage>();
                pricesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
                pricesSelfPage = pricesSelfPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаляем графики")]
        public void T04_DeleteTimesTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Times.Mouseover();
            adminPage.TimesPickup.Click();
            var timesPickupPage = adminPage.GoTo<TimesPickupPage>();
            timesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            timesPickupPage = timesPickupPage.SeachButtonRowClickAndGo();

            while (timesPickupPage.Table.GetRow(0).Name.IsPresent)
            {
                timesPickupPage.Table.GetRow(0).ActionsDelete.Click();
                timesPickupPage.Aletr.Accept();
                timesPickupPage = timesPickupPage.GoTo<TimesPickupPage>();
                timesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                timesPickupPage = timesPickupPage.SeachButtonRowClickAndGo();
            }

            timesPickupPage.AdminCompanies.Click();
            timesPickupPage.Times.Mouseover();
            timesPickupPage.TimesCourier.Click();
            var timesCourierPage = adminPage.GoTo<TimesPage>();
            timesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
            timesCourierPage = timesCourierPage.SeachButtonRowClickAndGo();

            while (timesCourierPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                timesCourierPage.Table.GetRow(0).ActionsDelete.Click();
                timesPickupPage.Aletr.Accept();
                timesCourierPage = timesCourierPage.GoTo<TimesPage>();
                timesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
                timesCourierPage = timesCourierPage.SeachButtonRowClickAndGo();
            }
            timesCourierPage.AdminCompanies.Click();
            timesCourierPage.Times.Mouseover();
            timesCourierPage.TimesSelf.Click();
            var timesSelfPage = adminPage.GoTo<TimesPage>();
            timesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
            timesSelfPage = timesSelfPage.SeachButtonRowClickAndGo();

            while (timesSelfPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                timesSelfPage.Table.GetRow(0).ActionsDelete.Click();
                timesPickupPage.Aletr.Accept();
                timesSelfPage = timesSelfPage.GoTo<TimesPage>();
                timesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
                timesSelfPage = timesSelfPage.SeachButtonRowClickAndGo();
            }
        }
        [Test, Description("удаление точки доставки")]
        public void T05_DeleteDeliveryPointTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.DeliveryPoints.Click();
            var deliveryPointsPage = adminPage.GoTo<DeliveryPointsPage>();
            deliveryPointsPage.Table.RowSearch.CompanyName.SetValue(companyName);
            deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();

            while (deliveryPointsPage.Table.GetRow(0).Name.IsPresent)
            {
                deliveryPointsPage.Table.GetRow(0).ActionsDelete.Click();
                deliveryPointsPage.Aletr.Accept();
                deliveryPointsPage = deliveryPointsPage.GoTo<DeliveryPointsPage>();
                deliveryPointsPage.Table.RowSearch.CompanyName.SetValue(companyName);
                deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаление календаря")]
        public void T06_DeleteCalendarsTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            //            удаление календаря если он был
            adminPage.AdminCompanies.Click();
            adminPage.Calendars.Click();
            var calendarsPage = adminPage.GoTo<CalendarsPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            while (calendarsPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                calendarsPage.Table.GetRow(0).ActionsDelete.Click();
                calendarsPage.Aletr.Accept();
                calendarsPage = calendarsPage.GoTo<CalendarsPage>();
                calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
                calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаление графика забора")]
        public void T07_DeletePickupTimetableTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            //            удаление графика забора если он был
            adminPage.AdminCompanies.Click();
            adminPage.PickupTimetable.Click();
            var pickupTimetablePage = adminPage.GoTo<PickupTimetablePage>();
            pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            while (pickupTimetablePage.Table.GetRow(0).Name.IsPresent)
            {
                pickupTimetablePage.Table.GetRow(0).ActionsDelete.Click();
                pickupTimetablePage.Aletr.Accept();
                pickupTimetablePage = pickupTimetablePage.GoTo<PickupTimetablePage>();
                pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            }

            pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyName);
            pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            while (pickupTimetablePage.Table.GetRow(0).Name.IsPresent)
            {
                pickupTimetablePage.Table.GetRow(0).ActionsDelete.Click();
                pickupTimetablePage = pickupTimetablePage.GoTo<PickupTimetablePage>();
                pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyName);
                pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            }
        }


        [Test, Description("Удаления веса")]
        public void T08_DeleteWeightTest()
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
                intervalsWeightPage.Aletr.Accept();
                intervalsWeightPage = intervalsWeightPage.GoTo<IntervalsWeightPage>();
                intervalsWeightPage.Table.RowSearch.Name.SetValue(weightName);
                intervalsWeightPage = intervalsWeightPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаление размера")]
        public void T09_DeleteSizeTest()
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
                intervalsSizePage.Aletr.Accept();
                intervalsSizePage = intervalsSizePage.GoTo<IntervalsSidePage>();
                intervalsSizePage.Table.RowSearch.Name.SetValue(sideName);
                intervalsSizePage = intervalsSizePage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("удаление компании")]
        public void T10_DeleteCompanyTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<CompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(0).Name.IsPresent)
            {
                companiesPage.Table.GetRow(0).ActionsDelete.Click();
                companiesPage.Aletr.Accept();
                companiesPage = companiesPage.GoTo<CompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }

            companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(0).Name.IsPresent)
            {
                companiesPage.Table.GetRow(0).ActionsDelete.Click();
                companiesPage.Aletr.Accept();
                companiesPage = companiesPage.GoTo<CompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
        }
        [Test, Description("Удаляем магазины usera")]
        public void T11_DeleteUserShopTest()
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
                shopsPage.Aletr.Accept();
                shopsPage = shopsPage.GoTo<UsersShopsPage>();
                shopsPage.Table.RowSearch.Name.SetValue(userShopName);
                shopsPage = shopsPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаляем склады usera")]
        public void T12_DeleteUserWarehouseTest()
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
                warehousesPage.Aletr.Accept();
                warehousesPage = warehousesPage.GoTo<UsersWarehousesPage>();
                warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName);
                warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            }
        }
        
        [Test, Description("Удаляем usera")]
        public void T13_DeleteUserTest()
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
                usersPage.Aletr.Accept();
                usersPage = usersPage.GoTo<UsersPage>();
                usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
                usersPage = usersPage.SeachButtonRowClickAndGo();
            }
            usersPage.UsersTable.RowSearch.UserEmail.SetValue(pickupNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();
            while (usersPage.UsersTable.GetRow(0).UserEmail.IsPresent)
            {
                usersPage.UsersTable.GetRow(0).ActionsDelete.Click();
                usersPage.Aletr.Accept();
                usersPage = usersPage.GoTo<UsersPage>();
                usersPage.UsersTable.RowSearch.UserEmail.SetValue(pickupNameAndPass);
                usersPage = usersPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("удаление юр лица")]
        public void T14_DeleteLegalEntitiesTest()
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
                legalEntitiesPage.Aletr.Accept();
                legalEntitiesPage = legalEntitiesPage.GoTo<LegalEntitiesPage>();
                legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
                legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();
            }
        }
    }
}