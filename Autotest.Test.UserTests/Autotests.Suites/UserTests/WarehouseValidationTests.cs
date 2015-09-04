using Autotests.WebPages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
{
    public class WarehouseValidationTests : ConstVariablesTestBase
    {
        [Test, Description("Создание склада. Проверка валидации полей")]
        public void WarehouseValidationTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserWarehouses.Click();
            var warehousesListPage = userPage.GoTo<UserWarehousesPage>();
            warehousesListPage.WarehousesCreate.Click();
            var warehouseCreatePage = warehousesListPage.GoTo<UserWarehouseCreatePage>();
            warehouseCreatePage.CreateButton.ClickAndWaitTextError();

            warehouseCreatePage.ErrorText[0].WaitText("Название обязательно к заполнению");
            warehouseCreatePage.ErrorText[1].WaitText("Улица обязательно к заполнению");
            warehouseCreatePage.ErrorText[2].WaitText("Дом обязательно к заполнению");
            warehouseCreatePage.ErrorText[3].WaitText("Номер помещения обязательно к заполнению");
            warehouseCreatePage.ErrorText[4].WaitText("Контактное лицо обязательно к заполнению");
            warehouseCreatePage.ErrorText[5].WaitText("Контактный телефон обязательно к заполнению");
            warehouseCreatePage.ErrorText[6].WaitText(@"в поле ""График работы"" должен быть заполнен хотя бы один рабочий день");
            warehouseCreatePage.ErrorText[7].WaitText("Город обязательно к заполнению");

            warehouseCreatePage.Name.SetValueAndWait("1");
            warehouseCreatePage.CreateButton.ClickAndWaitTextError();
            warehouseCreatePage.ErrorText[0].WaitText("Название должно быть не менее 3 символа(ов)");

            warehouseCreatePage.Name.SetValueAndWait("12");
            warehouseCreatePage.CreateButton.ClickAndWaitTextError();
            warehouseCreatePage.ErrorText[0].WaitText("Название должно быть не менее 3 символа(ов)");

            warehouseCreatePage.Name.SetValueAndWait("nam");
            warehouseCreatePage.CreateButton.ClickAndWaitTextError();
            warehouseCreatePage.ErrorText[0].WaitText("Улица обязательно к заполнению");
            warehouseCreatePage.ErrorText[1].WaitText("Дом обязательно к заполнению");
            warehouseCreatePage.ErrorText[2].WaitText("Номер помещения обязательно к заполнению");
            warehouseCreatePage.ErrorText[3].WaitText("Контактное лицо обязательно к заполнению");
            warehouseCreatePage.ErrorText[4].WaitText("Контактный телефон обязательно к заполнению");
            warehouseCreatePage.ErrorText[5].WaitText(@"в поле ""График работы"" должен быть заполнен хотя бы один рабочий день");
            warehouseCreatePage.ErrorText[6].WaitText("Город обязательно к заполнению");

            warehouseCreatePage.Name.SetValueAndWait(userWarehouseName + "1");
            warehouseCreatePage.Street.SetValueAndWait("улица");
//            warehouseCreatePage.CreateButton.ClickAndWaitTextError();
//            warehouseCreatePage.ErrorText[0].WaitText("Дом обязательно к заполнению");
//            warehouseCreatePage.ErrorText[1].WaitText("Номер помещения обязательно к заполнению");
//            warehouseCreatePage.ErrorText[2].WaitText("Контактное лицо обязательно к заполнению");
//            warehouseCreatePage.ErrorText[3].WaitText("Контактный телефон обязательно к заполнению");
//            warehouseCreatePage.ErrorText[4].WaitText(@"в поле ""График работы"" должен быть заполнен хотя бы один рабочий день");
//            warehouseCreatePage.ErrorText[5].WaitText("Город обязательно к заполнению");

            warehouseCreatePage.House.SetValueAndWait("доме");
            warehouseCreatePage.CreateButton.ClickAndWaitTextError();
            warehouseCreatePage.ErrorText[0].WaitText("Номер помещения обязательно к заполнению");
            warehouseCreatePage.ErrorText[1].WaitText("Контактное лицо обязательно к заполнению");
            warehouseCreatePage.ErrorText[2].WaitText("Контактный телефон обязательно к заполнению");
            warehouseCreatePage.ErrorText[3].WaitText(@"в поле ""График работы"" должен быть заполнен хотя бы один рабочий день");
            warehouseCreatePage.ErrorText[4].WaitText("Город обязательно к заполнению");

            warehouseCreatePage.Flat.SetValueAndWait("квартира");
            warehouseCreatePage.CreateButton.ClickAndWaitTextError();
            warehouseCreatePage.ErrorText[0].WaitText("Контактное лицо обязательно к заполнению");
            warehouseCreatePage.ErrorText[1].WaitText("Контактный телефон обязательно к заполнению");
            warehouseCreatePage.ErrorText[2].WaitText(@"в поле ""График работы"" должен быть заполнен хотя бы один рабочий день");
            warehouseCreatePage.ErrorText[3].WaitText("Город обязательно к заполнению");

            warehouseCreatePage.ContactPerson.SetValueAndWait("ContactPerson");
            warehouseCreatePage.CreateButton.ClickAndWaitTextError();
            warehouseCreatePage.ErrorText[0].WaitText("Контактный телефон обязательно к заполнению");
            warehouseCreatePage.ErrorText[1].WaitText(@"в поле ""График работы"" должен быть заполнен хотя бы один рабочий день");
            warehouseCreatePage.ErrorText[2].WaitText("Город обязательно к заполнению");

            warehouseCreatePage.ContactPhone.SetValueAndWait("1111111111");
            warehouseCreatePage.CreateButton.ClickAndWaitTextError();
            warehouseCreatePage.ErrorText[0].WaitText(@"в поле ""График работы"" должен быть заполнен хотя бы один рабочий день");
            warehouseCreatePage.ErrorText[1].WaitText("Город обязательно к заполнению");

            warehouseCreatePage.GetDay(0).FromHour.SetValueAndWait("0:00");
            warehouseCreatePage.GetDay(0).ToHour.SetValueAndWait("11:00");
            warehouseCreatePage.CreateButton.ClickAndWaitTextError();
            warehouseCreatePage.ErrorText[0].WaitText("Город обязательно к заполнению");

            warehouseCreatePage.City.SetFirstValueSelect("Москва");
            warehouseCreatePage.CreateButton.Click();
            warehousesListPage = warehouseCreatePage.GoTo<UserWarehousesPage>();

            warehousesListPage.Table.FindRowByName(userWarehouseName + "1").Name.WaitText(userWarehouseName + "1");
        }

    }
}