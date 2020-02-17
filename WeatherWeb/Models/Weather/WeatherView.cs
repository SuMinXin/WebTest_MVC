using System.ComponentModel.DataAnnotations;

namespace WeatherWeb.Models
{
    public class Locations
    {
        public string id { get; set; }

        [Display(Name = "Location")]
        public string name { get; set; }
    }
}