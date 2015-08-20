using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class WarehouseEditingTests : ConstVariablesTestBase
    {
        [Test, Description("Редактирование параметров склада")]
        public void WarehouseEditingTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserWarehouses.Click();
            var warehousesListPage = userPage.GoTo<UserWarehousesPage>();
            warehousesListPage.WarehousesCreate.Click();
            var warehouseCreatePage = warehousesListPage.GoTo<UserWarehouseCreatePage>();

            warehouseCreatePage.LabelDirectory.WaitText("Создание склада");
            warehouseCreatePage.Name.SetValueAndWait(userWarehouseName + "2");
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
            warehouseCreatePage.Freshlogic.SetValue("Freshlogic");
            warehouseCreatePage.CreateButton.WaitValue("Создать");
            warehouseCreatePage.CreateButton.Click();
            warehousesListPage = warehouseCreatePage.GoTo<UserWarehousesPage>();

            var row = warehousesListPage.Table.FindRowByName(userWarehouseName + "2");
            row.Name.WaitText(userWarehouseName + "2");
            row.City.WaitText("Москва");
            row.Address.WaitText("Улица, Дом Квартира");
            row.Contact.WaitText("test_legalEntity (+7 (111)111-1111 tester@user.ru)");
            row.TimeWork.WaitText("1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23");

            row.Name.Click();
            warehouseCreatePage = warehousesListPage.GoTo<UserWarehouseCreatePage>();

            warehouseCreatePage.LabelDirectory.WaitText("Редактирование склада");
            warehouseCreatePage.Name.SetValueAndWait(userWarehouseName + "3");
            warehouseCreatePage.Street.SetValueAndWait("Улица2");
            warehouseCreatePage.House.SetValueAndWait("Дом2");
            warehouseCreatePage.Flat.SetValueAndWait("Квартира2");
            warehouseCreatePage.ContactPerson.SetValueAndWait(legalEntityName + "2");
            warehouseCreatePage.ContactPhone.SetValueAndWait("2222222222");
            warehouseCreatePage.ContactEmail.SetValueAndWait( "2" + userNameAndPass);
            warehouseCreatePage.City.SetFirstValueSelect("Санкт-Петербург");

            warehouseCreatePage.GetDay(0).FromHour.SetValueAndWait("0:00");
            warehouseCreatePage.GetDay(0).ToHour.SetValueAndWait("22:22");
            for (int i = 1; i < 7; i++)
            {
                warehouseCreatePage.GetDay(i).FromHour.SetValueAndWait("0:00");
                warehouseCreatePage.GetDay(i).ToHour.SetValueAndWait("0:00");
            }
            warehouseCreatePage.Freshlogic.SetValue("Freshlogic2");
            warehouseCreatePage.CreateButton.WaitValue("Редактировать");
            warehouseCreatePage.CreateButton.Click();
            warehousesListPage = warehouseCreatePage.GoTo<UserWarehousesPage>();

            row = warehousesListPage.Table.FindRowByName(userWarehouseName + "3");
            row.Name.WaitText(userWarehouseName + "3");
            row.City.WaitText("Санкт-Петербург");
            row.Address.WaitText("Улица2, Дом2 Квартира2");
            row.Contact.WaitText("test_legalEntity2 (+7 (222)222-2222 2tester@user.ru)");
            row.TimeWork.WaitText("0:00-22:22,Выходной,Выходной,Выходной,Выходной,Выходной,Выходной");
        }

    }
}