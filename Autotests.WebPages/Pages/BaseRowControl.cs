using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class BaseRowControl 
        : HtmlControl
    {

        public BaseRowControl(int index)
            : base(By.XPath(string.Format("//tbody")))
        {
            ID = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[1]", index)));

//TODO     Важно! Name не во всех таблицах есть! на самом деле это по сути второй столбец
            Name = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[2]", index)));

            ColumnThree = new StaticText(By.XPath(string.Format("//tbody/tr[{0}]/td[3]", index)));

            ActionsEdit = new Link(By.XPath(string.Format("//tbody/tr[{0}]/*/a[1]", index)));
            ActionsDelete = new Link(By.XPath(string.Format("//tbody/tr[{0}]/*/a[2]", index)));
        }
        public StaticText ID { get; private set; }
        public StaticText Name { get; private set; }
        public StaticText ColumnThree { get; private set; }

        public Link ActionsEdit { get; private set; }
        public Link ActionsDelete { get; private set; }
    }
}