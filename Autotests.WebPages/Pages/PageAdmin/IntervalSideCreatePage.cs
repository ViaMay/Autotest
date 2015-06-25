using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class IntervalSideCreatePage : AdminBaseListCreatePage
    {
        public IntervalSideCreatePage()
        {
            Name = new TextInput(By.Name("name"));
            Side1Min = new TextInput(By.Name("side1_min"));
            Side2Min = new TextInput(By.Name("side2_min"));
            Side3Min = new TextInput(By.Name("side3_min"));
            SidesSumMin = new TextInput(By.Name("sides_sum_min"));
            VolumeMin = new TextInput(By.Name("volume_min"));
            Side1Max = new TextInput(By.Name("side1_max"));
            Side2Max = new TextInput(By.Name("side2_max"));
            Side3Max = new TextInput(By.Name("side3_max"));
            SidesSumMax = new TextInput(By.Name("sides_sum_max"));
            VolumeMax = new TextInput(By.Name("volume_max"));
        }
        public TextInput Name { get; set; }
        public TextInput Side1Min { get; set; }
        public TextInput Side2Min { get; set; }
        public TextInput Side3Min { get; set; }
        public TextInput SidesSumMin { get; set; }
        public TextInput VolumeMin { get; set; }
        public TextInput Side1Max { get; set; }
        public TextInput Side2Max { get; set; }
        public TextInput Side3Max { get; set; }
        public TextInput SidesSumMax { get; set; }
        public TextInput VolumeMax { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Name.WaitVisible();
        }
    }
}