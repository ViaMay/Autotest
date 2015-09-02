using Autotests.WebPages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
{
    public class WarehouseDocumentsShowTests : ConstVariablesTestBase
    {
        [Test, Description("просмотр документов склада")]
        public void WarehouseDocumentsShowTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserWarehouses.Click();
            var warehousesListPage = userPage.GoTo<UserWarehousesPage>();
            
            var row = warehousesListPage.Table.FindRowByName(userWarehouseName);
            row.Name.WaitText(userWarehouseName);
            row.City.WaitText("Москва");
            row.Address.WaitText("555444, Улица, Дом Квартира");
            row.Contact.WaitText("test_legalEntity (+7 (111)111-1111 tester@user.ru)");
            row.TimeWork.WaitText("1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23");

            row.Documents.WaitText("посмотреть");
            row.Documents.Click();

            var documentsPreparedPage = warehousesListPage.GoTo<DocumentsPreparedPage>();
            documentsPreparedPage.WarehouseSelect.WaitText(userWarehouseName);
            documentsPreparedPage.InstructionsForUse.WaitText("Инструкция по использованию");
            documentsPreparedPage.Stickers.WaitText("Наклейки");
            documentsPreparedPage.TextAlert.WaitText("Примечание Передача заказов без наклеек не допускается.");
            
            documentsPreparedPage.Acts.WaitText("Акты для компании забора");
            documentsPreparedPage.Acts.Click();
            warehousesListPage.GoTo<DocumentsPreparedPage>();
            documentsPreparedPage.TextAlert.WaitText("Примечание Спросите у курьера из какой он компании, перед тем как передавать груз.");

            documentsPreparedPage.ActsTC.WaitText("Акты для самостоятельной передачи заказов на склады ТК");
            documentsPreparedPage.ActsTC.Click();
            warehousesListPage.GoTo<DocumentsPreparedPage>();
            documentsPreparedPage.TextAlert.WaitText("Примечание Проконсультируйтесь с менеджерами по поводу доступности опции для вашего магазина!");
        }

    }
}