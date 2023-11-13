using Newtonsoft.Json;
using Weather.Models.Cityes;

namespace Weather.Services
{
    public class ReadCityesFromFile
    {
        public IEnumerable<Root> RussianCityes { get; set; }
        public ReadCityesFromFile()
        {
            RussianCityes = GetCityFromFileAsync().Result;
        }
        public async Task<IEnumerable<Root>> GetCityFromFileAsync()
        {
            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, "Res", "towns-russia.json");
                if (File.Exists(filePath))
                {
                    string json = await File.ReadAllTextAsync(filePath);
                    this.RussianCityes = JsonConvert.DeserializeObject<IEnumerable<Root>>(json) ?? new List<Root>();
                    return RussianCityes;
                }
                else
                {
                    throw new FileNotFoundException("Файл не найден, попробуйте придти позже.");
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Данные недоступны для отображения.", ex);
            }
        }
        public IEnumerable<Root> GetCityes() => RussianCityes;
    }
}
