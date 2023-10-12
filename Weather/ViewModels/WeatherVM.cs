using System.ComponentModel.DataAnnotations;

namespace Weather.ViewModels
{
    public class WeatherVM
    {
        public string Date { get; set; } = string.Empty;
        public string LocalDateAndTime
        {
            get { return LocalTime; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    value = string.Empty;
                else
                {
                    string[] data = value.Split(' ');
                    LocalTime = data[1];
                }
            }
        }
        public string LocalTime {get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public float TempC { get; set; } = float.MinValue;
        public string ImageSrc { get; set; } = string.Empty;

    }
}
