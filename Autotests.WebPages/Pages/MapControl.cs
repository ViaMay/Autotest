using Autotests.Utilities.WebTestCore.SystemControls;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class MapControl : HtmlControl
    {
        public MapControl(By locator, HtmlControl container = null)
            : base(locator, container)
        {
            CityName = new TextInput(By.Name("ddelivery_city"));
            TakeHere = new LinkWaiter(By.XPath("//div/div[2]/div[4]/div[5]/a"));
            ImageLocator = new LinkWaiter(By.CssSelector("ymaps.ymaps-image"));
        }

        public TextInput CityName { get; set; }
        public LinkWaiter TakeHere { get; set; }
        public LinkWaiter ImageLocator { get; set; }

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
            Name = new LinkWaiter(By.XPath(string.Format("//li[{0}]/a/span[1]", index + 1)));
            Price = new LinkWaiter(By.XPath(string.Format("//li[{0}]/a/span[2]", index + 1)));
            Date = new LinkWaiter(By.XPath(string.Format("//li[{0}]/a/span[3]", index + 1)));
        }

        public LinkWaiter Name { get; private set; }
        public LinkWaiter Price { get; private set; }
        public LinkWaiter Date { get; private set; }
    }
}