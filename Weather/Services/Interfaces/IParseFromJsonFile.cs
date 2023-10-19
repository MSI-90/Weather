using Weather.Models.CityesOfRussia;

namespace Weather.Services.Interfaces
{
    public interface IParseFromJsonFile
    {
        Task<IEnumerable<Rootobject>> GetCityFromFile();
    }
}
