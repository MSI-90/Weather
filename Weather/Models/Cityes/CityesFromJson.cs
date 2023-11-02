namespace Weather.Models.Cityes
{
    public class District
    {
        public string Label { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }

    public class Locality
    {
        public string Type { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public List<District> Districts { get; set; } = new List<District>();
    }

    public class Root
    {
        public string Type { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public List<Locality> Localities { get; set; } = new List<Locality>();
    }
}
