using Microsoft.Extensions.Configuration;
using Weather.Models;
using Weather.Services.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using Weather.ViewModels;
using Weather.Models.OnWeek;

namespace Weather.Services
{
    public class WeatherModelService : IWeatherConnection
    {
        private readonly IConfiguration _config;
        public WeatherModelService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<WeatherModel> GetDataAsync(CityToFind cityToFind)
        {
            using (var client = new HttpClient())
            {
                string startConnectionApiService = _config.GetSection("ConnectionData")["StartString"];
                string key = _config.GetSection("ConnectionData")["ConnectionKey"];
                string lang = _config.GetSection("ConnectionData")["lang"];

                using (var request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri($"{startConnectionApiService}?{nameof(key)}={key}&{lang}&q={cityToFind.City}");
                    request.Method = HttpMethod.Get;

                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var obj = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<WeatherModel>(obj);

                        return result;
                    }
                    return new WeatherModel() { };
                }
            }
        }
        public async Task<WeatherOnWeek> GetDataOnWeekAsync(CityToFind cityToFind)
        {
            using (var client = new HttpClient())
            {
                string startConnectionApiService = _config.GetSection("ConnectionData")["StartString"];
                string key = _config.GetSection("ConnectionData")["ConnectionKey"];
                string lang = _config.GetSection("ConnectionData")["lang"];

                using (var request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri($"{startConnectionApiService}?{nameof(key)}={key}&{lang}&q={cityToFind.City}&days=7");
                    request.Method = HttpMethod.Get;

                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var obj = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<WeatherOnWeek>(obj);

                        return result;
                    }
                    return new WeatherOnWeek() { };
                }
            }
        }
    }
}