using Weather.Models.Cityes;

namespace Weather.Services.Interfaces
{
    public interface ICitiesParseJsonFile
    {
        Task<IEnumerable<Root>> GetCityFromFileAsync();
        Task<CityGroupModel> GetCityesGroupAsync(IEnumerable<Root> cityes);
    }
}
