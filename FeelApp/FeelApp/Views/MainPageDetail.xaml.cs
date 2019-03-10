using FeelApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FeelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public MainPageDetail()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vm = this.BindingContext as MainPageViewModel;

            vm.InitHelp();
        }
    }
}