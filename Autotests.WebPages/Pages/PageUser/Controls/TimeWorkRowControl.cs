using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class TimeWorkRowControl
        : HtmlControl
    {

        public TimeWorkRowControl(int index)
            : base(By.ClassName("control-group"), null)

        {
            FromHour = new TextInput(By.Id(string.Format("from_hour_{0}", index)));
            ToHour = new TextInput(By.Id(string.Format("to_hour_{0}", index)));
        }
        public TextInput ToHour { get; private set; }
        public TextInput FromHour { get; private set; }
    }
}