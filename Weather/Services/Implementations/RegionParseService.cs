using Newtonsoft.Json;
using Weather.Models.Cityes;
using Weather.Services.Interfaces;
using Weather.ViewModels;

namespace Weather.Services
{
    public class RegionParseService : ICitiesParseJsonFile
    {
        public async Task<IEnumerable<Root>> GetCityFromFileAsync()
        {
            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, "Res", "towns-russia.json");
                string json = await File.ReadAllTextAsync(filePath);
                var cityList = JsonConvert.DeserializeObject<IEnumerable<Root>>(json) ?? new List<Root>();
                return cityList;
            }
            catch (Exception ex)
            {
                throw new Exception("Данные недоступны для отображения.", ex);
            }
        }
        public async Task<RegionGroupModel> GetCityesGroupAsync(IEnumerable<Root> cityes)
        {
            var list = cityes?.OrderBy(name => name.Label).ToList();
            List<char> firstLettersInList = new List<char>();
            if (list != null)
            {
                var cityGroup = new RegionGroupModel();
                HashSet<char> firstLetters = new HashSet<char>();
                foreach (var item in list)
                {
                    firstLetters.Add((item.Label).First());
                    var cityesList = item.Localities.ToList();
                    cityGroup.CityesInRegion.Add(item.Label, cityesList.Select(c => c.Label).ToList());
                }
                firstLettersInList = firstLetters.ToList();

                for (int r = 0; r < firstLettersInList.Count; r++)
                {
                    var regions = new List<string>();
                    foreach (var item in list)
                    {
                        if (item.Label.StartsWith(firstLettersInList[r]))
                        {
                            switch (item.Type)
                            {
                                case "obl":
                                    regions.Add(item.Label + " область" + " ");
                                    break;
                                case "republic":
                                    regions.Add("Республика " + item.Label + " ");
                                    break;
                                case "aobl":
                                    regions.Add(item.Label + " АО" + " ");
                                    break;
                                case "kray":
                                    regions.Add(item.Label + " край" + " ");
                                    break;
                                default:
                                    regions.Add(item.Label + " ");
                                    break;
                            }
                        }
                    }
                    cityGroup.RegiosByGroupWithCharKey.Add(firstLettersInList[r], regions);
                    cityGroup.RegionsByGropWithNumberKeys.Add(r, regions);
                }
                return cityGroup;
            }
            else
            {
                return new RegionGroupModel();
            }
        }
        public async Task<CityesInRegion> GetCityesInRegionAsync(string region)
        {
            if (!string.IsNullOrEmpty(region))
            {
                var cityesFromRegion = new CityesInRegion();
                try
                {
                    var dataList = await GetCityFromFileAsync();
                    var regWithKeys = await GetCityesGroupAsync(dataList);
                    var regions = regWithKeys.CityesInRegion;
                    foreach (var item in regions)
                    {
                        if (region.Contains(item.Key))
                        {
                            cityesFromRegion.City = item.Value;
                        }
                    }
                }
                catch { throw; }
                return cityesFromRegion;
            }
            else 
                return new CityesInRegion();
        }
    }
}
