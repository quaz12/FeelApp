using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FeelApp.Model
{
    public class LoginResponse
    {
        public string error { get; set; }
        public bool succcess { get; set; }
        public string message { get; set; }
        
        public int Id { get; set; }
        public int UserType { get; set; }
        public ObservableCollection<Login> data { get; set; }
    }
}
