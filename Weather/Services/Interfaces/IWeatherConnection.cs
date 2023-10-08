using Weather.Models.search;
using Weather.Models.current;
using Weather.Models.OnWeek;
using Weather.ViewModels;

namespace Weather.Services.Interfaces
{
    public interface IWeatherConnection
    {
        public string Error { get; set; }
        Task<IEnumerable<NewItem>> GetCitiesAsync(CityToFind cityToFind);
        Task<WeatherModel> GetDataAsync(CityToFind cityToFind);
        Task<WeatherOnWeek> GetDataOnWeekAsync(CityToFind cityToFind);
    }
}
