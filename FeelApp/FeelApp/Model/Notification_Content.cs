using System;
using System.Collections.Generic;
using System.Text;

namespace FeelApp.Model
{
    public class Notification_Content
    {
        public NotificationContent notification_content { get; set; }
    }
    public class NotificationContent
    {
        public string name { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

    
}
