using System;
using System.Collections.Generic;
using System.Text;

namespace FeelApp.Model
{
    public class CallHelp
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Emergency { get; set; }
        public int Floor { get; set; }
        public bool IsSafe { get; set; }
        public int HelpType { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
    }
}
