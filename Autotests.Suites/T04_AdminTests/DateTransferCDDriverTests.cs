using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T04_AdminTests
{
    public class DateTransferCDDriverTests : SendOrdersBasePage
    {
//        [Test, Description("Для разных драйверов"), Ignore]
//        [TestCase("Aplix_child")]
//        [TestCase("Aplix_post_ems")]
//        [TestCase("Aplix_qiwi")]
//        [TestCase("Aplix")]
//        [TestCase("Aurama")]
//        [TestCase("Boxberry")]
//        [TestCase("Dpd")]
//        [TestCase("Dpdclassic")]
//        [TestCase("Dpdconsumer")]
//        [TestCase("Dpdeconomy")]
//        [TestCase("Dpdexpress")]
//        [TestCase("Ekbdostavka")]
//        [TestCase("Freshlogic")]
//        [TestCase("Fsd")]
//        [TestCase("Fsdexpress")]
//        [TestCase("Hermes")]
//        [TestCase("Imlexpress")]
//        [TestCase("Kit")]
//        [TestCase("Kts")]
//        [TestCase("Lenod")]
//        [TestCase("Litops")]
//        [TestCase("Logibox")]
//        [TestCase("Pickpoint")]
//        [TestCase("Pochtarossii")]
//        [TestCase("Pochtarossiiems")]
//        [TestCase("Pochtarossiifirstclass")]
//        [TestCase("Qiwi")]
//        [TestCase("Sdek")]
//        [TestCase("Sdek136")]
//        [TestCase("Sdek137")]
//        [TestCase("Selfpick")]
//        [TestCase("Smartcourier")]
//        [TestCase("Telepost")] 
        public void Test(string driver)
        {
            LoginAsAdmin(adminName, adminPass);
            string[] ordersID = SendOrdersRequest();

            //            Создание исходщих завок
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/process_i_orders");
            adminMaintenancePage.AlertText.WaitTextContains("Processed");

            var оrdersInputPage = LoadPage<OrdersInputPage>("admin/orders/?&filters[_id]=" + ordersID[0]);
            оrdersInputPage.Table.GetRow(0).OrderOutput.Click();
            var оrdersOutputPage = оrdersInputPage.GoTo<OrdersOutputPage>();
            оrdersOutputPage.Table.GetRow(0).ActionsEdit.Click();
            var оrderEditOutputPage = оrdersOutputPage.GoTo<OrderOutputEditingPage>();
            var transferCDOutputDate = оrderEditOutputPage.TransferCDDate.GetValue();

            оrderEditOutputPage.TestSentToTC.Click();
            оrderEditOutputPage = оrderEditOutputPage.GoTo<OrderOutputEditingPage>();
        }


    }
}