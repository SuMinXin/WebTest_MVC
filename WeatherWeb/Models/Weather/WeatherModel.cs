using System.Collections.Generic;
using static WeatherWeb.Models.WeatherApi;

namespace WeatherWeb.Models
{
    public class WeatherModel : BaseModel
    {
        private static WeatherApi weather = new WeatherApi();

        public List<Locations> locationList { get; set; }

        public Locations location { get; set; }

        public Weather currentWeather { get; set; }

        public void getLocation()
        {
            List<Location> locations = weather.Location<List<Location>>(ApiType.City);
            //Locations
            locationList = new List<Locations>();
            locations.ForEach(x => {
                x.towns.ForEach(t => {
                    locationList.Add(new Locations() {
                        id = t.id,
                        name = x.name + ", " + t.name,
                    });
                });
            });
        }

        public void getWeather(string id)
        {
            currentWeather = weather.Weather<Weather>(ApiType.Weather, id);
        }


        public WeatherModel(bool init = true)
        {
            //init
            if (init) {
                getLocation();
            }
        }

    }
}