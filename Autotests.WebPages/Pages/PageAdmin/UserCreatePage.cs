using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class UserCreatePage : AdminBaseListCreatePage
    {
        public UserCreatePage()
        {
            Name = new TextInput(By.Name("name"));
            Phone = new TextInput(By.Name("phone"));
            UserEmail = new TextInput(By.Name("username"));
            UserPassword = new TextInput(By.Name("password"));

            UserGroups = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            UserGroupsAddButton = new ButtonInput(By.XPath("//button[@type='button']"));

            Key = new TextInput(By.Name("public_key"));
            ResponsibleName = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 1));
            IsMaster = new CheckBox(By.Name("is_master"));

            OfficialName = new TextInput(By.Name("official_name"));
            Director = new TextInput(By.Name("director"));
            OnBasis = new TextInput(By.Name("on_basis"));
            Contract = new TextInput(By.Name("contract"));
            ContractDate = new TextInput(By.Name("contract_date"));
            OfficialAddress = new TextInput(By.Name("official_address"));
            Address = new TextInput(By.Name("address"));
            Inn = new TextInput(By.Name("inn"));
            Ogrn = new TextInput(By.Name("ogrn"));
            BankName = new TextInput(By.Name("bank_name"));
            BankBik = new TextInput(By.Name("bank_bik"));
            BankKs = new TextInput(By.Name("bank_ks"));
            BankRs = new TextInput(By.Name("bank_rs"));
        }

        public TextInput Name { get; set; }
        public TextInput Phone { get; set; }
        public TextInput UserEmail { get; set; }
        public TextInput UserPassword { get; set; }

        public AutocompleteControl UserGroups { get; set; }
        public ButtonInput UserGroupsAddButton { get; set; }

        public TextInput Key { get; set; }
        public AutocompleteControl ResponsibleName { get; set; }
        public CheckBox IsMaster { get; set; }

        public TextInput OfficialName { get; set; }
        public TextInput Director { get; set; }
        public TextInput OnBasis { get; set; }
        public TextInput Contract { get; set; }
        public TextInput ContractDate { get; set; }
        public TextInput OfficialAddress { get; set; }
        public TextInput Address { get; set; }
        public TextInput Inn { get; set; }
        public TextInput Ogrn { get; set; }
        public TextInput BankName { get; set; }
        public TextInput BankBik { get; set; }
        public TextInput BankKs { get; set; }
        public TextInput BankRs { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            UserEmail.WaitVisible();
        }
    }
}