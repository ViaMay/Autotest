using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class DocumentsPreparedPage : UserPageBase
    {
        public DocumentsPreparedPage()
        {
            WarehouseSelect = new Select(By.ClassName("check_warehouse"));

            InstructionsForUse = new Link(By.LinkText("Инструкция по использованию"));
            Stickers = new Link(By.LinkText("Наклейки"));
            Acts = new Link(By.LinkText("Акты для компании забора"));
            ActsTC= new Link(By.LinkText("Акты для самостоятельной передачи заказов на склады ТК"));
            TextAlert = new StaticText(By.ClassName("alert"));
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            WarehouseSelect.WaitVisible();
        }

        public Select WarehouseSelect { get; set; }
        public Link InstructionsForUse { get; set; }
        public Link Stickers { get; set; }
        public Link Acts { get; set; }
        public Link ActsTC { get; set; }
        public StaticText TextAlert { get; set; }
    }
}
