using FeelApp.Helpers;
using FeelApp.Model;
using FeelApp.ViewModel.Base;
using FeelApp.ViewModel.Commands;
using FeelApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FeelApp.ViewModel
{
    public class NotificationTemplateViewModel : BaseViewModel
    {
        public NotificationTemplateViewModel(Page page)
        {
            this.Page = page;
            GetTemplate();
        }

        public async Task GetTemplate()
        {
            _notificationList = new ObservableCollection<NotificationTemplate>();
            var list = new ObservableCollection<NotificationTemplate>();
            var response = await Api.GetNotificationTemplate();
            if (response.success)
            {
                list = response.data;

            }
            NotificationList = list;
        }

        private ObservableCollection<NotificationTemplate> _notificationList;
        public ObservableCollection<NotificationTemplate> NotificationList
        {
            get { return _notificationList; }
            set { SetProperty(ref _notificationList, value, "NotificationList"); }
        }

        private event EventHandler<ItemTappedEventArgs> _itemTappedEventHandler;
        public EventHandler<ItemTappedEventArgs> ItemTappedEventHandler
        {
            get { return _itemTappedEventHandler = _itemTappedEventHandler ?? new EventHandler<ItemTappedEventArgs>(ItemTappedEvent); }
        }

        private async void ItemTappedEvent(object sender, ItemTappedEventArgs e)
        {
            var getItem = e.Item as NotificationTemplate;
            var itemId = getItem.Id;
            Globals.notificationTemplate = getItem;
            var page = new NotificationPage(false);
            await Application.Current.MainPage.Navigation.PushAsync(page, true);

        }

        private event EventHandler<EventArgs> _deleteHandler;
        public EventHandler<EventArgs> DeleteHandler
        {
            get { return _deleteHandler = _deleteHandler ?? new EventHandler<EventArgs>(DeleteEvent); }
        }

        private async void DeleteEvent(object sender, EventArgs e)
        {

           
            var getItem = sender as MenuItem;
            var item = getItem.BindingContext as NotificationTemplate;
            var id = item.Id;

            var response = await Api.DeleteTemplate(id);
            if(response.success)
            {
                await Page.DisplayAlert("Success", "Successfuly deleted", "Ok");
                await GetTemplate();
            }
            else
            {
                await Page.DisplayAlert("Success", response.error, "Ok");
            }
        }

        private event EventHandler<EventArgs> _editHandler;
        public EventHandler<EventArgs> EditHandler
        {
            get { return _editHandler = _editHandler ?? new EventHandler<EventArgs>(EditEvent); }
        }

        private void EditEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand = _addCommand ?? new DelegateCommand(async (obj) => await AddEvent()); }
        }

        private async Task AddEvent()
        {
            var page = new NotificationPage(true);
            await Application.Current.MainPage.Navigation.PushAsync(page, true);
        }

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get { return _backCommand = _backCommand ?? new DelegateCommand((obj) => BackEvent()); }
        }

        private async Task BackEvent()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }

}
