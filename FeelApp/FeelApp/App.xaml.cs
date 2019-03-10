using Esri.ArcGISRuntime;
using FeelApp.Helpers;
using FeelApp.ViewModel;
using FeelApp.Views;
using Microsoft.AppCenter;

using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microsoft.AppCenter.Push;


namespace FeelApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            // Deployed applications must be licensed at the Lite level or greater. 
            // See https://developers.arcgis.com/licensing for further details.

            // Initialize the ArcGIS Runtime before any components are created.
            ArcGISRuntimeEnvironment.Initialize();

            // The root page of your application
            var email = Settings.SaveEmail;
            var userType = Settings.SaveUserType;
            if (email != "")
            {
                Globals.UserID = Settings.SaveID;
                Globals.UserType = Settings.SaveUserType;
                //Settings.SaveUserType = response.UserType;
                //Settings.SaveID = response.Id;
                if (userType == 1)
                {
                    var page = new MainPage(userType);
                    MainPage = new NavigationPage(page);
                    Application.Current.MainPage = new NavigationPage(page);
                }
                else
                {
                    var page = new MainPage(userType);
                    Application.Current.MainPage = new NavigationPage(page);
                    //var userPage = new CallHelpPage();
                    //await Application.Current.MainPage.Navigation.PushAsync(userPage, true);
           
                }

                //if (userType == 1)
                //{
                //    var page = new MainPage(userType);
                //    Application.Current.MainPage = new NavigationPage(page);
                //    IsBusy = false;
                //    VisibleButton = true;
                //}
                //if (userType == 2)
                //{
                //    var page = new MainPage(userType);
                //    Application.Current.MainPage = new NavigationPage(page);
                //    //var userPage = new CallHelpPage();
                //    //await Application.Current.MainPage.Navigation.PushAsync(userPage, true);
                //    IsBusy = false;
                //    VisibleButton = true;
                //}

              
            }
            else
            {
                var page = new LoginPage();

                //var page = new MapPage();
                MainPage = new NavigationPage(page);
            }
          
          
        }

        protected override void OnStart()
        {
            base.OnStart();
            AppCenter.Start("2b7ed114-6f61-4915-b9c0-869112176f02", typeof(Push));
           
        }

    }
}