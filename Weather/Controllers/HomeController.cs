using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Weather.Models;
using Weather.Models.search;
using Weather.Services.Interfaces;
using Weather.ViewModels;

namespace Weather.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherConnection _connection;
        public HomeController(IWeatherConnection connection)
        {
            _connection = connection;
        }

        public IActionResult Index(SearchCities model)
        {
            return View(model);
        }
        public IActionResult Details(WeatherVM model)
        {
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Search(CityToFind cityName)
        {
            if (ModelState.IsValid)
            {
                var model = _connection.GetCitiesAsync(cityName).Result;

                if (model.Count() > 0)
                {
                    //var viewModel = new SearchCities
                    //{
                    //    Newitem = model
                    //};
                    return RedirectToAction("Index", model);
                }
                //if (model?.location != null)
                //{
                //    var viewModel = new WeatherVM()
                //    {
                //        Name = model.location.name,
                //        Region = model.location.region,
                //        Country = model.location.country,
                //        TempC = model.current.temp_c,
                //        ImageSrc = model.current.condition.icon
                //    };
                //    return RedirectToAction("Details", viewModel);
                //}
                //else
                //    ModelState.AddModelError(String.Empty, "Местоположение не найдено");

            }

            return View("Index");
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        
    }
}