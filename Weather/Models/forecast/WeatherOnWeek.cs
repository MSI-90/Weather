namespace Weather.Models.OnWeek
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Astro
    {
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string Moonrise { get; set; }
        public string Moonset { get; set; }
        public string MoonPhase { get; set; }
        public int MoonIllumination { get; set; }
        public int IsMoonUp { get; set; }
        public int IsSunUp { get; set; }
    }

    public class Condition
    {
        public string Text { get; set; }
        public string Icon { get; set; }
        public int Code { get; set; }
    }

    public class Current
    {
        public int LastUpdatedEpoch { get; set; }
        public string LastUpdated { get; set; }
        public double TempC { get; set; }
        public double TempF { get; set; }
        public int IsDay { get; set; }
        public Condition Condition { get; set; }
        public double WindMph { get; set; }
        public double WindKph { get; set; }
        public int WindDegree { get; set; }
        public string WindDir { get; set; }
        public double PressureMb { get; set; }
        public double PressureIn { get; set; }
        public double PrecipMm { get; set; }
        public double PrecipIn { get; set; }
        public int Humidity { get; set; }
        public int Cloud { get; set; }
        public double FeelslikeC { get; set; }
        public double FeelslikeF { get; set; }
        public double VisKm { get; set; }
        public double VisMiles { get; set; }
        public double Uv { get; set; }
        public double GustMph { get; set; }
        public double GustKph { get; set; }
    }

    public class Day
    {
        public double MaxtempC { get; set; }
        public double MaxtempF { get; set; }
        public double MintempC { get; set; }
        public double MintempF { get; set; }
        public double AvgtempC { get; set; }
        public double AvgtempF { get; set; }
        public double MaxwindMph { get; set; }
        public double MaxwindKph { get; set; }
        public double TotalprecipMm { get; set; }
        public double TotalprecipIn { get; set; }
        public double TotalsnowCm { get; set; }
        public double AvgvisKm { get; set; }
        public double AvgvisMiles { get; set; }
        public double Avghumidity { get; set; }
        public int DailyWillItRain { get; set; }
        public int DailyChanceOfRain { get; set; }
        public int DailyWillItSnow { get; set; }
        public int DailyChanceOfSnow { get; set; }
        public Condition Condition { get; set; }
        public double Uv { get; set; }
    }

    public class Forecast
    {
        public List<Forecastday> Forecastday { get; set; }
    }

    public class Forecastday
    {
        public string Date { get; set; }
        public int DateEpoch { get; set; }
        public Day Day { get; set; }
        public Astro Astro { get; set; }
        public List<Hour> Hour { get; set; }
    }

    public class Hour
    {
        public int TimeEpoch { get; set; }
        public string Time { get; set; }
        public double TempC { get; set; }
        public double TempF { get; set; }
        public int IsDay { get; set; }
        public Condition Condition { get; set; }
        public double WindMph { get; set; }
        public double WindKph { get; set; }
        public int WindDegree { get; set; }
        public string WindDir { get; set; }
        public double PressureMb { get; set; }
        public double PressureIn { get; set; }
        public double PrecipMm { get; set; }
        public double PrecipIn { get; set; }
        public int Humidity { get; set; }
        public int Cloud { get; set; }
        public double FeelslikeC { get; set; }
        public double FeelslikeF { get; set; }
        public double WindchillC { get; set; }
        public double WindchillF { get; set; }
        public double HeatindexC { get; set; }
        public double HeatindexF { get; set; }
        public double DewpointC { get; set; }
        public double DewpointF { get; set; }
        public int WillItRain { get; set; }
        public int ChanceOfRain { get; set; }
        public int WillItSnow { get; set; }
        public int ChanceOfSnow { get; set; }
        public double VisKm { get; set; }
        public double VisMiles { get; set; }
        public double GustMph { get; set; }
        public double GustKph { get; set; }
        public double Uv { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string TzId { get; set; }
        public int LocaltimeEpoch { get; set; }
        public string Localtime { get; set; }
    }

    public class WeatherOnWeek
    {
        public Location Location { get; set; }
        public Current Current { get; set; }
        public Forecast Forecast { get; set; }
    }



}