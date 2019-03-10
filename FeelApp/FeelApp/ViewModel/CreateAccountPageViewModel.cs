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
    public class CreateAccountPageViewModel :BaseViewModel
    {
        public CreateAccountPageViewModel(Page page)
        {
            this.Page = page;
            _name = "";
            _email = "";
            _password = "";
            _confirmPasword = "";
            _contact = "";
            _emergency = "";
         
        }

        public async Task CreateAccountEvent()
        {
            
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Contact) && !string.IsNullOrEmpty(Emergency))
            {
                if (Regex.Match(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
                {
                    if (Password == ConfirmPassword)
                    {
                        if(Settings.SaveUserType == 1)
                        {
                            var response = await Api.CreateAdmin(Name, Email, Password, Contact, Emergency);
                            if (response.success)
                            {
                                await this.Page.DisplayAlert("Success", "Successfully registered account", "Ok");
                                await this.Page.Navigation.PopAsync();
                            }
                            else
                            {
                                await this.Page.DisplayAlert("Error", response.message, "Ok");
                            }
                        }
                        else
                        {
                            var response = await Api.CreateAccount(Name, Email, Password, Contact, Emergency);
                            if (response.success)
                            {
                                await this.Page.DisplayAlert("Success", "Successfully registered account", "Ok");
                                await this.Page.Navigation.PopAsync();
                            }
                            else
                            {
                                await this.Page.DisplayAlert("Error", response.message, "Ok");
                            }
                        }
                       
                    }
                    else
                    {
                        Password = "";
                        ConfirmPassword = "";
                        await this.Page.DisplayAlert("Error", "Password does not match", "Ok");
                    }
                }
                else
                {
                    await this.Page.DisplayAlert("Error", "Email is not valid", "Ok");
                }
            }
            else
            {
               await this.Page.DisplayAlert("Error", "Please Fillup all fields", "Ok");
            }
        }

        private ICommand _createAccountCommand;
        public ICommand CreateAccountCommand
        {
            get { return _createAccountCommand = _createAccountCommand ?? new DelegateCommand(async (obj) => await CreateAccountEvent()); }
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

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get { return _backCommand = _backCommand ?? new DelegateCommand(async (obj) => await BackEvent()); }
        }

        private async Task BackEvent()
        {
            await Page.Navigation.PopAsync();
        }

    }
}
