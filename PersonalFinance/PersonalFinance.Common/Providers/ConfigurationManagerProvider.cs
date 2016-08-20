using System;
using System.Configuration;
using PersonalFinance.Common.IocAttributes;

namespace PersonalFinance.Common.Providers
{
    public interface IConfigurationManagerProvider
    {
        bool IsDebugMode();
        string GetConfigValue(string key);
        bool GetConfigBoolValue(string key);

        string GetConnectionString(string name = null);
    }
    
    [Singleton]
    public class ConfigurationManagerProvider : IConfigurationManagerProvider
    {
        private readonly string _defaultConnectionStringName;

        public ConfigurationManagerProvider(string defaultConnectionStringName)
        {
            
            _defaultConnectionStringName = defaultConnectionStringName;
        }

        public bool IsDebugMode()
        {
            return GetConfigBoolValue("DebugMode");
        }

        public string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public bool GetConfigBoolValue(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (value == null)
                return false;
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// Get Connection string from web.config
        /// </summary>
        /// <param name="name">Optionally pass name as parameter if null then first connectionstring is obtained, if name is not specified then default is used.</param>
        /// <returns></returns>
        public string GetConnectionString(string name = null)
        {
            return string.IsNullOrEmpty(name)
                ? ConfigurationManager.ConnectionStrings[_defaultConnectionStringName].ConnectionString
                : ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}