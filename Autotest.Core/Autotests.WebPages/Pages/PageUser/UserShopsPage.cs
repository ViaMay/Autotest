using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageUser.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class UserShopsPage : UserPageBase
    {
        public UserShopsPage()
        {
            ShopsCreate = new Link(By.LinkText("Создать новый магазин"));
            Table = new ShopsListControl(By.ClassName("table"));

            AletrDelete = new AlertControl();
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            ShopsCreate.WaitVisible();
        }

        public Link ShopsCreate { get; set; }
        public ShopsListControl Table { get; set; }
        public AlertControl AletrDelete { get; set; }
    }
}