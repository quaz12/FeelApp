using FeelApp.Helpers;
using FeelApp.Model;
using FeelApp.ViewModel.Base;
using FeelApp.ViewModel.Commands;
using FeelApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FeelApp.ViewModel
{
    public class AdministratorPageViewModel :BaseViewModel
    {
        public ObservableCollection<AllProfiles> _AdminList;
        public AdministratorPageViewModel(Page page)
        {
            this.Page = page;
            _AdminList = new ObservableCollection<AllProfiles>();
            _title = "";
            _isVisible = false;
            if (Globals.ListType == 1)
            {
                Title = "ADMINSTRATORS";
            }

            if (Globals.ListType == 2)
            {
                Title = "USERS";
              
            }

            if (Globals.UserType == 1)
            {
               
                IsVisible = true;
            }

            if (Globals.UserType == 2)
            {
              
                IsVisible = false;
            }
            GetAdminList();
        }

        public async Task GetAdminList()
        {
            var response = await Api.GetProfiles();
            var list = response.data;
            ObservableCollection<AllProfiles> adminList = new ObservableCollection<AllProfiles>();
            foreach (var item in list)
            {
                
                if(item.UserType == 1 && Globals.ListType == 1)
                {
                    adminList.Add(item);
                   
                }
                if(item.UserType == 2 && Globals.ListType == 2)
                {
                   
                    adminList.Add(item);
                }
                AdminList = adminList;
                _AdminList = adminList;
            }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, "Title"); }
        }
        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value, "IsVisible"); }
        }
        private ObservableCollection<AllProfiles> _adminList;
        public ObservableCollection<AllProfiles> AdminList
        {
            get { return _adminList; }
            set { SetProperty(ref _adminList, value, "AdminList"); }
        }

        private ICommand _callCommand;
        public ICommand CallCommand
        {
            get { return _callCommand = _callCommand ?? new DelegateCommand(async (obj) => await CallEvent(obj)); }
        }

        private async Task CallEvent(object obj)
        {
            try
            {
                var item = (obj as AllProfiles);
                var number = item.Contact;
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get { return _backCommand = _backCommand ?? new DelegateCommand(async (obj) => await BackEvent(obj)); }
        }

        private async Task BackEvent(object obj)
        {
            await Page.Navigation.PopAsync();
        }

        private event EventHandler<TextChangedEventArgs> _textChangedEventHandler;
        public EventHandler<TextChangedEventArgs> TextChangedEventHandler
        {
            get { return _textChangedEventHandler = _textChangedEventHandler ?? new EventHandler<TextChangedEventArgs>(TextChangeEvent); }
        }

        private void TextChangeEvent(object sender, TextChangedEventArgs e)
        {

            var search = e.NewTextValue.ToLower();
            if(!string.IsNullOrWhiteSpace(search))
            {
                var adminList = AdminList;
                var filter = adminList.Where(x => x.Name.ToLower().Contains(search));
                var lstFilter = new ObservableCollection<AllProfiles>();
                foreach (var item in filter)
                {
                    lstFilter.Add(item);
                }
                AdminList = lstFilter;

            }
            else
            {
                AdminList = _AdminList;
            }

        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand = _addCommand ?? new DelegateCommand(async (obj) => await AddEvent()); }
        }

        private async Task AddEvent()
        {
            var page = new CreateAccountPage();
            await Application.Current.MainPage.Navigation.PushAsync(page, true);
        }

    }
}
