using FeelApp.Model;
using FeelApp.ViewModel.Base;
using FeelApp.ViewModel.Commands;
using FeelApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FeelApp.ViewModel
{
    public class UserPageViewModel : BaseViewModel
    {
        public UserPageViewModel(Page page)
        {
            this.Page = page;
            InitList();
        }
        private ICommand _showHideMenuCommand;
        public ICommand ShowHideMenuCommand
        {
            get { return _showHideMenuCommand = _showHideMenuCommand ?? new DelegateCommand((obj) => ShowHideMenu()); }
        }

        private void ShowHideMenu()
        {
            var mainPage = this.Page as UserPage;

            if (mainPage.IsPresented)
            {

                var isPresented = false;
                Presented = isPresented;

                mainPage.IsPresented = Presented;
            }
            else
            {

                var isPresented = true;
                Presented = isPresented;

                mainPage.IsPresented = Presented;
            }
        }

        private bool _presented;
        public bool Presented
        {
            get { return _presented; }
            set { SetProperty(ref _presented, value, "Presented"); }
        }

        private void InitList()
        {
            var list = new ObservableCollection<MasterItems>();
            list.Add(new MasterItems { Id = 0, Text = "HELP TIPS", Image = "iconCall.png" });
            list.Add(new MasterItems { Id = 1, Text = "NOTIFICATIONS", Image = "iconNotification.png" });
            list.Add(new MasterItems { Id = 2, Text = "ADMINISTRATORS", Image = "iconAdmin.png" });
            list.Add(new MasterItems { Id = 3, Text = "EDIT ACCOUNT", Image = "iconEdit.png" });
            list.Add(new MasterItems { Id = 4, Text = "LOG OUT", Image = "iconLogOut.png" });
            MasterItemsList = list;
        }

        private ObservableCollection<MasterItems> _masterItemsList;
        public ObservableCollection<MasterItems> MasterItemsList
        {
            get { return _masterItemsList; }
            set { SetProperty(ref _masterItemsList, value, "MasterItemsList"); }
        }
    }
}
