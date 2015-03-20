using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class Shop : ConstVariablesTestBase
    {
//        "test_userShops_via_Api\r\nAPI ключ для модуля: 1a82a2953f2800fa43a7424c41b797dd"
//"Квебек"
//"Удалить"
//"Редактировать"
//"Доставка курьером до двери"
//"Доставка до пункта самовывоза"
        [Test, Description("Создания магазина"), Ignore]
        public void T02_CreateShopTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();

            while (shopsListPage.Table.GetRow(0).Name.IsPresent)
            {
                shopsListPage.Table.GetRow(0).ActionsDelete.Click();
                shopsListPage.AletrDelete.WaitText("Вы действительно хотите удалить магазин?");
                shopsListPage.AletrDelete.Accept();
                shopsListPage = shopsListPage.GoTo<UserShopsPage>();
            }

            shopsListPage.ShopsCreate.Click();
            var shopCreatePage = shopsListPage.GoTo<UserShopCreatePage>();
            shopCreatePage.Name.SetValueAndWait(userShops);
            shopCreatePage.Address.SetValueAndWait("Москва");
            shopCreatePage.Warehouse.SelectValue(userWarehouses);
            shopCreatePage.CreateButton.Click();
            shopsListPage = shopCreatePage.GoTo<UserShopsPage>();
            shopsListPage.Table.GetRow(0).Name.WaitPresence();
        }
    }
}