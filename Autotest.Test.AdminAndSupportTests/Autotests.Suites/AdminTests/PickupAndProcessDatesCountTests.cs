 using System;
using System.Globalization;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.AdminTests
{
//    "Проверяем расчет даты процесинга - она не должна зависить от календаря и" +
//                           "равна PickupDate из заявки + то что стоит в графике забора" +
//                           "для Личного Кабинета PickupDate = текущи день"
//                           "так же проверяем зависимость PickupDate от кадлендаря"
    public class PickupAndProcessDatesCountTests : SendOrdersBasePage
    {
        [Test, Description("Проверка зависимости от графика забора, PickupDate = null, в графике сегоднешнее дата 23:45, без календаря")]
        public void PickupDateNullTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.PickupTimetable.Click();
            var pickupTimetablePage = adminPage.GoTo<AdminBaseListPage>();
            pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
//            создаем график забора на сегоднешнею дату 23:45
            while (pickupTimetablePage.Table.GetRow(0).Name.IsPresent)
            {
                pickupTimetablePage.Table.GetRow(0).ActionsDelete.Click();
                pickupTimetablePage.Aletr.Accept();
                pickupTimetablePage = pickupTimetablePage.GoTo<AdminBaseListPage>();
                pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            }
            pickupTimetablePage.Create.Click();
            var pickupTimetableCreatePage = pickupTimetablePage.GoTo<PickupTimetableCreatePage>();
            pickupTimetableCreatePage.Company.SetFirstValueSelect(companyPickupName);
            pickupTimetableCreatePage.PickupTime.SelectByText("23:45");
            pickupTimetableCreatePage.PickupPeriod.SelectByText("Сегодня");
            pickupTimetableCreatePage.SaveButton.Click();
            pickupTimetablePage = pickupTimetableCreatePage.GoTo<AdminBaseListPage>();

//           удаляем календарь если он был
            pickupTimetablePage.AdminCompanies.Click();
            pickupTimetablePage.Calendars.Click();
            var calendarsPage = pickupTimetablePage.GoTo<AdminBaseListPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            while (calendarsPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                calendarsPage.Table.GetRow(0).ActionsDelete.Click();
                calendarsPage.Aletr.Accept();
                calendarsPage = calendarsPage.GoTo<AdminBaseListPage>();
                calendarsPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            }
//            отправляем запрос АПИ заявку без времение PickupDate
            string orderID = SendOrderCourirsRequest();
            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/");
            оrdersInputPage.Table.RowSearch.ID.SetValue(orderID);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            оrdersInputPage.Table.GetRow(0).ID.WaitText(orderID);
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();

            var оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            var processDate = оrderEditPage.ProcessDate.GetValue();
            var pickupDate = оrderEditPage.PickupDate.GetValue();
//            так как в графике забора стоит сегодняшняя дата, 23:45; дата процессинга ближайшая не прошедшая дата в этом графике
            var processDateExpect = nowDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " 23:45:00";
            Assert.AreEqual(processDateExpect, processDate);

//            дата забора ожидаема равна дате процессинга + час
            var pickupDateExpect = DateTime.ParseExact(processDate, "yyyy-MM-dd HH:mm:ss", null)
                .AddHours(1)
                .ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            Assert.AreEqual(pickupDateExpect, pickupDate);
        }

        [Test, Description("проверка зависимости от графика забора, PickupDate заполнена, в графике забора сегодня 23:45, без календаря")]
        public void PickupDateNotNullTest()
        {           
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.PickupTimetable.Click();
            var pickupTimetablePage = adminPage.GoTo<AdminBaseListPage>();
            pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            //            создаем график забора на сегоднешнею дату 23:45
            while (pickupTimetablePage.Table.GetRow(0).Name.IsPresent)
            {
                pickupTimetablePage.Table.GetRow(0).ActionsDelete.Click();
                pickupTimetablePage.Aletr.Accept();
                pickupTimetablePage = pickupTimetablePage.GoTo<AdminBaseListPage>();
                pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            }
            pickupTimetablePage.Create.Click();
            var pickupTimetableCreatePage = pickupTimetablePage.GoTo<PickupTimetableCreatePage>();
            pickupTimetableCreatePage.Company.SetFirstValueSelect(companyPickupName);
            pickupTimetableCreatePage.PickupTime.SelectByText("23:45");
            pickupTimetableCreatePage.PickupPeriod.SelectByText("Сегодня");
            pickupTimetableCreatePage.SaveButton.Click();
            pickupTimetablePage = pickupTimetableCreatePage.GoTo<AdminBaseListPage>();
//           удаляем календарь если он был
            pickupTimetablePage.AdminCompanies.Click();
            pickupTimetablePage.Calendars.Click();
            var calendarsPage = pickupTimetablePage.GoTo<AdminBaseListPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            while (calendarsPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                calendarsPage.Table.GetRow(0).ActionsDelete.Click();
                calendarsPage.Aletr.Accept();
                calendarsPage = calendarsPage.GoTo<AdminBaseListPage>();
                calendarsPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            }
//            дата забора которую передаем в запросе
            var pickupDate = nowDate.AddDays(2).AddHours(1).AddMinutes(15);
            string orderID = SendOrderSelfRequest(pickupDate);
            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/");
            оrdersInputPage.Table.RowSearch.ID.SetValue(orderID);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            оrdersInputPage.Table.GetRow(0).ID.WaitText(orderID);
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();

            var оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            var processDateExpect = оrderEditPage.ProcessDate.GetValue();
            var pickupDateExpect = оrderEditPage.PickupDate.GetValue();
//              дата процессинга на день рашь даты забора,ближайший повремени
            var processDate = pickupDate.AddDays(-1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " 23:45:00";
            Assert.AreEqual(processDateExpect, processDate);

//            дата забора равна той дате что отправляем в API 
            Assert.AreEqual(pickupDateExpect, pickupDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
        }

        [Test, Description("Проверка зависимости от графика забора, PickupDate = null, " +
                           "в графике сегодняшняя дата 23:45, в календаре выходной на дату забора")]
        public void PickupDateNullAndСalendarTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.PickupTimetable.Click();
            var pickupTimetablePage = adminPage.GoTo<AdminBaseListPage>();
            pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            //            создаем график забора на сегодняшнюю дату 23:45
            while (pickupTimetablePage.Table.GetRow(0).Name.IsPresent)
            {
                pickupTimetablePage.Table.GetRow(0).ActionsDelete.Click();
                pickupTimetablePage.Aletr.Accept();
                pickupTimetablePage = pickupTimetablePage.GoTo<AdminBaseListPage>();
                pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            }
            pickupTimetablePage.Create.Click();
            var pickupTimetableCreatePage = pickupTimetablePage.GoTo<PickupTimetableCreatePage>();
            pickupTimetableCreatePage.Company.SetFirstValueSelect(companyPickupName);
            pickupTimetableCreatePage.PickupTime.SelectByText("23:45");
            pickupTimetableCreatePage.PickupPeriod.SelectByText("Сегодня");
            pickupTimetableCreatePage.SaveButton.Click();
            pickupTimetablePage = pickupTimetableCreatePage.GoTo<AdminBaseListPage>();
            //           удаляем календарь если он был
            //          делаем выходной на сегодня
            pickupTimetablePage.AdminCompanies.Click();
            pickupTimetablePage.Calendars.Click();
            var calendarsPage = pickupTimetablePage.GoTo<AdminBaseListPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            while (calendarsPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                calendarsPage.Table.GetRow(0).ActionsDelete.Click();
                calendarsPage.Aletr.Accept();
                calendarsPage = calendarsPage.GoTo<AdminBaseListPage>();
                calendarsPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            }
            calendarsPage.Create.Click();
            var calendarCreatePage = calendarsPage.GoTo<CalendarCreatePage>();
            calendarCreatePage.Type.SelectByText("Забор");
            calendarCreatePage.Company.SetFirstValueSelect(companyPickupName);
            calendarCreatePage.Date.SetValueAndWait(nowDate.AddDays(1).ToString("dd.MM.yyyy"));
            calendarCreatePage.SaveButton.Click();
            calendarsPage = calendarCreatePage.GoTo<AdminBaseListPage>();

            string orderID = SendOrderSelfRequest();
            var sendingTime = DateTime.Now;
            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/");
            оrdersInputPage.Table.RowSearch.ID.SetValue(orderID);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            оrdersInputPage.Table.GetRow(0).ID.WaitText(orderID);
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();

            var оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            var processDateExpect = оrderEditPage.ProcessDate.GetValue();
            var pickupDateExpect = оrderEditPage.PickupDate.GetValue();
//            так как в графике забора стоит сегоднешняя дата, 23:45; дата процессинга ближайщая не прошедшая дата в этом графике
            var processDate = nowDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " 23:45:00";
            Assert.AreEqual(processDateExpect, processDate);

//            дата забора ожидаема равна дате процессинга + час
            var pickupDate = DateTime.ParseExact(processDate, "yyyy-MM-dd HH:mm:ss", null)
                .AddHours(1)
                .ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            Assert.AreEqual(pickupDateExpect, pickupDate);
        }

        [Test, Description("проверка зависимости от графика забора, PickupDate = заполнена," +
                           " в графике сегодняшняя дата 23:45, в календаре выходной на дату забора")]
        public void PickupDateNotNullAndСalendarTest()
        {          
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.PickupTimetable.Click();
            var pickupTimetablePage = adminPage.GoTo<AdminBaseListPage>();
            pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();

            while (pickupTimetablePage.Table.GetRow(0).Name.IsPresent)
            {
                pickupTimetablePage.Table.GetRow(0).ActionsDelete.Click();
                pickupTimetablePage.Aletr.Accept();
                pickupTimetablePage = pickupTimetablePage.GoTo<AdminBaseListPage>();
                pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            }
            pickupTimetablePage.Create.Click();
            var pickupTimetableCreatePage = pickupTimetablePage.GoTo<PickupTimetableCreatePage>();
            pickupTimetableCreatePage.Company.SetFirstValueSelect(companyPickupName);
            pickupTimetableCreatePage.PickupTime.SelectByText("23:45");
            pickupTimetableCreatePage.PickupPeriod.SelectByText("Сегодня");
            pickupTimetableCreatePage.SaveButton.Click();
            pickupTimetablePage = pickupTimetableCreatePage.GoTo<AdminBaseListPage>();

            //           удаляем календарь если он был
            //создаем в каледнаре выходной на дату забора
            pickupTimetablePage.AdminCompanies.Click();
            pickupTimetablePage.Calendars.Click();
            var calendarsPage = pickupTimetablePage.GoTo<AdminBaseListPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            while (calendarsPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                calendarsPage.Table.GetRow(0).ActionsDelete.Click();
                calendarsPage.Aletr.Accept();
                calendarsPage = calendarsPage.GoTo<AdminBaseListPage>();
                calendarsPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            }
            calendarsPage.Create.Click();
            var pickupDate = nowDate.AddDays(2).AddHours(1).AddMinutes(15);
//          делаем выходной на дату забора которую укажем в АПИ запросе
            var calendarCreatePage = calendarsPage.GoTo<CalendarCreatePage>();
            calendarCreatePage.Type.SelectByText("Забор");
            calendarCreatePage.Company.SetFirstValueSelect(companyPickupName);
            calendarCreatePage.Date.SetValueAndWait(pickupDate.ToString("dd.MM.yyyy"));
            calendarCreatePage.SaveButton.Click();
            calendarsPage = calendarCreatePage.GoTo<AdminBaseListPage>();

            string orderID = SendOrderCourirsRequest(pickupDate);
            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/");
            оrdersInputPage.Table.RowSearch.ID.SetValue(orderID);
            оrdersInputPage = оrdersInputPage.SeachButtonRowClickAndGo();
            оrdersInputPage.Table.GetRow(0).ID.WaitText(orderID);
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();

            var оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            //            формат даты "yyyy-MM-dd HH:mm:ss" такой
            var processDateExpect = оrderEditPage.ProcessDate.GetValue();
            var pickupDateExpect = оrderEditPage.PickupDate.GetValue();
            //            дата процессинга равна ближайшей дате предыдущей забора 23:45
            var processDate = pickupDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " 23:45:00";
            Assert.AreEqual(processDateExpect, processDate);

            //            дата забора равна той дате что отправляем в API + 1 день так как в дату забора выходной
            Assert.AreEqual(pickupDateExpect, pickupDate.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
        }
    }
}