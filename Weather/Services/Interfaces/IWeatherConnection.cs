using Weather.Models.search;
using Weather.Models.current;
using Weather.Models.OnWeek;
using Weather.ViewModels;

namespace Weather.Services.Interfaces
{
    public interface IWeatherConnection
    {
        Task<IEnumerable<NewItem>> GetCitiesAsync(CityToFind cityToFind);
        Task<WeatherModel> GetDataAsync(float lat, float lon);
        Task<WeatherOnWeek> GetDataOnWeekAsync(CityToFind cityToFind);
    }
}
