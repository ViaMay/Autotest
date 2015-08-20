using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class ShopsListControl : BaseTableListControl
    {
        public ShopsListControl(By className)
            : base(className)
        {
        }

        public ShopRowControl GetRow(int index)
        {
            var row = new ShopRowControl(index + 1);
            row.WaitPresenceWithRetries();
            return row;
        }
    }
}