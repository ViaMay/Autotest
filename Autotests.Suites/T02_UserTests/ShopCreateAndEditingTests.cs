using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.T02_UserTests
{
    public class ShopCreateAndEditingTests : ConstVariablesTestBase
    {
        [Test, Description("Создания и редактирование магазина")]
        public void ShopCreateAndEditingTest()
        {
            UserHomePage userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            
            for (var i = 0; i < shopsListPage.Table.Count; i++)
            {
                if (!shopsListPage.Table.GetRow(i).Name.GetText().Contains(userShopName + "_2") &&
                    !shopsListPage.Table.GetRow(i).Name.GetText().Contains(userShopName + "_3")) continue;
                shopsListPage.Table.GetRow(0).ActionsDelete.Click();
                shopsListPage.AletrDelete.WaitText("Вы действительно хотите удалить магазин?");
                shopsListPage.AletrDelete.Accept();
                shopsListPage = shopsListPage.GoTo<UserShopsPage>();
            }

            shopsListPage.ShopsCreate.Click();
            var shopCreatePage = shopsListPage.GoTo<UserShopCreatePage>();
            shopCreatePage.Name.SetValueAndWait(userShopName + "_2");
            shopCreatePage.Address.SetValueAndWait("Квебек");
            shopCreatePage.Warehouse.SelectValue(userWarehouseName);
            shopCreatePage.CreateButton.Click();
            shopsListPage = shopCreatePage.GoTo<UserShopsPage>();
            var row = shopsListPage.Table.FindRowByName(userShopName + "_2");
            row.Address.WaitText("Квебек");
            row.OrdersCreateSelf.WaitText("Доставка до пункта самовывоза");
            row.OrdersCreateCourier.WaitText("Доставка курьером до двери");
            row.ActionsEdit.WaitText("Редактировать");
            row.ActionsDelete.WaitText("Удалить");

            row.ActionsEdit.Click();
            shopCreatePage = shopsListPage.GoTo<UserShopCreatePage>();
            shopCreatePage.Name.SetValueAndWait(userShopName + "_3");
            shopCreatePage.Address.SetValueAndWait("Квебек3");
            shopCreatePage.Warehouse.SelectValue(userWarehouseName); 
            shopCreatePage.CreateButton.Click();
            shopsListPage = shopCreatePage.GoTo<UserShopsPage>();
            row = shopsListPage.Table.FindRowByName(userShopName + "_3");
            row.Address.WaitText("Квебек3");
        }
    }
}