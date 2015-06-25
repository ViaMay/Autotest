using Autotests.WebPages.Pages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T04_AdminTests
{

    public class ClickAllAdminPagesTests : ConstVariablesTestBase
    {
        [Test, Description("по всем страницам справочника")]
        public void ClickAllDirectoryListTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.DirectoryList.Click();
            adminPage.Geography.Mouseover();
            adminPage.GeographyRegions.Click();
            var page = adminPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Регионы""");
            page.Create.Click();
            var createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Регионы""");

            createPage.DirectoryList.Click();
            createPage.Geography.Mouseover();
            createPage.GeographyCities.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Города""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Города""");

            createPage.DirectoryList.Click();
            createPage.Geography.Mouseover();
            createPage.GeographyDestinations.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Направления""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Направления""");

            createPage.DirectoryList.Click();
            createPage.Intervals.Mouseover();
            createPage.IntervalsCodes.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Штрих-коды компании""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Штрих-коды компании""");

            createPage.DirectoryList.Click();
            createPage.DirectoryСalendars.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Эталонный календарь выходных дней""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Эталонный календарь выходных дней""");

            createPage.DirectoryList.Click();
            createPage.DirectoryStatus.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Статусы компаний доставки""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Статусы компаний доставки""");
        }

        [Test, Description("по всем страницам компании")]
        public void ClickAllCompaniesListTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.CompanyWarehouses.Click();
            var page = adminPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Склады компаний""");
            page.Create.Click();
            var createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText("Склад компании");

            createPage.AdminCompanies.Click();
            createPage.Managers.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Менеджеры""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Менеджеры""");

            createPage.AdminCompanies.Click();
            createPage.SmsTemplates.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""SMS-шаблоны""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""SMS-шаблоны""");

            createPage.AdminCompanies.Click();
            createPage.OrderEditTemplates.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Редактирование заявок - шаблоны""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Редактирование заявок - шаблоны""");

            createPage.AdminCompanies.Click();
            createPage.Fees.Mouseover();
            createPage.FeesValue.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Комиссия""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Комиссия""");

            createPage.AdminCompanies.Click();
            createPage.Fees.Mouseover();
            createPage.FeesDeclaredPrice.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Комиссия страховки""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Комиссия страховки""");

            createPage.AdminCompanies.Click();
            createPage.Fees.Mouseover();
            createPage.FeesPaymentPrice.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Комиссия на наложенный платеж""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Комиссия на наложенный платеж""");

            createPage.AdminCompanies.Click();
            createPage.Margins.Mouseover();
            createPage.MargindisCounts.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Скидки""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Скидки""");
        }

        [Test, Description("по всем страницам для пользователя")]
        public void ClickAllUsersListTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersGroups.Click();
            var page = adminPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Группы""");
            page.Create.Click();
            var createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Группы""");

            createPage.AdminUsers.Click();
            createPage.UsersSupport.Mouseover();
            createPage.UsersSupportQuestion.Click();
            var pageS = createPage.GoTo<SupportAdminListPage>();
            pageS.Create.Click();
            createPage = pageS.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Запросы""");

            createPage.AdminUsers.Click();
            createPage.UsersSupport.Mouseover();
            createPage.UsersSupportTypes.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Типы тикетов""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Типы тикетов""");

            createPage.AdminUsers.Click();
            createPage.UsersSupport.Mouseover();
            createPage.UsersSupportFreshDesk.Click();
//            var pageFreshDesk = createPage.GoTo<SupportFreshDeskPage>();
//            pageFreshDesk.LabelDirectory.WaitText("Служба поддержки");
        }
        
        [Test, Description("по всем страницам для закавов")]
        public void ClickAllOrdersListTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.Orders.Click();
            adminPage.Сalculator.Click();
            var page = adminPage.GoTo<СalculatorPage>();
            page.LabelDirectory.WaitText("Маршрут");
        }

        [Test, Description("по всем страницам для очетов")]
        public void ClickAllReportsListTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.Reports.Click();
            adminPage.ReportsRequest.Mouseover();
            adminPage.ReportsOrder.Click();
            var page = adminPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Отчет по заявкам");

            page.Reports.Click();
            page.ReportsRequest.Mouseover();
            page.ReportsOrdePickupr.Click();
            page = page.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Отчет по заявкам");

            page.Reports.Click();
            page.ReportsRequest.Mouseover();
            page.ReportsOrderUnchanging.Click();
            page = page.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Заявки со статусом ""На складе ИМ"", который долго не меняется""");

            page.Reports.Click();
            page.ReportsRequest.Mouseover();
            page.ReportsOrderPickupWarehouse.Click();
            page = page.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Заказы на складе забора");
            
            page.Reports.Click();
            page.ReportsCalculatorLog.Click();
            page = page.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Журнал калькулятора""");
            page.Create.Click();
            var createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Журнал калькулятора""");

            createPage.Reports.Click();
            createPage.ReportsDLog.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Общий журнал");

            page.Reports.Click();
            page.ReportsData.Click();
            page = page.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Целосность данных");

            page.Reports.Click();
            page.ReportsExportCsv.Click();
            page = page.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Экспорт в CSV""");

            page.Reports.Click();
            page.ReportsPimpayLog.Click();
            page = page.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""История подключений PimPay""");
