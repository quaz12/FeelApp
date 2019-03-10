using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FeelApp.ViewModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfflineMap : ContentPage
    {
        public OfflineMap()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //BindingContext = new OfflineViewModel();
            GenerateList();
        }

        private void GenerateList()
        {
            ObservableCollection<string> PickerList = new ObservableCollection<string>();
            var list = new ObservableCollection<string>();
            list.Add("First Floor");
            list.Add("Second Floor");
            list.Add("Third Floor");
            list.Add("Fourth Floor");
            PickerList = list;
            picker.ItemsSource = PickerList;

        }

        private void item(object sender, EventArgs e)
        {
            var item = picker.SelectedItem;
            if (item == "First Floor")
            {
                img.Source = "FirstFloor.png";
            }
            if (item == "Second Floor")
            {
                img.Source = "SecondFloor.png";
            }
            if (item == "Third Floor")
            {
                img.Source = "ThirdFloor.png";
            }
            if (item == "Fourth Floor")
            {
                img.Source = "FourthFloor.png";
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}