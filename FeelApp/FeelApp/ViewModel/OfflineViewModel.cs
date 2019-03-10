using FeelApp.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace FeelApp.ViewModel
{
    public class OfflineViewModel :BaseViewModel
    {
        public OfflineViewModel()
        {
            _selectedItem = "";
            _image = "";
            fillList();

        }

        private void fillList()
        {
            _pickerList = new ObservableCollection<string>();
            var list = new ObservableCollection<string>();
            list.Add("First Floor");
            list.Add("Second Floor");
            list.Add("Third Floor");
            list.Add("Fourth Floor");
            PickerList = list;
        }

        private ObservableCollection<string> _pickerList;
        public ObservableCollection<string> PickerList
        {
            get { return _pickerList; }
            set { SetProperty(ref _pickerList, value, "PickerList"); }
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value, "Image"); }
        }

        private string _selectedItem;
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value, "SelectedItem");
                //put here your code  
                if(_selectedItem == "First Floor")
                {
                    Image = "FirstFloor.png";
                }
                if (_selectedItem == "Second Floor")
                {
                    Image = "SecondFloor.png";
                }
                if (_selectedItem == "Third Floor")
                {
                    Image = "ThirdFloor.png";
                }
                if (_selectedItem == "Fourth Floor")
                {
                    Image = "FourthFloor.png";
                }

            }
        }
    }
}
