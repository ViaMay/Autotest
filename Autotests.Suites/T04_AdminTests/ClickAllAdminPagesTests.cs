using Autotests.WebPages.Pages;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageFreshDesk;
using NUnit.Framework;

namespace Autotests.Tests.T04_AdminTests
{

    public class ClickAllAdminPagesTests : ConstVariablesTestBase
    {
        [Test, Description("по всем страницам справочника"), Ignore]
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
            createPage.IntervalsWeight.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Веса""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Веса""");

            createPage.DirectoryList.Click();
            createPage.Intervals.Mouseover();
            createPage.IntervalsSize.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Размеры""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Размеры""");

            createPage.DirectoryList.Click();
            createPage.Intervals.Mouseover();
            createPage.IntervalsCodes.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Штрих-коды компании""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Штрих-коды компании""");

            createPage.DirectoryList.Click();
            createPage.LegalEntities.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Юридическое лицо""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Юридическое лицо""");

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

        [Test, Description("по всем страницам справочника"), Ignore]
        public void ClickAllDirectoryListEditTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.DirectoryList.Click();
            adminPage.Geography.Mouseover();
            adminPage.GeographyRegions.Click();
            var page = adminPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Регионы""");
            page.Table.GetRow(0).ActionsEdit.Click();
            var editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Регионы""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.DirectoryList.Click();
            editPage.Geography.Mouseover();
            editPage.GeographyCities.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Города""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Города""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.DirectoryList.Click();
            editPage.Geography.Mouseover();
            editPage.GeographyDestinations.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Направления""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Направления""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.DirectoryList.Click();
            editPage.Intervals.Mouseover();
            editPage.IntervalsWeight.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Веса""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Веса""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.DirectoryList.Click();
            editPage.Intervals.Mouseover();
            editPage.IntervalsSize.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Размеры""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Размеры""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.DirectoryList.Click();
            editPage.Intervals.Mouseover();
            editPage.IntervalsCodes.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Штрих-коды компании""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Штрих-коды компании""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.DirectoryList.Click();
            editPage.LegalEntities.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Юридическое лицо""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Юридическое лицо""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.DirectoryList.Click();
            editPage.DirectoryСalendars.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Эталонный календарь выходных дней""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Эталонный календарь выходных дней""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.DirectoryList.Click();
            editPage.DirectoryStatus.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Статусы компаний доставки""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Статусы компаний доставки""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");
        }

        [Test, Description("по всем страницам компании"), Ignore]
        public void ClickAllCompaniesListTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<CompaniesPage>();
            companiesPage.Create.Click();
            var companyCreatePage = companiesPage.GoTo<CompanyCreatePage>();
            companyCreatePage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Компании""");

            companyCreatePage.AdminCompanies.Click();
            companyCreatePage.Calendars.Click();
            var page = companyCreatePage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Календарь выходных дней компании""");
            page.Create.Click();
            var createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Календарь выходных дней компании""");

            createPage.AdminCompanies.Click();
            createPage.PickupTimetable.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Время отправки забора""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Время отправки забора""");

            createPage.AdminCompanies.Click();
            createPage.CompanyWarehouses.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Склады компаний""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText("Склад компании");

            createPage.AdminCompanies.Click();
            createPage.DeliveryPoints.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Пункты выдачи""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Пункты выдачи""");

            createPage.AdminCompanies.Click();
            createPage.Managers.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Менеджеры""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Менеджеры""");

            createPage.AdminCompanies.Click();
            createPage.PaymentPrice.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Возможность наложенного платежа""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Возможность наложенного платежа""");

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
            createPage.Times.Mouseover();
            createPage.TimesCourier.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Сроки доставки курьерки""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Сроки доставки курьерки""");

            createPage.AdminCompanies.Click();
            createPage.Times.Mouseover();
            createPage.TimesPickup.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Время забора""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Время забора""");
            createPage.AdminCompanies.Click();

            createPage.Times.Mouseover();
            createPage.TimesSelf.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Сроки доставки""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Сроки доставки""");

            createPage.AdminCompanies.Click();
            createPage.Prices.Mouseover();
            createPage.PricesCourier.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Цены на курьерку""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Цены на курьерку""");

            createPage.AdminCompanies.Click();
            createPage.Prices.Mouseover();
            createPage.PricesPickup.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Цены забора""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Цены забора""");
            createPage.AdminCompanies.Click();

            createPage.Prices.Mouseover();
            createPage.PricesSelf.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Цены на самовывоз""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Цены на самовывоз""");

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
            createPage.MarginsValue.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Наценки""");

            page.AdminCompanies.Click();
            page.Margins.Mouseover();
            page.MargindisCounts.Click();
            page = page.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Скидки""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Скидки""");
        }

        [Test, Description("по всем страницам компании"), Ignore]
        public void ClickAllCompaniesListEditTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<CompaniesPage>();
            companiesPage.Table.GetRow(0).ActionsEdit.Click();
            var editPage = companiesPage.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains("Компания");

            editPage.AdminCompanies.Click();
            editPage.Calendars.Click();
            var page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Календарь выходных дней компании""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Календарь выходных дней компании""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.PickupTimetable.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Время отправки забора""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Время отправки забора""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.CompanyWarehouses.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Склады компаний""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains("Склад компании");

            editPage.AdminCompanies.Click();
            editPage.DeliveryPoints.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Пункты выдачи""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Пункты выдачи""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.Managers.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Менеджеры""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Менеджеры""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.PaymentPrice.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Возможность наложенного платежа""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Возможность наложенного платежа""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.SmsTemplates.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""SMS-шаблоны""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""SMS-шаблоны""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.OrderEditTemplates.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Редактирование заявок - шаблоны""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Редактирование заявок - шаблоны""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.Times.Mouseover();
            editPage.TimesCourier.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Сроки доставки курьерки""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Сроки доставки курьерки""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.Times.Mouseover();
            editPage.TimesPickup.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Время забора""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Время забора""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.Times.Mouseover();
            editPage.TimesSelf.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Сроки доставки""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Сроки доставки""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.Prices.Mouseover();
            editPage.PricesCourier.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Цены на курьерку""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Цены на курьерку""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.Prices.Mouseover();
            editPage.PricesPickup.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Цены забора""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Цены забора""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.Prices.Mouseover();
            editPage.PricesSelf.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Цены на самовывоз""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Цены на самовывоз""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.Fees.Mouseover();
            editPage.FeesValue.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Комиссия""");
            if (page.Table.GetRow(0).ActionsEdit.IsPresent)
            {
                page.Table.GetRow(0).ActionsEdit.Click();
                editPage = page.GoTo<AdminBaseListCreatePage>();
                editPage.LabelDirectory.WaitTextContains(@"""Комиссия""");
                editPage.LabelDirectory.WaitTextContains(@"Edit record");

                editPage.AdminCompanies.Click();
                editPage.Fees.Mouseover();
                editPage.FeesDeclaredPrice.Click();
                page = editPage.GoTo<AdminBaseListPage>();
            }
            else
            {
                page.AdminCompanies.Click();
                page.Fees.Mouseover();
                page.FeesDeclaredPrice.Click();
                page = page.GoTo<AdminBaseListPage>();
            }
            page.LabelDirectory.WaitText(@"Справочник ""Комиссия страховки""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Комиссия страховки""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.Fees.Mouseover();
            editPage.FeesPaymentPrice.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Комиссия на наложенный платеж""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"Комиссия на наложенный платеж");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminCompanies.Click();
            editPage.Margins.Mouseover();
            editPage.MarginsValue.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Наценки""");

            page.AdminCompanies.Click();
            page.Margins.Mouseover();
            page.MargindisCounts.Click();
            page = page.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Скидки""");
            if (page.Table.GetRow(0).ActionsEdit.IsPresent)
            {
                page.Table.GetRow(0).ActionsEdit.Click();
                editPage = page.GoTo<AdminBaseListCreatePage>();
                editPage.LabelDirectory.WaitTextContains(@"""Скидки""");
                editPage.LabelDirectory.WaitTextContains(@"Edit record");
            }
        }

        [Test, Description("по всем страницам для пользователя"), Ignore]
        public void ClickAllUsersListEditTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersGroups.Click();
            var page = adminPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Группы""");
            page.Table.GetRow(0).ActionsEdit.Click();
            var editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Группы""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminUsers.Click();
            editPage.Users.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Пользователи""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains("Пользователь");

            editPage.AdminUsers.Click();
            editPage.UsersShops.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Интернет-Магазины""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Интернет-Магазины""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminUsers.Click();
            editPage.UsersWarehouses.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Склады""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"Склад компании");

            editPage.AdminUsers.Click();
            editPage.UsersSupport.Mouseover();
            editPage.UsersSupportQuestion.Click();
            var pageS = editPage.GoTo<SupportAdminListPage>();
            pageS.Table.GetRow(0).ColumnThreeLink.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Запросы""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.AdminUsers.Click();
            editPage.UsersSupport.Mouseover();
            editPage.UsersSupportTypes.Click();
            page = editPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Типы тикетов""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Типы тикетов""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");
        }

        [Test, Description("по всем страницам для пользователя"), Ignore]
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
            createPage.Users.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Пользователи""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Пользователь");

            createPage.AdminUsers.Click();
            createPage.UsersShops.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Интернет-Магазины""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Интернет-Магазины""");

            createPage.AdminUsers.Click();
            createPage.UsersWarehouses.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Склады""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Склад компании");

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
            var pageFreshDesk = createPage.GoTo<SupportFreshDeskPage>();
            pageFreshDesk.LabelDirectory.WaitText("Служба поддержки");
        }

        [Test, Description("по всем страницам для заказов"), Ignore]
        public void ClickAllOrdersListTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.Orders.Click();
            adminPage.Сalculator.Click();
            var pageC = adminPage.GoTo<СalculatorPage>();
            pageC.LabelDirectory.WaitText("Маршрут");

            pageC.Orders.Click();
            pageC.OrderInput.Click();
            var page = pageC.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Заявки""");

            page.Orders.Click();
            page.OrderOutput.Click();
            page = page.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Исходящие заявки""");
            page.Create.Click();
            var createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Исходящие заявки""");

            createPage.Orders.Click();
            createPage.OrderPickup.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Заявки на забор""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Заявки на забор""");

            createPage.Orders.Click();
            createPage.Documents.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Документы""");
            page.Create.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitText(@"Новая запись в справочнике ""Документы""");
        }

        [Test, Description("по всем страницам для заказов"), Ignore]
        public void ClickAllOrdersListEditTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.Orders.Click();
            adminPage.OrderInput.Click();
            var оrdersInputPage = adminPage.GoTo<OrdersInputPage>();
            оrdersInputPage.LabelDirectory.WaitText(@"Справочник ""Заявки""");
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();
            var оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            оrderEditPage.LabelDirectory.WaitTextContains("Заявка №");

            оrderEditPage.Orders.Click();
            оrderEditPage.OrderOutput.Click();
            var orderOutputPage = оrderEditPage.GoTo<OrdersOutputPage>();
            orderOutputPage.LabelDirectory.WaitText(@"Справочник ""Исходящие заявки""");
            orderOutputPage.Table.GetRow(0).ActionsEdit.Click();
            var оrderEditOutPage = orderOutputPage.GoTo<OrderOutputEditingPage>();
            оrderEditOutPage.LabelDirectory.WaitTextContains("Исходящая заявка №");

            оrderEditOutPage.Orders.Click();
            оrderEditOutPage.OrderPickup.Click();
            var page = оrderEditOutPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Заявки на забор""");
            page.Table.GetRow(0).ActionsEdit.Click();
            var createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitTextContains(@"""Заявки на забор""");
            createPage.LabelDirectory.WaitTextContains("Edit record");

            createPage.Orders.Click();
            createPage.Documents.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Документы""");
