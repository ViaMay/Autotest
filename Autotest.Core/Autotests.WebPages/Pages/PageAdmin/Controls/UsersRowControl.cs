using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class UsersRowControl 
        : BaseRowControl
    {

        public UsersRowControl(int index)
            : base(index)
        {
            UserEmail = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[2]", index)));
            OfficialName = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[3]", index)));
            Logins = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[4]", index)));
            UserLastLogin = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[5]", index)));
            UserActive = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[6]", index)));
            ActionsAuth = new Link(By.XPath(string.Format("//tbody/tr[{0}]/td[7]/a[3]", index)));
        }
        public StaticText UserEmail { get; private set; }
        public StaticText OfficialName { get; private set; }
        public StaticText Logins { get; private set; }
        public StaticText UserLastLogin { get; private set; }
        public StaticText UserActive { get; private set; }
        public Link ActionsAuth { get; private set; }
    }
}