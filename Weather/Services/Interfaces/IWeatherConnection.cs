using Weather.Models;
using Weather.Models.OnWeek;
using Weather.ViewModels;

namespace Weather.Services.Interfaces
{
    public interface IWeatherConnection
    {
        Task<WeatherModel> GetDataAsync(CityToFind cityToFind);
        Task<WeatherOnWeek> GetDataOnWeekAsync(CityToFind cityToFind);
    }
}
