using Weather.Models;
using Weather.ViewModels;

namespace Weather.Services.Interfaces
{
    public interface IWeatherConnection
    {
        Task<WeatherModel> GetDataAsync(CityToFind cityToFind);
    }
}
