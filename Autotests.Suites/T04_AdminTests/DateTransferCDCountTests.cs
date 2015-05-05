using System;
using System.Globalization;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T04_AdminTests
{
    public class DateTransferCDCountTests : SendOrdersBasePage
    {
        [Test, Description("Проверяем расчет даты передачи в КД если не стоит галочка Единый забор у компании доставки")
        ]
        public void SinglePickupFalseTest()
        {
            //            Ставим нет (галочку) в компании нашей у поля Единый забор
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<CompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();
            string singlePickup = companiesPage.Table.GetRow(0).SinglePickup.GetText();
            if (singlePickup == "Yes")
            {
                companiesPage.Table.GetRow(0).ActionsEdit.Click();
                var companyCreatePage = companiesPage.GoTo<CompanyCreatePage>();
                companyCreatePage.SinglePickup.Click();
                companyCreatePage.SaveButton.Click();
                companiesPage = companyCreatePage.GoTo<CompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
            companiesPage.Table.GetRow(0).SinglePickup.WaitText("No");

            string[] ordersID = SendOrdersRequest();
            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/");
            оrdersInputPage.Table.RowSearch.ID.SetValue(ordersID[0]);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            оrdersInputPage.Table.GetRow(0).ID.WaitText(ordersID[0]);
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();

            var оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            var pickupDate = DateTime.ParseExact(оrderEditPage.PickupDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);
            var transferCDDate = DateTime.ParseExact(оrderEditPage.TransferCDDate.GetValue(), "yyyy-MM-dd HH:mm:ss",
                null);
//            Сравниваем даты они долны быть равны
            Assert.AreEqual(pickupDate, transferCDDate, "Дата передачи в КД должна быть равна Дате заборы");

            оrderEditPage.Orders.Click();
            оrderEditPage.OrderInput.Click();
            оrdersInputPage = оrderEditPage.GoTo<OrdersInputPage>();
            оrdersInputPage.Table.RowSearch.ID.SetValue(ordersID[1]);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            оrdersInputPage.Table.GetRow(0).ID.WaitText(ordersID[1]);
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();

            оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            pickupDate = DateTime.ParseExact(оrderEditPage.PickupDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);
            transferCDDate = DateTime.ParseExact(оrderEditPage.TransferCDDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);
//            Сравниваем даты они долны быть равны
            Assert.AreEqual(pickupDate, transferCDDate, "Дата передачи в КД должна быть равна Дате заборы");
        }

        [Test,
         Description(
             "Проверяем расчет даты передачи в КД если стоит галочка Единый забор у компании доставки и нет календаря")]
        public void SinglePickupTrueNotСalendarTest()
        {
//            Ставим Да (галочку) в компании нашей у поля Единый забор
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<CompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();
            string singlePickup = companiesPage.Table.GetRow(0).SinglePickup.GetText();
            if (singlePickup == "No")
            {
                companiesPage.Table.GetRow(0).ActionsEdit.Click();
                var companyCreatePage = companiesPage.GoTo<CompanyCreatePage>();
                companyCreatePage.SinglePickup.Click();
                companyCreatePage.SaveButton.Click();
                companiesPage = companyCreatePage.GoTo<CompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
            companiesPage.Table.GetRow(0).SinglePickup.WaitText("Yes");

//           удаляем календарь если он был
            //создаем в каледнаре выходной на завтрашнию дату у этой компании доставки
            companiesPage.AdminCompanies.Click();
            companiesPage.Calendars.Click();
            var calendarsPage = companiesPage.GoTo<CalendarsPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            while (calendarsPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                calendarsPage.Table.GetRow(0).ActionsDelete.Click();
                calendarsPage = calendarsPage.GoTo<CalendarsPage>();
                calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
                calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            }

            string[] ordersID = SendOrdersRequest();
            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/");
            оrdersInputPage.Table.RowSearch.ID.SetValue(ordersID[0]);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            оrdersInputPage.Table.GetRow(0).ID.WaitText(ordersID[0]);
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();

            var оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            DateTime pickupDate = DateTime.ParseExact(оrderEditPage.PickupDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);
            DateTime transferCDDate = DateTime.ParseExact(оrderEditPage.TransferCDDate.GetValue(), "yyyy-MM-dd HH:mm:ss",
                null);

//            пребовляем два дня к pickupDate так как по формуле следующий день который не выходной
            Assert.AreEqual(pickupDate.AddDays(1), transferCDDate, "Дата передачи в КД должна быть равна следующему рабочему" +
                                                                   " дню по календарю компании доставки от Даты заборы");

            оrderEditPage.Orders.Click();
            оrderEditPage.OrderInput.Click();
            оrdersInputPage = оrderEditPage.GoTo<OrdersInputPage>();
            оrdersInputPage.Table.RowSearch.ID.SetValue(ordersID[1]);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            оrdersInputPage.Table.GetRow(0).ID.WaitText(ordersID[1]);
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();

            оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            pickupDate = DateTime.ParseExact(оrderEditPage.PickupDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);
            transferCDDate = DateTime.ParseExact(оrderEditPage.TransferCDDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);

            //            пребовляем два дня к pickupDate так как по формуле следующий день который не выходной
            Assert.AreEqual(pickupDate.AddDays(1), transferCDDate, "Дата передачи в КД должна быть равна следующему рабочему" +
                                                                   " дню по календарю компании доставки от Даты заборы");
        }

        [Test,
         Description(
             "Проверяем расчет даты передачи в КД если стоит галочка Единый забор у компании доставки и есть календаря")]
        public void SinglePickupTrueYesСalendarTest()
        {
            //            Ставим Да (галочку) в компании нашей у поля Единый забор
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<CompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();
            string singlePickup = companiesPage.Table.GetRow(0).SinglePickup.GetText();
            if (singlePickup == "No")
            {
                companiesPage.Table.GetRow(0).ActionsEdit.Click();
                var companyCreatePage = companiesPage.GoTo<CompanyCreatePage>();
                companyCreatePage.SinglePickup.Click();
                companyCreatePage.SaveButton.Click();
                companiesPage = companyCreatePage.GoTo<CompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
            companiesPage.Table.GetRow(0).SinglePickup.WaitText("Yes");

            //           удаляем календарь если он был
            //создаем в каледнаре выходной на завтрашнию дату у этой компании доставки
            companiesPage.AdminCompanies.Click();
            companiesPage.Calendars.Click();
            var calendarsPage = companiesPage.GoTo<CalendarsPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            while (calendarsPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                calendarsPage.Table.GetRow(0).ActionsDelete.Click();
                calendarsPage = calendarsPage.GoTo<CalendarsPage>();
                calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
                calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            }
            calendarsPage.CalendarsCreate.Click();
//                        текущий день плюс два деня в формате "dd.MM.yyyy" так как следующий день- день забора
            string nowDateString = nowDate.AddDays(2).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            var calendarCreatePage = calendarsPage.GoTo<CalendarCreatePage>();
            calendarCreatePage.Type.SelectByText("Доставка");
            calendarCreatePage.Company.SetFirstValueSelect(companyName);
            calendarCreatePage.Date.SetValueAndWait(nowDateString);
            calendarCreatePage.SaveButton.Click();
            calendarsPage = calendarCreatePage.GoTo<CalendarsPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            calendarsPage.Table.GetRow(0).ColumnThree.WaitText(companyName);

            string[] ordersID = SendOrdersRequest();
            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/");
            оrdersInputPage.Table.RowSearch.ID.SetValue(ordersID[0]);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            оrdersInputPage.Table.GetRow(0).ID.WaitText(ordersID[0]);
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();

            var оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            DateTime pickupDate = DateTime.ParseExact(оrderEditPage.PickupDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);
            DateTime transferCDDate = DateTime.ParseExact(оrderEditPage.TransferCDDate.GetValue(), "yyyy-MM-dd HH:mm:ss",
                null);

            //            пребовляем два дня к pickupDate так как по формуле следующий день который не выходной
                        Assert.AreEqual(pickupDate.AddDays(2), transferCDDate, "Дата передачи в КД должна быть равна следующему рабочему" +
                                                                               " дню по календарю компании доставки от Даты заборы");

            оrderEditPage.Orders.Click();
            оrderEditPage.OrderInput.Click();
            оrdersInputPage = оrderEditPage.GoTo<OrdersInputPage>();
            оrdersInputPage.Table.RowSearch.ID.SetValue(ordersID[1]);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            оrdersInputPage.Table.GetRow(0).ID.WaitText(ordersID[1]);
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();

            оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            pickupDate = DateTime.ParseExact(оrderEditPage.PickupDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);
            transferCDDate = DateTime.ParseExact(оrderEditPage.TransferCDDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);

//                        пребовляем два дня к pickupDate так как по формуле следующий день который не выходной
                        Assert.AreEqual(pickupDate.AddDays(2), transferCDDate, "Дата передачи в КД должна быть равна следующему рабочему" +
                                                                               " дню по календарю компании доставки от Даты заборы");
        }

        [Test,
         Description(
             "Проверяем расчет даты передачи в КД если стоит галочка Единый забор у компании доставки и есть календаря на день забора")]
        public void SinglePickupTrueYesСalendarTest02()
        {
            //            Ставим Да (галочку) в компании нашей у поля Единый забор
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<CompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();
            string singlePickup = companiesPage.Table.GetRow(0).SinglePickup.GetText();
            if (singlePickup == "No")
            {
                companiesPage.Table.GetRow(0).ActionsEdit.Click();
                var companyCreatePage = companiesPage.GoTo<CompanyCreatePage>();
                companyCreatePage.SinglePickup.Click();
                companyCreatePage.SaveButton.Click();
                companiesPage = companyCreatePage.GoTo<CompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
            companiesPage.Table.GetRow(0).SinglePickup.WaitText("Yes");

            //           удаляем календарь если он был
            //создаем в каледнаре выходной на завтрашнию дату у этой компании доставки
            companiesPage.AdminCompanies.Click();
            companiesPage.Calendars.Click();
            var calendarsPage = companiesPage.GoTo<CalendarsPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            while (calendarsPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                calendarsPage.Table.GetRow(0).ActionsDelete.Click();
                calendarsPage = calendarsPage.GoTo<CalendarsPage>();
                calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
                calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            }
            calendarsPage.CalendarsCreate.Click();
            //                        текущий день плюс один день в формате "dd.MM.yyyy"
            string nowDateString = nowDate.AddDays(1).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            var calendarCreatePage = calendarsPage.GoTo<CalendarCreatePage>();
            calendarCreatePage.Type.SelectByText("Доставка");
            calendarCreatePage.Company.SetFirstValueSelect(companyName);
            calendarCreatePage.Date.SetValueAndWait(nowDateString);
            calendarCreatePage.SaveButton.Click();
            calendarsPage = calendarCreatePage.GoTo<CalendarsPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            calendarsPage.Table.GetRow(0).ColumnThree.WaitText(companyName);

            string[] ordersID = SendOrdersRequest();
            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/");
            оrdersInputPage.Table.RowSearch.ID.SetValue(ordersID[0]);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            оrdersInputPage.Table.GetRow(0).ID.WaitText(ordersID[0]);
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();

            var оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            DateTime pickupDate = DateTime.ParseExact(оrderEditPage.PickupDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);
            DateTime transferCDDate = DateTime.ParseExact(оrderEditPage.TransferCDDate.GetValue(), "yyyy-MM-dd HH:mm:ss",
                null);

            //                        пребовляем день к pickupDate так как по формуле следующий день который не выходной,  выходного нет
            Assert.AreEqual(pickupDate.AddDays(1), transferCDDate, "Дата передачи в КД должна быть равна следующему рабочему" +
                                                                   " дню по календарю компании доставки от Даты заборы");

            оrderEditPage.Orders.Click();
            оrderEditPage.OrderInput.Click();
            оrdersInputPage = оrderEditPage.GoTo<OrdersInputPage>();
            оrdersInputPage.Table.RowSearch.ID.SetValue(ordersID[1]);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            оrdersInputPage.Table.GetRow(0).ID.WaitText(ordersID[1]);
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();

            оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            pickupDate = DateTime.ParseExact(оrderEditPage.PickupDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);
            transferCDDate = DateTime.ParseExact(оrderEditPage.TransferCDDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);

            //                        пребовляем день к pickupDate так как по формуле следующий день который не выходной,  выходного нет
            Assert.AreEqual(pickupDate.AddDays(1), transferCDDate, "Дата передачи в КД должна быть равна следующему рабочему" +
                                                                   " дню по календарю компании доставки от Даты заборы");
        }
    }
}