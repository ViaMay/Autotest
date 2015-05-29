using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageUser.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class OrderCourirsEditingPage : UserPageBase
    {
        public OrderCourirsEditingPage()
        {
            City = new StaticText(By.Name("to_city__value__"));
            DeclaredPrice = new TextInput(By.Name("declared_price"));
            Weight = new TextInput(By.Name("weight"));
            Width = new TextInput(By.Name("dimension_side1"));
            Height = new TextInput(By.Name("dimension_side2"));
            Length = new TextInput(By.Name("dimension_side3"));

            CanceledButton = new ButtonInput(By.CssSelector("div.form-actions > input.btn.btn-primary.btn-success"));
            SaveChangeButton = new ButtonInput(By.CssSelector("div.form-actions > input.btn.btn-primary.pull-right"));

            BuyerPostalCode = new TextInput(By.Name("to_postal_code"));
            BuyerStreet = new TextInput(By.Name("to_street"));
            BuyerHouse = new TextInput(By.Name("to_house"));
            BuyerFlat = new TextInput(By.Name("to_flat"));
            BuyerName = new TextInput(By.Name("to_name"));
            BuyerPhone = new TextInput(By.Name("to_phone"));
            BuyerEmail = new TextInput(By.Name("to_email"));

            PaymentPrice = new TextInput(By.Name("payment_price"));
            OrderNumber = new TextInput(By.Name("shop_refnum"));
            GoodsDescription = new TextInput(By.Name("goods_description"));
            OrderComment = new TextInput(By.Name("order_comment"));
            ItemsCount = new StaticText(By.Name("items_count"));

            ActionErrorText = new ErrorActionTextControl(By.ClassName("form-horizontal"));
            ErrorText = new ErrorTextControl(By.ClassName("form-horizontal"));
            AletrError = new AlertControl();
         }

        public OrderArticleStaticRowControl GetArticleRow(int index)
        {
            var row = new OrderArticleStaticRowControl(index);
            return row;
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            City.WaitVisible();
        }
        public StaticText City { get; set; }
        public TextInput DeclaredPrice { get; set; }
        public TextInput Weight { get; set; }
        public TextInput Width { get; set; }
        public TextInput Height { get; set; }
        public TextInput Length { get; set; }

        public ButtonInput CanceledButton { get; set; }
        public ButtonInput SaveChangeButton { get; set; }

        public TextInput BuyerPostalCode { get; set; }
        public TextInput BuyerStreet { get; set; }
        public TextInput BuyerHouse { get; set; }
        public TextInput BuyerFlat { get; set; }
        public TextInput BuyerName { get; set; }
        public TextInput BuyerPhone { get; set; }
        public TextInput BuyerEmail { get; set; }
        public TextInput PaymentPrice { get; set; }
        public TextInput OrderNumber { get; set; }
        public TextInput GoodsDescription { get; set; }
        public TextInput OrderComment { get; set; }
        public StaticText ItemsCount { get; set; }

        public ErrorActionTextControl ActionErrorText { get; set; }
        public ErrorTextControl ErrorText { get; set; }
        public AlertControl AletrError { get; set; }
    }
}