using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System.Diagnostics;
using Weather.Models.Cityes;
using Weather.Models.search;
using Weather.Services;
using Weather.Services.Implementations;
using Weather.Services.Interfaces;
using Weather.ViewModels;

namespace Weather.Controllers
{
    [DefaultBreadcrumb]
    public class HomeController : Controller
    {
        private readonly ReadCityesFromFile _russianCityes;
        private readonly ICookieTools _cookieTools;
        private readonly IWeatherConnection _connection;
        private readonly ICitiesParseJsonFile _parseFromJsonFile;
        public HomeController(IWeatherConnection connection, ICitiesParseJsonFile parseFromJsonFile, 
            ReadCityesFromFile russianCityes, ICookieTools cookieTools)
        {
            _connection = connection;
            _parseFromJsonFile = parseFromJsonFile;
            _russianCityes = russianCityes;
            _cookieTools = cookieTools;
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
            if (!ModelState.IsValid || cityName?.City == null)
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
                    {
                        return View(model);
                    }
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
                var city = new CityToFind();
                city.City = name;

                var confirmCity = await _connection.GetCitiesAsync(city);
                if (confirmCity.Count() > 0)
                {
                    foreach (var item in confirmCity)
                    {
                        if (item.Name == name)
                        {
                            if (Single.TryParse(lat, out float latitude) && Single.TryParse(lon, out float longitude))
                            {
                                var model = await _connection.GetDataAsync(latitude, longitude);

                                if (model?.Location != null)
                                {
                                    //set cookie
                                    _cookieTools.SetOnce(name);

                                    var weatherService = new WeaterVMService(model);
                                    var viewModel = weatherService.GetMyCurrentWeatherAsync(name);
                                    return View(viewModel);

                                }
                            }
                            return View();
                        }
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
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
                if (model.CityesListWithFirstLetter.Count > 0 && model.CityesListWithNumberKey.Count > 0)
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
        public async Task<IActionResult> OnDetailsFromRegion (string cityName)
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
                    var myVariant = modelCityList/*.Where(model => model.Country == "Россия")*/;
                    if (myVariant.Any())
                        return View("Search", myVariant);
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Нет данных для отображения");
                        return View("Search");
                    }
                }
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Index"); }
        }
        async Task<CityesByRegionsModel> GetDataForIndex()
        {
            var data = new CityesByRegionsModel();

            IEnumerable<Root> cityesFromJson = new List<Root>();
            cityesFromJson = data.CityesFromJson = _russianCityes.GetCityes();

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