using FeelApp.Helpers;
using FeelApp.Model;
using FeelApp.ViewModel.Base;
using FeelApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FeelApp.ViewModel
{
    public class NotificationListViewModel :BaseViewModel
    {
        public NotificationListViewModel(Page page)
        {
            this.Page = page;
            GetNotifications();
        }



        private async Task GetNotifications()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                Task.Run(async () =>
                {
                    var notification = new ObservableCollection<Notifications>();
                    var response = await Api.GetNotifications();
                    if (response.success)
                    {

                        Notification = response.data;
                        Notification.OrderByDescending(i => i.Date);
                    }
                    else
                    {
                        await Page.DisplayAlert("Error", response.error, "Ok");
                    }
                });
                return true;
            });
           
        }

        private ObservableCollection<Notifications> _notification;
        public ObservableCollection<Notifications> Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, value, "Notification"); }
        }
        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get { return _backCommand = _backCommand ?? new DelegateCommand((obj) => BackEvent()); }
        }

        private async Task BackEvent()
        {
            await Page.Navigation.PopAsync();
        }

    }
}
