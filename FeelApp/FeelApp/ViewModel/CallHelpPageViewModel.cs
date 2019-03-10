using FeelApp.Helpers;
using FeelApp.Services;
using FeelApp.ViewModel.Base;
using FeelApp.ViewModel.Commands;
using FeelApp.Views;

using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FeelApp.ViewModel
{
    public class CallHelpPageViewModel :BaseViewModel
    {

        public CallHelpPageViewModel(Page page)
        {
            this.Page = page;
            _presented = false;
            var userType = Globals.UserType;
            if(userType == 2)
            {
                _isUser = true;
            }
            else _isUser = false;
        }

        private ICommand _helpCommand;
        public ICommand HelpCommand
        {
            get { return _helpCommand = _helpCommand ?? new DelegateCommand(async (obj) => await HelpEvent()); }
        }

        private async Task HelpEvent()
        {

            //var id = Globals.UserID;
            //var date = DateTime.Now.ToString("yyyy-MM-dd");
            //var response = await Api.CallHelp(id, 1, 1, date);
            Globals.HelpType = 1;
            var page = new SelectFloorPage();
            await Page.Navigation.PushAsync(page, true);
        }

        private ICommand _safeCommand;
        public ICommand SafeCommand
        {
            get { return _safeCommand = _safeCommand ?? new DelegateCommand(async (obj) => await SafeEvent()); }
        }

        private async Task SafeEvent()
        {
            var id = Globals.UserID;
            //var date = DateTime.Now.ToString("yyyy-MM-dd");
            //var response = await Api.CallHelp(id, 1, 3, date);
            //Globals.HelpType = 3;
            //var page = new SelectFloorPage();
            //await Page.Navigation.PushAsync(page, true);
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var response = await Api.CallHelp(id, 1, 3, date);
            if (response.success)
            {
                await Page.DisplayAlert("Success", "Stay safe", "Ok");


                await Page.Navigation.PopAsync();
            }
            else
            {
                await Page.DisplayAlert("Error", $"{response.message}", "Ok");
                await Page.Navigation.PopAsync();
            }
        }

        private ICommand _cautiousCommand;
        public ICommand CautiousCommand
        {
            get { return _cautiousCommand = _cautiousCommand ?? new DelegateCommand(async (obj) => await CautiousEvent()); }
        }

        private async Task CautiousEvent()
        {
            //var id = Globals.UserID;
            //var date = DateTime.Now.ToString("yyyy-MM-dd");
            //var response = await Api.CallHelp(id, 1, 2, date);
            //if(response.success)
            //{

            //}
            Globals.HelpType = 2;
            var page = new SelectFloorPage();
            await Page.Navigation.PushAsync(page, true);
        }

        private ICommand _showHideMenuCommand;
        public ICommand ShowHideMenuCommand
        {
            get { return _showHideMenuCommand = _showHideMenuCommand ?? new DelegateCommand((obj) => ShowHideMenu()); }
        }

        private void ShowHideMenu()
        {
            var mainPage = App.Current.MainPage.Navigation.NavigationStack[0] as MainPage;
            
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
        private bool _presented;
        public bool Presented
        {
            get { return _presented; }
            set { SetProperty(ref _presented, value, "Presented"); }
        }
        private bool _isUser;
        public bool IsUser
        {
            get { return _isUser; }
            set { SetProperty(ref _isUser, value, "IsUser"); }
        }

        private ICommand _scanCommand;
        public ICommand ScanCommand
        {
            get { return _scanCommand = _scanCommand ?? new DelegateCommand(async (obj) => await ScanEvent()); }
        }

        private async Task ScanEvent()
        {
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

                            page = new MapPage(shapeFile, pathFile, false, false);
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
        }
    }
}
