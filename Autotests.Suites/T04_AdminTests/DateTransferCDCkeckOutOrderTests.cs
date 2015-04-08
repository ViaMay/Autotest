using System;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T04_AdminTests
{
    public class DateTransferCDCkeckOutOrderTests : SendOrdersBasePage
    {
        [Test, Description("Проверяем пернос даты из входящей завки в исходящию")]
        public void CkeckEqualInputOutputDatesTest()
        {
            LoginAsAdmin(adminName, adminPass);
            string[] ordersID = SendOrdersRequest();

//            Создание исходщих завок
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");
            adminMaintenancePage.AlertText.WaitTextContains("Processed");

//            Входящая заявка 1
            var оrderEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersID[0]);
            var transferCDInputDate = DateTime.ParseExact(оrderEditPage.TransferCDDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);

            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/?&filters[_id]=" + ordersID[0]);
            оrdersInputPage.Table.GetRow(0).OrderOutput.Click();
            var оrdersOutputPage = оrdersInputPage.GoTo<OrdersOutputPage>();
            оrdersOutputPage.Table.GetRow(0).ActionsEdit.Click();
//            исходящая заявка 1
            var оrderEditOutputPage = оrdersOutputPage.GoTo<OrderOutputEditingPage>();
            var transferCDOutputDate = DateTime.ParseExact(оrderEditOutputPage.TransferCDDate.GetValue(), "dd.MM.yyyy HH:mm:ss", null);

//            Сравнение двух дат
            Assert.AreEqual(transferCDInputDate, transferCDOutputDate, "Даты должны совпадать у исходящей и входящей заявки");
           
//            Входящая заявка 2
            оrderEditPage = LoadPage<OrderInputEditingPage>("admin/orders/edit/" + ordersID[0]);
            transferCDInputDate = DateTime.ParseExact(оrderEditPage.TransferCDDate.GetValue(), "yyyy-MM-dd HH:mm:ss", null);

            оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/?&filters[_id]=" + ordersID[0]);
            оrdersInputPage.Table.GetRow(0).OrderOutput.Click();
            оrdersOutputPage = оrdersInputPage.GoTo<OrdersOutputPage>();
            оrdersOutputPage.Table.GetRow(0).ActionsEdit.Click();
//            исходящая заявка 2
            оrderEditOutputPage = оrdersOutputPage.GoTo<OrderOutputEditingPage>();
            transferCDOutputDate = DateTime.ParseExact(оrderEditOutputPage.TransferCDDate.GetValue(), "dd.MM.yyyy HH:mm:ss", null);

//            Сравнение двух дат
            Assert.AreEqual(transferCDInputDate, transferCDOutputDate, "Даты должны совпадать у исходящей и входящей заявки");
       
        }
    }
}