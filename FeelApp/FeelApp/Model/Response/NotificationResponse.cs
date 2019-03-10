using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FeelApp.Model.Response
{
    public class NotificationResponse
    {
        public string error { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public ObservableCollection<Notifications> data { get; set; }
    }
}
