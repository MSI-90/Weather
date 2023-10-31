using Weather.Models.Cityes;

namespace Weather.ViewModels
{
    public class CityesByRegionsModel
    {
        internal IEnumerable<Root> CityesFromJson { get; set; } = new List<Root>();
        internal RegionGroupModel RegionGroup {  get; set; } = new RegionGroupModel();
    }
}
