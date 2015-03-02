using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser.Controls
{
    public class OrderArticleRowControl
        : HtmlControl
    {
        public OrderArticleRowControl(int index)
            : base(string.Format("group_{0}", index + 1))
        {
            Name = new TextInput(By.Name(string.Format("order_content_name_{0}", index + 1)), this);
            Article = new TextInput(By.Name(string.Format("order_content_article_{0}", index + 1)), this);
            Count = new TextInput(By.Name(string.Format("order_content_count_{0}", index + 1)), this);
            Add = new ButtonInput(By.Name(string.Format("item_button_add")), this);
            Remove = new ButtonInput(By.Name(string.Format("item_button_remove")), this);
        }

        public TextInput Name { get; private set; }
        public TextInput Article { get; private set; }
        public TextInput Count { get; private set; }
        public ButtonInput Add { get; private set; }
        public ButtonInput Remove { get; private set; }
    }

    public class OrderArticleStaticRowControl
    : HtmlControl
    {
        public OrderArticleStaticRowControl(int index)
            : base(string.Format("group_{0}", index + 1))
        {
            Name = new StaticText(By.Name(string.Format("order_content_name_{0}", index + 1)), this);
            Article = new StaticText(By.Name(string.Format("order_content_article_{0}", index + 1)), this);
            Count = new StaticText(By.Name(string.Format("order_content_count_{0}", index + 1)), this);
        }

        public StaticText Name { get; private set; }
        public StaticText Article { get; private set; }
        public StaticText Count { get; private set; }
    }
}