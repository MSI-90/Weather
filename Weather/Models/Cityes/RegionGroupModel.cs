namespace Weather.Models.Cityes
{
    public class RegionGroupModel
    {
        internal Dictionary<char, List<string>> RegiosByGroupWithCharKey { get; set; } = new Dictionary<char, List<string>>();
        internal Dictionary<int, List<string>> RegionsByGropWithNumberKeys { get; set; } = new Dictionary<int, List<string>>();
        internal Dictionary<string, List<string>> CityesInRegion { get; set; } = new Dictionary<string, List<string>>();
    }
}
