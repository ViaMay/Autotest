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

            AdminСompanies = new Link(By.LinkText("Компании"));
            Сompanies = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li"));
            PaymentPrice = new Link(By.LinkText("Наложенный платеж"));
            DeliveryPoints = new Link(By.LinkText("Пункты выдачи"));
            Prices = new Link(By.LinkText("Цены"));
            PricesPickup = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(9) > ul > li:nth-child(1)"));
            PricesSelf = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(9) ul > li:nth-child(2)"));
            PricesCourier = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(9) ul > li:nth-child(3)"));

            AdminUsers = new Link(By.LinkText("Пользователи"));
            Users = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li"));
            UsersWarehouses = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(5)"));
            UsersShops = new Link(By.CssSelector("li.dropdown.open > ul.dropdown-menu > li:nth-child(4)"));

            UserLogOut = new Link(By.LinkText("Выход"));

            Loader = new LoaderControl();
        }

        public Link DDeliveryLink { get; set; }

        public Link Intervals { get; set; }
        public Link IntervalsWeight { get; set; }
        public Link IntervalsSize { get; set; }

        public Link AdminСompanies { get; set; }
        public Link Сompanies { get; set; }
        public Link PaymentPrice { get; set; }
        public Link DeliveryPoints { get; set; }
        public Link Prices { get; set; }
        public Link PricesSelf { get; set; }
        public Link PricesPickup { get; set; }
        public Link PricesCourier { get; set; }

        public Link AdminReference { get; set; }
        public Link LegalEntities { get; set; }

        public Link AdminUsers { get; set; }
        public Link Users { get; set; }
        public Link UsersWarehouses { get; set; }
        public Link UsersShops { get; set; }

        public Link UserLogOut { get;  set; }

        public LoaderControl Loader { get; private set; }

        public override void BrowseWaitVisible()
        {
            DDeliveryLink.WaitVisible();
            UserLogOut.WaitVisible();
        }

        public DefaultPage LoginOut()
        {
            DDeliveryLink.Click();
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