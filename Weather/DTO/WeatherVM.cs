﻿using System.ComponentModel.DataAnnotations;

namespace Weather.ViewModels
{
    internal class WeatherVM
    {
        internal string[] LocalDateAndTime { get; set; } = new string[2];
        internal string Date { get; set; } = string.Empty;
        internal string Name { get; set; } = string.Empty;
        internal string Region { get; set; } = string.Empty;
        internal string Country { get; set; } = string.Empty;
        internal float TempC { get; set; }
        public float FeelsLike { get; set; }
        internal string ImageSrc { get; set; } = string.Empty;
        internal string WeatherAsText { get; set; } = string.Empty;
        internal string[] WindDegreesAndText { get; set; } = new string[3];
    }
}