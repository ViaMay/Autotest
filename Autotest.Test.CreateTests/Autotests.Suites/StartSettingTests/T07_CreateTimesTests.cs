using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.StartSettingTests
{
    public class T07CreateTimesTests : ConstVariablesTestBase
    {
        [Test, Description("Создания сроков забора")]
        public void CreateTimesPickupTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Times.Mouseover();
            adminPage.TimesPickup.Click();
            var timesPickupPage = adminPage.GoTo<AdminBaseListPage>();
            timesPickupPage.LabelDirectory.WaitText(@"Справочник ""Время забора""");
            timesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            timesPickupPage = timesPickupPage.SeachButtonRowClickAndGo();

            while (timesPickupPage.Table.GetRow(0).Name.IsPresent)
            {
                timesPickupPage.Table.GetRow(0).ActionsDelete.Click();
                timesPickupPage.Aletr.Accept();
                timesPickupPage = timesPickupPage.GoTo<AdminBaseListPage>();
                timesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                timesPickupPage = timesPickupPage.SeachButtonRowClickAndGo();
            }
            timesPickupPage.Create.Click();
            var timePickupCreatePage = timesPickupPage.GoTo<TimePickupCreatePage>();
            timePickupCreatePage.CompanyName.SetFirstValueSelect(companyPickupName);
            timePickupCreatePage.City.SetFirstValueSelect("Москва");
            timePickupCreatePage.MaxTime.SetValueAndWait("1");
            timePickupCreatePage.MinTime.SetValueAndWait("1");
            timePickupCreatePage.SaveButton.Click();
            timesPickupPage = timePickupCreatePage.GoTo<AdminBaseListPage>();

            timesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            timesPickupPage = timesPickupPage.SeachButtonRowClickAndGo();
            timesPickupPage.Table.GetRow(0).Name.WaitText(companyPickupName);
        }

        [Test, Description("Создания сроков курьера")]
        public void CreatePriceCourierTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Times.Mouseover();
            adminPage.TimesCourier.Click();
            var timesCourierPage = adminPage.GoTo<AdminBaseListPage>();
            timesCourierPage.LabelDirectory.WaitText(@"Справочник ""Сроки доставки курьерки""");
            timesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
            timesCourierPage = timesCourierPage.SeachButtonRowClickAndGo();

            while (timesCourierPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                timesCourierPage.Table.GetRow(0).ActionsDelete.Click();
                timesCourierPage = timesCourierPage.GoTo<AdminBaseListPage>();
                timesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
                timesCourierPage = timesCourierPage.SeachButtonRowClickAndGo();
            }
            timesCourierPage.Create.Click();
            var timeCourierCreatePage = timesCourierPage.GoTo<TimeCreatePage>();
            timeCourierCreatePage.CompanyName.SetFirstValueSelect(companyName);
            timeCourierCreatePage.Route.SetFirstValueSelect("2", "г. Москва #151184 - г. Москва #151184");
            timeCourierCreatePage.MaxTime.SetValueAndWait("1");
            timeCourierCreatePage.MinTime.SetValueAndWait("1");
            timeCourierCreatePage.SaveButton.Click();
            timesCourierPage = timeCourierCreatePage.GoTo<AdminBaseListPage>();

            timesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
            timesCourierPage = timesCourierPage.SeachButtonRowClickAndGo();
            timesCourierPage.Table.GetRow(0).ColumnThree.WaitText(companyName);
        }

        [Test, Description("Создания сроков самовывоза")]
        public void CreateSelfPriceTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Times.Mouseover();
            adminPage.TimesSelf.Click();
            var timesSelfPage = adminPage.GoTo<AdminBaseListPage>();
            timesSelfPage.LabelDirectory.WaitText(@"Справочник ""Сроки доставки""");
            timesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
            timesSelfPage = timesSelfPage.SeachButtonRowClickAndGo();

            while (timesSelfPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                timesSelfPage.Table.GetRow(0).ActionsDelete.Click();
                timesSelfPage = timesSelfPage.GoTo<AdminBaseListPage>();
                timesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
                timesSelfPage = timesSelfPage.SeachButtonRowClickAndGo();
            }
            timesSelfPage.Create.Click();
            var timeSelfCreatePage = timesSelfPage.GoTo<TimeCreatePage>();
            timeSelfCreatePage.CompanyName.SetFirstValueSelect(companyName);
            timeSelfCreatePage.Route.SetFirstValueSelect("2", "г. Москва #151184 - г. Москва #151184");
            timeSelfCreatePage.MaxTime.SetValueAndWait("1");
            timeSelfCreatePage.MinTime.SetValueAndWait("1");
            timeSelfCreatePage.SaveButton.Click();
            timesSelfPage = timeSelfCreatePage.GoTo<AdminBaseListPage>();

            timesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
            timesSelfPage = timesSelfPage.SeachButtonRowClickAndGo();
            timesSelfPage.Table.GetRow(0).ColumnThree.WaitText(companyName);
        }
    }
}