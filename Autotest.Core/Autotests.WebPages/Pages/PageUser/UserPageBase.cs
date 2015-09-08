using Autotests.Utilities.WebTestCore.Pages;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public abstract class UserPageBase : CommonPageBase
    {
        protected UserPageBase()
        {
            DDeliveryLink = new Link(By.LinkText("DDelivery"));

            UseProfile = new Link(By.LinkText("Ваш профиль"));
            UserWarehouses = new Link(By.LinkText("Склады"));
            UserShops = new Link(By.LinkText("Магазины"));
            UserEditing = new Link(By.LinkText("Редактирование"));
            UserChangePassword = new Link(By.LinkText("Сменить пароль"));
            UserLogOut = new Link(By.LinkText("Выход"));

            Orders = new Link(By.LinkText("Заказы"));
            OrderNew = new Link(By.LinkText("Новый"));
            OrderCreateSelf = new Link(By.LinkText("Самовывоз"));
            OrderCreateCourirs = new Link(By.LinkText("Курьерская доставка"));

            Documents = new Link(By.LinkText("Документы"));
            DocumentsCreate = new Link(By.LinkText("Подготовить"));
            DocumentsList = new Link(By.LinkText("Подготовленные"));

            Calculator = new Link(By.LinkText("Калькулятор"));

            Support = new Link(By.LinkText("Поддержка"));
            SupportCreate = new Link(By.LinkText("Создать запрос"));
            SupportList = new Link(By.LinkText("Мои запросы"));

            Loader = new LoaderControl();
        }
        public Link DDeliveryLink { get; set; }

        public Link UseProfile { get;  set; }
        public Link UserWarehouses { get; private set; }
        public Link UserShops { get; private set; }
        public Link UserEditing { get; private set; }
        public Link UserChangePassword { get; private set; }
        public Link UserLogOut { get; private set; }

        public Link Orders { get; set; }
        public Link OrderNew { get; private set; }
        public Link OrderCreateSelf { get; private set; }
        public Link OrderCreateCourirs { get; private set; }

        public Link Documents { get; set; }
        public Link DocumentsCreate { get; private set; }
        public Link DocumentsList { get; private set; }

        public Link Calculator { get; set; }

        public Link Support { get; set; }
        public Link SupportCreate { get; private set; }
        public Link SupportList { get; private set; }

        public LoaderControl Loader { get; private set; }

        public override void BrowseWaitVisible()
        {
            WebDriverCache.WebDriver.WaitForAjax();
            UseProfile.WaitVisible();
            Orders.WaitVisible();
            Documents.WaitVisible();
            Calculator.WaitVisible();
            Support.WaitVisible();
        }

        public DefaultPage LoginOut()
        {
            UseProfile.Click();
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