using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection;
using WeatherWeb.Models.Utility;

namespace WeatherWeb.Models
{
    public class WeatherApi : BaseModel
    {
        public enum ApiType  {
            [Description("/all.json")]
            City,
            [Description("/weathers/{0}.json")]
            Weather
        }

        public T Location<T>(ApiType type) {
           string response = WebRequestHelper.GetApi(APIURL.Weather + GetDescription(type));
           return JsonConvert.DeserializeObject<T>(response);
        }

        public T Weather<T>(ApiType type, string id)
        {
            string response = WebRequestHelper.GetApi(APIURL.Weather + string.Format(GetDescription(type), id));
            return JsonConvert.DeserializeObject<T>(response);
        }

        private static string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}