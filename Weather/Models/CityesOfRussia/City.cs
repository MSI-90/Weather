namespace Weather.Models.CityesOfRussia
{

    public class Rootobject
    {
        public Item[] Newitem { get; set; }
    }

    public class Item
    {
        public string type { get; set; }
        public string slug { get; set; }
        public string label { get; set; }
        public Locality[] localities { get; set; }
    }

    public class Locality
    {
        public string type { get; set; }
        public string slug { get; set; }
        public string label { get; set; }
        public District[] districts { get; set; }
    }

    public class District
    {
        public string label { get; set; }
        public string slug { get; set; }
    }

}
