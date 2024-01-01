using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Weather.DTO;
using Weather.Models.current;
using Weather.Models.OnWeek;
using Weather.ViewModels;

namespace Weather.Services
{
    class WeaterVMService
    {
        protected WeatherModel CurrentWeather { get; set; }
        protected WeatherOnWeek WeatherOnWeek { get; set; }
        public WeaterVMService(WeatherModel currentWeather)
        {
            CurrentWeather = currentWeather;
        }
        public WeaterVMService(WeatherOnWeek onWeek)
        {
            WeatherOnWeek = onWeek;
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
        internal ObjectResult WeatherOnNextDays()
        {
            try
            {
                if (WeatherOnWeek == null)
                    return new ObjectResult(null);

                WeatherVMDays[] weatherDays = new WeatherVMDays[WeatherOnWeek.Forecast.Forecastday.Count];

                for (int i = 0; i < weatherDays.Length; i++)
                {
                    DateTimeOffset dateTime = DateTimeOffset.FromUnixTimeSeconds(WeatherOnWeek.Forecast.Forecastday[i].DateEpoch);
                    DateTime date = dateTime.DateTime;
                    DayOfWeek dayOfWeek = date.DayOfWeek;
                    string day = date.ToString("dddd", new System.Globalization.CultureInfo("ru-RU")).First().ToString().ToUpper() + 
                        date.ToString("dddd", new System.Globalization.CultureInfo("ru-RU")).Substring(1);

                    weatherDays[i] = new WeatherVMDays()
                    {
                        Date = date.ToString("dd.MM.yyyy"),
                        DayOfWeek = day,
                        MaxTemp = WeatherOnWeek.Forecast.Forecastday[i].Day.MaxtempC,
                        MinTemp = WeatherOnWeek.Forecast.Forecastday[i].Day.MintempC,
                        AvgTemp = WeatherOnWeek.Forecast.Forecastday[i].Day.AvgtempC,
                        WindGust = WindSpeed(WeatherOnWeek.Forecast.Forecastday[i].Day.MaxwindKph),
                        AvgVisInKm = WeatherOnWeek.Forecast.Forecastday[i].Day.AvgvisKm,
                        Humidity = (byte)WeatherOnWeek.Forecast.Forecastday[i].Day.Avghumidity,
                        WeatherText = WeatherOnWeek.Forecast.Forecastday[i].Day.Condition.Text,
                        WeatherImg = WeatherOnWeek.Forecast.Forecastday[i].Day.Condition.Icon
                    };

                    for (int j = 0; j <= 23; j++) 
                    {
                        weatherDays[i].Hours.Add(new Hours
                        {
                            TimeOfHours = WeatherOnWeek.Forecast.Forecastday[i].Hour[j].Time.Split(' '),
                            IsDay = (byte)WeatherOnWeek.Forecast.Forecastday[i].Hour[j].IsDay,
                            WeatherImg = WeatherOnWeek.Forecast.Forecastday[i].Hour[j].Condition.Icon,
                            WeatherText = WeatherOnWeek.Forecast.Forecastday[i].Hour[j].Condition.Text,
                            TempC = WeatherOnWeek.Forecast.Forecastday[i].Hour[j].TempC,
                            FeelsLikeC = WeatherOnWeek.Forecast.Forecastday[i].Hour[j].FeelslikeC,
                            WindSpeed = WindSpeed(WeatherOnWeek.Forecast.Forecastday[i].Hour[j].WindKph),
                            WindGust = WindSpeed(WeatherOnWeek.Forecast.Forecastday[i].Hour[j].GustKph),
                            WindDegreesAndText = GetWindCourse(WeatherOnWeek.Forecast.Forecastday[i].Hour[j].WindDegree, WeatherOnWeek.Forecast.Forecastday[i].Hour[j].WindDir),
                            Pressure = GetPressure(WeatherOnWeek.Forecast.Forecastday[i].Hour[j].PressureMb),
                            Humidity = WeatherOnWeek.Forecast.Forecastday[i].Hour[j].Humidity
                        });
                    }
                }

                return new ObjectResult(weatherDays);
            }
            catch
            {
                throw new Exception("Не можем отобразить информацию о погоде, повторите операцию позже");
                //return new StatusCodeResult(500);
            }
        }
        private string[] GetWindCourse(int windDegree, string windDir)
        {
            if (!string.IsNullOrEmpty(windDir))
            {
                string[] course = windDir switch
                {
                    "N" => new string[] { "С", "Север", windDegree.ToString() },
                    "NNE" => new string[] { "С СВ", "Север Северо-Восток", windDegree.ToString() },
                    "NE" => new string[] { "СВ", "Северо-Восток", windDegree.ToString() },
                    "ENE" => new string[] { "В СВ", "Восток Северо-Восток", windDegree.ToString() },
                    "E" => new string[] { "В", "Восток", windDegree.ToString() },
                    "ESE" => new string[] { "В ЮВ", "Восток Юго-Восток", windDegree.ToString() },
                    "SE" => new string[] { "ЮВ", "Юго-Восток", windDegree.ToString() },
                    "SSE" => new string[] { "Ю ЮВ", "Юг Юго-Восток", windDegree.ToString() },
                    "S" => new string[] { "Ю", "Юг", windDegree.ToString() },
                    "SSW" => new string[] { "Ю ЮЗ", "Юг Юго-Запад", windDegree.ToString() },
                    "SW" => new string[] { "ЮЗ", "Юго-Запад", windDegree.ToString() },
                    "WSW" => new string[] { "З ЮЗ", "Запад Юго-Запад", windDegree.ToString() },
                    "W" => new string[] { "З", "Запад", windDegree.ToString() },
                    "WNW" => new string[] { "З СЗ", "Запад Северо-Запад", windDegree.ToString() },
                    "NW" => new string[] { "СЗ", "Северо-Запад", windDegree.ToString() },
                    "NNW" => new string[] { "С СЗ", "Север Северо-Запад", windDegree.ToString() }
                };
               return course;
            }
            return new string[] { string.Empty };
        }
        private float WindSpeed(double speed)
        {
            if (speed >= 0)
            {
                float speedMeters = Convert.ToSingle(speed) * 1000 / 3600;
                return (float)Math.Round(speedMeters, 1);
            }
            return default;
        }
        private string TimesOfDay(int? dayOrnight)
        {
            if (dayOrnight == 0)
                return "темно";
            return "светло";
        }
        private int GetPressure(double pressureMb)
        {
            if (pressureMb <= 0d)
                return 0;

            double mmHg = 0.75006375541921d;
            return (int)Math.Round(pressureMb * mmHg);

        }
    }
}
