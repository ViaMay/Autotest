using System.Threading;
using Autotests.Utilities.WebTestCore.SystemControls;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageFreshDesk
{
    public class SupportTicketsPage : SupportFreshDeskPage
    {
        public SupportTicketsPage()
        {

            Table = new SupportTicketsListControl("ticket-list");
            TicketFilters = new Link(By.XPath("//*[@id='ticket-filters']/a"));
            TicketFiltersAll = new Link(By.LinkText("Все заявки"));
            TableLoading = new StaticText(By.CssSelector("div.loading.loading-box"));
            
        }

        public void WaitTableVisible()
        {
            Thread.Sleep(2000);
//            var second = 0;
//            while (!TableLoading.IsPresent)
//            {
//                second = second + 1;
//                if (second >= 60) Assert.AreEqual(!TableLoading.IsPresent, true, "Время ожидание завершено. Не найден элемент");
//                Thread.Sleep(1000);
//            }
        }


        public SupportTicketsListControl Table { get; set; }
        public Link TicketFilters { get; set; }
        public Link TicketFiltersAll { get; set; }
        private StaticText TableLoading { get; set; }

        public override void BrowseWaitVisible()
        {
            LabelDirectory.WaitVisible();
        }
    }
}