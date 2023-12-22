using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Weather.ViewModels
{
    public class WeatherVM
    {
        internal string[] LocalDateAndTime { get; set; } = new string[2];
        internal string TimesOfDay { get; set; } = string.Empty;
        internal string Date { get; set; } = string.Empty;
        internal string Name { get; set; } = string.Empty;
        internal string Region { get; set; } = string.Empty;
        internal string Country { get; set; } = string.Empty;
        internal float TempC { get; set; } = 0f;
        public float FeelsLike { get; set; } = 0f;
        internal string ImageSrc { get; set; } = string.Empty;
        internal string WeatherAsText { get; set; } = string.Empty;
        internal string[] WindDegreesAndText { get; set; } = new string[3];
        internal string WindSpeed { get; set; } = string.Empty;
        internal string WeatherText {  get; set; } = string.Empty;
        internal string WindGust { get; set; } = string.Empty;
    }
}
