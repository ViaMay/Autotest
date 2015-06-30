using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebWorms.FuncTests;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageFreshDesk
{
    public class SupportFreshDeskPage : CommonPageBase
    {
        public SupportFreshDeskPage()
        {
            LabelDirectory = new StaticText(By.CssSelector("body > div > header > div > div > h1"));
            HomeLink = new Link(By.LinkText("Главная"));
            SolutionsLink = new Link(By.LinkText("Решения"));
            TicketsLink = new Link(By.LinkText("Заявки"));
        }

        public StaticText LabelDirectory { get; set; }
        public Link HomeLink { get; private set; }
        public Link SolutionsLink { get; private set; }
        public Link TicketsLink { get; private set; }
        
        public override void BrowseWaitVisible()
        {
            LabelDirectory.WaitVisible();
        }
    }
}