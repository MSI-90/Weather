using Microsoft.AspNetCore.Mvc.TagHelpers;
using Newtonsoft.Json;
using Weather.Models.Cityes;

namespace Weather.Services
{
    public class ReadCityesFromFile
    {
        public IEnumerable<Root> RussianCityes { get; set; }
        private readonly IConfiguration _configuration;
        public ReadCityesFromFile(IConfiguration configuration)
        {
            _configuration = configuration;
            RussianCityes = GetCityFromFileAsync().GetAwaiter().GetResult();
        }
        public async Task<IEnumerable<Root>> GetCityFromFileAsync()
        {
            try
            {   
                string filePath = Path.Combine(Environment.CurrentDirectory + "/" + _configuration.GetSection("Resources")["Folder"] + "/" + _configuration.GetSection("Resources")["File"]);
                if (File.Exists(filePath))
                {
                    string json = await File.ReadAllTextAsync(filePath);
                    this.RussianCityes = JsonConvert.DeserializeObject<IEnumerable<Root>>(json) ?? new List<Root>();
                    return RussianCityes;
                }
                else
                    return Enumerable.Empty<Root>();
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Файл не найден, попробуйте придти позже.");
            }
            catch (NullReferenceException)
            {
                return Enumerable.Empty<Root>();
                throw new Exception("Что - то пошло не так");
            }
            catch (Exception ex)
            {
                throw new Exception("Данные недоступны для отображения.", ex);
            }
        }
        public IEnumerable<Root> GetCityes() => RussianCityes;
    }
}
