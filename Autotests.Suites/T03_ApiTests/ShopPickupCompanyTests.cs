using System.Collections.Specialized;
using System.Globalization;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class ShopPickupCompanyTests : ConstVariablesTestBase
    {
        [Test, Description("Получить компанию забора ИМ")]
        public void ShopPickupCompanуTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();
            var companiesPage =
                LoadPage<AdminBaseListPage>("/admin/companies/?&filters[name]=" + companyPickupName);
            var companyPickupId = companiesPage.Table.GetRow(0).ID.GetText();

//           Порверка если компания забора указана
            var responsePickupCompanу = (ApiResponse.ResponsePickupCompany)apiRequest.GET("api/v1/" + keyShopPublic + "/shop_pickup_company.json",
                new NameValueCollection { });
            Assert.IsTrue(responsePickupCompanу.Success);
            Assert.AreEqual(responsePickupCompanу.Response.Id, companyPickupId);
            Assert.AreEqual(responsePickupCompanу.Response.Name, companyPickupName);

//            проверка если нету компании забора
            var usersWarehousesPage =
                LoadPage<AdminBaseListPage>("/admin/warehouses/?&filters[name]=" + userWarehouseName);
            var warehousesId = companiesPage.Table.GetRow(0).ID.GetText();

            shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_ApiAdmin");
            while (shopsPage.Table.GetRow(0).Name.IsPresent)
            {
                shopsPage.Table.GetRow(0).ActionsDelete.Click();
                shopsPage.Aletr.Accept();
                shopsPage = shopsPage.GoTo<UsersShopsPage>();
                shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_ApiAdmin");
            }

//            Создание магазина
            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST(keyShopPublic + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiAdmin"},
                    {"warehouse", warehousesId},
                    {"address", "Москва"}
                }
                );
            Assert.IsTrue(responseShop.Success);
            
//            удаление компании забора
            shopsPage = LoadPage<UsersShopsPage>("/admin/shops/?&filters[name]=" + userShopName + "_ApiAdmin");
            shopsPage.Table.GetRow(0).ActionsEdit.Click();

            var shopEditPage = shopsPage.GoTo<UserAdminShopCreatePage>();
            shopEditPage.CompanyPickup.SetValueAndWait("  ");
            shopEditPage.CreateButton.Click();
            shopsPage = shopEditPage.GoTo<UsersShopsPage>();

//            отправляем запрос так что в магазин с пустой компанией забора
            var responsePickupCompanуError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/shop_pickup_company.json",
                new NameValueCollection { });
            Assert.IsFalse(responsePickupCompanуError.Success);
            Assert.AreEqual(responsePickupCompanуError.Response.ErrorText, "У интернет-магазина не указана компания забора");
        }
    }
}