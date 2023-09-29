using System.ComponentModel.DataAnnotations;

namespace Weather.ViewModels
{
    public class WeatherVM
    {
        public string Name { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public float TempC { get; set; }
        public string ImageSrc { get; set; }
    }
}
