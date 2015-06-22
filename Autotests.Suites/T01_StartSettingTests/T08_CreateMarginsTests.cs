using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
{
    public class T08CreateMarginsTests : ConstVariablesTestBase
    {
        [Test, Description("редактирование наценок")]
        public void CreateMarginsTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Margins.Mouseover();
            adminPage.MarginsValue.Click();
            var marginsValuePage = adminPage.GoTo<MarginsValuePage>();

            Assert.AreEqual(marginsValuePage.Table.GetRow(0).Name.GetText(), "Забор");
            marginsValuePage.Table.GetRow(0).ActionsEdit.Click();
            var marginsValueCreatePage = marginsValuePage.GoTo<MarginsValueCreatePage>();
            marginsValueCreatePage.Value.SetValue(marginsPickup.ToString());
            marginsValueCreatePage.Mode.SelectValue("Рубли");
            marginsValueCreatePage.SaveButton.Click();
            marginsValuePage = marginsValueCreatePage.GoTo<MarginsValuePage>();

            Assert.AreEqual(marginsValuePage.Table.GetRow(2).Name.GetText(), "Самовывоз");
            marginsValuePage.Table.GetRow(2).ActionsEdit.Click();
            marginsValueCreatePage = marginsValuePage.GoTo<MarginsValueCreatePage>();
            marginsValueCreatePage.Value.SetValue(marginsSelf.ToString());
            marginsValueCreatePage.Mode.SelectValue("Рубли");
            marginsValueCreatePage.SaveButton.Click();
            marginsValuePage = marginsValueCreatePage.GoTo<MarginsValuePage>();

            Assert.AreEqual(marginsValuePage.Table.GetRow(4).Name.GetText(), "Курьерская доставка");
            marginsValuePage.Table.GetRow(4).ActionsEdit.Click();
            marginsValueCreatePage = marginsValuePage.GoTo<MarginsValueCreatePage>();
            marginsValueCreatePage.Value.SetValue(marginsCourirs.ToString());
            marginsValueCreatePage.Mode.SelectValue("Рубли");
            marginsValueCreatePage.SaveButton.Click();
            marginsValuePage = marginsValueCreatePage.GoTo<MarginsValuePage>();

        }

    }
}