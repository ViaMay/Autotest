using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
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
            var timesPickupPage = adminPage.GoTo<TimesPickupPage>();
            timesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            timesPickupPage = timesPickupPage.SeachButtonRowClickAndGo();

            while (timesPickupPage.Table.GetRow(0).Name.IsPresent)
            {
                timesPickupPage.Table.GetRow(0).ActionsDelete.Click();
                timesPickupPage = timesPickupPage.GoTo<TimesPickupPage>();
                timesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                timesPickupPage = timesPickupPage.SeachButtonRowClickAndGo();
            }
            timesPickupPage.TimesPickupCreate.Click();
            var timePickupCreatePage = timesPickupPage.GoTo<TimePickupCreatePage>();
            timePickupCreatePage.CompanyName.SetFirstValueSelect(companyPickupName);
            timePickupCreatePage.City.SetFirstValueSelect("Москва");
            timePickupCreatePage.MaxTime.SetValueAndWait("1");
            timePickupCreatePage.MinTime.SetValueAndWait("1");
            timePickupCreatePage.SaveButton.Click();
            timesPickupPage = timePickupCreatePage.GoTo<TimesPickupPage>();

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
            var timesCourierPage = adminPage.GoTo<TimesPage>();
            timesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
            timesCourierPage = timesCourierPage.SeachButtonRowClickAndGo();

            while (timesCourierPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                timesCourierPage.Table.GetRow(0).ActionsDelete.Click();
                timesCourierPage = timesCourierPage.GoTo<TimesPage>();
                timesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
                timesCourierPage = timesCourierPage.SeachButtonRowClickAndGo();
            }
            timesCourierPage.TimesCourierCreate.Click();
            var timeCourierCreatePage = timesCourierPage.GoTo<TimeCreatePage>();
            timeCourierCreatePage.CompanyName.SetFirstValueSelect(companyName);
            timeCourierCreatePage.Route.SetFirstValueSelect("2", "г. Москва #151184 - г. Москва #151184");
            timeCourierCreatePage.MaxTime.SetValueAndWait("1");
            timeCourierCreatePage.MinTime.SetValueAndWait("1");
            timeCourierCreatePage.SaveButton.Click();
            timesCourierPage = timeCourierCreatePage.GoTo<TimesPage>();

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
            var timesSelfPage = adminPage.GoTo<TimesPage>();
            timesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
            timesSelfPage = timesSelfPage.SeachButtonRowClickAndGo();

            while (timesSelfPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                timesSelfPage.Table.GetRow(0).ActionsDelete.Click();
                timesSelfPage = timesSelfPage.GoTo<TimesPage>();
                timesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
                timesSelfPage = timesSelfPage.SeachButtonRowClickAndGo();
            }
            timesSelfPage.TimesCourierCreate.Click();
            var timeSelfCreatePage = timesSelfPage.GoTo<TimeCreatePage>();
            timeSelfCreatePage.CompanyName.SetFirstValueSelect(companyName);
            timeSelfCreatePage.Route.SetFirstValueSelect("2", "г. Москва #151184 - г. Москва #151184");
            timeSelfCreatePage.MaxTime.SetValueAndWait("1");
            timeSelfCreatePage.MinTime.SetValueAndWait("1");
            timeSelfCreatePage.SaveButton.Click();
            timesSelfPage = timeSelfCreatePage.GoTo<TimesPage>();

            timesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
            timesSelfPage = timesSelfPage.SeachButtonRowClickAndGo();
            timesSelfPage.Table.GetRow(0).ColumnThree.WaitText(companyName);
        }
    }
}