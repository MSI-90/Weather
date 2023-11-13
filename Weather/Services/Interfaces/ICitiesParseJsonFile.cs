using Weather.Models.Cityes;
using Weather.ViewModels;

namespace Weather.Services.Interfaces
{
    public interface ICitiesParseJsonFile
    {
        RegionGroupModel GetCityesGroup(IEnumerable<Root> cityes);
        Task<CityesInRegion> GetCityesInRegionAsync(string region);
    }
}
