using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class PaymentPriceFeePage : AdminPageBase
    {
        public PaymentPriceFeePage()
        {
            LabelDirectory = new StaticText(By.CssSelector("legend"));
            Create = new Link(By.LinkText("Создать"));

            Table = new BaseTableListControl(By.ClassName("table"));
        }


        public PaymentPriceFeePage SeachButtonRowClickAndGo()
        {
            Table.RowSearch.SeachButton.Click();
            return GoTo<PaymentPriceFeePage>();
        }

        public StaticText LabelDirectory { get; set; }
        public Link Create { get; set; }
        public BaseTableListControl Table { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitText(@"Справочник ""Комиссия на наложенный платеж""");
            Create.WaitVisible();
        }
    }
}