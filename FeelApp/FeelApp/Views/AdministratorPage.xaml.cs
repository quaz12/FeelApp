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
	public partial class AdministratorPage : ContentPage
	{
		public AdministratorPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new AdministratorPageViewModel(this);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var sb = ((SearchBar)sender);
            var vm = this.BindingContext as AdministratorPageViewModel;
            
            vm.TextChangedEventHandler?.Invoke(sender, e);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = this.BindingContext as AdministratorPageViewModel;
            await vm.GetAdminList();
        }
    }
}