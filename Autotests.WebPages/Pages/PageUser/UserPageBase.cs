﻿using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.Utilities.WebWorms.FuncTests;
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

//            Orders = new Link("");
//
//            OrdersCreate = new Link("");
//            OrdersCreateCourirs = new Link("");
//
//            Documents = new Link("");
//
//            DocumentsCreate = new Link("");
//            DocumentsList = new Link("");

            Calculator = new Link(By.LinkText("Калькулятор"));
//
//            Support = new Link("");
//            SupportCreate = new Link("");
//            SupportList = new Link("");

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
        public Link OrdersCreate { get; private set; }
        public Link OrdersCreateCourirs { get; private set; }

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
            UseProfile.WaitVisible();
//            Orders.WaitVisible();
//            Documents.WaitVisible();
//            Calculator.WaitVisible();
//            Support.WaitVisible();
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