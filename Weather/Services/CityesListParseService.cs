using Newtonsoft.Json;
using Weather.Models.CityesOfRussia;
using Weather.Services.Interfaces;

namespace Weather.Services
{
    public class CityesListParseService : IParseFromJsonFile
    {
        public async Task<IEnumerable<Rootobject>> GetCityFromFile()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "Res", "towns-russia.json");
            string json = await File.ReadAllTextAsync(filePath);

            var cityList = JsonConvert.DeserializeObject<IEnumerable<Rootobject>>(json);
            return cityList;
        }
    }
}
