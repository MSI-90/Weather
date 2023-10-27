using Newtonsoft.Json;
using System.Reflection;
using System.Linq;
using Weather.Models.Cityes;
using Weather.Services.Interfaces;
using System.Collections.Generic;
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
                var cityList = JsonConvert.DeserializeObject<IEnumerable<Root>>(json);
                return cityList;
            }
            catch (Exception fileExcepton)
            {
                throw new Exception(); //ToDo решить этот момент лучше
            }
        }
        public async Task<RegionGroupModel> GetCityesGroupAsync(IEnumerable<Root> cityes)
        {
            var list = cityes?.OrderBy(name => name.label).ToList();
            List<char> firstLettersInList = null;
            if (list != null)
            {
                var cityGroup = new RegionGroupModel();
                HashSet<char> firstLetters = new HashSet<char>();
                foreach (var item in list)
                {
                    firstLetters.Add((item.label).First());
                    var cityesList = item.localities.ToList();
                    cityGroup.CityesInRegion.Add(item.label, cityesList.Select(c => c.label).ToList());
                }
                firstLettersInList = firstLetters.ToList();

                for (int r = 0; r < firstLettersInList.Count; r++)
                {
                    var regions = new List<string>();
                    foreach (var item in list)
                    {
                        if (item.label.StartsWith(firstLettersInList[r]))
                        {
                            switch (item.type)
                            {
                                case "obl":
                                    regions.Add(item.label + " область" + " ");
                                    break;
                                case "republic":
                                    regions.Add("Республика " + item.label + " ");
                                    break;
                                case "aobl":
                                    regions.Add(item.label + " АО" + " ");
                                    break;
                                case "kray":
                                    regions.Add(item.label + " край" + " ");
                                    break;
                                default:
                                    regions.Add(item.label + " ");
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
            var dataList = GetCityFromFileAsync().Result;
            var regWithKeys = GetCityesGroupAsync(dataList).Result;
            var regions = regWithKeys.CityesInRegion;

            var cityesFromRegion = new CityesInRegion();

            foreach (var item in regions)
            {
                if (region.Contains(item.Key))
                {
                    cityesFromRegion.City = item.Value;
                }
            }

            return cityesFromRegion;

        }
    }
}
