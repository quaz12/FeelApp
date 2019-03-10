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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FeelApp.ViewModel
{
    public class NotificationPageViewModel :BaseViewModel
    {
        ObservableCollection<string> recipients;
       

        public NotificationPageViewModel(Page page, bool isAdd)
        {

            this.Page = page;
            _message = "";
          
            _isAdd = isAdd;
            _isSend = !isAdd;

            if(Globals.notificationTemplate.Notification != null)
            {
                Message = Globals.notificationTemplate.Notification;
            }
           


            getPhoneNumbers();
        }

        private async Task getPhoneNumbers()
        {
            var response = await Api.GetProfiles();
            recipients = new ObservableCollection<string>();
            foreach (var item in response.data)
            {
                recipients.Add(item.Contact);
            }
        }

        private ICommand _sendCommand;
        public ICommand SendCommand
        {
            get { return _sendCommand = _sendCommand ?? new DelegateCommand((obj) =>  SendEvent()); }
        }

        private async Task SendEvent()
        {
            
            try
            {
                var date = DateTime.Now.ToString("MM-dd-yyyy");

                var content = new Notification_Content();
                content.notification_content = new NotificationContent();
                content.notification_content.name = "FeelApp";
                content.notification_content.title = "Alert!";
                content.notification_content.body = Message;

                var apiResponse = await Api.PushNotification(content);
                var response = await Api.CreateNotification(Message, date);
                if (response.success)
                {
                    var message = new SmsMessage(Message, recipients);
                    await Sms.ComposeAsync(message);
                }
                else
                {
                    await Page.DisplayAlert("Error", response.message, "Ok");
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                await Page.DisplayAlert("Error", "Sms is not supported on this device.", "Ok");
                // Sms is not supported on this device.
            }
            catch (Exception ex)
            {
                await Page.DisplayAlert("Error", ex.ToString(), "Ok");
                // Other error has occurred.
            }
        }

        private ICommand _showHideMenuCommand;
        public ICommand ShowHideMenuCommand
        {
            get { return _showHideMenuCommand = _showHideMenuCommand ?? new DelegateCommand((obj) => ShowHideMenu()); }
        }

        private async Task ShowHideMenu()
        {
            var mainPage = this.Page as MainPage;

            if (mainPage.IsPresented)
            {
                var isPresented = false;
                Presented = isPresented;
                mainPage.IsPresented = Presented;
            }
            else
            {
                var isPresented = true;
                Presented = isPresented;
                mainPage.IsPresented = Presented;
            }

        }
        private bool _isAdd;
        public bool IsAdd
        {
            get { return _isAdd; }
            set { SetProperty(ref _isAdd, value, "IsAdd"); }
        }

        private bool _isSend;
        public bool IsSend
        {
            get { return _isSend; }
            set { SetProperty(ref _isSend, value, "IsSend"); }
        }

        private bool _presented;
        public bool Presented
        {
            get { return _presented; }
            set { SetProperty(ref _presented, value, "Presented"); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value, "Message"); }
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

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand = _saveCommand ?? new DelegateCommand((obj) => SaveEvent()); }
        }

        private async Task SaveEvent()
        {
            var response = await Api.AddTemplate(Message);
            if (response.success)
            {
                await Page.DisplayAlert("Success", "Successfully added new notificaiton", "Ok");
                await Page.Navigation.PopAsync();

            }
            else await Page.DisplayAlert("Error", response.message, "Ok");


        }
    }
}
