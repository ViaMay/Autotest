using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class TimeWorkRowControl
        : BaseRowControl
    {

        public TimeWorkRowControl(int index)
            : base(index)
        {
            FromHour = new TextInput(By.Id(string.Format("from_hour_{0}", index - 1)));
            ToHour = new TextInput(By.Id(string.Format("to_hour_{0}", index - 1)));
        }
        public TextInput ToHour { get; private set; }
        public TextInput FromHour { get; private set; }
    }
}