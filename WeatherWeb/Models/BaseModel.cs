
using System;
using System.Configuration;

namespace WeatherWeb.Models
{
    public class BaseModel
    {
        public static string GetDateTimeString() {
             return DateTime.Now.ToString("yyyyMMssHHmmss"); 
        }
        public static class APIURL
        {
            public static string Weather
            {
                get { return ConfigurationManager.AppSettings["weatherAPI"]; }
            }
        }
    }
}