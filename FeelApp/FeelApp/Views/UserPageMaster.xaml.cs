using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FeelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPageMaster : ContentPage
    {
        public ListView ListView;

        public UserPageMaster()
        {
            InitializeComponent();

            BindingContext = new UserPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class UserPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<UserPageMenuItem> MenuItems { get; set; }
            
            public UserPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<UserPageMenuItem>(new[]
                {
                    new UserPageMenuItem { Id = 0, Title = "Page 1" },
                    new UserPageMenuItem { Id = 1, Title = "Page 2" },
                    new UserPageMenuItem { Id = 2, Title = "Page 3" },
                    new UserPageMenuItem { Id = 3, Title = "Page 4" },
                    new UserPageMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}