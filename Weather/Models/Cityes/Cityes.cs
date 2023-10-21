namespace Weather.Models.Cityes
{
    public class District
    {
        public string label { get; set; }
        public string slug { get; set; }
    }

    public class Locality
    {
        public string type { get; set; }
        public string slug { get; set; }
        public string label { get; set; }
        public List<District> districts { get; set; }
    }

    public class Root
    {
        public string type { get; set; }
        public string slug { get; set; }
        public string label { get; set; }
        public List<Locality> localities { get; set; }
    }
}
