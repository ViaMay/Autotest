using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class OrderCourirsPage : UserPageBase
    {
        public OrderCourirsPage()
        {
            StatusOrder = new StaticText(By.CssSelector("legend > span"));
            BackOrders = new ButtonInput(By.LinkText("Вернутся к списку"));

            TableSender = new SenderListControl(By.XPath("//div[@class='span5']/table[1]"));
            TableRecipient = new RecipientListControl(By.XPath("//div[@class='span4']/table[1]"));
            TablePrice = new PriceListControl(By.XPath("//div[@class='span6']/table[1]"));
            TableSize = new SizeListControl(By.XPath("//div[@class='span3']/table[1]"));
            TableArticle = new ArticelListControl(By.XPath("//div[@class='span6'][2]/table[1]"));
            TableArticle = new ArticelListControl(By.XPath("//div[5]/div[@class='span6']/table[1]"));

            СonfirmButton = new ButtonInput(By.CssSelector("div.form-actions > form > button"));
            UndoButton = new ButtonInput(By.CssSelector("div.form-actions > form > button"));
            
         }
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            StatusOrder.WaitVisible();
        }
        public StaticText StatusOrder { get; set; }
        public ButtonInput BackOrders { get; set; }
        public ButtonInput UndoButton { get; set; }
        public ButtonInput СonfirmButton { get; set; }

        public SenderListControl TableSender { get; set; }
        public RecipientListControl TableRecipient { get; set; }
        public PriceListControl TablePrice { get; set; }
        public SizeListControl TableSize { get; set; }
        public ArticelListControl TableArticle { get; set; } 
    }

    public class SenderListControl : HtmlControl
    {
        public SenderListControl(By className)
            : base(className)
        {
            locator = className.ToString().Replace("By.XPath: ", "");

            City = new StaticText(By.XPath(string.Format("{0}/tbody/tr[1]/td", locator)));
            Address = new StaticText(By.XPath(string.Format("{0}/tbody/tr[2]/td", locator)));
            Name = new StaticText(By.XPath(string.Format("{0}/tbody/tr[3]/td", locator)));
            Phone = new StaticText(By.XPath(string.Format("{0}/tbody/tr[4]/td", locator)));
            Delivery = new StaticText(By.XPath(string.Format("{0}/tbody/tr[5]/td", locator)));
        }
        public StaticText City { get; private set; }
        public StaticText Address { get; private set; }
        public StaticText Name { get; private set; }
        public StaticText Phone { get; private set; }
        public StaticText Delivery { get; private set; }
        private readonly string locator;
    }

    public class RecipientListControl : HtmlControl
    {
        public RecipientListControl(By className)
            : base(className)
        {
            locator = className.ToString().Replace("By.XPath: ", "");

            City = new StaticText(By.XPath(string.Format("{0}/tbody/tr[1]/td", locator)));
            Address = new StaticText(By.XPath(string.Format("{0}/tbody/tr[2]/td", locator)));
            Name = new StaticText(By.XPath(string.Format("{0}/tbody/tr[3]/td", locator)));
            Email = new StaticText(By.XPath(string.Format("{0}/tbody/tr[4]/td", locator)));
            Phone = new StaticText(By.XPath(string.Format("{0}/tbody/tr[5]/td", locator)));
            Issue = new StaticText(By.XPath(string.Format("{0}/tbody/tr[6]/td", locator)));
            PickupCompany = new StaticText(By.XPath(string.Format("{0}/tbody/tr[7]/td", locator)));
            DeliveryCompany = new StaticText(By.XPath(string.Format("{0}/tbody/tr[8]/td", locator)));
        }
        public StaticText City { get; private set; }
        public StaticText Address { get; private set; }
        public StaticText Name { get; private set; }
        public StaticText Email { get; private set; }
        public StaticText Phone { get; private set; }
        public StaticText Issue { get; private set; }
        public StaticText PickupCompany { get; private set; }
        public StaticText DeliveryCompany { get; private set; }
        private readonly string locator;
    }

    public class PriceListControl : HtmlControl
    {
        public PriceListControl(By className)
            : base(className)
        {
            locator = className.ToString().Replace("By.XPath: ", "");

            PaymentPrice = new StaticText(By.XPath(string.Format("{0}/tbody/tr[1]/td", locator)));
            DeclaredPrice = new StaticText(By.XPath(string.Format("{0}/tbody/tr[2]/td", locator)));
            Insurance = new StaticText(By.XPath(string.Format("{0}/tbody/tr[3]/td", locator)));
            DeliveryPrice = new StaticText(By.XPath(string.Format("{0}/tbody/tr[4]/td", locator)));
            PickupPrice = new StaticText(By.XPath(string.Format("{0}/tbody/tr[5]/td", locator)));
        }
        public StaticText PaymentPrice { get; private set; }
        public StaticText DeclaredPrice { get; private set; }
        public StaticText Insurance { get; private set; }
        public StaticText DeliveryPrice { get; private set; }
        public StaticText PickupPrice { get; private set; }
        private readonly string locator;
    }

    public class SizeListControl : HtmlControl
    {
        public SizeListControl(By className)
            : base(className)
        {
            locator = className.ToString().Replace("By.XPath: ", "");

            Width = new StaticText(By.XPath(string.Format("{0}/tbody/tr[1]/td", locator)));
            Height = new StaticText(By.XPath(string.Format("{0}/tbody/tr[2]/td", locator)));
            Length = new StaticText(By.XPath(string.Format("{0}/tbody/tr[3]/td", locator)));
            Weight = new StaticText(By.XPath(string.Format("{0}/tbody/tr[4]/td", locator)));
        }
        public StaticText Width { get; private set; }
        public StaticText Height { get; private set; }
        public StaticText Length { get; private set; }
        public StaticText Weight { get; private set; }
        private readonly string locator;
    }

    public class ArticelListControl : HtmlControl
    {
        public ArticelListControl(By className)
            : base(className)
        {
            locator = className.ToString().Replace("By.XPath: ", "");
        }
        private readonly string locator;

        public ArticeRowControl GetRow(int index)
        {
            var row = new ArticeRowControl(index + 1, locator);
            return row;
        }
    }
    public class ArticeRowControl
        : HtmlControl
    {
        public ArticeRowControl(int index, string locator)
            : base(By.XPath(string.Format(locator)))
        {
            Name = new StaticText(By.XPath(string.Format("{1}/tbody/tr[{0}]/td[1]", index, locator)));
            Article = new StaticText(By.XPath(string.Format("{1}/tbody/tr[{0}]/td[2]", index, locator)));
            Count = new StaticText(By.XPath(string.Format("{1}/tbody/tr[{0}]/td[3]", index, locator)));
        }

        public StaticText Name { get; private set; }
        public StaticText Article { get; private set; }
        public StaticText Count { get; private set; }
    }
}
