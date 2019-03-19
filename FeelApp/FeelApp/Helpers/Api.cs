using FeelApp.Model;
using FeelApp.Model.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FeelApp.Helpers
{
  
    public class Api
    {
        public static string baseUrl = "http://166.62.54.215";

        public async static Task<LoginResponse> Login(string email, string password)
        {            
            var resource = $"/api/login/verify?email={email}&password={password}";//?email={encodeEmail}&password={encodePassword}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.PostAsync(url, null);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<LoginResponse>(response);
            return getResponse;
         

        }

        public async static Task<RegisterResponse> CreateAccount(string name, string email, string password, string contact, string emergency )
        {
            var resource = $"/api/account/create?name={name}&email={email}&password={password}&contact={contact}&emergency={emergency}&userType=2";//?email={encodeEmail}&password={encodePassword}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.PostAsync(url, null);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<RegisterResponse>(response);
            return getResponse;


        }
        public async static Task<RegisterResponse> CreateAdmin(string name, string email, string password, string contact, string emergency)
        {
            var resource = $"/api/account/create?name={name}&email={email}&password={password}&contact={contact}&emergency={emergency}&userType=1";//?email={encodeEmail}&password={encodePassword}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.PostAsync(url, null);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<RegisterResponse>(response);
            return getResponse;


        }

        public async static Task<RegisterResponse> CreateRescuer(string name, string email, string password, string contact, string emergency)
        {
            var resource = $"/api/account/create?name={name}&email={email}&password={password}&contact={contact}&emergency={emergency}&userType=3";//?email={encodeEmail}&password={encodePassword}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.PostAsync(url, null);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<RegisterResponse>(response);
            return getResponse;


        }

        public async static Task<CallHelpResponse> GetHelpList()
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            //var date = "2019-01-29";
            var resource = $"/api/help/list?date={date}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.GetAsync(url);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<CallHelpResponse>(response);
            return getResponse;
        }

        public async static Task<RegisterResponse> CallHelp(int acctId, int floor, int helpType, string date)
        {
            var resource = $"/api/help/callhelp?acctId={acctId}&floor={floor}&date={date}&help={helpType}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.PostAsync(url, null);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<RegisterResponse>(response);
            return getResponse;


        }
        public async static Task<GetProfilesResponse> GetProfiles()
        {
       
            var resource = $"/api/users/all";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.GetAsync(url);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<GetProfilesResponse>(response);
            return getResponse;
        }

        public async static Task<EditResponse> EditProfile( string name, string contact, string emergency, string email, string password)
        {
            var id = Globals.UserID;
            var resource = $"/api/users/editprofile?id={id}&name={name}&contact={contact}&emergency={emergency}&email={email}&password={password}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.PostAsync(url, null);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<EditResponse>(response);
            return getResponse;


        }

        public async static Task<GetProfilesResponse> GetProfile()
        {
            var id = Globals.UserID;
            var resource = $"/api/users/profile?accountId={id}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.GetAsync(url);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<GetProfilesResponse>(response);
            return getResponse;
        }

        public async static Task<NotificationResponse> GetNotifications()
        {
            var id = Globals.UserID;
            var resource = $"/api/notifications/all";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.GetAsync(url);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<NotificationResponse>(response);
            return getResponse;
        }

        public async static Task<NotificationResponse> CreateNotification(string message, string date)
        {
            var id = Globals.UserID;
            var resource = $"/api/notifications/create?notification={message}&Date={date}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.PostAsync(url, null);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<NotificationResponse>(response);
            return getResponse;


        }

        public async static Task<NotificationTemplateResponse> GetNotificationTemplate()
        {
            var id = Globals.UserID;
            var resource = $"/api/notifications/get/template";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.GetAsync(url);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<NotificationTemplateResponse>(response);
            return getResponse;


        }

        public async static Task<RegisterResponse> DeleteTemplate(int id)
        {
            
            var resource = $"/api/notifications/delete/template?id={id}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.PostAsync(url,null);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<RegisterResponse>(response);
            return getResponse;


        }

        public async static Task<RegisterResponse> AddTemplate(string notification)
        {
            
            var resource = $"/api/notifications/create/template?notification={notification}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.PostAsync(url, null);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<RegisterResponse>(response);
            return getResponse;


        }

        public async static Task<PushNotificationResponse> PushNotification(Notification_Content content)
        {
            var url = "https://api.appcenter.ms/v0.1/apps/quaz456-gmail.com/Feel-App/push/notifications";
            var json = JsonConvert.SerializeObject(content);
          
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-Token", "7181f4c485f31d8fcc3f86430cb1e9e50b493c5e");
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            
            var result = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<PushNotificationResponse>(response);
            return getResponse;


        }

        public async static Task<CoordinateResponse> GetCoord(int id)
        {
            
           
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var resource = $"/api/coord/get?AccountId={id}&Date={date}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.GetAsync(url);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<CoordinateResponse>(response);
            return getResponse;


        }

        public async static Task<CoordinateResponse> CreateCoord(double lon, double lat)
        {
            var id = Settings.SaveID;
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var resource = $"/api/coord/post?AccountId={id}&Long={lon}&Lat={lat}&Date={date}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.PostAsync(url, null);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<CoordinateResponse>(response);
            return getResponse;


        }

        public async static Task<CoordinateResponse> UpdateCoord(double lon, double lat)
        {
            var id = Settings.SaveID;
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var resource = $"/api/coord/update?AccountId={id}&Long={lon}&Lat={lat}&Date={date}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.PostAsync(url, null);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<CoordinateResponse>(response);
            return getResponse;


        }

        public async static Task<RegisterResponse> GoToSafe(int Id)
        {
            var resource = $"/api/help/safe?Id={Id}";
            var url = $"{baseUrl}{resource}";

            var client = new HttpClient();

            var result = await client.PostAsync(url, null);
            string response = await result.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<RegisterResponse>(response);
            return getResponse;


        }
    }
}
