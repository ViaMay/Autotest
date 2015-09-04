using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class UserShopCreatePage : UserPageBase
    {
        public UserShopCreatePage()
        {
            Name = new TextInput(By.Name("name"));
            Address = new TextInput(By.Name("address"));
            Warehouse = new Select(By.Name("warehouse"));

            CreateButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Name.WaitVisible();
            Address.WaitVisible();
            Warehouse.WaitVisible();
        }

        public TextInput Name { get; set; }
        public TextInput Address { get; set; }
        public Select Warehouse { get; set; }
        public ButtonInput CreateButton { get; set; }
    }
}
