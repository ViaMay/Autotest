using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class ShopRowControl
        : BaseRowControl
    {

        public ShopRowControl(int index)
            : base(index)
        {
            KeyPrivate  = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[4]", index)));
            KeyPublic = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[5]", index)));
        }
        public StaticText KeyPrivate { get; private set; }
        public StaticText KeyPublic { get; private set; }
    }
}