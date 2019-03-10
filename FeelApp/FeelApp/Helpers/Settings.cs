using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeelApp.Helpers
{
    public static class Settings
    {
        #region Setting Constants

        private const string emailKey = "email_key";
        private const string passwordKey = "password_key";
        private const string userType = "userType_key";
        private const string idKey = "id_key";

        private static readonly string SettingsDefault = string.Empty;
        private static readonly int intUserType = 0;
        private static readonly int intId = 0;

        #endregion

        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }


        public static string SaveEmail
        {
            get
            {
                return AppSettings.GetValueOrDefault(emailKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(emailKey, value);
            }
        }

        public static string SavePassword
        {
            get
            {
                return AppSettings.GetValueOrDefault(passwordKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(passwordKey, value);
            }
        }

        public static int SaveUserType
        {
            get
            {
                return AppSettings.GetValueOrDefault(userType, intUserType);
            }
            set
            {
                AppSettings.AddOrUpdateValue(userType, value);
            }
        }

        public static int SaveID
        {
            get
            {
                return AppSettings.GetValueOrDefault(idKey, intId);
            }
            set
            {
                AppSettings.AddOrUpdateValue(idKey, value);
            }
        }
    }
}
