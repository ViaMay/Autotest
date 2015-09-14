using System.Diagnostics;
using System.Threading;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.WebPages.Pages.PageUser.Controls;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class OrderCourirsCreatePage : UserPageBase
    {
        public OrderCourirsCreatePage()
        {
            CityToConbobox = new ComboboxControl(BY.NthOfClass("combobox-container", 0));
            CityTo = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));

            DeclaredPrice = new TextInput(By.Name("declared_price"));
            Weight = new TextInput(By.Name("weight"));
            Width = new TextInput(By.Name("dimension_side1"));
            Height = new TextInput(By.Name("dimension_side2"));
            Length = new TextInput(By.Name("dimension_side3"));

            СountedButton = new ButtonInput(By.Name("recalc"));

            SaveDraftButton = new ButtonInput(By.CssSelector("div.form-actions > input.btn.btn-primary.btn-success"));
            SendOrderButton = new ButtonInput(By.CssSelector("div.form-actions > input.btn.btn-primary.pull-right"));

            DeliveryList = new RadioButtonListControl("radio_div");

            BuyerPostalCode = new TextInput(By.Name("to_postal_code"));
            BuyerStreet = new TextInput(By.Name("to_street"));
            BuyerHouse = new TextInput(By.Name("to_house"));
            BuyerFlat = new TextInput(By.Name("to_flat"));
            BuyerName = new TextInput(By.Name("to_name"));
            BuyerPhone = new TextInput(By.Name("to_phone"));
            BuyerPhoneAdd = new TextInput(By.Name("to_add_phone"));
            BuyerEmail = new TextInput(By.Name("to_email"));
            ItemsCount = new TextInput(By.Name("items_count"));

            PaymentPrice = new TextInput(By.Name("payment_price"));
            OrderNumber = new TextInput(By.Name("shop_refnum"));
            GoodsDescription = new TextInput(By.Name("goods_description"));
            OrderComment = new TextInput(By.Name("order_comment"));
            IsCargoVolume = new CheckBox(By.Name("is_cargo_volume"));

            Countedloader = new StaticControl(By.CssSelector("#radio_div > div > imj"));

            TextRadioButtonError = new StaticText(By.ClassName("help-block"));
            ActionErrorText = new ErrorActionTextControl(By.ClassName("form-horizontal"));
            ErrorText = new ErrorTextControl(By.ClassName("form-horizontal"));
         }

        public OrderArticleRowControl GetArticleRow(int index)
        {
            var row = new OrderArticleRowControl(index);
            return row;
        }

        public void WaitCounted(int timeout = 2000, int waitTimeout = 100)
        {
            var w = Stopwatch.StartNew();
            while (Countedloader.IsPresent)
            {
                Thread.Sleep(waitTimeout);
                if (w.ElapsedMilliseconds > timeout) Assert.AreEqual(Countedloader.IsPresent, false, "Время ожидание завершено. Не найден элемент");
            }
            while (!DeliveryList[0].IsPresent)
            {
                Thread.Sleep(waitTimeout);
                if (w.ElapsedMilliseconds > timeout) Assert.AreEqual(DeliveryList[0].IsPresent, true, "Время ожидание завершено. Не найден элемент");
            }
        }

        public void WaitTextRadioButtonError(string value, int timeout = 2000, int waitTimeout = 100)
        {
            var w = Stopwatch.StartNew();
            while (!TextRadioButtonError.IsPresent)
            {
                Thread.Sleep(waitTimeout);
                if (w.ElapsedMilliseconds > timeout) Assert.AreEqual(Countedloader.IsPresent, false, "Время ожидание завершено. Не найден элемент");
            }
            TextRadioButtonError.WaitText(value);
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            CityTo.WaitVisible();
        }
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

        public RadioButtonListControl DeliveryList { get; set; }

        public TextInput BuyerPostalCode { get; set; }
        public TextInput BuyerStreet { get; set; }
        public TextInput BuyerHouse { get; set; }
        public TextInput BuyerFlat { get; set; }
        public TextInput BuyerName { get; set; }
        public TextInput BuyerPhone { get; set; }
        public TextInput BuyerPhoneAdd { get; set; }
        public TextInput BuyerEmail { get; set; }
        public TextInput PaymentPrice { get; set; }
        public TextInput OrderNumber { get; set; }
        public TextInput GoodsDescription { get; set; }
        public TextInput OrderComment { get; set; }
        public TextInput ItemsCount { get; set; }
        public CheckBox IsCargoVolume { get; set; }

        private StaticControl Countedloader { get; set; }
        public ErrorActionTextControl ActionErrorText { get; set; }
        public ErrorTextControl ErrorText { get; set; }
        public StaticText TextRadioButtonError { get; set; }
    }
}