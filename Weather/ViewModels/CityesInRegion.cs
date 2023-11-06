namespace Weather.ViewModels
{
    public class CityesInRegion
    {
        internal List<string> City { get; set; } = new List<string>();
        internal Dictionary<char, List<string>> CityesListWithFirstLetter { get; set; } = new Dictionary<char, List<string>>();
    }
}
