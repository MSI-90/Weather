using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult> Search(CityToFind cityName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<NewItem> model;
            try
            {
                model = await _connection.GetCitiesAsync(cityName);
                if (!string.IsNullOrEmpty(_connection.Error))
                {
                    return BadRequest(_connection.Error);   
                }
                else
                {
                    if (model.Any())
                    {
                        CityNameAfterSearchInRussian.Name = cityName.City;
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError("notData", "Нет данных для отображения");
                        return View("Index");
                    }
                }         
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Details(string lat, string lon)
        {
            try
            {
                float latitude = Convert.ToSingle(lat);
                float longitude = Convert.ToSingle(lon);
                var model = await _connection.GetDataAsync(latitude, longitude);
                if (model?.location != null)
                {
                    var viewModel = new WeatherVM()
                    {
                        Name = CityNameAfterSearchInRussian.Name ?? model.location.name,
                        LocalDateAndTime = model.location.localtime,
                        Region = model.location.region,
                        Country = model.location.country,
                        TempC = model.current.temp_c,
                        ImageSrc = model.current.condition.icon
                    };
                    return View(viewModel);
                }
                else
                {
                    ModelState.AddModelError("notLocation", "Местоположение не найдено");
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}