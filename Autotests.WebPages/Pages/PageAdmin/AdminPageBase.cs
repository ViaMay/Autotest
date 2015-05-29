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

            AdminReference = new Link(By.LinkText("Справочники"));
            LegalEntities = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(3)"));
            Intervals = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(2)"));
            IntervalsWeight = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(2)> ul > li:nth-child(1)"));
            IntervalsSize = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(2)> ul > li:nth-child(2)"));

            AdminCompanies = new Link(By.LinkText("Компании"));
            Companies = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li"));
            Managers = new Link(By.LinkText("Менеджеры"));
            Calendars = new Link(By.LinkText("Календари"));
            PickupTimetable = new Link(By.LinkText("График забора"));
            PaymentPrice = new Link(By.LinkText("Наложенный платеж"));
            DeliveryPoints = new Link(By.LinkText("Пункты выдачи"));
            Prices = new Link(By.LinkText("Цены"));
            PricesPickup = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(11) > ul > li:nth-child(1)"));
            PricesSelf = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(11) ul > li:nth-child(2)"));
            PricesCourier = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(11) ul > li:nth-child(3)"));

            Times = new Link(By.LinkText("Сроки"));
            TimesPickup = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(10) > ul > li:nth-child(1)"));
            TimesSelf = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(10) ul > li:nth-child(2)"));
            TimesCourier = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(10) ul > li:nth-child(3)"));

            AdminUsers = new Link(By.LinkText("Пользователи"));
            Users = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li"));
            UsersWarehouses = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(5)"));
            UsersShops = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(4)"));

            Orders = new Link(By.LinkText("Заказы"));
            OrderInput = new Link(By.LinkText("Входящие"));
            OrderOutput = new Link(By.LinkText("Исходящие"));

            UserLogOut = new Link(By.LinkText("Выход"));

            Loader = new LoaderControl();
        }

        public Link DDeliveryLink { get; set; }

        public Link Intervals { get; set; }
        public Link IntervalsWeight { get; set; }
        public Link IntervalsSize { get; set; }

        public Link AdminCompanies { get; set; }
        public Link Companies { get; set; }
        public Link Managers { get; set; }
        public Link PickupTimetable { get; set; }
        public Link Calendars { get; set; }
        public Link PaymentPrice { get; set; }
        public Link DeliveryPoints { get; set; }
        public Link Prices { get; set; }
        public Link PricesSelf { get; set; }
        public Link PricesPickup { get; set; }
        public Link PricesCourier { get; set; }

        public Link Times { get; set; }
        public Link TimesSelf { get; set; }
        public Link TimesPickup { get; set; }
        public Link TimesCourier { get; set; }

        public Link AdminReference { get; set; }
        public Link LegalEntities { get; set; }

        public Link AdminUsers { get; set; }
        public Link Users { get; set; }
        public Link UsersWarehouses { get; set; }
        public Link UsersShops { get; set; }

        public Link Orders { get; set; }
        public Link OrderInput { get; set; }
        public Link OrderOutput { get; set; }

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