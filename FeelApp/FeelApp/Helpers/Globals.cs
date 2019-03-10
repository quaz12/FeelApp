using FeelApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FeelApp.Helpers
{
    public class Globals
    {
        public static int UserID { get; set; }
        public static int UserType { get; set; }
        public static ObservableCollection<CallHelp> ListHelp { get; set; }
        public static string HelpListTitle { get; set; }
        public static int Floor { get; set; }
        public static int HelpType { get; set; }
        public static int ListType { get; set; }
        public static NotificationTemplate notificationTemplate { get; set; } = new NotificationTemplate();

        public static int HelpAccountId { get; set; }
    }
}
