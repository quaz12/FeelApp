using FeelApp.Helpers;
using FeelApp.Model;
using FeelApp.ViewModel.Base;
using FeelApp.ViewModel.Commands;
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
    public class HelpListViewModel:BaseViewModel
    {
        public HelpListViewModel(Page page)
        {
            this.Page = page;
            _helpList = Globals.ListHelp;

            getFloors();

            _title = Globals.HelpListTitle;
            _isSafe = true;
            if (Globals.HelpListTitle == "SAFE LIST")
            {
                IsSafe = false;
            }
        }

        private void getFloors()
        {
            foreach(var item in HelpList)
            {
                if (Globals.HelpListTitle == "SAFE LIST") item.IsSafe = false;               
                else item.IsSafe = true;

                if (item.Floor == 1) item.Location = "First Floor";
                if (item.Floor == 2) item.Location = "Second Floor";
                if (item.Floor == 3) item.Location = "Third Floor";
                if (item.Floor == 4) item.Location = "Fourth Floor";
              
                    
            }
        }

        private ObservableCollection<CallHelp> _helpList;
        public ObservableCollection<CallHelp> HelpList
        {
            get { return _helpList; }
            set { SetProperty(ref _helpList, value, "HelpList"); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, "Title"); }
        }

        private bool _isSafe;
        public bool IsSafe
        {
            get { return _isSafe; }
            set { SetProperty(ref _isSafe, value, "IsSafe"); }
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

        private ICommand _callCommand;
        public ICommand CallCommand
        {
            get { return _callCommand = _callCommand ?? new DelegateCommand(async (obj) => await CallEvent(obj)); }
        }

        private async Task CallEvent(object obj)
        {
            try
            {
                var item = (obj as CallHelp);
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

        private ICommand _mapCommand;
        public ICommand MapCommand
        {
            get { return _mapCommand = _mapCommand ?? new DelegateCommand(async (obj) => await MapEvent(obj)); }
        }

        private async Task MapEvent(object obj)
        {
            var item = (obj as CallHelp);
            var id = item.AccountId;
            Globals.HelpAccountId = id;
            string shapeFile = "GroundFloor.shp";
            string pathFile = "GroundFloorDirections.shp";
            if (item.Floor == 1)
            {
                shapeFile = "GroundFloor.shp";
                pathFile = "GroundFloorDirections.shp";
            }
            if (item.Floor == 2)
            {
                shapeFile = "SecondFloor.shp";
                pathFile = "SecondFloorDirection.shp";
            }
            if (item.Floor == 3)
            {
                shapeFile = "ThirdFloor.shp";
                pathFile = "SecondFloorDirection.shp";
            }
            if (item.Floor == 4)
            {
                shapeFile = "FourthFloor.shp";
                pathFile = "SecondFloorDirection.shp";
            }


            var page = new MapPage(shapeFile, pathFile, false, true);
            await Page.Navigation.PushAsync(page, true);

        }
    }
}
