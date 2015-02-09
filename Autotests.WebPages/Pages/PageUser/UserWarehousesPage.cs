using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageUser.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class UserWarehousesPage : UserPageBase
    {
        public UserWarehousesPage()
        {
            WarehousesCreate = new Link(By.LinkText("Создать новый"));
            Table = new WarehousesListControl(By.ClassName("table"));
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            WarehousesCreate.WaitVisible();
        }

        public Link WarehousesCreate { get; set; }
        public WarehousesListControl Table { get; set; }
    }
}