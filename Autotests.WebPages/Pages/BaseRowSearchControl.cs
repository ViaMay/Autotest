using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class BaseRowSearchControl : HtmlControl
    {
        public BaseRowSearchControl(By className)
            : base(className)
        {
            ID = new TextInput(By.Name("_id"));
 //TODO     Важно! на страницы поиcка компании использовать Name а не CompanyName для поиска по имени компании
            Name = new TextInput(By.Name("name"));
            CompanyName = new TextInput(By.Name("company"));
            ShopName = new TextInput(By.Name("shop"));

            SeachButton = new ButtonInput(By.XPath(@"//*[@id=""filter_column__id""]/div/div/i"));
        }
         
        public ButtonInput SeachButton { get; private set; }
        public TextInput ID { get; private set; }
        public TextInput Name { get; private set; }
        public TextInput CompanyName { get; private set; }
        public TextInput ShopName { get; private set; }
    }
}