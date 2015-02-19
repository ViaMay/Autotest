using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class MapControl : HtmlControl
    {
        public MapControl(By locator, HtmlControl container = null)
            : base(locator, container)
        {
            CityName = new TextInput(By.Name("ddelivery_city"));
            TakeHere = new Link(By.XPath("//div/div[2]/div[4]/div[5]/a"));
            ImageLocator = new Link(By.CssSelector("ymaps.ymaps-image"));
        }

        public TextInput CityName { get; set; }
        public Link TakeHere { get; set; }
        public Link ImageLocator { get; set; }

        public MapCompanyRowControl GetMapCompanyRow(int index)
        {
            var row = new MapCompanyRowControl(index);
            return row;
        }
    }

    public class MapCompanyRowControl
        : HtmlControl
    {
        public MapCompanyRowControl(int index)
            : base(By.ClassName("map-popup__main__right__btn"))

        {
            Name = new StaticText(By.XPath(string.Format("//li[{0}]/a/span[1]", index + 1)));
            Price = new StaticText(By.XPath(string.Format("//li[{0}]/a/span[2]", index + 1)));
            Date = new StaticText(By.XPath(string.Format("//li[{0}]/a/span[3]", index + 1)));
        }

        public StaticText Name { get; private set; }
        public StaticText Price { get; private set; }
        public StaticText Date { get; private set; }
    }
}