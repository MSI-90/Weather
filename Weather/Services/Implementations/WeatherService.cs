using Weather.Services.Interfaces;
using Newtonsoft.Json;
using Weather.ViewModels;
using Weather.Models.OnWeek;
using Weather.Models.current;
using Weather.Models.search;

namespace Weather.Services
{
    public class WeatherService : IWeatherConnection
    {
        private readonly IConfiguration _config;
        public WeatherService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<NewItem>> GetCitiesAsync(CityToFind cityToFind)
        {
            using (var client = new HttpClient())
            {
                string searchString = _config.GetSection("ConnectionData")["SearchString"];
                string key = _config.GetSection("ConnectionData")["ConnectionKey"];
                string lang = _config.GetSection("ConnectionData")["lang"];

                using (var request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri($"{searchString}?{nameof(key)}={key}&{nameof(lang)}={lang}&q={cityToFind.City}");
                    request.Method = HttpMethod.Get;
                    try
                    {
                        var response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            var obj = await response.Content.ReadAsStringAsync();
                            while (obj != "[]")
                            {
                                var result = JsonConvert.DeserializeObject<IEnumerable<NewItem>>(obj);
                                return result ?? Enumerable.Empty<NewItem>();
                            }
                        }
                    }
                    catch {throw;}
                    return new List<NewItem>();
                }
            }
        }
        public async Task<WeatherModel> GetDataAsync(float lat, float lon)
        {
            using (var client = new HttpClient())
            {
                string currentString = _config.GetSection("ConnectionData")["CurrentString"];
                string key = _config.GetSection("ConnectionData")["ConnectionKey"];
                string lang = _config.GetSection("ConnectionData")["lang"];

                string latitude = Convert.ToString(lat);
                latitude = latitude.Replace(',', '.');
                string longitude = Convert.ToString(lon);
                longitude = longitude.Replace(",", ".");

                using (var request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri($"{currentString}?{nameof(key)}={key}&{nameof(lang)}={lang}&q={latitude},{longitude}");
                    request.Method = HttpMethod.Get;
                    try
                    {
                        HttpResponseMessage response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            var obj = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<WeatherModel>(obj);
                            return result ?? new WeatherModel();
                        }
                    }
                    catch {throw;}
                    return new WeatherModel();
                }
            }
        }
        public async Task<WeatherOnWeek> GetDataOnPeriodAsync(CityToFind cityToFind, int numberOfDays)
        {
            using (var client = new HttpClient())
            {
                string forecastString = _config.GetSection("ConnectionData")["ForecastString"];
                string key = _config.GetSection("ConnectionData")["ConnectionKey"];
                string lang = _config.GetSection("ConnectionData")["lang"];

                using (var request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri($"{forecastString}?{nameof(key)}={key}&q={cityToFind.City}&days={numberOfDays}&{nameof(lang)}={lang}");
                    request.Method = HttpMethod.Get;
                    try
                    {
                        HttpResponseMessage response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            var obj = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<WeatherOnWeek>(obj);

                            return new WeatherOnWeek();
                        }
                    }
                    catch {throw;}
                    return new WeatherOnWeek();
                }
            }
        }
    }
}