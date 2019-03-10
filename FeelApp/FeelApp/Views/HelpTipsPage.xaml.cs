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
	public partial class HelpTipsPage : ContentPage
	{
		public HelpTipsPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new HelpTipsViewModel(this);
        }
	}
}