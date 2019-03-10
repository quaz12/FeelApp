using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FeelApp.Model.Response
{
    public class EditResponse
    {
        public string error { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public ObservableCollection<string> data { get; set; }
          
    }
}
