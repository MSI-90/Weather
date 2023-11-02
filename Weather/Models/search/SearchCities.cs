using System.ComponentModel.DataAnnotations;

namespace Weather.Models.search
{

    public class SearchCities
    {
        public NewItem[] NewItem { get; set; }
    }

    public class NewItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public float Lat { get; set; }
        public float Lon { get; set; }
        public string Url { get; set; } = string.Empty;
    }

}
