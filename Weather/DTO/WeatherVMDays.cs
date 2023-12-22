namespace Weather.DTO
{
    internal class WeatherVMDays
    {
        public string Date {  get; set; } = string.Empty;
        public double MaxTemp { get; set; } = 0d;
        public double MinTemp { get; set; } = 0d;
        public double AvgTemp { get; set; } = 0d;
        public double WindSpeed { get; set; } = 0d;
        public double AvgVisInKm { get; set; } = 0d;
        public byte Humidity { get; set; } = 0;
        public string WeatherText { get; set; } = string.Empty;
        public string WeatherImg {  get; set; } = string.Empty;
    }
}
