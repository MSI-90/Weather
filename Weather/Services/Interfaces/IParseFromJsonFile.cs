using Weather.Models.Cityes;

namespace Weather.Services.Interfaces
{
    public interface IParseFromJsonFile
    {
        Task<IEnumerable<Root>> GetCityFromFile();
    }
}
