using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.StartSettingTests
{
    public class T09CreateOrderedIttemplatesTests : ConstVariablesTestBase
    {
        [Test, Description("Создание шаблонов")]
        public void CreateOrderedIttemplatesTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.OrderedIttemplates.Click();
            var orderedIttemplatesPage = adminPage.GoTo<OrderedIttemplatesPage>();

            orderedIttemplatesPage.Table.RowSearch.CompanyName.SetValue(companyName);
            orderedIttemplatesPage = orderedIttemplatesPage.SeachButtonRowClickAndGo();

            while (orderedIttemplatesPage.Table.GetRow(0).Name.IsPresent)
            {
                orderedIttemplatesPage.Table.GetRow(0).ActionsDelete.Click();
                orderedIttemplatesPage.Aletr.Accept();
                orderedIttemplatesPage = orderedIttemplatesPage.GoTo<OrderedIttemplatesPage>();
                orderedIttemplatesPage.Table.RowSearch.CompanyName.SetValue(companyName);
                orderedIttemplatesPage = orderedIttemplatesPage.SeachButtonRowClickAndGo();
            }
            orderedIttemplatesPage.Create.Click();
            var orderedIttemplatesCreatePage = orderedIttemplatesPage.GoTo<OrderedIttemplatesCreatePage>();
            orderedIttemplatesCreatePage.CompanyName.SetFirstValueSelect(companyName);
            orderedIttemplatesCreatePage.Through.SelectValue("Email");
            orderedIttemplatesCreatePage.Action.SelectValue("Отмена заказа");
            orderedIttemplatesCreatePage.ThroughEmail.SetValueAndWait(adminName);
            orderedIttemplatesCreatePage.Subject.SetValueAndWait("Отмена заказа #dd");
            orderedIttemplatesCreatePage.Message.SetValueAndWait("Сообщение на отмену заказа #dd");
            orderedIttemplatesCreatePage.SaveButton.Click();
            orderedIttemplatesPage = orderedIttemplatesCreatePage.GoTo<OrderedIttemplatesPage>();

            orderedIttemplatesPage.Create.Click();
            orderedIttemplatesCreatePage = orderedIttemplatesPage.GoTo<OrderedIttemplatesCreatePage>();
            orderedIttemplatesCreatePage.CompanyName.SetFirstValueSelect(companyName);
            orderedIttemplatesCreatePage.Through.SelectValue("Email");
            orderedIttemplatesCreatePage.Action.SelectValue("Редактирование заказа");
            orderedIttemplatesCreatePage.ThroughEmail.SetValueAndWait(adminName);
            orderedIttemplatesCreatePage.Subject.SetValueAndWait("Редактирование заказа #dd");
            orderedIttemplatesCreatePage.Message.SetValueAndWait("список измененных полей и новых значений: #changed");
            orderedIttemplatesCreatePage.SaveButton.Click();
            orderedIttemplatesPage = orderedIttemplatesCreatePage.GoTo<OrderedIttemplatesPage>();
        }
    }
}