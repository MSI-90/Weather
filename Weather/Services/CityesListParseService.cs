using Newtonsoft.Json;
using Weather.Models.Cityes;
using Weather.Services.Interfaces;

namespace Weather.Services
{
    public class CityesListParseService : IParseFromJsonFile
    {
        public async Task<IEnumerable<Root>> GetCityFromFile()
        {
            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, "Res", "towns-russia.json");
                string json = await File.ReadAllTextAsync(filePath);
                var cityList = JsonConvert.DeserializeObject<IEnumerable<Root>>(json);
                return cityList;
            }
            catch (Exception fileExcepton)
            {
                throw new Exception(); //ToDo решить этот момент лучше
            }
        }
    }
}
