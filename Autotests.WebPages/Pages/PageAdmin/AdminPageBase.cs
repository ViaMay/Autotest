using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.Utilities.WebWorms.FuncTests;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public abstract class AdminPageBase : CommonPageBase
    {
        protected AdminPageBase()
        {
            DDeliveryLink = new Link(By.LinkText("DDelivery"));

            DirectoryList = new Link(By.LinkText("Справочники"));

            Geography = new Link(By.LinkText("География"));
            GeographyRegions = new Link(By.LinkText("Регионы"));
            GeographyCities = new Link(By.LinkText("Города"));
            GeographyDestinations = new Link(By.LinkText("Направления"));

            Intervals = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(2)"));
            IntervalsWeight = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(2)> ul > li:nth-child(1)"));
            IntervalsSize = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(2)> ul > li:nth-child(2)"));
            IntervalsCodes = new Link(By.LinkText("Штрих-коды"));

            LegalEntities = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(3)"));
            DirectoryСalendars = new Link(By.LinkText("Календарь"));
            DirectoryStatus = new Link(By.LinkText("Статусы"));

            AdminCompanies = new Link(By.LinkText("Компании"));
            Companies = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li"));
            Calendars = new Link(By.LinkText("Календари"));
            PickupTimetable = new Link(By.LinkText("График забора"));
            CompanyWarehouses = new Link(By.LinkText("Склады"));
            DeliveryPoints = new Link(By.LinkText("Пункты выдачи"));
            Managers = new Link(By.LinkText("Менеджеры"));
            PaymentPrice = new Link(By.LinkText("Наложенный платеж"));
            SmsTemplates = new Link(By.LinkText("SMS-информирование"));
            OrderEditTemplates = new Link(By.LinkText("Редактирование заявок"));
            
            Prices = new Link(By.LinkText("Цены"));
            PricesPickup = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(11) > ul > li:nth-child(1)"));
            PricesSelf = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(11) ul > li:nth-child(2)"));
            PricesCourier = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(11) ul > li:nth-child(3)"));
            PricesPacking = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(11) ul > li:nth-child(4)"));

            Times = new Link(By.LinkText("Сроки"));
            TimesPickup = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(10) > ul > li:nth-child(1)"));
            TimesSelf = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(10) ul > li:nth-child(2)"));
            TimesCourier = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(10) ul > li:nth-child(3)"));

            Fees = new Link(By.LinkText("Комиссии"));
            FeesValue = new Link(By.LinkText("Системы"));
            FeesDeclaredPrice = new Link(By.LinkText("Оценочной стоимости"));
            FeesPaymentPrice = new Link(By.LinkText("Наложенного платежа"));

            Margins = new Link(By.LinkText("Наценки"));
            MarginsValue = new Link(By.LinkText("Значения"));
            MargindisCounts = new Link(By.LinkText("Скидки"));

            OrderedIttemplates = new Link(By.LinkText("Редактирование заявок"));

            AdminUsers = new Link(By.LinkText("Пользователи"));
            Users = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li"));
            UsersGroups = new Link(By.LinkText("Группы"));
            UsersWarehouses = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(5)"));
            UsersShops = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(4)"));
            UsersSupport = new Link(By.LinkText("Поддержка"));
            UsersSupportQuestion = new Link(By.LinkText("Вопросы"));
            UsersSupportTypes = new Link(By.LinkText("Настройка"));
            UsersSupportFreshDesk = new Link(By.LinkText("FreshDesk"));

            Orders = new Link(By.LinkText("Заказы"));
            OrderPickup = new Link(By.LinkText("Забор"));
            OrderInput = new Link(By.LinkText("Входящие"));
            OrderOutput = new Link(By.LinkText("Исходящие"));
            Documents = new Link(By.LinkText("Документы"));
            Сalculator = new Link(By.LinkText("Калькулятор"));

            Reports = new Link(By.LinkText("Отчеты"));
            ReportsRequest = new Link(By.LinkText("Заявки"));
            ReportsOrder = new Link(By.LinkText("Отчет по заявкам"));
            ReportsOrdePickupr = new Link(By.LinkText("Отчет по сортировке"));
            ReportsOrderUnchanging = new Link(By.LinkText("Неизменяемые заявки"));
            ReportsOrderPickupWarehouse = new Link(By.LinkText("Заявки на складе заборщика"));
            ReportsCalculatorLog = new Link(By.LinkText("Журнал калькулятора"));
            ReportsDLog = new Link(By.LinkText("Общий журнал"));
            ReportsData = new Link(By.LinkText("Целосность данных"));
            ReportsExportCsv = new Link(By.LinkText("Экспорт CSV"));
            ReportsPimpayLog = new Link(By.LinkText("История подключений PimPay"));
            
            System = new Link(By.LinkText("Система"));
            SystemMaintenance = new Link(By.LinkText("Ручное управление"));
            SystemCronjobs = new Link(By.LinkText("Планировщик задач"));
            SystemTools = new Link(By.LinkText("Инструменты"));
            SystemToolsValidatePrice = new Link(By.LinkText("Проверка прайсов"));
            SystemToolsPrintStickers = new Link(By.LinkText("Печать наклеек"));

            UserLogOut = new Link(By.LinkText("Выход"));

            Loader = new LoaderControl();
        }

        public Link DDeliveryLink { get; set; }

        public Link DirectoryList { get; set; }
        public Link LegalEntities { get; set; }
        public Link DirectoryСalendars { get; set; }
        public Link DirectoryStatus { get; set; }

        public Link Geography { get; set; }
        public Link GeographyRegions { get; set; }
        public Link GeographyCities { get; set; }
        public Link GeographyDestinations { get; set; }

        public Link Intervals { get; set; }
        public Link IntervalsWeight { get; set; }
        public Link IntervalsSize { get; set; }
        public Link IntervalsCodes { get; set; }

        public Link AdminCompanies { get; set; }
        public Link Companies { get; set; }
        public Link Managers { get; set; }
        public Link PickupTimetable { get; set; }
        public Link Calendars { get; set; }
        public Link PaymentPrice { get; set; }
        public Link DeliveryPoints { get; set; }
        public Link CompanyWarehouses { get; set; }
        public Link SmsTemplates { get; set; }
        public Link OrderEditTemplates { get; set; }

        public Link Prices { get; set; }
        public Link PricesSelf { get; set; }
        public Link PricesPickup { get; set; }
        public Link PricesCourier { get; set; }
        public Link PricesPacking { get; set; }

        public Link Times { get; set; }
        public Link TimesSelf { get; set; }
        public Link TimesPickup { get; set; }
        public Link TimesCourier { get; set; }

        public Link Margins { get; set; }
        public Link MarginsValue { get; set; }
        public Link MargindisCounts { get; set; }

        public Link Fees { get; set; }
        public Link FeesValue { get; set; }
        public Link FeesDeclaredPrice { get; set; }
        public Link FeesPaymentPrice { get; set; }

        public Link OrderedIttemplates { get; set; }

        public Link AdminUsers { get; set; }
        public Link Users { get; set; }
        public Link UsersGroups { get; set; }
        public Link UsersWarehouses { get; set; }
        public Link UsersShops { get; set; }
        public Link UsersSupport { get; set; }
        public Link UsersSupportQuestion { get; set; }
        public Link UsersSupportTypes { get; set; }
        public Link UsersSupportFreshDesk { get; set; }

        public Link Orders { get; set; }
        public Link OrderPickup { get; set; }
        public Link OrderInput { get; set; }
        public Link OrderOutput { get; set; }
        public Link Documents { get; set; }
        public Link Сalculator { get; set; }
        
        public Link Reports { get; set; }
        public Link ReportsRequest { get; set; }
        public Link ReportsOrder { get; set; }
        public Link ReportsOrdePickupr { get; set; }
        public Link ReportsOrderUnchanging { get; set; }
        public Link ReportsOrderPickupWarehouse { get; set; }
        public Link ReportsCalculatorLog { get; set; }
        public Link ReportsDLog { get; set; }
        public Link ReportsData { get; set; }
        public Link ReportsExportCsv { get; set; }
        public Link ReportsPimpayLog { get; set; }

        public Link System { get; set; }
        public Link SystemMaintenance { get; set; }
        public Link SystemCronjobs { get; set; }
        public Link SystemTools { get; set; }
        public Link SystemToolsValidatePrice { get; set; }
        public Link SystemToolsPrintStickers { get; set; }

        public Link UserLogOut { get;  set; }

        public LoaderControl Loader { get; private set; }

        public override void BrowseWaitVisible()
        {
            DDeliveryLink.WaitVisible();
            UserLogOut.WaitVisible();
        }

        public DefaultPage LoginOut()
        {
            UserLogOut.Click();
            return GoTo<DefaultPage>();
        }


        public void DownloadPdfFile(Link link, int maximalWaitTime = 15000, int expectedFilesCount = 1)
        {
            link.Click();
            WebDriverCache.WebDriver.WaitDownloadFiles(expectedFilesCount, maximalWaitTime);
            Loader.WaitInvisibleWithRetries();
        }

        public void ClearDownloadDirectory()
        {
            WebDriverCache.WebDriver.CleanDownloadDirectory();
        }
    }
}