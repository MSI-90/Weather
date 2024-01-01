namespace Weather.DTO
{
    internal class WeatherVMDays
    {
        public string Date {  get; set; } = string.Empty;
        public string DayOfWeek {  get; set; } = string.Empty;
        public double MaxTemp { get; set; } = 0d;
        public double MinTemp { get; set; } = 0d;
        public double AvgTemp { get; set; } = 0d;
        //public float WindSpeed { get; set; } = 0f;
        public float WindGust { get; set; } = 0f;
        public double AvgVisInKm { get; set; } = 0d;
        public byte Humidity { get; set; } = 0;
        public string WeatherText { get; set; } = string.Empty;
        public string WeatherImg {  get; set; } = string.Empty;
        public List<Hours> Hours { get; set; } = new List<Hours>();
    }
    internal class Hours
    {
        public string[] TimeOfHours {  get; set; } = new string[2];
        public byte IsDay { get; set; } = 0;
        public string WeatherImg { get; set; } = string.Empty;
        public string WeatherText { get; set; } = string.Empty;
        public double TempC { get; set; } = 0d;
        public double FeelsLikeC { get; set; } = 0f;
        public float WindSpeed { get; set; } = 0f;
        public float WindGust { get; set; } = 0f;
        public string[] WindDegreesAndText {  get; set; } = new string[3];
        public int Pressure { get; set; } = 0;
        public int Humidity { get; set; } = 0;
    }
}
