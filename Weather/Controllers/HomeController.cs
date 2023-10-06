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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(WeatherVM model)
        {
            
            return View(model);
        }

        [HttpPost]
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

                if (model.Any())
                {
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Нет данных для отображения");
                    return View("Index");
                }
                        
            }
            catch (Exception ex)
            {
                throw;
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

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}