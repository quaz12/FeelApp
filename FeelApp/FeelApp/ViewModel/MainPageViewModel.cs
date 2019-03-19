using FeelApp.Helpers;
using FeelApp.Model;
using FeelApp.Services;
using FeelApp.ViewModel.Base;
using FeelApp.ViewModel.Commands;
using FeelApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace FeelApp.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {

        ObservableCollection<CallHelp> helpList = new ObservableCollection<CallHelp>();
        ObservableCollection<CallHelp> safeList = new ObservableCollection<CallHelp>();
        ObservableCollection<CallHelp> cautiousList = new ObservableCollection<CallHelp>();
        public  MainPageViewModel(Page page)
        {
            this.Page = page;
            _presented = false;
            _help = "0";
            _safe = "0";
            _cautious = "0";

            InitList();
            
            InitHelp();
            StartTimer();
        }

        public async Task InitHelp()
        {
            var response = await Api.GetHelpList();
            //1-help
            //2-cautious
            //3=safe
            helpList = new ObservableCollection<CallHelp>();
            safeList = new ObservableCollection<CallHelp>();
            cautiousList = new ObservableCollection<CallHelp>();
            if (response.success)
            {

                foreach (var item in response.data)
                {

                    if (item.HelpType == 1)
                    {
                        helpList.Add(item);
                    }
                    if (item.HelpType == 2)
                    {
                        cautiousList.Add(item);
                    }
                    if (item.HelpType == 3)
                    {
                        safeList.Add(item);
                    }

                }

            }
            else
            {
                await this.Page.DisplayAlert("Error", response.message, "Ok");

            }

            Help = helpList.Count.ToString();
            Cautious = cautiousList.Count.ToString();
            Safe = safeList.Count.ToString();
           
          
         

        }

        private void StartTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(7), () =>
            {
                Task.Run(async () =>
                {

                   await InitHelp();

                });
                return true;
            });
        }

        private ICommand _showHideMenuCommand;
        public ICommand ShowHideMenuCommand
        {
            get { return _showHideMenuCommand = _showHideMenuCommand ?? new DelegateCommand((obj) => ShowHideMenu()); }
        }

        private void ShowHideMenu()
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
        private string _safe;
        public string Safe
        {
            get { return _safe; }
            set { SetProperty(ref _safe, value, "Safe"); }
        }
        private string _cautious;
        public string Cautious
        {
            get { return _cautious; }
            set { SetProperty(ref _cautious, value, "Cautious"); }
        }
        private string _help;
        public string Help
        {
            get { return _help; }
            set { SetProperty(ref _help, value, "Help"); }
        }

        private bool _presented;
        public bool Presented
        {
            get { return _presented; }
            set { SetProperty(ref _presented, value, "Presented"); }
        }

        private void InitList()
        {
            var list = new ObservableCollection<MasterItems>();
            if (Globals.UserType == 1)
            {
              //Admin
                list.Add(new MasterItems { Id = 0, Text = "CALL FOR HELP", Image = "iconCall.png" });
                list.Add(new MasterItems { Id = 1, Text = "SCAN QR CODE", Image = "iconFloorPlan.png" });
                list.Add(new MasterItems { Id = 8, Text = "OFFLINE FLOOR PLAN", Image = "iconFloorPlan.png" });
                list.Add(new MasterItems { Id = 6, Text = "HELP TIPS", Image = "iconTips.png" });
                list.Add(new MasterItems { Id = 7, Text = "NOTIFICATIONS", Image = "iconNotification.png" });
                list.Add(new MasterItems { Id = 2, Text = "ADMINISTRATORS", Image = "iconAdmin.png" });
                list.Add(new MasterItems { Id = 9, Text = "ADD RESCUER", Image = "iconAdmin.png" });
                list.Add(new MasterItems { Id = 3, Text = "USERS", Image = "iconUsers.png" });
                list.Add(new MasterItems { Id = 4, Text = "EDIT ACCOUNT", Image = "iconEdit.png" });
               
                list.Add(new MasterItems { Id = 5, Text = "LOG OUT", Image = "iconLogOut.png" });
               
            }
            if(Globals.UserType == 2)
            {
                //list.Add(new MasterItems { Id = 1, Text = "SCAN QR CODE", Image = "iconFloorPlan.png" });
                list.Add(new MasterItems { Id = 8, Text = "OFFLINE FLOOR PLAN", Image = "iconFloorPlan.png" });
                list.Add(new MasterItems { Id = 6, Text = "HELP TIPS", Image = "iconTips.png" });
                list.Add(new MasterItems { Id = 7, Text = "NOTIFICATIONS", Image = "iconNotification.png" });
                list.Add(new MasterItems { Id = 2, Text = "ADMINISTRATORS", Image = "iconAdmin.png" });
                list.Add(new MasterItems { Id = 4, Text = "EDIT ACCOUNT", Image = "iconEdit.png" });
                list.Add(new MasterItems { Id = 5, Text = "LOG OUT", Image = "iconLogOut.png" });
            }
            if(Globals.UserType == 3)
            {
                //Rescuer
                list.Add(new MasterItems { Id = 8, Text = "OFFLINE FLOOR PLAN", Image = "iconFloorPlan.png" });
                list.Add(new MasterItems { Id = 6, Text = "HELP TIPS", Image = "iconTips.png" });
                list.Add(new MasterItems { Id = 7, Text = "NOTIFICATIONS", Image = "iconNotification.png" });
                list.Add(new MasterItems { Id = 2, Text = "ADMINISTRATORS", Image = "iconAdmin.png" });
                list.Add(new MasterItems { Id = 4, Text = "EDIT ACCOUNT", Image = "iconEdit.png" });
                list.Add(new MasterItems { Id = 5, Text = "LOG OUT", Image = "iconLogOut.png" });
            }

          
            MasterItemsList = list;
        }

        private ObservableCollection<MasterItems> _masterItemsList;
        public ObservableCollection<MasterItems> MasterItemsList
        {
            get { return _masterItemsList; }
            set { SetProperty(ref _masterItemsList, value, "MasterItemsList"); }
        }

        private async void ItemTappedEvent(object sender, ItemTappedEventArgs e)
        {
            var getItem = e.Item as MasterItems;
            var itemId = getItem.Id;

            switch(itemId)
            {
                case 0://callhelp
                    var callHelpPage = new CallHelpPage();
                    
                    await MainPage.DetailPage.Navigation.PushAsync(callHelpPage, true);
                    ShowHideMenu();
                    //await App.Current.MainPage.Navigation.PushAsync(callHelpPage, true);
                    break;
                case 1://GetFloorPlan

                    try
                    {

                        var scanner = DependencyService.Get<IQrScanningService>();
                        var result = await scanner.ScanAsync();
                        //var result = "FourthFloor";


                        if (result != null)
                        {
                            switch (result)
                            {
                                case "GroundFloor":
                                    var shapeFile = "GroundFloor.shp";
                                    var pathFile = "GroundFloorDirections.shp";
                                    
                                    var page = new MapPage(shapeFile, pathFile, false, false);
                                    await MainPage.DetailPage.Navigation.PushAsync(page, true);
                                    break;
                                case "SecondFloor":
                                    shapeFile = "SecondFloor.shp";
                                    pathFile = "SecondFloorDirection.shp";
                                    
                                    page = new MapPage(shapeFile, pathFile, false,false);
                                    await MainPage.DetailPage.Navigation.PushAsync(page, true);
                                    break;
                                case "ThirdFloor":
                                    shapeFile = "ThirdFloor.shp";
                                    pathFile = "SecondFloorDirection.shp";

                                    page = new MapPage(shapeFile, pathFile, false, false);
                                    await MainPage.DetailPage.Navigation.PushAsync(page, true);
                                    break;
                                case "FourthFloor":
                                    shapeFile = "FourthFloor.shp";
                                    pathFile = "SecondFloorDirection.shp";

                                    page = new MapPage(shapeFile, pathFile, false, false);
                                    await MainPage.DetailPage.Navigation.PushAsync(page, true);
                                    break;
                                default:
                                    break;
                            }


                        }
                        else
                        {
                           
                        }
                    }
                    catch (Exception ex)
                    {

                        
                    }
                    break;
                case 2://AdministratorPage
                    Globals.ListType = 1;
                    Globals.CreateRescuer = false;
                    var adminPage = new AdministratorPage();
                 
                    await MainPage.DetailPage.Navigation.PushAsync(adminPage, true);
                    ShowHideMenu();
                    break;
                case 3:
                    Globals.ListType = 2;
                    var userPage = new AdministratorPage();
                  
                    await MainPage.DetailPage.Navigation.PushAsync(userPage, true);
                    ShowHideMenu();
                    break;
                case 4:
                    var edtiPage = new EditPage();                    
                    await MainPage.DetailPage.Navigation.PushAsync(edtiPage, true);
                    ShowHideMenu();
                    break;
                case 5://Logout
                    Settings.SaveEmail = "";
                    Settings.SavePassword ="";
                    Settings.SaveUserType = 0;
                    Settings.SaveID = 0;
                    Application.Current.MainPage = new LoginPage();
                    break;
                case 6://HelpTips
                    var helpTip = new HelpTipsPage();
                    await MainPage.DetailPage.Navigation.PushAsync(helpTip, true);
                    ShowHideMenu();
                    break;
                case 7://NotificationPage
                    var notificationPage = new NotificationList();
                    ShowHideMenu();
                    await MainPage.DetailPage.Navigation.PushAsync(notificationPage, true);
                    break;

                case 8://FloorPlan
                    var offlinPage = new OfflineMap();
                    ShowHideMenu();
                    await MainPage.DetailPage.Navigation.PushAsync(offlinPage, true);
                    break;
                case 9://FloorPlan
                    var createAccountPage = new CreateAccountPage();
                    Globals.CreateRescuer = true;
                    ShowHideMenu();
                    await MainPage.DetailPage.Navigation.PushAsync(createAccountPage, true);
                    break;
            }



        }

        private ICommand _helpCommand;
        public ICommand HelpCommand
        {
            get { return _helpCommand = _helpCommand ?? new DelegateCommand(async (obj) => await Helpevent()); }
        }

        private async Task Helpevent()
        {
            Globals.HelpListTitle = "HELP LIST";
            Globals.ListHelp = helpList;
            var page = new HelpList();
            await Application.Current.MainPage.Navigation.PushAsync(page, true);
           //await this.Page.Navigation.PushAsync(page, true);
        }

        private ICommand _safeCommand;
        public ICommand SafeCommand
        {
            get { return _safeCommand = _safeCommand ?? new DelegateCommand(async (obj) => await SafeEvent()); }
        }

        private async Task SafeEvent()
        {
            Globals.HelpListTitle = "SAFE LIST";
            Globals.ListHelp = safeList;
            var page = new HelpList();
            await this.Page.Navigation.PushAsync(page, true);
        }

        private ICommand _cautiousCommand;
        public ICommand CautiousCommand
        {
            get { return _cautiousCommand = _cautiousCommand ?? new DelegateCommand(async (obj) => await CautiousEvent()); }
        }

        private async Task CautiousEvent()
        {
            Globals.HelpListTitle = "CAUTIOUS LIST";
            Globals.ListHelp = cautiousList;
            var page = new HelpList();
            await this.Page.Navigation.PushAsync(page, true);
        }

        private event EventHandler<ItemTappedEventArgs> _itemTappedEventHandler;
        public EventHandler<ItemTappedEventArgs> ItemTappedEventHandler
        {
            get { return _itemTappedEventHandler = _itemTappedEventHandler ?? new EventHandler<ItemTappedEventArgs>(ItemTappedEvent); }
        }

      
        private ICommand _notificationCommand;
        public ICommand NotificationCommand
        {
            get { return _notificationCommand = _notificationCommand ?? new DelegateCommand(async (obj) => await NotificationEvent()); }
        }

        private async Task NotificationEvent()
        {
            var notificationPage = new NotificationiListTeplate();
            //ShowHideMenu();
            //await MainPage.DetailPage.Navigation.PushAsync(notificationPage, true);
            await Application.Current.MainPage.Navigation.PushAsync(notificationPage, true);
        }
    }
}
