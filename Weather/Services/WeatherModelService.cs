using Microsoft.Extensions.Configuration;
using Weather.Services.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using Weather.ViewModels;
using Weather.Models.OnWeek;
using Weather.Models.current;
using Weather.Models.search;

namespace Weather.Services
{
    public class WeatherModelService : IWeatherConnection
    {
        private readonly IConfiguration _config;
        public WeatherModelService(IConfiguration config)
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

                    var response = await client.SendAsync(request);
                    if(response.IsSuccessStatusCode)
                    {
                        var obj = await response.Content.ReadAsStringAsync();
                        while(obj != "[]")
                        {
                            var result = JsonConvert.DeserializeObject<IEnumerable<NewItem>>(obj);
                            return result ?? Enumerable.Empty<NewItem>();
                        }
                    }
                    return new NewItem[default];
                }
            }
        }
        public async Task<WeatherModel> GetDataAsync(CityToFind cityToFind)
        {
            using (var client = new HttpClient())
            {
                string currentString = _config.GetSection("ConnectionData")["CurrentString"];
                string key = _config.GetSection("ConnectionData")["ConnectionKey"];
                string lang = _config.GetSection("ConnectionData")["lang"];

                using (var request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri($"{currentString}?{nameof(key)}={key}&{nameof(lang)}={lang}&q={cityToFind.City}");
                    request.Method = HttpMethod.Get;

                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var obj = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<WeatherModel>(obj);

                        return result;
                    }
                    return new WeatherModel();
                }
            }
        }
        public async Task<WeatherOnWeek> GetDataOnWeekAsync(CityToFind cityToFind)
        {
            using (var client = new HttpClient())
            {
                string forecastString = _config.GetSection("ConnectionData")["ForecastString"];
                string key = _config.GetSection("ConnectionData")["ConnectionKey"];
                string lang = _config.GetSection("ConnectionData")["lang"];

                using (var request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri($"{forecastString}?{nameof(key)}={key}&q={cityToFind.City}&days=7&{nameof(lang)}={lang}");
                    request.Method = HttpMethod.Get;

                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var obj = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<WeatherOnWeek>(obj);

                        return result;
                    }
                    return new WeatherOnWeek();
                }
            }
        }
    }
}