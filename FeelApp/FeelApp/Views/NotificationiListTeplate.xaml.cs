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
	public partial class NotificationiListTeplate : ContentPage
	{
		public NotificationiListTeplate ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new NotificationTemplateViewModel(this);
        }

        private void MenuItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            var vm = this.BindingContext as NotificationTemplateViewModel;

            vm.ItemTappedEventHandler?.Invoke(sender, e);

        }

        private void Delete(object sender, EventArgs e)
        {
            //((ListView)sender).SelectedItem = null;
            var mi = ((MenuItem)sender);
            var vm = this.BindingContext as NotificationTemplateViewModel;

            vm.DeleteHandler?.Invoke(mi, e);
        }

        private void Edit(object sender, EventArgs e)
        {
            //((ListView)sender).SelectedItem = null;
            var mi = ((MenuItem)sender);
            var vm = this.BindingContext as NotificationTemplateViewModel;

            vm.EditHandler?.Invoke(mi, e);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = this.BindingContext as NotificationTemplateViewModel;
            await vm.GetTemplate();
        }
    }
}