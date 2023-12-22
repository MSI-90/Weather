using Weather.Models.search;
using Weather.Models.current;
using Weather.Models.Search;
using Weather.Models.Cityes;
using Weather.Models.OnWeek;

namespace Weather.Services.Interfaces
{
    public interface IWeatherConnection
    {
        Task<IEnumerable<NewItem>> GetCitiesAsync(CityToFind cityToFind);
        Task<WeatherModel> GetDataAsync(float lat, float lon);
        Task<WeatherOnWeek> GetDataOnPeriodAsync(CityToFind cityToFind, int numberOfDays);
    }
}
