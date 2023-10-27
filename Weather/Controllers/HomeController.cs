using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using Weather.Models;
using Weather.Models.Cityes;
using Weather.Models.search;
using Weather.Services.Interfaces;
using Weather.ViewModels;

namespace Weather.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherConnection _connection;
        private readonly ICitiesParseJsonFile _parseFromJsonFile;
        public HomeController(IWeatherConnection connection, ICitiesParseJsonFile parseFromJsonFile)
        {
            _connection = connection;
            _parseFromJsonFile = parseFromJsonFile;
        }
        public async Task<ViewResult> Index()
        {
            var model = GetCityFromFile().Result;
            return View(model);
        }

        [HttpGet]
        [Route("search")]
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
                        return new ErrorController().Error(_connection.Error);
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
                catch (SocketException)
                {
                    return new ErrorController().Error("Ответ от удалённого сервера занимает слишком много времени, попробуйте повторить запрос.");
                }
                catch (Exception ex)
                {
                    return new ErrorController().Error(ex.Message);
                }
            }
        }

        [Route("details")]
        public async Task<IActionResult> Details(string name, string lat, string lon)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Местоположение не найдено");
                return View();
            }

            try
            {
                if ((Single.TryParse(lat, out float latitude)) && (Single.TryParse(lon, out float longitude)))
                {
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
                }
                return View();
            }
            catch (Exception ex)
            {
                return new ErrorController().Error(ex.Message);
            }
        }
        public async Task<ViewResult> CityesReg(string region)
        {
            ViewData["Region"] = region;
            var model = await _parseFromJsonFile.GetCityesInRegionAsync(region);

            return View(model);
        }
        public async Task<IEnumerable<Root>> GetCityFromFile()
        {
            IEnumerable<Root> data = await _parseFromJsonFile.GetCityFromFileAsync();
            return data;
        }
    }
}