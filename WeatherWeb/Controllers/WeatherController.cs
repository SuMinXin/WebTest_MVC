using System.Linq;
using System.Web.Mvc;
using WeatherWeb.Models;

namespace WeatherWeb.Controllers
{
    public class WeatherController : Controller
    {
        [HttpGet]
        public ActionResult CheckWeather()
        {
            WeatherModel model = new WeatherModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxLocation(string term = "")
        {
            WeatherModel model = new WeatherModel();
            if (model.locationList != null && model.locationList.Any())
            {
                // return Json(model.locationList.Where(c => c.name.Contains(term)).Select(a => new { label = a.name, id = a.id }), JsonRequestBehavior.AllowGet);
                return Json(model.locationList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("");
            }
        }

        [HttpGet]
        public ActionResult AjaxWeather(string id)
        {
            WeatherModel model = new WeatherModel(false);
            model.getWeather(id);
            if (model.currentWeather != null )
            {
                return Json(model.currentWeather, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("Fail");
            }
        }
    }
}