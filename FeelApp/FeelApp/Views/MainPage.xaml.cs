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
    public partial class MainPage : MasterDetailPage
    {
        public static NavigationPage DetailPage { get; set; }
        public MainPage(int userType)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new MainPageViewModel(this);
            Detail.BindingContext = this.BindingContext;
            Master.BindingContext = this.BindingContext;
            
            if (userType == 2)
            {
                DetailPage = new NavigationPage(new CallHelpPage());
                Detail = DetailPage;
            }
            if(userType == 1)
            {
                DetailPage = new NavigationPage(new MainPageDetail());
                Detail = DetailPage;
            }
            if (userType == 3)
            {
                DetailPage = new NavigationPage(new RescuerMainPage());
                Detail = DetailPage;
            }

        }


    }
}