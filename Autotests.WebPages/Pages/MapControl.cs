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
//            City = new SelectMap(By.ClassName("delivery-place"));
            City = new SelectMap(By.XPath("//div[@id='ddelivery']/div/div/div/div/input"));
            TakeHere = new LinkMap(By.XPath("//div/div[2]/div[4]/div[5]/a"));
            ImageLocator = new LinkMap(By.CssSelector("ymaps.ymaps-image"));
            HasFittingRoom = new LinkMap(By.XPath("//div[3]/div[3]/div[2]/p[2]/a[2]"));
        }

        public TextInput CityName { get; set; }
        public LinkMap TakeHere { get; set; }
        public SelectMap City { get; set; }
        public LinkMap ImageLocator { get; set; }
        public LinkMap HasFittingRoom { get; set; }

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
            Name = new LinkMap(By.XPath(string.Format("//li[{0}]/a/span[1]/img[2]", index + 1)));
            Price = new LinkMap(By.XPath(string.Format("//li[{0}]/a/span[2]", index + 1)));
            Date = new LinkMap(By.XPath(string.Format("//li[{0}]/a/span[3]", index + 1)));
        }

        public LinkMap Name { get; private set; }
        public LinkMap Price { get; private set; }
        public LinkMap Date { get; private set; }
    }
}