using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
{
    public class T03CreateSizeTests : ConstVariablesTestBase
    {
        [Test, Description("Создания веса")]
        public void CreateWeightTest()
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
                intervalsWeightPage = intervalsWeightPage.GoTo<IntervalsWeightPage>();
                intervalsWeightPage.Table.RowSearch.Name.SetValue(weightName);
                intervalsWeightPage = intervalsWeightPage.SeachButtonRowClickAndGo();
            }
            intervalsWeightPage.IntervalWeightCreate.Click();
            var intervalWeightCreatePage = intervalsWeightPage.GoTo<IntervalWeightCreatePage>();
            intervalWeightCreatePage.Name.SetValueAndWait(weightName);
            intervalWeightCreatePage.Min.SetValueAndWait(weightMin);
            intervalWeightCreatePage.Max.SetValueAndWait(weightMax);
            intervalWeightCreatePage.SaveButton.Click();
            intervalsWeightPage = intervalWeightCreatePage.GoTo<IntervalsWeightPage>();

            intervalsWeightPage.Table.RowSearch.Name.SetValue(weightName);
            intervalsWeightPage = intervalsWeightPage.SeachButtonRowClickAndGo();
            intervalsWeightPage.Table.GetRow(0).Name.WaitText(weightName);
        }

        [Test, Description("Создания размера")]
        public void CreateSizeTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminReference.Click();
            adminPage.Intervals.Mouseover();
            adminPage.IntervalsSize.Click();
            var intervalsSizePage = adminPage.GoTo<IntervalsSizePage>();
            intervalsSizePage.Table.RowSearch.Name.SetValue(sizeName);
            intervalsSizePage = intervalsSizePage.SeachButtonRowClickAndGo();

            while (intervalsSizePage.Table.GetRow(0).Name.IsPresent)
            {
                intervalsSizePage.Table.GetRow(0).ActionsDelete.Click();
                intervalsSizePage = intervalsSizePage.GoTo<IntervalsSizePage>();
                intervalsSizePage.Table.RowSearch.Name.SetValue(sizeName);
                intervalsSizePage = intervalsSizePage.SeachButtonRowClickAndGo();
            }
            intervalsSizePage.IntervalSizeCreate.Click();
            var intervalSizeCreatePage = intervalsSizePage.GoTo<IntervalSizeCreatePage>();
            intervalSizeCreatePage.Name.SetValueAndWait(sizeName);
            intervalSizeCreatePage.Side1Min.SetValueAndWait(side1Min);
            intervalSizeCreatePage.Side2Min.SetValueAndWait(side2Min);
            intervalSizeCreatePage.Side3Min.SetValueAndWait(side3Min);
            intervalSizeCreatePage.SidesSumMin.SetValueAndWait(sidesSumMin);
            intervalSizeCreatePage.VolumeMin.SetValueAndWait(volumeMin);
            intervalSizeCreatePage.Side1Max.SetValueAndWait(side1Max);
            intervalSizeCreatePage.Side2Max.SetValueAndWait(side2Max);
            intervalSizeCreatePage.Side3Max.SetValueAndWait(side3Max);
            intervalSizeCreatePage.SidesSumMax.SetValueAndWait(sidesSumMax);
            intervalSizeCreatePage.VolumeMax.SetValueAndWait(volumeMax);
            intervalSizeCreatePage.SaveButton.Click();
            intervalsSizePage = intervalSizeCreatePage.GoTo<IntervalsSizePage>();

            intervalsSizePage.Table.RowSearch.Name.SetValue(sizeName);
            intervalsSizePage = intervalsSizePage.SeachButtonRowClickAndGo();
            intervalsSizePage.Table.GetRow(0).Name.WaitText(sizeName);
        }
    }
}