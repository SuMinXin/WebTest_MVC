

using System;
using System.Collections.Generic;

namespace WeatherWeb.Models
{
    public class Location
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Town> towns { get; set; }
        public Location() {
            towns = new List<Town>();
        }
    }

    public class Town
    {
        public string id { get; set; }
        public string name { get; set; }
        public string postal { get; set; }
    }

    public class Weather
    {
        public string desc { get; set; }
        public int temperature { get; set; }
        public int felt_air_temp { get; set; }
        public int humidity { get; set; }
        public int rainfall { get; set; }
        public DateTime at { get; set; }
        public string time { get { return at.ToString("yyyy-MM-dd HH:mm"); } }
        public List<WeatherSpecial> specials { get; set; }
    }

    public class WeatherSpecial
    {
        public string title { get; set; }
        public string status { get; set; }
        public string desc { get; set; }
        public DateTime at { get; set; }
    }

}