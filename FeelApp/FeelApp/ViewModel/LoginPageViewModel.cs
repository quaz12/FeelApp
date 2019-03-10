using FeelApp.Helpers;
using FeelApp.Model;
using FeelApp.ViewModel.Base;
using FeelApp.ViewModel.Commands;
using FeelApp.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FeelApp.ViewModel
{
    public class LoginPageViewModel : BaseViewModel
    {
        public   LoginPageViewModel(Page page)
        {
            this.Page = page;
            //_email = "sample@sample.com";
            //_password = "sample";
            _email = "admin@admin.com";
            _password = "admin";
            _isbusy = false;
            _visibleButton = true;
            Init();
        }

        private async Task Init()
        {
            //GroundFloor
            string fileName = "GroundFloor.cpg";
            await CopyShapeFile(fileName);
            
            fileName = "GroundFloor.dbf";
            await CopyShapeFile(fileName);

            fileName = "GroundFloor.prj";
            await CopyShapeFile(fileName);

            fileName = "GroundFloor.qpj";
            await CopyShapeFile(fileName);

            fileName = "GroundFloor.shx";
            await CopyShapeFile(fileName);

            fileName = "GroundFloor.shp";
            await CopyShapeFile(fileName);

            //Path
            fileName = "GroundFloorDirections.cpg";
            await CopyShapeFile(fileName);

            fileName = "GroundFloorDirections.dbf";
            await CopyShapeFile(fileName);

            fileName = "GroundFloorDirections.prj";
            await CopyShapeFile(fileName);

            fileName = "GroundFloorDirections.qpj";
            await CopyShapeFile(fileName);

            fileName = "GroundFloorDirections.shx";
            await CopyShapeFile(fileName);

            fileName = "GroundFloorDirections.shp";
            await CopyShapeFile(fileName);
          

            //SecondFloor
            fileName = "SecondFloor.cpg";
            await CopyShapeFile(fileName);

            fileName = "SecondFloor.dbf";
            await CopyShapeFile(fileName);

            fileName = "SecondFloor.prj";
            await CopyShapeFile(fileName);

            fileName = "SecondFloor.qpj";
            await CopyShapeFile(fileName);

            fileName = "SecondFloor.shx";
            await CopyShapeFile(fileName);

            fileName = "SecondFloor.shp";
            await CopyShapeFile(fileName);

            //Path
            fileName = "SecondFloorDirection.cpg";
            await CopyShapeFile(fileName);

            fileName = "SecondFloorDirection.dbf";
            await CopyShapeFile(fileName);

            fileName = "SecondFloorDirection.prj";
            await CopyShapeFile(fileName);

            fileName = "SecondFloorDirection.qpj";
            await CopyShapeFile(fileName);

            fileName = "SecondFloorDirection.shx";
            await CopyShapeFile(fileName);

            fileName = "SecondFloorDirection.shp";
            await CopyShapeFile(fileName);
            //ThirdFloor
            fileName = "ThirdFloor.cpg";
            await CopyShapeFile(fileName);

            fileName = "ThirdFloor.dbf";
            await CopyShapeFile(fileName);

            fileName = "ThirdFloor.prj";
            await CopyShapeFile(fileName);

            fileName = "ThirdFloor.qpj";
            await CopyShapeFile(fileName);

            fileName = "ThirdFloor.shx";
            await CopyShapeFile(fileName);

            fileName = "ThirdFloor.shp";
            await CopyShapeFile(fileName);

            //FourthFloor
            fileName = "FourthFloor.cpg";
            await CopyShapeFile(fileName);

            fileName = "FourthFloor.dbf";
            await CopyShapeFile(fileName);

            fileName = "FourthFloor.prj";
            await CopyShapeFile(fileName);

            fileName = "FourthFloor.qpj";
            await CopyShapeFile(fileName);

            fileName = "FourthFloor.shx";
            await CopyShapeFile(fileName);

            fileName = "FourthFloor.shp";
            await CopyShapeFile(fileName);
        }

        public async Task CopyShapeFile(string filename)
        {
           
            //Move the shape files from assets folder to Personal/Library/Shapefiles
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(folder, "Library", "ShapeFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string outFileName = Path.Combine(path, filename);
            if(!File.Exists(outFileName))
            {
                var myInput = Android.App.Application.Context.Assets.Open(filename);

                var myOutput = new FileStream(outFileName, FileMode.Create);

                byte[] buffer = new byte[1024];
                int length;
                try
                {
                    while ((length = myInput.Read(buffer, 0, 1024)) > 0)
                        myOutput.Write(buffer, 0, length);

                    myOutput.Flush();
                    myOutput.Close();
                    myInput.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }



                if (Directory.Exists(outFileName))
                {

                }
          
            }
         
           
        
            
        }

        public async Task CreateAccountEvent()
        {
            var page = new CreateAccountPage();
            await App.Current.MainPage.Navigation.PushAsync(page, true);
        }

        private ICommand _createAccountCommand;
        public ICommand CreateAccountCommand
        {
            get { return _createAccountCommand = _createAccountCommand ?? new DelegateCommand(async (obj) => await CreateAccountEvent()); }
        }
        public async Task SignInEvent()
        {
            IsBusy = true;
            VisibleButton = false;
            var conn = CrossConnectivity.Current.IsConnected;
            if (conn)
            {
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                {
                    var response = await Api.Login(Email, Password);
                    if (response.succcess)
                    {

                        Globals.UserID = response.Id;
                        Globals.UserType = response.UserType;
                        Settings.SaveEmail = Email;
                        Settings.SavePassword = Password;
                        Settings.SaveUserType = response.UserType;
                        Settings.SaveID = response.Id;
                        var userType = response.UserType;//change to usertype
                        if (userType == 1)
                        {
                            var page = new MainPage(userType);
                            Application.Current.MainPage = new NavigationPage(page);
                            IsBusy = false;
                            VisibleButton = true;
                        }
                        if (userType == 2)
                        {
                            var page = new MainPage(userType);
                            Application.Current.MainPage = new NavigationPage(page);
                            //var userPage = new CallHelpPage();
                            //await Application.Current.MainPage.Navigation.PushAsync(userPage, true);
                            IsBusy = false;
                            VisibleButton = true;
                        }
                    }
                    else
                    {
                        await this.Page.DisplayAlert("Error", response.message, "Ok");
                        IsBusy = false;
                        VisibleButton = true;
                    }
                }
                else
                {
                    await this.Page.DisplayAlert("Error", "Please enter username and password", "Ok");
                    IsBusy = false;
                    VisibleButton = true;
                }
            }
            else
            {
                await this.Page.DisplayAlert("Error", "No Internet connectiong going offline mode", "Ok");
                var page = new OfflineMap();
                await Application.Current.MainPage.Navigation.PushAsync(page, true);
                IsBusy = false ;
                VisibleButton = true;
            }
       

            //var key = "AIzaSyCfVAXYn4cpGDOa4DRw2jfA7cYTZoczR44";
            //var url = "feeldb-ccb13.firebaseapp.com";

            //var profile = 

            //var reponse = await Api.Login(Email, Password);
            //var x = 1123;




        }
        
        private bool _visibleButton;
        public bool VisibleButton
        {
            get { return _visibleButton; }
            set { SetProperty(ref _visibleButton, value, "VisibleButton"); }
        }
        private bool _isbusy;
        public bool IsBusy
        {
            get { return _isbusy; }
            set { SetProperty(ref _isbusy, value, "IsBusy"); }
        }

        public ObservableCollection<Profile> Profile()
        {
            var _profile = new ObservableCollection<Profile>();
            return _profile;
        }

        private ICommand _signInCommand;
        public ICommand SignInCommand
        {
            get { return _signInCommand = _signInCommand ?? new DelegateCommand(async (obj) => await SignInEvent()); }
        }
    
        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value, "Email"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value, "Password"); }
        }
    }
}
