using Weather.Models.Cityes;
using Weather.ViewModels;

namespace Weather.Services.Interfaces
{
    public interface ICitiesParseJsonFile
    {
        Task<IEnumerable<Root>> GetCityFromFileAsync();
        RegionGroupModel GetCityesGroup(IEnumerable<Root> cityes);
        Task<CityesInRegion> GetCityesInRegionAsync(string region);
    }
}
