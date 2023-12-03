using System.ComponentModel.DataAnnotations;

namespace Weather.ViewModels
{
    internal class WeatherVM
    {
        internal string LocalTime;
        
        internal string LocalDateAndTime
        {
            get { return LocalTime; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    LocalTime = string.Empty;
                else
                {
                    string[] data = value.Split(' ');
                    LocalTime = data[1];
                }
            }
        }
        internal string Date { get; set; } = string.Empty;
        internal string Name { get; set; } = string.Empty;
        internal string Region { get; set; } = string.Empty;
        internal string Country { get; set; } = string.Empty;
        internal float TempC { get; set; }
        internal string ImageSrc { get; set; } = string.Empty;
        internal string WeatherAsText { get; set; } = string.Empty;
        internal string WindDegreesAndText {  get; set; } = string.Empty;
    }
}