// TODO           это не удаления это вторая ссылка
            page.Table.GetRow(0).ActionsDelete.Click();
            createPage = page.GoTo<AdminBaseListCreatePage>();
            createPage.LabelDirectory.WaitTextContains(@"""Документы""");
            createPage.LabelDirectory.WaitTextContains("Edit record");
        }

        [Test, Description("по всем страницам для отчетов"), Ignore]
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
        }

        [Test, Description("по всем страницам для отчетов"), Ignore]
        public void ClickAllReportsListEditTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);

            adminPage.Reports.Click();
            adminPage.ReportsRequest.Mouseover();
            adminPage.ReportsOrderUnchanging.Click();
            var оrdersInputPage = adminPage.GoTo<OrdersInputPage>();
            оrdersInputPage.LabelDirectory.WaitText(@"Справочник ""Заявки со статусом ""На складе ИМ"", который долго не меняется""");
            оrdersInputPage.Table.GetRow(0).MoreInfo.Click();
            var оrderEditPage = оrdersInputPage.GoTo<OrderInputEditingPage>();
            оrderEditPage.LabelDirectory.WaitTextContains("Заявка №");


            оrderEditPage.Reports.Click();
            оrderEditPage.ReportsCalculatorLog.Click();
            var page = оrderEditPage.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Журнал калькулятора""");
            if (page.Table.GetRow(0).ActionsEdit.IsPresent)
            {
                page.Table.GetRow(0).ActionsEdit.Click();
                var editPage = page.GoTo<AdminBaseListCreatePage>();
                editPage.LabelDirectory.WaitTextContains(@"""Журнал калькулятора""");
                editPage.LabelDirectory.WaitTextContains(@"Edit record");

                editPage.Reports.Click();
                editPage.ReportsDLog.Click();
                page = editPage.GoTo<AdminBaseListPage>();
            }
            else
            {
                page.Reports.Click();
                page.ReportsDLog.Click();
                page = page.GoTo<AdminBaseListPage>();
            }
            page.LabelDirectory.WaitText(@"Общий журнал");
            page.Table.GetRow(0).ActionsEdit.Click();
            var editPageLog = page.GoTo<AdminBaseListCreatePage>();
            editPageLog.LabelDirectory.WaitTextContains(@"Запись в логе");

            editPageLog.Reports.Click();
            editPageLog.ReportsExportCsv.Click();
            page = editPageLog.GoTo<AdminBaseListPage>();
            page.LabelDirectory.WaitText(@"Справочник ""Экспорт в CSV""");
            page.Table.GetRow(0).ActionsEdit.Click();
            editPageLog = page.GoTo<AdminBaseListCreatePage>();
            editPageLog.LabelDirectory.WaitTextContains(@"Edit record");
            editPageLog.LabelDirectory.WaitTextContains(@"""Экспорт в CSV""");
        }

        [Test, Description("по всем страницам для системы"), Ignore]
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
            createPage.SystemCronjobs.Click();
            pageS = createPage.GoTo<AdminBaseListPage>();
            pageS.LabelDirectory.WaitText(@"Справочник ""Регулярные действия""");
            pageS.Table.GetRow(0).ActionsEdit.Click();
            var editPage = page.GoTo<AdminBaseListCreatePage>();
            editPage.LabelDirectory.WaitTextContains(@"""Регулярные действия""");
            editPage.LabelDirectory.WaitTextContains(@"Edit record");

            editPage.System.Click();
            editPage.SystemTools.Mouseover();
            editPage.SystemToolsValidatePrice.Click();
            pageS = editPage.GoTo<AdminBaseListPage>();
            pageS.LabelDirectory.WaitText(@"Проверка прайсов");

            pageS.System.Click();
            pageS.SystemTools.Mouseover();
            pageS.SystemToolsPrintStickers.Click();
            pageS = pageS.GoTo<AdminBaseListPage>();
            pageS.LabelDirectory.WaitText(@"Печать наклеек");
        }

        [Test, Description("по всем страницам для выход"), Ignore]
        public void ClickAllExitListTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.UserLogOut.Click();
            var page = adminPage.GoTo<DefaultPage>();
        }

        [Test, Description("по всем страницам для ссылки"), Ignore]
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