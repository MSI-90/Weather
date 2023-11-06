using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
            try
            {
                var model = await GetDataForIndex();
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(CityToFind cityName)
        {
            
            if (!ModelState.IsValid || cityName == null)
            {
                return View("Index");
            }
            else
            {
                try
                {
                    IEnumerable<NewItem> model = new List<NewItem>();
                    model = await _connection.GetCitiesAsync(cityName);

                    if (model.Any())
                        return View(model);
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Нет данных для отображения");
                        return View();
                    }
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Возникла ошибка, повторите попытку позднее");
                    return View();
                }
            }
        }

        [Route("details")]
        public async Task<IActionResult> Details(string name, string lat, string lon)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Remove("name");
                ModelState.Remove("lat");
                ModelState.Remove("lon");
                ModelState.AddModelError(string.Empty, "Местоположение не найдено");
                return View();
            }

            try
            {
                if ((Single.TryParse(lat, out float latitude)) && (Single.TryParse(lon, out float longitude)))
                {
                    var model = await _connection.GetDataAsync(latitude, longitude);
                    if (model?.Location != null)
                    {
                        var viewModel = new WeatherVM()
                        {
                            Name = name,
                            LocalDateAndTime = model.Location.Localtime,
                            Region = model.Location.Region,
                            Country = model.Location.Country,
                            TempC = model.Current.Temp_c,
                            ImageSrc = model.Current.Condition.Icon
                        };
                        return View(viewModel);
                    }
                }
                return View();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Возникла ошибка, повторите попытку позднее");
                return View();
            }
        }
        public async Task<IActionResult> CityesReg(string region)
        {
            try
            {
                var model = await _parseFromJsonFile.GetCityesInRegionAsync(region);
                if (model != null)
                {
                    int count = 0;
                    if (model.City.Count > 0)
                        count = model.City.Count;

                    ViewBag.Count = count;
                    ViewData["Region"] = region;
                    return View(model);
                }
                return View();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Возникла ошибка, повторите попытку позднее");
                return View();
            }
            
        }
        public async Task<CityesByRegionsModel> GetDataForIndex()
        {
            var data = new CityesByRegionsModel();
            var cityesFromJson = data.CityesFromJson = await _parseFromJsonFile.GetCityFromFileAsync();
            data.RegionGroup = _parseFromJsonFile.GetCityesGroup(cityesFromJson);
            return data;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            #region
            //for logging
            //var exceptionData = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //string path = exceptionData?.Path ?? "";
            //string errorMessage = exceptionData?.Error?.ToString();
            #endregion
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}