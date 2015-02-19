using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageUser.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class OrderSelfCreatePage : UserPageBase
    {
        public OrderSelfCreatePage()
        {
            Weight = new TextInput(By.Name("weight"));
            Width = new TextInput(By.Name("dimension_side1"));
            Height = new TextInput(By.Name("dimension_side2"));
            Length = new TextInput(By.Name("dimension_side3"));
            OrderNumber = new TextInput(By.Name("shop_refnum"));

            СountedButton = new ButtonInput(By.Name("recalc"));
            
            BuyerName = new TextInput(By.Name("to_name"));
            BuyerPhone = new TextInput(By.Name("to_phone"));
            BuyerEmail = new TextInput(By.Name("to_email"));
            DeclaredPrice = new TextInput(By.Name("declared_price"));
            PaymentPrice = new TextInput(By.Name("payment_price"));
            GoodsDescription = new TextInput(By.Name("goods_description"));

//            DeclaredPrice = new StaticText(By.Name("//fieldset/div[@class='point_info']/div/"));
//            PaymentPrice = new StaticText(By.Name("payment_price"));
//            GoodsDescription = new StaticText(By.Name("goods_description"));

            SaveDraftButton = new ButtonInput(By.CssSelector("div.form-actions > input.btn.btn-primary.btn-success"));
            SendOrderButton = new ButtonInput(By.CssSelector("div.form-actions > input.btn.btn-primary.pull-right"));

            ActionErrorText = new ErrorActionTextControl(By.ClassName("form-horizontal"), null);
            ErrorText = new ErrorTextControl(By.ClassName("form-horizontal"), null);
            AletrError = new AlertControl();

            MapOrders = new MapControl(By.Id("ddelivery"));
         }

        public OrderArticleRowControl GetArticleRow(int index)
        {
            var row = new OrderArticleRowControl(index);
            return row;
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Weight.WaitVisible();
        }
        public TextInput Weight { get; set; }
        public TextInput Width { get; set; }
        public TextInput Height { get; set; }
        public TextInput Length { get; set; }
        public TextInput OrderNumber { get; set; }

        public ButtonInput СountedButton { get; set; }
        
        public TextInput BuyerName { get; set; }
        public TextInput BuyerPhone { get; set; }
        public TextInput BuyerEmail { get; set; }
        public TextInput DeclaredPrice { get; set; }
        public TextInput PaymentPrice { get; set; }
        public TextInput GoodsDescription { get; set; }

        public ButtonInput SaveDraftButton { get; set; }
        public ButtonInput SendOrderButton { get; set; }
        
        public ErrorActionTextControl ActionErrorText { get; set; }
        public ErrorTextControl ErrorText { get; set; }
        public AlertControl AletrError { get; set; }

        public MapControl MapOrders { get; set; }
    }
}