using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T01_StartSettingTests
{
    public class T01CreateLegalEntityTest : ConstVariablesTestBase
    {
        [Test, Description("Создания юредического лица для Компании")]
        public void CreateLegalEntityTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminReference.Click();
            adminPage.LegalEntities.Click();
            var legalEntitiesPage = adminPage.GoTo<LegalEntitiesPage>();

            legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
            legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();

            while (legalEntitiesPage.Table.GetRow(0).Name.IsPresent)
            {
                legalEntitiesPage.Table.GetRow(0).ActionsDelete.Click();
                legalEntitiesPage = legalEntitiesPage.GoTo<LegalEntitiesPage>();
                legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
                legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();
            }
            legalEntitiesPage.LegalEntityCreate.Click();
            var legalEntityCreatePage = legalEntitiesPage.GoTo<LegalEntityCreatePage>();
            legalEntityCreatePage.NameEntity.SetValueAndWait(legalEntityName);
            legalEntityCreatePage.SaveButton.Click();
            legalEntitiesPage = legalEntityCreatePage.GoTo<LegalEntitiesPage>();

            legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
            legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();
            legalEntitiesPage.Table.GetRow(0).Name.WaitText(legalEntityName);
        }

    }
}