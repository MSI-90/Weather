using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using Weather.Models;
using Weather.Models.search;
using Weather.Services.Interfaces;
using Weather.ViewModels;

namespace Weather.Controllers
{
    //[Route("Weather")]
    public class HomeController : Controller
    {
        private readonly IWeatherConnection _connection;
        public HomeController(IWeatherConnection connection)
        {
            _connection = connection;
        }
        public ViewResult Index()
        {
            return View();
        }
        public ViewResult Search()
        {
            return View();
        }

        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult> Search(CityToFind? cityName)
        {
            if (!ModelState.IsValid || cityName == null)
            {
                return View("Index");
            }
            else
            {
                try
                {
                    IEnumerable<NewItem> model;
                    model = await _connection.GetCitiesAsync(cityName);
                    if (!string.IsNullOrEmpty(_connection.Error))
                    {
                        return BadRequest(_connection.Error);
                    }
                    else
                    {
                        if (model.Any())
                            return View(model);
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Нет данных для отображения");
                            return View("Index");
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [Route("Details")]
        public async Task<IActionResult> Details(string name, string lat, string lon)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Местоположение не найдено");
                return View();
            }

            try
            {
                float latitude = Convert.ToSingle(lat);
                float longitude = Convert.ToSingle(lon);
                var model = await _connection.GetDataAsync(latitude, longitude);
                if (model?.location != null)
                {
                    var viewModel = new WeatherVM()
                    {
                        Name = name,
                        LocalDateAndTime = model.location.localtime,
                        Region = model.location.region,
                        Country = model.location.country,
                        TempC = model.current.temp_c,
                        ImageSrc = model.current.condition.icon
                    };
                    return View(viewModel);
                }
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}