using System.Threading;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.WebPages.Pages.PageUser.Controls;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class OrderCreateCourirsPage : UserPageBase
    {
        public OrderCreateCourirsPage()
        {
            AletrError = new AlertControl();

            CityToConbobox = new ComboboxControl(BY.NthOfClass("combobox-container", 0));
            CityTo = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));

            DeclaredPrice = new TextInput(By.Name("declared_price"));
            Weight = new TextInput(By.Name("weight"));
            Width = new TextInput(By.Name("dimension_side1"));
            Height = new TextInput(By.Name("dimension_side2"));
            Length = new TextInput(By.Name("dimension_side3"));

            СountedButton = new ButtonInput(By.Name("recalc"));
            TextHelpValidation = new StaticText(By.ClassName("help-block"));

            SaveDraftButton = new ButtonInput(By.CssSelector("input.btn.btn-primary.btn-success"));
            SendOrderButton = new ButtonInput(By.CssSelector("input.btn.btn-primary.pull-right"));

            DeliveryList = new RadioButtonListControl("radio_div");

            BuyerPostalCode = new TextInput(By.Name("to_postal_code"));
            BuyerStreet = new TextInput(By.Name("to_street"));
            BuyerHouse = new TextInput(By.Name("to_house"));
            BuyerFlat = new TextInput(By.Name("to_flat"));
            BuyerName = new TextInput(By.Name("to_name"));
            BuyerPhone = new TextInput(By.Name("to_phone"));
            BuyerEmail = new TextInput(By.Name("to_email"));

            PaymentPrice = new StaticText(By.Name("payment_price"));
            OrderNumber = new TextInput(By.Name("shop_refnum"));
            GoodsDescription = new TextInput(By.Name("goods_description"));

            Countedloader = new StaticControl(By.CssSelector("#radio_div > div > imj"));

            ActionErrorText = new ErrorActionTextControl(By.ClassName("form-horizontal"), null);
            ErrorText = new ErrorTextControl(By.ClassName("form-horizontal"), null);
         }

        public OrderArticleRowControl GetArticleRow(int index)
        {
            var row = new OrderArticleRowControl(index);
            return row;
        }

        public void WaitCounted()
        {
            var second = 0;
            while (Countedloader.IsPresent)
            {
                second = second + 1;
                if (second >= 60) Assert.AreEqual(Countedloader.IsPresent, false, "Время ожидание завершено. Не найден элемент");
            } second = 0;
            while (!DeliveryList[0].IsPresent)
            {
                second = second + 1;
                Thread.Sleep(10);
                if (second >= 1000) Assert.AreEqual(DeliveryList[0].IsPresent, true, "Время ожидание завершено. Не найден элемент");
            }
        }

        public void WaitTextRadioButtonError(string value)
        {
            var second = 0;
            while (!TextHelpValidation.IsPresent)
            {
                second = second + 1;
                if (second >= 1000) Assert.AreEqual(Countedloader.IsPresent, false, "Время ожидание завершено. Не найден элемент");
                Thread.Sleep(10);
            }
            TextHelpValidation.WaitText(value);
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            CityTo.WaitVisible();
        }
        public AlertControl AletrError { get; set; }

        public AutocompleteControl CityTo { get; set; }
        public ComboboxControl CityToConbobox { get; set; }
        public TextInput DeclaredPrice { get; set; }
        public TextInput Weight { get; set; }
        public TextInput Width { get; set; }
        public TextInput Height { get; set; }
        public TextInput Length { get; set; }

        public ButtonInput СountedButton { get; set; }
        public ButtonInput SaveDraftButton { get; set; }
        public ButtonInput SendOrderButton { get; set; }

        public StaticText TextHelpValidation { get; set; }
        public RadioButtonListControl DeliveryList { get; set; }

        public TextInput BuyerPostalCode { get; set; }
        public TextInput BuyerStreet { get; set; }
        public TextInput BuyerHouse { get; set; }
        public TextInput BuyerFlat { get; set; }
        public TextInput BuyerName { get; set; }
        public TextInput BuyerPhone { get; set; }
        public TextInput BuyerEmail { get; set; }
        public StaticText PaymentPrice { get; set; }
        public TextInput OrderNumber { get; set; }
        public TextInput GoodsDescription { get; set; }

        public StaticControl Countedloader { get; set; }
        public ErrorActionTextControl ActionErrorText { get; set; }
        public ErrorTextControl ErrorText { get; set; }
    }
}