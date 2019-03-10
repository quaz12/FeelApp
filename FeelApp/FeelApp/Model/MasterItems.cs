using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FeelApp.Model
{
    public class MasterItems
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ImageSource Image { get; set; }
        public Page page { get; set; }
    }
}
