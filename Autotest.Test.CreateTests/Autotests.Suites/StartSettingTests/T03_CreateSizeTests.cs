using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.StartSettingTests
{
    public class T03CreateSizeTests : ConstVariablesTestBase
    {
        [Test, Description("Создания веса")]
        public void CreateWeightTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.DirectoryList.Click();
            adminPage.Intervals.Mouseover();
            adminPage.IntervalsWeight.Click();
            var intervalsWeightPage = adminPage.GoTo<AdminBaseListPage>();
            intervalsWeightPage.LabelDirectory.WaitText(@"Справочник ""Веса""");
            intervalsWeightPage.Table.RowSearch.Name.SetValue(weightName);
            intervalsWeightPage = intervalsWeightPage.SeachButtonRowClickAndGo();

            while (intervalsWeightPage.Table.GetRow(0).Name.IsPresent)
            {
                intervalsWeightPage.Table.GetRow(0).ActionsDelete.Click();
                intervalsWeightPage.Aletr.Accept();
                intervalsWeightPage = intervalsWeightPage.GoTo<AdminBaseListPage>();
                intervalsWeightPage.Table.RowSearch.Name.SetValue(weightName);
                intervalsWeightPage = intervalsWeightPage.SeachButtonRowClickAndGo();
            }
            intervalsWeightPage.Create.Click();
            var intervalWeightCreatePage = intervalsWeightPage.GoTo<IntervalWeightCreatePage>();
            intervalWeightCreatePage.Name.SetValueAndWait(weightName);
            intervalWeightCreatePage.Min.SetValueAndWait(weightMin.ToString());
            intervalWeightCreatePage.Max.SetValueAndWait(weightMax.ToString());
            intervalWeightCreatePage.SaveButton.Click();
            intervalsWeightPage = intervalWeightCreatePage.GoTo<AdminBaseListPage>();

            intervalsWeightPage.Table.RowSearch.Name.SetValue(weightName);
            intervalsWeightPage = intervalsWeightPage.SeachButtonRowClickAndGo();
            intervalsWeightPage.Table.GetRow(0).Name.WaitText(weightName);
        }

        [Test, Description("Создания размера")]
        public void CreateSideTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.DirectoryList.Click();
            adminPage.Intervals.Mouseover();
            adminPage.IntervalsSize.Click();
            var intervalsSizePage = adminPage.GoTo<AdminBaseListPage>();
            intervalsSizePage.LabelDirectory.WaitText(@"Справочник ""Размеры""");
            intervalsSizePage.Table.RowSearch.Name.SetValue(sideName);
            intervalsSizePage = intervalsSizePage.SeachButtonRowClickAndGo();

            while (intervalsSizePage.Table.GetRow(0).Name.IsPresent)
            {
                intervalsSizePage.Table.GetRow(0).ActionsDelete.Click();
                intervalsSizePage.Aletr.Accept();
                intervalsSizePage = intervalsSizePage.GoTo<AdminBaseListPage>();
                intervalsSizePage.Table.RowSearch.Name.SetValue(sideName);
                intervalsSizePage = intervalsSizePage.SeachButtonRowClickAndGo();
            }
            intervalsSizePage.Create.Click();
            var intervalSizeCreatePage = intervalsSizePage.GoTo<IntervalSideCreatePage>();
            intervalSizeCreatePage.Name.SetValueAndWait(sideName);
            intervalSizeCreatePage.Side1Min.SetValueAndWait(side1Min.ToString());
            intervalSizeCreatePage.Side2Min.SetValueAndWait(side2Min.ToString());
            intervalSizeCreatePage.Side3Min.SetValueAndWait(side3Min.ToString());
//            intervalSizeCreatePage.SidesSumMin.SetValueAndWait(sidesSumMin.ToString());
//            intervalSizeCreatePage.VolumeMin.SetValueAndWait(volumeMin.ToString());  
            intervalSizeCreatePage.SidesSumMin.SetValueAndWait("");
            intervalSizeCreatePage.VolumeMin.SetValueAndWait("");
            intervalSizeCreatePage.Side1Max.SetValueAndWait(side1Max.ToString());
            intervalSizeCreatePage.Side2Max.SetValueAndWait(side2Max.ToString());
            intervalSizeCreatePage.Side3Max.SetValueAndWait(side3Max.ToString());
//            intervalSizeCreatePage.SidesSumMax.SetValueAndWait(sidesSumMax.ToString());
//            intervalSizeCreatePage.VolumeMax.SetValueAndWait(volumeMax.ToString());
            intervalSizeCreatePage.SidesSumMax.SetValueAndWait("");
            intervalSizeCreatePage.VolumeMax.SetValueAndWait("");
            intervalSizeCreatePage.SaveButton.Click();
            intervalsSizePage = intervalSizeCreatePage.GoTo<AdminBaseListPage>();

            intervalsSizePage.Table.RowSearch.Name.SetValue(sideName);
            intervalsSizePage = intervalsSizePage.SeachButtonRowClickAndGo();
            intervalsSizePage.Table.GetRow(0).Name.WaitText(sideName);
        }
    }
}