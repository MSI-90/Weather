using NuGet.Versioning;
using Weather.Models.current;
using Weather.ViewModels;

namespace Weather.Services
{
    class WeaterVMService
    {
        protected WeatherModel CurrentWeather { get; set; }
        public WeaterVMService(WeatherModel currentWeather)
        {
            CurrentWeather = currentWeather;
        }

        internal WeatherVM GetMyCurrentWeatherAsync(string name)
        {
            try
            {
                if (CurrentWeather == null)
                    return new WeatherVM();

                float temperature = 0f;
                _ = CurrentWeather.Current.Temp_c == -0f ? temperature = 0f : temperature = CurrentWeather.Current.Temp_c;

                float feelsLikeC = CurrentWeather.Current.Feelslike_c == -0f ? feelsLikeC = 0f : feelsLikeC = CurrentWeather.Current.Feelslike_c;

                var model = new WeatherVM()
                {
                    Name = name,
                    LocalDateAndTime = CurrentWeather.Location.Localtime.Split(' '),
                    TimesOfDay = TimesOfDay(CurrentWeather.Current.Is_day),
                    Region = CurrentWeather.Location.Region,
                    Country = CurrentWeather.Location.Country,
                    TempC = temperature,
                    FeelsLike = feelsLikeC,
                    ImageSrc = CurrentWeather.Current.Condition.Icon,
                    WeatherAsText = CurrentWeather.Current.Condition.Text,
                    WindDegreesAndText = GetWindCourse(CurrentWeather.Current.Wind_degree, CurrentWeather.Current.Wind_dir),
                    WindSpeed = WindSpeed(CurrentWeather.Current.Wind_kph),
                    WeatherText = CurrentWeather.Current.Condition.Text,
                    WindGust = WindSpeed(CurrentWeather.Current.Gust_kph),
                };
                return model;

            }
            catch
            {
                throw new Exception("Не можем отобразить информацию о погоде, повторите операцию позже");
            }
        }
        private string[] GetWindCourse(int windDegree, string windDir)
        {
            if (!string.IsNullOrEmpty(windDir))
            {
                string[] course = windDir switch
                {
                    "N" => new string[] { "С", "Север", windDegree.ToString() },
                    "NNE" => new string[] { "ССВ", "Север Северо-Восток", windDegree.ToString() },
                    "NE" => new string[] { "СВ", "Северо-Восток", windDegree.ToString() },
                    "ENE" => new string[] { "ВСВ", "Восток Северо-Восток", windDegree.ToString() },
                    "E" => new string[] { "В", "Восток", windDegree.ToString() },
                    "ESE" => new string[] { "ВЮВ", "Восток Юго-Восток", windDegree.ToString() },
                    "SE" => new string[] { "ЮВ", "Юго-Восток", windDegree.ToString() },
                    "SSE" => new string[] { "ЮЮВ", "Юг Юго-Восток", windDegree.ToString() },
                    "S" => new string[] { "Ю", "Юг", windDegree.ToString() },
                    "SSW" => new string[] { "ЮЮЗ", "Юг Юго-Запад", windDegree.ToString() },
                    "SW" => new string[] { "ЮЗ", "Юго-Запад", windDegree.ToString() },
                    "WSW" => new string[] { "ЗЮЗ", "Запад Юго-Запад", windDegree.ToString() },
                    "W" => new string[] { "З", "Запад", windDegree.ToString() },
                    "WNW" => new string[] { "ЗСЗ", "Запад Северо-Запад", windDegree.ToString() },
                    "NW" => new string[] { "СЗ", "Северо-Запад", windDegree.ToString() },
                    "NNW" => new string[] { "ССЗ", "Север Северо-Запад", windDegree.ToString() }
                };
               return course;
            }
            return new string[] { string.Empty };
        }
        private string WindSpeed(float speed)
        {
            if (speed >= 0)
            {
                float speedMeters = speed * 1000 / 3600;
                speedMeters = ((float)Math.Round(speedMeters, 1));
                return speedMeters.ToString();
            }
            return "0";
        }
        private string TimesOfDay(int? dayOrnight)
        {
            if (dayOrnight == 0)
                return "темно";
            return "светло";
        }
    }
}
