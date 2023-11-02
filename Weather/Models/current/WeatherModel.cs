namespace Weather.Models.current
{
    public class WeatherModel
    {
        public Location Location { get; set; }
        public Current Current { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public string Tz_id { get; set; }
        public int Localtime_epoch { get; set; }
        public string Localtime { get; set; }
    }

    public class Current
    {
        public int Last_updated_epoch { get; set; }
        public string Last_updated { get; set; }
        public float Temp_c { get; set; }
        public float Temp_f { get; set; }
        public int Is_day { get; set; }
        public Condition Condition { get; set; }
        public float Wind_mph { get; set; }
        public float Wind_kph { get; set; }
        public int Wind_degree { get; set; }
        public string Wind_dir { get; set; }
        public float Pressure_mb { get; set; }
        public float Pressure_in { get; set; }
        public float Precip_mm { get; set; }
        public float Precip_in { get; set; }
        public int Humidity { get; set; }
        public int Cloud { get; set; }
        public float Feelslike_c { get; set; }
        public float Feelslike_f { get; set; }
        public float Vis_km { get; set; }
        public float Vis_miles { get; set; }
        public float Uv { get; set; }
        public float Gust_mph { get; set; }
        public float Gust_kph { get; set; }
    }

    public class Condition
    {
        public string Text { get; set; }
        public string Icon { get; set; }
        public int Code { get; set; }
    }

}
