using System;
using System.Collections.Generic;
using System.Text;

namespace FeelApp.Model.Response
{
    public class PushNotificationResponse
    {
        public string notification_id { get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }
        public string code { get; set; }
    }
}
