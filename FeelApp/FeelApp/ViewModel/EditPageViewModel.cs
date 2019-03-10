using FeelApp.Helpers;
using FeelApp.ViewModel.Base;
using FeelApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FeelApp.ViewModel
{
    public class EditPageViewModel :BaseViewModel
    {
        public EditPageViewModel(Page page)
        {
            this.Page = page;
            _name = "";
            _contact = "";
            _emergency = "";
            _email = "";
            _password = "";
            _confirmPasword = "";
            GetProfile();

        }

        private async Task GetProfile()
        {
            var response = await Api.GetProfile();
            var getData = response.data;

            Name = getData[0].Name;
            Contact = getData[0].Contact;
            Emergency = getData[0].Emergency;
            Email = Settings.SaveEmail;
            Password = Settings.SavePassword;
            ConfirmPassword = Settings.SavePassword;
        }

        public async Task CreateAccountEvent()
        {

          

        }

        private ICommand _editAccountCommand;
        public ICommand EditAccountCommand
        {
            get { return _editAccountCommand = _editAccountCommand ?? new DelegateCommand(async (obj) => await EditAccountEvent()); }
        }

        private async Task EditAccountEvent()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Contact) && !string.IsNullOrEmpty(Emergency))
            {
                if (Regex.Match(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
                {
                    if (Password == ConfirmPassword)
                    {
                        var response = await Api.EditProfile(Name, Contact, Emergency, Email, Password);
                        if (response.success)
                        {
                            Settings.SaveEmail = Email;
                            Settings.SavePassword = Password;
                            
                            await Page.DisplayAlert("Success", "Profile Successfuly Changed", "Ok");
                            await Page.Navigation.PopAsync();
                        }
                        else
                        {
                            await Page.DisplayAlert("Error", response.message, "Ok");
                        }
                    }
                }
            }
                     
        }

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get { return _backCommand = _backCommand ?? new DelegateCommand(async (obj) => await BackEvent()); }
        }

        private async Task BackEvent()
        {
            await Page.Navigation.PopAsync();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value, "Name"); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value, "Email"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value, "Password"); }
        }
        private string _confirmPasword;
        public string ConfirmPassword
        {
            get { return _confirmPasword; }
            set { SetProperty(ref _confirmPasword, value, "ConfirmPassword"); }
        }
        private string _contact;
        public string Contact
        {
            get { return _contact; }
            set { SetProperty(ref _contact, value, "Contact"); }
        }
        private string _emergency;
        public string Emergency
        {
            get { return _emergency; }
            set { SetProperty(ref _emergency, value, "Emergency"); }
        }

    }
}
