using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FeelApp.ViewModel.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public Page Page { get; set; }

        public INavigation Navigation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T item, T value, string property)
        {
            if (EqualityComparer<T>.Default.Equals(item, default(T)))
                item = Activator.CreateInstance<T>();

            if (!item.Equals(value))
            {
                item = value;
                NotifyPropertyChanged(property);
            }
        }
    }
}
