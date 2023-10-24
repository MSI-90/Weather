using Newtonsoft.Json;
using System.Reflection;
using System.Linq;
using Weather.Models.Cityes;
using Weather.Services.Interfaces;

namespace Weather.Services
{
    public class CityesParseService : ICitiesParseJsonFile
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
        public async Task<CityGroupModel> GetCityesGroupAsync(IEnumerable<Root> cityes)
        {
            var list = cityes?.OrderBy(name => name.label).ToList();
            List<char> firstLettersInList = null;
            if (list != null)
            {
                var cityGroup = new CityGroupModel();
                HashSet<char> firstLetters = new HashSet<char>();
                foreach (var item in list)
                {
                    firstLetters.Add((item.label).First());
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
                return new CityGroupModel();
            }
        }
    }
}
