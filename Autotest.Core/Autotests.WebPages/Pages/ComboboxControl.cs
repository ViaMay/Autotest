using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class ComboboxControl : HtmlControl
    {
        public ComboboxControl(By locator, HtmlControl container = null)
            : base(locator, container)
        {
// TODO не работает так как сейчас можем искать AutocompleteControl только по индексу!
//            Value = new AutocompleteControl(BY.ClassName("ajax-combobox", this));
            Remove = new ButtonInput(By.ClassName("icon-remove"), this);
            Caret = new ButtonInput(By.ClassName("caret"), this);
        }

//        public AutocompleteControl Value { get; set; }
        public ButtonInput Remove { get; set; }
        public ButtonInput Caret { get; set; }
    }
}