using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class PaymentPricePage : AdminPageBase
    {
        public PaymentPricePage()
        {
            LabelDirectoryCompanies = new StaticText(By.CssSelector("legend"));
            CompanyCreate = new Link(By.LinkText("Создать"));

            Table = new BaseTableListControl(By.ClassName("table"));
        }

        
        public PaymentPricePage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<PaymentPricePage>();
        }

        public StaticText LabelDirectoryCompanies { get; set; }
        public Link CompanyCreate { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectoryCompanies.WaitText(@"Справочник ""Возможность наложенного платежа""");
            CompanyCreate.WaitVisible();
        }
    }
}