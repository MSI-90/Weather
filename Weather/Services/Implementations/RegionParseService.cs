﻿using Newtonsoft.Json;
using NuGet.DependencyResolver;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public RegionGroupModel GetCityesGroup(IEnumerable<Root> cityes)
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
                    var regWithKeys = GetCityesGroup(dataList);
                    var cityesInRegion = regWithKeys.CityesInRegion;
                    var firstLetter = new HashSet<char>();
                    string[] regionArr = region.Split();
                    foreach (var item in cityesInRegion)
                    {
                        for (int i = 0; i < regionArr.Length; i++)
                        {
                            if (regionArr[i] == item.Key)
                            {
                                cityesFromRegion.City = item.Value;
                                string[] cityArr = new string[cityesFromRegion.City.Count()];

                                for (int z = 0; z < item.Value.Count(); z++)
                                {
                                    cityArr[z] = cityesFromRegion.City[z];
                                }

                                foreach (var city in cityesFromRegion.City)
                                {
                                    firstLetter.Add(city.First());
                                }

                                var firstLetterList = firstLetter.ToList();

                                foreach (var letter in firstLetterList)
                                {
                                    var list = new List<string>();
                                    foreach (var city in cityArr)
                                    if (city.StartsWith(letter))
                                    {
                                        list.Add(city);
                                    }

                                    if (list.Count > 0)
                                    {
                                        cityesFromRegion.CityesListWithFirstLetter.Add(letter, list);
                                    }
                                }

                                #region
                                //var key = firstLetterList[j];
                                //if (cityArr[j].StartsWith(key) && cityesFromRegion.CityesListWithFirstLetter.ContainsKey(key))
                                //{
                                //    cityesFromRegion.CityesListWithFirstLetter[key].Add(cityArr[j]);
                                //}
                                //else
                                //{
                                //    if (cityArr[j].StartsWith(key) && !cityesFromRegion.CityesListWithFirstLetter.ContainsKey(key))
                                //    {
                                //        List<string> list = new List<string>();
                                //        list.Add(cityArr[j]);
                                //        cityesFromRegion.CityesListWithFirstLetter.Add(key, list);
                                //    }
                                //}

                                //for (int j = 0; j < cityesFromRegion.City.Count(); j++)
                                //{
                                //    var key = firstLetter.ToList();

                                //    if (cityesFromRegion.CityesListWithFirstLetter.ContainsKey(key[j]))
                                //    {
                                //        cityesFromRegion.CityesListWithFirstLetter[key[j]] = item.Value;
                                //    }
                                //    else
                                //    {
                                //        foreach (var city in item.Value)
                                //        {
                                //            if (cityesFromRegion.CityesListWithFirstLetter.ContainsKey(key[j]))
                                //                cityesFromRegion.CityesListWithFirstLetter[key[j]] = item.Value;
                                //            else
                                //                cityesFromRegion.CityesListWithFirstLetter[key[j]] = city;
                                //        }

                                //    }
                                //}
                                #endregion
                            }
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
