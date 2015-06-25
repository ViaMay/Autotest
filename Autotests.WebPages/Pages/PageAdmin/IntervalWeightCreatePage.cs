using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class IntervalWeightCreatePage : AdminBaseListCreatePage
    {
        public IntervalWeightCreatePage()
        {
            Name = new TextInput(By.Name("name"));
            Min = new TextInput(By.Name("min"));
            Max = new TextInput(By.Name("max"));
        }

        public TextInput Name { get; set; }
        public TextInput Min { get; set; }
        public TextInput Max { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Name.WaitVisible();
        }
    }
}