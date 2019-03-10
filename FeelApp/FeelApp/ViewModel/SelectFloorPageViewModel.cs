using FeelApp.Helpers;
using FeelApp.ViewModel.Base;
using FeelApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FeelApp.ViewModel
{
    public class SelectFloorPageViewModel :BaseViewModel
    {
        public SelectFloorPageViewModel(Page page)
        {
            this.Page = page;
        }

        private ICommand _firstCommand;
        public ICommand FirstCommand
        {
            get { return _firstCommand = _firstCommand ?? new DelegateCommand(async (obj) => await FirstEvent()); }
        }

        private async Task FirstEvent()
        {
            
            var id = Globals.UserID;
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (Globals.HelpType == 1)
            {
               
                var response = await Api.CallHelp(id, 1, 1, date);
                if(response.success)
                {
                    await Page.DisplayAlert("Success", "Please dont close the app so that we can track your location for rescue", "Ok");
                    await GotoFLoorPlan(1);
                }
                else
                {
                    await Page.DisplayAlert("Error", $"{response.message}", "Ok");
                    await GotoFLoorPlan(1);
                }
            }
            if (Globals.HelpType == 2)
            {
             
                var response = await Api.CallHelp(id, 1, 2, date);
                if (response.success)
                {
                    await Page.DisplayAlert("Success", "Please dont close the app so that we can track your location for rescue", "Ok");
                    await GotoFLoorPlan(1);
                }
                else
                {
                    await Page.DisplayAlert("Error", $"{response.message}", "Ok");
                    await GotoFLoorPlan(1);
                }
            }
            if (Globals.HelpType == 3)
            {
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
          

        }

        public async Task GotoFLoorPlan(int floor)
        {
            var shapeFile = "";
            var pathFile = "";
            MapPage page;
            if (floor ==1)
            {
                shapeFile = "GroundFloor.shp";
                pathFile = "GroundFloorDirections.shp";

               
               
            }
            if (floor == 2)
            {
                shapeFile = "SecondFloor.shp";
                pathFile = "SecondFloorDirection.shp";

               
               
            }
            if (floor == 3)
            {
                shapeFile = "ThirdFloor.shp";
                pathFile = "SecondFloorDirection.shp";

              
              
            }
            if (floor == 4)
            {
                shapeFile = "FourthFloor.shp";
                pathFile = "SecondFloorDirection.shp";




            }
            page = new MapPage(shapeFile, pathFile, true,false);
            await Page.Navigation.PushAsync(page, true);
        }

        private ICommand _secondCommand;
        public ICommand SecondCommand
        {
            get { return _secondCommand = _secondCommand ?? new DelegateCommand(async (obj) => await SecondEvent()); }
        }

        private async Task SecondEvent()
        {
            var helpMessage = "Stay Cautious";
            var id = Globals.UserID;
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (Globals.HelpType == 1)
            {

                var response = await Api.CallHelp(id, 2, 1, date);
                if (response.success)
                {
                    await Page.DisplayAlert("Success", "Please dont close the app so that we can track your location for rescue", "Ok");
                    await GotoFLoorPlan(2);
                }
                else
                {
                    await Page.DisplayAlert("Error", $"{response.message}", "Ok");
                    await GotoFLoorPlan(2);
                }
            }
            if (Globals.HelpType == 2)
            {

                var response = await Api.CallHelp(id, 2, 2, date);
                if (response.success)
                {
                    await Page.DisplayAlert("Success", "Please dont close the app so that we can track your location for rescue", "Ok");
                    await GotoFLoorPlan(2);
                }
                else
                {
                    await Page.DisplayAlert("Error", $"{response.message}", "Ok");
                    await GotoFLoorPlan(2);
                }
            }
            if (Globals.HelpType == 3)
            {
                var response = await Api.CallHelp(id, 2, 3, date);
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
          
        }

        private ICommand _thirdCommand;
        public ICommand ThirdCommand
        {
            get { return _thirdCommand = _thirdCommand ?? new DelegateCommand(async (obj) => await ThirdEvent()); }
        }

        private async Task ThirdEvent()
        {
        
            var id = Globals.UserID;
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (Globals.HelpType == 1)
            {

                var response = await Api.CallHelp(id, 3, 1, date);
                if (response.success)
                {
                    await Page.DisplayAlert("Success", "Please dont close the app so that we can track your location for rescue", "Ok");
                    await GotoFLoorPlan(3);
                }
                else
                {
                    await Page.DisplayAlert("Error", $"{response.message}", "Ok");
                    await GotoFLoorPlan(3);
                }
            }
            if (Globals.HelpType == 2)
            {

                var response = await Api.CallHelp(id, 3, 2, date);
                if (response.success)
                {
                    await Page.DisplayAlert("Success", "Please dont close the app so that we can track your location for rescue", "Ok");
                    await GotoFLoorPlan(3);
                }
                else
                {
                    await Page.DisplayAlert("Error", $"{response.message}", "Ok");
                    await GotoFLoorPlan(3);
                }
            }
            if (Globals.HelpType == 3)
            {
                var response = await Api.CallHelp(id, 3, 3, date);
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
            
        }

        private ICommand _fourthCommand;
        public ICommand FourthCommand
        {
            get { return _fourthCommand = _fourthCommand ?? new DelegateCommand(async (obj) => await FourthEvent()); }
        }

        private async Task FourthEvent()
        {
            var id = Globals.UserID;
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (Globals.HelpType == 1)
            {

                var response = await Api.CallHelp(id, 4, 1, date);
                if (response.success)
                {
                    await Page.DisplayAlert("Success", "Please dont close the app so that we can track your location for rescue", "Ok");
                    await GotoFLoorPlan(4);
                }
            }
            if (Globals.HelpType == 2)
            {

                var response = await Api.CallHelp(id, 4, 2, date);
                if (response.success)
                {
                    await Page.DisplayAlert("Success", "Please dont close the app so that we can track your location for rescue", "Ok");
                    await GotoFLoorPlan(4);
                }
            }
            if (Globals.HelpType == 3)
            {
                var response = await Api.CallHelp(id, 4, 3, date);
                if (response.success)
                {
                    await Page.DisplayAlert("Success", "Stay safe", "Ok");
                    await Page.Navigation.PopAsync();
                }
            }
           
        }
    }
}