//            page.Create.Click();
//            createPage = page.GoTo<AdminBaseListCreatePage>();
//            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""История подключений PimPay""");
        }

        [Test, Description("по всем страницам для очетов")]
        public void ClickAllSystemListTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.System.Click();
            adminPage.SystemMaintenance.Click();
            var page = adminPage.GoTo<AdminMaintenancePage>();
            page.LabelDirectory.WaitText(@"Системные действия");

            page.System.Click();
            page.SystemCronjobs.Click();
            var pageS = page.GoTo<AdminBaseListPage>();
            pageS.LabelDirectory.WaitText(@"Справочник ""Регулярные действия""");
            pageS.Create.Click();
            var createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Регулярные действия""");

            createPage.System.Click();
            createPage.SystemTools.Mouseover();
            createPage.SystemToolsValidatePrice.Click();
            pageS = createPage.GoTo<AdminBaseListPage>();
            pageS.LabelDirectory.WaitText(@"Проверка прайсов");

            pageS.System.Click();
            pageS.SystemTools.Mouseover();
            pageS.SystemToolsPrintStickers.Click();
            pageS = pageS.GoTo<AdminBaseListPage>();
            pageS.LabelDirectory.WaitText(@"Печать наклеек");
        }

        [Test, Description("по всем страницам для очетов")]
        public void ClickAllExitListTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.UserLogOut.Click();
            var page = adminPage.GoTo<DefaultPage>();
        }


        [Test, Description("по всем страницам для очетов")]
        public void ClickAllReferenceListTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            var pageCreate = LoadPage<AdminBaseListCreatePage>("admin/calendar/fill_calendar");
            pageCreate.LabelDirectory.WaitText("Заполнить календарь компании");

            var page = LoadPage<AdminBaseListPage>("admin/companydeliverystatushistory/");
            page.LabelDirectory.WaitText(@"Справочник ""История статусов доставки ТК""");

            page = LoadPage<AdminBaseListPage>("admin/companies_history?");
            pageCreate.LabelDirectory.WaitText(@"История юридических лиц компании");

            pageCreate = LoadPage<AdminBaseListCreatePage>("admin/margins/edit/1");
            pageCreate.LabelDirectory.WaitText(@"Edit record #1 in model ""Наценки""");

            page = LoadPage<AdminBaseListPage>("admin/users_history?");
            page.LabelDirectory.WaitText(@"История пользователя");

            pageCreate = LoadPage<AdminBaseListCreatePage>("admin/tickets/edit/1");
            pageCreate.LabelDirectory.WaitText(@"Edit record #1 in model ""Запросы""");

            pageCreate = LoadPage<AdminBaseListCreatePage>("admin/exportcsv/edit/1");
            pageCreate.LabelDirectory.WaitText(@"Edit record #1 in model ""Экспорт в CSV""");
        }
    }
}