using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System.Diagnostics;
using Weather.Models.search;
using Weather.Services.Interfaces;
using Weather.ViewModels;

namespace Weather.Controllers
{
    [DefaultBreadcrumb]
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
        [Breadcrumb("ViewData.Title")]
        public async Task<IActionResult> Search(CityToFind cityName)
        {
            if (!ModelState.IsValid || cityName.City == null)
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
        [Breadcrumb("ViewData.Title")]
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
                if (Single.TryParse(lat, out float latitude) && Single.TryParse(lon, out float longitude))
                {
                    var model = await _connection.GetDataAsync(latitude, longitude);
                    if (model?.Location != null)
                    {
                        float temperature = 0f;
                        _= model.Current.Temp_c == -0f ? temperature = 0f : temperature = model.Current.Temp_c;

                        var viewModel = new WeatherVM()
                        {
                            Name = name,
                            LocalDateAndTime = model.Location.Localtime,
                            Region = model.Location.Region,
                            Country = model.Location.Country,
                            TempC = temperature,
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

        [Breadcrumb("ViewData.Region")]
        public async Task<IActionResult> CityesReg(string region)
        {
            try
            {
                var model = await _parseFromJsonFile.GetCityesInRegionAsync(region);
                if (model != null)
                {
                    if (model.City.Count > 0)
                        ViewBag.Count = model.City.Count;

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

        [Breadcrumb("ViewData.Title")]
        public async Task<IActionResult> OnDetailsFromRegion(string cityName)
        {
            if (!ModelState.IsValid || cityName == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                IEnumerable<NewItem> modelCityList = new List<NewItem>();
                var city = new CityToFind { City = cityName };
                modelCityList = await _connection.GetCitiesAsync(city);
                if (modelCityList.Any())
                {
                    var myVariant = modelCityList.Where(model => model.Country == "Россия");
                    return View("Search", myVariant);
                }
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Index"); }
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